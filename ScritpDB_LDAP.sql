CREATE TABLE Users
(
    UserId INT IDENTITY(1,1) PRIMARY KEY,  -- ID único del usuario
    Username NVARCHAR(255) NOT NULL,        -- Nombre de usuario
    Email NVARCHAR(255),                    -- Correo electrónico
    DisplayName NVARCHAR(255),              -- Nombre para mostrar
    Department NVARCHAR(255),               -- Departamento
    Title NVARCHAR(255)                    -- Título
);

CREATE PROCEDURE SaveUser
    @Username NVARCHAR(255),
    @Email NVARCHAR(255),
    @DisplayName NVARCHAR(255),
    @Department NVARCHAR(255),
    @Title NVARCHAR(255)
AS
BEGIN
    INSERT INTO Users (Username, Email, DisplayName, Department, Title)
    VALUES (@Username, @Email, @DisplayName, @Department, @Title);
END;
