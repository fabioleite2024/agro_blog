/*
   quarta-feira, 10 de janeiro de 201808:57:46
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
CREATE TABLE dbo.Tmp_BlogPost
	(
	Id int NOT NULL IDENTITY (1, 1),
	Title nvarchar(150) NOT NULL,
	SubTitle nvarchar(100) NOT NULL,
	Author nvarchar(50) NOT NULL,
	Body nvarchar(MAX) NOT NULL,
	DateCreated date NOT NULL,
	MetaDescription nvarchar(400) NULL,
	ImagePath nvarchar(255) NULL,
	SlugTitle nvarchar(150) NOT NULL,
	DateModified date NULL,
	ModifiedBy nvarchar(50) NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_BlogPost SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_BlogPost ON
GO
IF EXISTS(SELECT * FROM dbo.BlogPost)
	 EXEC('INSERT INTO dbo.Tmp_BlogPost (Id, Title, SubTitle, Author, Body, DateCreated, MetaDescription, ImagePath, SlugTitle, DateModified, ModifiedBy)
		SELECT Id, Title, SubTitle, Author, Body, DateCreated, MetaDescription, ImagePath, SlugTitle, DateModified, ModifiedBy FROM dbo.BlogPost WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_BlogPost OFF
GO
DROP TABLE dbo.BlogPost
GO
EXECUTE sp_rename N'dbo.Tmp_BlogPost', N'BlogPost', 'OBJECT' 
GO
ALTER TABLE dbo.BlogPost ADD CONSTRAINT
	PK_BlogPost PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.BlogPost ADD CONSTRAINT
	Unique_Title_BlogPost UNIQUE NONCLUSTERED 
	(
	Title
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.BlogPost', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.BlogPost', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.BlogPost', 'Object', 'CONTROL') as Contr_Per 