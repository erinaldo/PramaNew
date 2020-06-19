USE PramaSQL
GO
CREATE VIEW Vista_Localidades_Provincias 
AS
SELECT Localidades.IdLocalidad,Localidades.Localidad, Localidades.CP, Localidades.IdProvincia, Localidades.Activo, Provincias.Provincia
FROM Localidades,Provincias
WHERE Localidades.IdProvincia = Provincias.IdProvincia
AND Localidades.Activo=1

