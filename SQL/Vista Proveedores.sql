use PramaSQL
go
create view Vista_Proveedores
as
select p.IdProveedor, p.NombreFantasia, p.RazonSocial, p.IdCondicionIva, i.CondicionIva, p.CUIT,
	   p.IngresosBrutos, p.FechaInicioActividad, p.IdCondicionCompra, c.CondicionCompra, p.Direccion,
	   p.IdLocalidad, l.Localidad, p.IdProvincia, pr.Provincia, p.Telefono, p.Fax, p.Celular,
	   p.MailEmpresa, p.Web, p.Contacto, p.MailContacto, p.CelularContacto, p.Observaciones, p.Insumos, p.Productos	
from Proveedores p, CondicionesIva i, CondicionesCompra c, Provincias pr, Localidades l
where p.IdCondicionIva = i.IdCondicionIva
and p.IdCondicionCompra = C.IdCondicionCompra
and p.IdLocalidad = L.IdLocalidad
and p.IdProvincia = Pr.IdProvincia
and p.Activo = 1