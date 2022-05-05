
Use FreedomStore;
GO

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'FreedomStoreUser')
BEGIN
    CREATE USER [FreedomStoreUser] FOR LOGIN [FreedomStoreUser]
    EXEC sp_addrolemember N'db_owner', N'FreedomStoreUser'
END;
GO

USE [FreedomStore]
GO
ALTER ROLE [db_datareader] ADD MEMBER [FreedomStoreUser]
GO
USE [FreedomStore]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [FreedomStoreUser]
GO
USE [FreedomStore]
GO
ALTER ROLE [db_owner] ADD MEMBER [FreedomStoreUser]
GO
