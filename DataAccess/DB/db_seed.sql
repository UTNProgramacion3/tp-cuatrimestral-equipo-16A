USE DB_CLINICA;

BEGIN TRANSACTION;

BEGIN TRY
    -- Direcciones
    INSERT INTO Direcciones (Calle, Numero, Piso, Depto, Localidad, Provincia, CodigoPostal)
    VALUES 
        ('Av. Libertador', 1000, NULL, NULL, 'Buenos Aires', 'Buenos Aires', '1000'),
        ('Av. 9 de Julio', 1500, NULL, NULL, 'Rosario', 'Santa Fe', '2000'),
        ('Calle San Mart�n', 800, 5, 'B', 'Mendoza', 'Mendoza', '5500'),
        ('Av. Rivadavia', 1200, 1, NULL, 'Tucum�n', 'Tucum�n', '4000'),
		 ('Av. Corrientes', 2500, NULL, NULL, 'Buenos Aires', 'Buenos Aires', '2000');
    
    -- Roles
    INSERT INTO Roles (Nombre)
    VALUES ('Admin'), ('Medico'), ('Recepcionista'), ('Empleado'), ('Paciente');
    
    -- Usuarios
    INSERT INTO Usuarios (Email, PasswordHash, RolId, FechaCreacion, Activo)
    VALUES 
        ('admin@admin.com', '4MgQRMh6GB1f1qRpofapbT+DeEf6f8XYkLgSYCAvFy5JrdNTqC+ijIuYP/hkBNBr', 1,'2024-11-28', 1)
    
    -- Personas
    INSERT INTO Personas (Nombre, Apellido, Documento, Telefono, FechaNacimiento, EmailPersonal, DireccionId, UsuarioId)
    VALUES 
        ('Juan', 'P�rez', 12345678, '555-1234', '1985-02-15', 'juan@correo.com', 1, 1);
    
    ---- Pacientes
    --INSERT INTO Pacientes (ObraSocial, NroAfiliado, PersonaId)
    --VALUES 
    --    ('OSDE', '12345', 1),
    --    ('Swiss Medical', '67890', 2),
    --    ('Medicus', '54321', 3),
    --    ('OSDE', '11223', 4),
    --    ('Prepaid Health', '98765', 5),
    --    ('Osplad', '14789', 6),
    --    ('IOMA', '96321', 7),
    --    ('Federada', '25864', 8);
    
    -- Especialidades
    INSERT INTO Especialidades (Nombre)
    VALUES 
        ('Cardiolog�a'), 
        ('Neurolog�a'), 
        ('Pediatr�a'),
        ('Dermatolog�a'),
        ('Endocrinolog�a');
    
    -- Cargos
    INSERT INTO Cargos (Nombre) 
    VALUES 
        ('Medico'), 
        ('Recepcionista'), 
        ('Supervisor');

    ---- Empleados
    --INSERT INTO Empleados (Legajo, EmailCorporativo, CargoId, PersonaId)
    --VALUES 
    --    (1001, 'dr.perez@hospital.com', 1, 1),
    --    (1002, 'dr.gonzalez@hospital.com', 1, 2),
    --    (1003, 'dr.lopez@hospital.com', 1, 3),
    --    (1004, 'dr.martinez@hospital.com', 1, 4),
    --    (1005, 'dr.sanchez@hospital.com', 1, 5),
    --    (1006, 'dr.rodriguez@hospital.com', 1, 6),
    --    (1007, 'dr.hernandez@hospital.com', 1, 7),
    --    (1008, 'dr.fernandez@hospital.com', 1, 8),
    --    (1009, 'dr.jimenez@hospital.com', 1, 9),
    --    (1010, 'dr.diaz@hospital.com', 1, 10),
    --    (1011, 'dr.gomez@hospital.com', 1, 11),
    --    (1012, 'dr.ruiz@hospital.com', 1, 12);
    
    ---- Medicos
    --INSERT INTO Medicos (Matricula, EspecialidadId, EmpleadoId)
    --VALUES 
    --    (1234, 1, 1),
    --    (5678, 2, 2),
    --    (9101, 3, 3),
    --    (1122, 4, 4),
    --    (3344, 5, 5);
    
    -- EstadosTurno
    INSERT INTO EstadoTurnos (Estado) 
    VALUES 
        ('Confirmado'), 
        ('Cancelado'), 
        ('Reprogramado'),
        ('Finalizado'),
        ('Ausente');
    
    -- Sedes
    INSERT INTO Sedes (Nombre, DireccionId) 
    VALUES 
        ('Sede Central', 1),
        ('Sede Norte', 2),
        ('Sede Sur', 3),
        ('Sede Interior', 4),
        ('Sede Uruguay', 3)
		;
    
    -- Turnos
    --INSERT INTO Turnos (IdMedico, IdPaciente, IdEstadoTurno, IdSede, Fecha, Hora, Observaciones)
    --VALUES 
    --    (1, 1, 1, 1, '2024-11-20', '10:00', 'Consulta inicial'),
    --    (2, 2, 2, 1, '2024-11-20', '11:00', 'Chequeo regular'),
    --    (3, 3, 3, 2, '2024-11-21', '14:00', 'Urgente'),
    --    (4, 4, 1, 2, '2024-11-22', '09:00', 'Control mensual'),
    --    (5, 5, 2, 3, '2024-11-22', '15:00', 'Examen de rutina');

	-- Jornadas de Trabajo
	--INSERT INTO JornadasTrabajo (SedeId)
	--VALUES 
	--	(1),
	--	(2),
	--	(3),
	--	(4);

	-- Dias Laborales
	--INSERT INTO DiasLaborales (Dia, Inicio, Fin, JornadaTrabajoId)
	--VALUES 
	--	(2, '08:00', '12:00', 1),
	--	(3, '14:00', '18:00', 1),
	--	(4, '09:00', '17:00', 1),
	--	(5, '10:00', '15:00', 1),
	--	(6, '08:00', '13:00', 1);


