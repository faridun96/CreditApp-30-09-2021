USE [CreditApp]
GO
/****** Object:  Table [dbo].[Applications]    Script Date: 30.09.2021 23:45:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applications](
	[ApplicationId] [int] IDENTITY(1,1) NOT NULL,
	[CreditSum] [int] NOT NULL,
	[Posted] [datetime] NOT NULL CONSTRAINT [DF_Applications_Posted]  DEFAULT (getdate()),
	[AppUserId] [int] NOT NULL,
	[Income] [int] NOT NULL,
	[Country] [nvarchar](50) NOT NULL,
	[Goal] [nvarchar](50) NOT NULL,
	[Age] [int] NOT NULL,
	[Family] [nvarchar](50) NOT NULL,
	[Score] [int] NOT NULL,
	[IsCompleted] [bit] NOT NULL CONSTRAINT [DF_Applications_IsCompleted]  DEFAULT ((0)),
	[Duration] [int] NOT NULL CONSTRAINT [DF_Applications_Duration]  DEFAULT ((1)),
	[Gender] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED 
(
	[ApplicationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AppUsers]    Script Date: 30.09.2021 23:45:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUsers](
	[AppUserId] [int] IDENTITY(1,1) NOT NULL,
	[AppUserName] [nvarchar](50) NOT NULL,
	[Psw] [nvarchar](50) NOT NULL,
	[IsAdmin] [bit] NOT NULL CONSTRAINT [DF_AppUsers_IsAdmin]  DEFAULT ((0)),
	[Passport] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AppUsers] PRIMARY KEY CLUSTERED 
(
	[AppUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Applications] ON 

INSERT [dbo].[Applications] ([ApplicationId], [CreditSum], [Posted], [AppUserId], [Income], [Country], [Goal], [Age], [Family], [Score], [IsCompleted], [Duration], [Gender]) VALUES (10, 3000, CAST(N'2021-09-30 21:27:34.403' AS DateTime), 6, 3000, N'Таджикистан', N'бытовая техника', 25, N'семьянин', 13, 1, 12, N'мужчина')
INSERT [dbo].[Applications] ([ApplicationId], [CreditSum], [Posted], [AppUserId], [Income], [Country], [Goal], [Age], [Family], [Score], [IsCompleted], [Duration], [Gender]) VALUES (11, 3000, CAST(N'2021-09-30 21:28:32.883' AS DateTime), 6, 1000, N'Таджикистан', N'телефон', 25, N'холост', 10, 0, 12, N'мужчина')
INSERT [dbo].[Applications] ([ApplicationId], [CreditSum], [Posted], [AppUserId], [Income], [Country], [Goal], [Age], [Family], [Score], [IsCompleted], [Duration], [Gender]) VALUES (15, 1, CAST(N'2021-09-30 23:20:58.923' AS DateTime), 7, 0, N'dfd', N'бытовая техника', 18, N'холост', 5, 0, 1, N'мужчина')
INSERT [dbo].[Applications] ([ApplicationId], [CreditSum], [Posted], [AppUserId], [Income], [Country], [Goal], [Age], [Family], [Score], [IsCompleted], [Duration], [Gender]) VALUES (16, 1000, CAST(N'2021-09-30 23:23:49.203' AS DateTime), 7, 3000, N'Таджикистан', N'бытовая техника', 25, N'семьянин', 13, 1, 12, N'мужчина')
SET IDENTITY_INSERT [dbo].[Applications] OFF
SET IDENTITY_INSERT [dbo].[AppUsers] ON 

INSERT [dbo].[AppUsers] ([AppUserId], [AppUserName], [Psw], [IsAdmin], [Passport]) VALUES (1, N'admin', N'1234', 1, N'A12121')
INSERT [dbo].[AppUsers] ([AppUserId], [AppUserName], [Psw], [IsAdmin], [Passport]) VALUES (6, N'919626062', N'1234', 0, N'A12121')
INSERT [dbo].[AppUsers] ([AppUserId], [AppUserName], [Psw], [IsAdmin], [Passport]) VALUES (7, N'907252573', N'1234', 0, N'A12121')
SET IDENTITY_INSERT [dbo].[AppUsers] OFF
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_AppUsers] FOREIGN KEY([AppUserId])
REFERENCES [dbo].[AppUsers] ([AppUserId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_AppUsers]
GO
