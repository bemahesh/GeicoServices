--first script
USE [master]
GO
CREATE LOGIN [FreedomStoreUser] WITH PASSWORD=N'myFreedom123', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
ALTER SERVER ROLE [dbcreator] ADD MEMBER [FreedomStoreUser]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [FreedomStoreUser]
GO
USE [master]
GO
CREATE USER [FreedomStoreUser] FOR LOGIN [FreedomStoreUser]
GO
USE [master]
GO
ALTER USER [FreedomStoreUser] WITH DEFAULT_SCHEMA=[dbo]
GO
USE [master]
GO
ALTER ROLE [db_datareader] ADD MEMBER [FreedomStoreUser]
GO
USE [master]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [FreedomStoreUser]
GO
USE [master]
GO
ALTER ROLE [db_owner] ADD MEMBER [FreedomStoreUser]
GO
