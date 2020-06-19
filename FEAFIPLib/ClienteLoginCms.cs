using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Threading;
using System.Globalization;
namespace FEAFIPLib
{


	class LoginTicket
	{

			// Entero de 32 bits sin signo que identifica el requerimiento
		public UInt32 UniqueId;
			// Momento en que fue generado el requerimiento
		public DateTime GenerationTime;
			// Momento en el que exoira la solicitud
		public DateTime ExpirationTime;
			// Identificacion del WSN para el cual se solicita el TA
		public string Service;
			// Firma de seguridad recibida en la respuesta
		public string Sign;
			// Token de seguridad recibido en la respuesta
		public string Token;

		public XmlDocument XmlLoginTicketRequest = null;
		public XmlDocument XmlLoginTicketResponse = null;
		public string RutaDelCertificadoFirmante;

		public string XmlStrLoginTicketRequestTemplate = "<loginTicketRequest><header><uniqueId></uniqueId><generationTime></generationTime><expirationTime></expirationTime></header><service></service></loginTicketRequest>";
			// OJO! NO ES THREAD-SAFE
		private static UInt32 _globalUniqueID = 0;

		public ulong CUIT;
		public string ObtenerLoginTicketResponse(string argServicio, string argUrlWsaa, string argRutaCertX509Firmante, string argPassword)
		{

			this.RutaDelCertificadoFirmante = argRutaCertX509Firmante;

			string cmsFirmadoBase64 = null;
			string loginTicketResponse = null;
			XmlNode xmlNodoUniqueId = null;
			XmlNode xmlNodoGenerationTime = null;
			XmlNode xmlNodoExpirationTime = null;
			XmlNode xmlNodoService = null;
			SecureString strPasswordSecureString = new SecureString();

			foreach (char character in argPassword.ToCharArray()) {
				strPasswordSecureString.AppendChar(character);
			}
			strPasswordSecureString.MakeReadOnly();

			// PASO 1: Genero el Login Ticket Request
			try {
				_globalUniqueID += 1;

				XmlLoginTicketRequest = new XmlDocument();
				XmlLoginTicketRequest.LoadXml(XmlStrLoginTicketRequestTemplate);

				xmlNodoUniqueId = XmlLoginTicketRequest.SelectSingleNode("//uniqueId");
				xmlNodoGenerationTime = XmlLoginTicketRequest.SelectSingleNode("//generationTime");
				xmlNodoExpirationTime = XmlLoginTicketRequest.SelectSingleNode("//expirationTime");
				xmlNodoService = XmlLoginTicketRequest.SelectSingleNode("//service");

				xmlNodoGenerationTime.InnerText = DateTime.Now.AddMinutes(-60).ToString("s");
				xmlNodoExpirationTime.InnerText = DateTime.Now.AddHours(+12).ToString("s");
				xmlNodoUniqueId.InnerText = Convert.ToString(_globalUniqueID);
				xmlNodoService.InnerText = argServicio;
				this.Service = argServicio;

			} catch (Exception excepcionAlGenerarLoginTicketRequest) {
				throw new Exception("***Error GENERANDO el LoginTicketRequest : " + excepcionAlGenerarLoginTicketRequest.Message + excepcionAlGenerarLoginTicketRequest.StackTrace);
			}

			// PASO 2: Firmo el Login Ticket Request

			try {
				X509Certificate2 certFirmante = CertificadosX509Lib.ObtieneCertificadoDesdeArchivo(RutaDelCertificadoFirmante, strPasswordSecureString);
				string[] subject = certFirmante.Subject.Split(',');
				foreach (string element in subject) {
					if (element.Trim().StartsWith("SERIALNUMBER=CUIT ")) {
						CUIT = ulong.Parse(element.Replace("SERIALNUMBER=CUIT ", ""));
					}
				}

                if (System.DateTime.Now < Properties.Settings.Default.ExpirationTime && argUrlWsaa.Equals(Properties.Settings.Default.UrlWsaa) && (CUIT == Properties.Settings.Default.Cuit) && (Service.Equals(Properties.Settings.Default.Service)))
                {
                    this.ExpirationTime = Properties.Settings.Default.ExpirationTime;
                    this.Token = Properties.Settings.Default.Token;
                    this.Sign = Properties.Settings.Default.Sign;
                    return "";
                }

				// Convierto el login ticket request a bytes, para firmar
				Encoding EncodedMsg = Encoding.UTF8;
				byte[] msgBytes = EncodedMsg.GetBytes(XmlLoginTicketRequest.OuterXml);

				// Firmo el msg y paso a Base64
				byte[] encodedSignedCms = CertificadosX509Lib.FirmaBytesMensaje(msgBytes, certFirmante);
				cmsFirmadoBase64 = Convert.ToBase64String(encodedSignedCms);

			} catch (Exception excepcionAlFirmar) {
				throw new Exception("***Error FIRMANDO el LoginTicketRequest : " + excepcionAlFirmar.Message);
			}

			// PASO 3: Invoco al WSAA para obtener el Login Ticket Response
			try {
				var binding = new BasicHttpBinding();
				binding.Security.Mode = BasicHttpSecurityMode.Transport;

				Wsaa.LoginCMSClient servicioWsaa = new Wsaa.LoginCMSClient(binding, new EndpointAddress(argUrlWsaa));
				loginTicketResponse = servicioWsaa.loginCms(cmsFirmadoBase64);

			} catch (Exception excepcionAlInvocarWsaa) {
				throw new Exception("***Error INVOCANDO al servicio WSAA : " + excepcionAlInvocarWsaa.Message);
			}


			// PASO 4: Analizo el Login Ticket Response recibido del WSAA
			try {
				XmlLoginTicketResponse = new XmlDocument();
				XmlLoginTicketResponse.LoadXml(loginTicketResponse);

				this.UniqueId = UInt32.Parse(XmlLoginTicketResponse.SelectSingleNode("//uniqueId").InnerText);
				this.GenerationTime = DateTime.Parse(XmlLoginTicketResponse.SelectSingleNode("//generationTime").InnerText);
				this.ExpirationTime = DateTime.Parse(XmlLoginTicketResponse.SelectSingleNode("//expirationTime").InnerText);
				this.Sign = XmlLoginTicketResponse.SelectSingleNode("//sign").InnerText;
				this.Token = XmlLoginTicketResponse.SelectSingleNode("//token").InnerText;

                Properties.Settings.Default.Token = this.Token;
                Properties.Settings.Default.Sign = this.Sign;
                Properties.Settings.Default.ExpirationTime = this.ExpirationTime;
                Properties.Settings.Default.UrlWsaa = argUrlWsaa;
                Properties.Settings.Default.Cuit = CUIT;
                Properties.Settings.Default.Service = Service;
                Properties.Settings.Default.Save();
            
            } catch (Exception excepcionAlAnalizarLoginTicketResponse) {
				throw new Exception("***Error ANALIZANDO el LoginTicketResponse : " + excepcionAlAnalizarLoginTicketResponse.Message);
			}

			return loginTicketResponse;

		}

	}
}
namespace FEAFIPLib
{

