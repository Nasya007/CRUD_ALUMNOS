CREATE PROCEDURE sp_ObtenerAlumnos
AS
BEGIN
	SELECT ID_Alumno, NoControl, Nombres, ApellidoPaterno, ApellidoMaterno, Activo, FechaDeNacimiento, ID_Carrera
	FROM Tb_Alumno 
	ORDER BY ApellidoPaterno ASC
END