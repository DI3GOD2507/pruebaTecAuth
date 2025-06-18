-- 1. Crear base de datos
CREATE DATABASE AuthServiceDB;
GO

USE AuthServiceDB;
GO

CREATE TABLE [dbo].[Users] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Username] NVARCHAR(100) NOT NULL UNIQUE,
    [Email] NVARCHAR(256) NOT NULL UNIQUE,
    [PasswordHash] NVARCHAR(512) NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETDATE(),
    [UpdatedAt] DATETIME2 NULL
);
GO

CREATE TABLE [dbo].[Roles] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL UNIQUE
);
GO

CREATE TABLE [dbo].[UserRoles] (
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    PRIMARY KEY (UserId, RoleId),
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (RoleId) REFERENCES Roles(Id) ON DELETE CASCADE
);
GO

CREATE LOGIN AuthServiceLogin WITH PASSWORD = 'P@ssword123!';
GO

CREATE USER AuthServiceUser FOR LOGIN AuthServiceLogin;
GO

ALTER ROLE db_datareader ADD MEMBER AuthServiceUser;
ALTER ROLE db_datawriter ADD MEMBER AuthServiceUser;
GO