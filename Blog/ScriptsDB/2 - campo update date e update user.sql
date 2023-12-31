/*
   quarta-feira, 3 de janeiro de 201822:15:24
   User: Rafael
   Server: DESKTOP-5IRCEMM\MSSQLSERVER_2014
   Database: BlogVarellaMotos
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.BlogPost ADD
	UpdateDate datetime NULL,
	UpdateUser nvarchar(50) NULL
GO
ALTER TABLE dbo.BlogPost SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.BlogPost', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.BlogPost', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.BlogPost', 'Object', 'CONTROL') as Contr_Per 