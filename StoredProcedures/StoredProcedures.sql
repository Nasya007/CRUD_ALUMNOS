CREATE PROCEDURE sp_ObtenerAlumnos
AS
BEGIN
	SELECT ID_Alumno, NoControl, Nombres, ApellidoPaterno, ApellidoMaterno, Activo, FechaDeNacimiento, c.Carrera
	FROM Tb_Alumno a
	INNER JOIN dbo.Tb_Carrera c ON a.ID_CARRERA = c.ID_Carrera
	ORDER BY ApellidoPaterno ASC;
END