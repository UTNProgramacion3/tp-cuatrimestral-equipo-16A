CREATE DATABASE DB_CLINICA;
GO

USE DB_Clinica;
GO

Begin try
	Begin transaction


    CREATE TABLE Direcciones (
    Id INT PRIMARY KEY IDENTITY,
    Calle VARCHAR(100),
    Numero INT,
    Piso VARCHAR(10),
    Depto VARCHAR(10),
    Localidad VARCHAR(50),
    Provincia VARCHAR(50),
    CodigoPostal VARCHAR(10)
);

CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(100) NOT NULL,
    Activo BIT DEFAULT 1
);

CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY,
    Email VARCHAR(100) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    FechaCreacion DATE DEFAULT GETDATE(),
    RolId INT NOT NULL,
    ImagenPerfil VARCHAR(255),
    Activo BIT DEFAULT 1,
    FOREIGN KEY (RolId) REFERENCES Roles(Id)
);

CREATE TABLE Personas (
    Id INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(100),
    Apellido VARCHAR(100),
    Documento INT UNIQUE NOT NULL,
    Telefono VARCHAR(20),
    FechaNacimiento DATE,
    EmailPersonal VARCHAR(100) UNIQUE,
    DireccionId INT,
    UsuarioId INT,
    FOREIGN KEY (DireccionId) REFERENCES Direcciones(Id),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);

CREATE TABLE Pacientes (
    Id INT PRIMARY KEY IDENTITY,
    ObraSocial VARCHAR(100),
    NroAfiliado VARCHAR(50),
    PersonaId INT NOT NULL,
    FOREIGN KEY (PersonaId) REFERENCES Personas(Id)
);

CREATE TABLE Sedes (
    Id INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(100) NOT NULL,
    DireccionId INT NOT NULL,
    FOREIGN KEY (DireccionId) REFERENCES Direcciones(Id)
);

CREATE TABLE JornadasTrabajo (
    Id INT PRIMARY KEY IDENTITY,
    SedeId INT
	FOREIGN KEY (SedeId) REFERENCES Sedes(Id)
);

CREATE TABLE Cargos (
    Id INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(50)
);

CREATE TABLE Empleados (
    Id INT PRIMARY KEY IDENTITY,
    Legajo INT UNIQUE NOT NULL,
    EmailCorporativo VARCHAR(100) UNIQUE,
    CargoId INT,
    JornadaTrabajoId INT,
    PersonaId INT,
    FOREIGN KEY (CargoId) REFERENCES Cargos(Id),
    FOREIGN KEY (JornadaTrabajoId) REFERENCES JornadasTrabajo(Id),
    FOREIGN KEY (PersonaId) REFERENCES Personas(Id)
);
	
--Agrego Estado Turno
	CREATE TABLE EstadoTurnos (
    Id INT PRIMARY KEY IDENTITY,
    Estado VARCHAR(100)
);
	

--Agrego SEDE
--Agrego Estado de turno
--Agrego Hora tipo TIME
--Modifico Fecha a tipo DATE
CREATE TABLE Turnos (
    Id INT PRIMARY KEY IDENTITY,
    IdMedico INT NOT NULL,
    IdPaciente INT NOT NULL,
	IdEstadoTurno INT NOT NULL,
	IdSede INT NOT NULL,
    Fecha DATE,
	Hora TIME(0),
    Observaciones TEXT,
    FOREIGN KEY (IdMedico) REFERENCES Empleados(Id),
    FOREIGN KEY (IdPaciente) REFERENCES Pacientes(Id),
	FOREIGN KEY (IdEstadoTurno) REFERENCES EstadoTurnos(Id),
	FOREIGN KEY (IdSede) REFERENCES Sedes(Id)
);

CREATE TABLE DiasLaborales (
    Id INT PRIMARY KEY IDENTITY,
    Dia INT,
    Inicio TIME,
    Fin TIME,
    JornadaTrabajoId INT,
    FOREIGN KEY (JornadaTrabajoId) REFERENCES JornadasTrabajo(Id)
);

CREATE TABLE Modulos (
    Id INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(100) NOT NULL
);

CREATE TABLE Permisos (
    Id INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(100) NOT NULL,
    ModuloId INT NOT NULL,
    FOREIGN KEY (ModuloId) REFERENCES Modulos(Id)
);

CREATE TABLE PermisosRoles (
    Id INT PRIMARY KEY IDENTITY,
    PermisoId INT NOT NULL,
    RolId INT NOT NULL,
    FOREIGN KEY (PermisoId) REFERENCES Permisos(Id),
    FOREIGN KEY (RolId) REFERENCES Roles(Id),
    UNIQUE (PermisoId, RolId)  
);

CREATE TABLE Logs (
    Id INT PRIMARY KEY IDENTITY,
    UsuarioId INT NOT NULL,
    Accion VARCHAR(100) NOT NULL,
    Fecha DATETIME DEFAULT GETDATE(),
    Detalles TEXT,
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);

CREATE TABLE Especialidades (
    Id INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(100) NOT NULL
);

CREATE TABLE Medicos (
    Id INT PRIMARY KEY IDENTITY,
    Matricula INT UNIQUE NOT NULL,
    EspecialidadId INT NOT NULL,
    EmpleadoId INT NOT NULL,
    FOREIGN KEY (EspecialidadId) REFERENCES Especialidades(Id),
    FOREIGN KEY (EmpleadoId) REFERENCES Empleados(Id)
);

	Commit transaction

End Try

Begin Catch
	Rollback transaction
	Print 'Error al crear la base de datos'
End Catch

GO

--FUNCIONES

CREATE FUNCTION fn_buscar_nombre(@id int)
Returns varchar(100)
	Begin
	
		Declare @nombre varchar (100)

		Select @nombre = Nombre
		From Personas
		Where Id = @id

		return @nombre
	End

GO

CREATE FUNCTION fn_buscar_apellido(@id int)
Returns varchar(100)
	Begin
	
	Declare @apellido varchar (100)

	Select @apellido = Apellido
	From Personas
	Where Id = @id

	return @apellido
	End

