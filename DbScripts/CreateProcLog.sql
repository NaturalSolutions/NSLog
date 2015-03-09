IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PR_LOG_MESSAGE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].PR_LOG_MESSAGE
GO
CREATE PROCEDURE dbo.PR_LOG_MESSAGE(
		@LOG_LEVEL int,
        @ORIGIN VARCHAR(8000),
        @SCOPE VARCHAR(400),
        @LOGUSER VARCHAR(8000),
        @DOMAINE VARCHAR(8000),
        @MESSAGE_NUMBER  BIGINT,
        @LOG_MESSAGE  VARCHAR(MAX)
        )
as
begin
    INSERT INTO [NSLog].[dbo].[TLOG_MESSAGES]
           ([JCRE]
           ,[LOG_LEVEL]
           ,[ORIGIN]
           ,[SCOPE]
           ,[LOGUSER]
           ,[DOMAINE]
           ,MESSAGE_NUMBER
           ,[LOG_MESSAGE])
     VALUES
           (Getdate()
           ,@LOG_LEVEL
           ,@ORIGIN
           ,@SCOPE
           ,@LOGUSER
           ,@DOMAINE
           ,@MESSAGE_NUMBER
           ,@LOG_MESSAGE)
end

GO


