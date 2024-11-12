CREATE DATABASE DB_CLINICA;
GO

USE DB_Clinica;
GO

CREATE TABLE Direcciones (
    Id INT PRIMARY KEY,
    Calle VARCHAR(100),
    Numero INT,
    Piso VARCHAR(10),
    Depto VARCHAR(10),
    Localidad VARCHAR(50),
    Provincia VARCHAR(50),
    CodigoPostal VARCHAR(10)
);
GO

CREATE TABLE Roles (
    Id INT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Activo BIT DEFAULT 1
);
GO

CREATE TABLE Usuarios (
    Id INT PRIMARY KEY,
    Email VARCHAR(100) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    FechaCreacion DATE DEFAULT GETDATE(),
    RolId INT NOT NULL,
    ImagenPerfil VARCHAR(255),
    Activo BIT DEFAULT 1,
    FOREIGN KEY (RolId) REFERENCES Roles(Id)
);
GO

CREATE TABLE Personas (
    Id INT PRIMARY KEY,
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
GO

CREATE TABLE Pacientes (
    Id INT PRIMARY KEY,
    ObraSocial VARCHAR(100),
    NroAfiliado VARCHAR(50),
    PersonaId INT NOT NULL,
    FOREIGN KEY (PersonaId) REFERENCES Personas(Id)
);
GO

CREATE TABLE JornadasTrabajo (
    Id INT PRIMARY KEY,
    SedeId INT
);
GO

CREATE TABLE Cargos (
    Id INT PRIMARY KEY,
    Nombre VARCHAR(50)
);
GO

CREATE TABLE Empleados (
    Id INT PRIMARY KEY,
    Legajo INT UNIQUE NOT NULL,
    EmailCorporativo VARCHAR(100) UNIQUE,
    CargoId INT,
    JornadaTrabajoId INT,
    PersonaId INT,
    FOREIGN KEY (CargoId) REFERENCES Cargos(Id),
    FOREIGN KEY (JornadaTrabajoId) REFERENCES JornadasTrabajo(Id),
    FOREIGN KEY (PersonaId) REFERENCES Personas(Id)
);
GO

CREATE TABLE Turnos (
    Id INT PRIMARY KEY,
    IdMedico INT NOT NULL,
    IdPaciente INT NOT NULL,
    Fecha DATETIME,
    EstadoTurno VARCHAR(50),
    Observaciones TEXT,
    FOREIGN KEY (IdMedico) REFERENCES Empleados(Id),
    FOREIGN KEY (IdPaciente) REFERENCES Pacientes(Id)
);
GO

CREATE TABLE DiasLaborales (
    Id INT PRIMARY KEY,
    Dia INT,
    Inicio TIME,
    Fin TIME,
    JornadaTrabajoId INT,
    FOREIGN KEY (JornadaTrabajoId) REFERENCES JornadasTrabajo(Id)
);
GO

CREATE TABLE Modulos (
    Id INT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL
);
GO

CREATE TABLE Permisos (
    Id INT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    ModuloId INT NOT NULL,
    FOREIGN KEY (ModuloId) REFERENCES Modulos(Id)
);
GO

CREATE TABLE PermisosRoles (
    Id INT PRIMARY KEY,
    PermisoId INT NOT NULL,
    RolId INT NOT NULL,
    FOREIGN KEY (PermisoId) REFERENCES Permisos(Id),
    FOREIGN KEY (RolId) REFERENCES Roles(Id),
    UNIQUE (PermisoId, RolId)  
);
GO

CREATE TABLE Sedes (
    Id INT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    DireccionId INT NOT NULL,
    FOREIGN KEY (DireccionId) REFERENCES Direcciones(Id)
);
GO

CREATE TABLE Logs (
    Id INT PRIMARY KEY,
    UsuarioId INT NOT NULL,
    Accion VARCHAR(100) NOT NULL,
    Fecha DATETIME DEFAULT GETDATE(),
    Detalles TEXT,
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);
GO

CREATE TABLE Especialidades (
    Id INT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL
);
GO

CREATE TABLE Medicos (
    Id INT PRIMARY KEY,
    Matricula INT UNIQUE NOT NULL,
    EspecialidadId INT NOT NULL,
    EmpleadoId INT NOT NULL,
    FOREIGN KEY (EspecialidadId) REFERENCES Especialidades(Id),
    FOREIGN KEY (EmpleadoId) REFERENCES Empleados(Id)
);
GO