--!!!!!!!!!!! ROLES Y PERMISOS A PARTIR DE AC� !!!!!!!!!!!!!!!!!

INSERT INTO Modulos (Nombre)
VALUES
('Gesti�n de Usuarios'),
('Gesti�n de Pacientes'),
('Gesti�n de M�dicos'),
('Gesti�n de Especialidades'),
('Gesti�n de Turnos'),
('Gesti�n de Sedes'),
('Mailing');

-- Gesti�n de Usuarios
INSERT INTO Permisos (Nombre, ModuloId)
VALUES
('Ver usuarios', 1),
('Crear usuarios', 1),
('Editar usuarios', 1),
('Eliminar usuarios', 1),
('Asignar Roles', 1);

-- Gesti�n de Pacientes
INSERT INTO Permisos (Nombre, ModuloId)
VALUES
('Ver pacientes', 2),
('Crear pacientes', 2),
('Editar informaci�n de pacientes', 2),
('Ver historial de turnos de pacientes', 2);

-- Gesti�n de M�dicos
INSERT INTO Permisos (Nombre, ModuloId)
VALUES
('Ver m�dicos', 3),
('Crear m�dicos', 3),
('Editar m�dicos', 3),
('Asignar especialidades a m�dicos', 3),
('Definir horarios de trabajo', 3);

-- Gesti�n de Especialidades
INSERT INTO Permisos (Nombre, ModuloId)
VALUES
('Ver especialidades', 4),
('Crear especialidades', 4),
('Editar especialidades', 4);

-- Gesti�n de Turnos
INSERT INTO Permisos (Nombre, ModuloId)
VALUES
('Ver turnos', 5),
('Asignar turnos', 5),
('Reprogramar turnos', 5),
('Cancelar turnos', 5),
('Visualizar horarios disponibles', 5),
('Modificar estado de turno', 5);

-- Gesti�n de Sedes
INSERT INTO Permisos (Nombre, ModuloId)
VALUES
('Ver sedes', 6),
('Crear sedes', 6),
('Editar sedes', 6);

-- Gesti�n de Usuario Paciente
--INSERT INTO Permisos (Nombre, ModuloId)
--VALUES
--('Ver sus turnos', 8),
--('Reprogramar sus turnos', 8),
--('Cancelar sus turnos', 8);

INSERT INTO Permisos (Nombre, ModuloId)
VALUES
('Recordatorio de turnos', 7),
('Notificacion de Reprogramacion', 7)

	--Administrador
INSERT INTO PermisosRoles (PermisoId, RolId)
SELECT Id, 1 FROM Permisos
WHERE ModuloId IN (1, 2, 3, 4, 5, 6, 7) -- Todos los m�dulos excepto Gesti�n de Usuario Paciente
AND NOT (ModuloId = 8); -- Excluye Gesti�n de Usuario Paciente

--Recepcionista
-- Gesti�n de Pacientes: todos los permisos
INSERT INTO PermisosRoles (PermisoId, RolId)
SELECT Id, 2 FROM Permisos
WHERE ModuloId = 2;


-- Gesti�n de M�dicos: Ver, Crear y Definir horarios
INSERT INTO PermisosRoles (PermisoId, RolId)
SELECT Id, 2 FROM Permisos
WHERE ModuloId = 3 AND Nombre IN ('Ver m�dicos', 'Crear m�dicos', 'Definir horarios de trabajo');


-- Gesti�n de Turnos: todos los permisos
INSERT INTO PermisosRoles (PermisoId, RolId)
SELECT Id, 2 FROM Permisos
WHERE ModuloId = 5;


-- Mailing: todos los permisos
INSERT INTO PermisosRoles (PermisoId, RolId)
SELECT Id, 2 FROM Permisos
WHERE ModuloId = 7;

--Medico
-- Gesti�n de Turnos: Ver turnos y Modificaci�n de turnos (agregar observaciones)
INSERT INTO PermisosRoles (PermisoId, RolId)
SELECT Id, 3 FROM Permisos
WHERE ModuloId = 5 AND Nombre IN ('Ver turnos', 'Modificar estado de turno');

-- Pacientes
-- Gesti�n de Usuario Paciente: todos los accesos
INSERT INTO PermisosRoles (PermisoId, RolId)
SELECT Id, 4 FROM Permisos
WHERE ModuloId = 8;

    
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    -- Agregar manejo de errores aqu�, por ejemplo, log o mensajes
    THROW;
END CATCH;