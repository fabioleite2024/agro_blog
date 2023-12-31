/* Para impedir possíveis problemas de perda de dados, analise este script detalhadamente antes de executá-lo fora do contexto do designer de banco de dados.*/
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
ALTER TABLE dbo.AspNetUsers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_BlogPost
	(
	Id int NOT NULL IDENTITY (1, 1),
	Title nvarchar(150) NOT NULL,
	SubTitle nvarchar(100) NOT NULL,
	AuthorId nvarchar(128) NOT NULL,
	Body nvarchar(MAX) NOT NULL,
	DateCreated datetime NOT NULL,
	MetaDescription nvarchar(400) NULL,
	ImagePath nvarchar(255) NULL,
	SlugTitle nvarchar(150) NOT NULL,
	DateModified datetime NULL,
	ModifiedBy nvarchar(50) NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_BlogPost SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_BlogPost ON
GO
IF EXISTS(SELECT * FROM dbo.BlogPost)
	 EXEC('INSERT INTO dbo.Tmp_BlogPost (Id, Title, SubTitle, Body, DateCreated, MetaDescription, ImagePath, SlugTitle, DateModified, ModifiedBy)
		SELECT Id, Title, SubTitle, Body, DateCreated, MetaDescription, ImagePath, SlugTitle, DateModified, ModifiedBy FROM dbo.BlogPost WITH (HOLDLOCK TABLOCKX)')
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
ALTER TABLE dbo.BlogPost ADD CONSTRAINT
	FK_BlogPost_AspNetUsers FOREIGN KEY
	(
	AuthorId
	) REFERENCES dbo.AspNetUsers
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
