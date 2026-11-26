IF DB_ID('Consultorio') IS NULL
BEGIN
    CREATE DATABASE Consultorio;
END
GO

USE Consultorio;
GO

IF OBJECT_ID('dbo.Turnos', 'U') IS NOT NULL DROP TABLE dbo.Turnos;
IF OBJECT_ID('dbo.Pacientes', 'U') IS NOT NULL DROP TABLE dbo.Pacientes;
IF OBJECT_ID('dbo.Profesionales', 'U') IS NOT NULL DROP TABLE dbo.Profesionales;
GO

CREATE TABLE dbo.Profesionales
(
    Id            INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Matrícula]   NVARCHAR(20)      NOT NULL UNIQUE,
    Nombre        NVARCHAR(80)      NOT NULL,
    Especialidad  NVARCHAR(60)      NOT NULL,
    ValorConsulta DECIMAL(10,2)     NOT NULL,
    Activo        BIT               NOT NULL
);

CREATE TABLE dbo.Pacientes
(
    Id     INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Dni    NVARCHAR(8)       NOT NULL UNIQUE,
    Nombre NVARCHAR(80)      NOT NULL,
    Activo BIT               NOT NULL
);

CREATE TABLE dbo.Turnos
(
    Id            INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ProfesionalId INT               NOT NULL,
    PacienteId    INT               NOT NULL,
    Inicio        DATETIME2         NOT NULL,
    Fin           DATETIME2         NOT NULL,
    Importe       DECIMAL(10,2)     NOT NULL,
    Estado        NVARCHAR(20)      NOT NULL,
    CONSTRAINT FK_Turnos_Profesionales FOREIGN KEY (ProfesionalId) REFERENCES dbo.Profesionales(Id),
    CONSTRAINT FK_Turnos_Pacientes     FOREIGN KEY (PacienteId)    REFERENCES dbo.Pacientes(Id)
);
GO

INSERT INTO dbo.Profesionales ([Matrícula], Nombre, Especialidad, ValorConsulta, Activo)
VALUES
    (N'MN-101', N'Dra. Laura Gómez',   N'Clínica Médica', 15000.00, 1),
    (N'MN-204', N'Dr. Martín Pérez',   N'Cardiología',    22000.00, 1),
    (N'MN-309', N'Dra. Sofía Álvarez', N'Dermatología',   18000.00, 0);

INSERT INTO dbo.Pacientes (Dni, Nombre, Activo)
VALUES
    (N'34111222', N'Lucas Fernández', 1),
    (N'38999444', N'Camila Torres',   1),
    (N'29555666', N'Jorge Morales',   0);
GO
