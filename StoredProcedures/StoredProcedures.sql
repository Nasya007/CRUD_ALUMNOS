CREATE PROCEDURE sp_ObtenerAlumnos
AS
BEGIN
	SELECT ID_Alumno, NoControl, Nombres, ApellidoPaterno, ApellidoMaterno, Activo, FechaDeNacimiento, a.ID_CARRERA, c.Carrera
	FROM Tb_Alumno a
	INNER JOIN Tb_Carrera c ON a.ID_CARRERA = c.ID_Carrera
	ORDER BY ApellidoPaterno ASC;
END

CREATE PROCEDURE sp_ObtenerAlumnoPorId
    @ID_Alumno INT
AS
BEGIN
    SELECT 
        a.ID_Alumno, 
        a.NoControl, 
        a.Nombres, 
        a.ApellidoPaterno, 
        a.ApellidoMaterno, 
        a.Activo, 
        a.FechaDeNacimiento,  
		a.ID_CARRERA,
        c.Carrera
    FROM 
        Tb_Alumno a
    INNER JOIN 
        Tb_Carrera c ON a.ID_CARRERA = c.ID_Carrera
    WHERE 
        a.ID_Alumno = @ID_Alumno;
END

CREATE PROCEDURE sp_agregarAlumno @NoControl VARCHAR(500), @Nombres VARCHAR(1000), @ApellidoPaterno VARCHAR(100), @ApellidoMaterno VARCHAR(100), @Activo BIT, @FechaDeNacimiento DATETIME, @ID_CARRERA INT
AS
BEGIN
	INSERT INTO Tb_Alumno (NoControl, Nombres, ApellidoPaterno, ApellidoMaterno, Activo, FechaDeNacimiento, ID_CARRERA)
		VALUES
		(@NoControl, @Nombres, @ApellidoPaterno, @ApellidoMaterno, @Activo, @FechaDeNacimiento, @ID_CARRERA)
END


CREATE PROCEDURE sp_EditarAlumnos @ID_Alumno INT, @NoControl VARCHAR(500), @Nombres VARCHAR(1000), @ApellidoPaterno VARCHAR(100), @ApellidoMaterno VARCHAR(100), @Activo BIT, @FechaDeNacimiento DATETIME, @ID_CARRERA INT
AS
BEGIN
	UPDATE Tb_Alumno
	SET
		NoControl = @NoControl,
		Nombres = @Nombres,
		ApellidoPaterno = @ApellidoPaterno,
		ApellidoMaterno = @ApellidoMaterno,
		Activo = @Activo,
		FechaDeNacimiento = @FechaDeNacimiento,
		ID_CARRERA = @ID_CARRERA
		WHERE ID_Alumno = @ID_Alumno;
END


CREATE PROCEDURE sp_eliminarAlumno
    @ID_Alumno INT
AS
BEGIN
    DELETE FROM 
        Tb_Alumno
    WHERE 
        ID_Alumno = @ID_Alumno;
END


CREATE PROCEDURE sp_ObtenerCarreras
AS
BEGIN
	SELECT ID_Carrera, Carrera
	FROM Tb_Carrera;
END