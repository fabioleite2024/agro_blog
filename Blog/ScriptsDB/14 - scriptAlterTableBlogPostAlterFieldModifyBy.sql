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
EXECUTE sp_rename N'dbo.BlogPost.ModifiedBy', N'Tmp_ModifiedById', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.BlogPost.Tmp_ModifiedById', N'ModifiedById', 'COLUMN' 
GO
ALTER TABLE dbo.BlogPost SET (LOCK_ESCALATION = TABLE)
GO
COMMIT