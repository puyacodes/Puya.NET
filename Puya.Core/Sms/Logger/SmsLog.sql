SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SmsLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SmsLog](
	[Serial] [int] IDENTITY(1,1) NOT NULL,
	[LogDate] [datetime] NULL,
	[Provider] [nvarchar](100) NULL,
	[LineNo] [varchar](25) NULL,
	[Topic] [nvarchar](100) NULL,
	[Category] [nvarchar](250) NULL,
	[MobileNo] [varchar](15) NULL,
	[Message] [nvarchar](4000) NULL,
	[Response] [nvarchar](max) NULL,
	[Status] [varchar](50) NULL,
	[RefCode] [varchar](20) NULL,
	[Success] [bit] NULL,
	[Data] [nvarchar](max) NULL,
	[Error] [nvarchar](max) NULL,
 CONSTRAINT [PK_SmsLog] PRIMARY KEY CLUSTERED 
(
	[Serial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_SmsLog_LogDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SmsLog] ADD  CONSTRAINT [DF_SmsLog_LogDate]  DEFAULT (getdate()) FOR [LogDate]
END
GO
