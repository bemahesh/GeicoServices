--second script
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