	class CertificadosX509Lib
	{


		public static bool VerboseMode = false;
		/// <summary>
		/// Firma mensaje
		/// </summary>
		/// <param name="argBytesMsg">Bytes del mensaje</param>
		/// <param name="argCertFirmante">Certificado usado para firmar</param>
		/// <returns>Bytes del mensaje firmado</returns>
		/// <remarks></remarks>
		public static byte[] FirmaBytesMensaje(byte[] argBytesMsg, X509Certificate2 argCertFirmante)
		{
			try {
				// Pongo el mensaje en un objeto ContentInfo (requerido para construir el obj SignedCms)
				ContentInfo infoContenido = new ContentInfo(argBytesMsg);
				SignedCms cmsFirmado = new SignedCms(infoContenido);

				// Creo objeto CmsSigner que tiene las caracteristicas del firmante
				CmsSigner cmsFirmante = new CmsSigner(argCertFirmante);
				cmsFirmante.IncludeOption = X509IncludeOption.EndCertOnly;

				if (VerboseMode) {
					Console.WriteLine("***Firmando bytes del mensaje...");
				}
				// Firmo el mensaje PKCS #7
				cmsFirmado.ComputeSignature(cmsFirmante);

				if (VerboseMode) {
					Console.WriteLine("***OK mensaje firmado");
				}

				// Encodeo el mensaje PKCS #7.
				return cmsFirmado.Encode();
			} catch (Exception excepcionAlFirmar) {
				throw new Exception("***Error al firmar: " + excepcionAlFirmar.Message);
			}
		}

		public static X509Certificate2 ObtieneCertificadoDesdeArchivo(string argArchivo, SecureString argPassword)
		{
			X509Certificate2 objCert = new X509Certificate2();
			try {
				if (argPassword.IsReadOnly()) {
					objCert.Import(FEAFIPLib.My.MyProject.Computer.FileSystem.ReadAllBytes(argArchivo), argPassword, X509KeyStorageFlags.PersistKeySet);
				} else {
					objCert.Import(FEAFIPLib.My.MyProject.Computer.FileSystem.ReadAllBytes(argArchivo));
				}
				return objCert;
			} catch (Exception excepcionAlImportarCertificado) {
				throw new Exception(excepcionAlImportarCertificado.Message + " " + excepcionAlImportarCertificado.StackTrace);
			}
		}

	}
}
namespace FEAFIPLib
{

	class ProgramaPrincipal
	{

		// Valores por defecto, globales en esta clase
		const string DEFAULT_URLWSAAWSDL = "https://wsaahomo.afip.gov.ar/ws/services/LoginCms?WSDL";
		const string DEFAULT_SERVICIO = "wsfe";

		const string DEFAULT_CERTSIGNER = "c:\\certificado.pfx";

		public static int Main(string[] args)
		{

			string strUrlWsaaWsdl = DEFAULT_URLWSAAWSDL;
			string strIdServicioNegocio = DEFAULT_SERVICIO;
			string strRutaCertSigner = DEFAULT_CERTSIGNER;
			string strPassword = "feafip";

			strIdServicioNegocio = "wsfe";
			strRutaCertSigner = "c:\\certificado.pfx";

			// Argumentos OK, entonces procesar normalmente...

			LoginTicket objTicketRespuesta = null;
			string strTicketRespuesta = null;


			try {

				objTicketRespuesta = new LoginTicket();

				strTicketRespuesta = objTicketRespuesta.ObtenerLoginTicketResponse(strIdServicioNegocio, strUrlWsaaWsdl, strRutaCertSigner, strPassword);


			} catch (Exception excepcionAlObtenerTicket) {
				Console.WriteLine("***EXCEPCION AL OBTENER TICKET:");
				Console.WriteLine(excepcionAlObtenerTicket.Message);
				return -10;

			}
			return 0;
		}



	}
}
