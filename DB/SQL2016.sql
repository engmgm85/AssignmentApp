USE [AssignmentAppData]
GO
/****** Object:  Table [dbo].[HearSource]    Script Date: 08/08/2021 12:12:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HearSource](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[ChoiceOrder] [int] NOT NULL,
 CONSTRAINT [PK_HearSource] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Survey]    Script Date: 08/08/2021 12:12:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Survey](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[Gender] [bit] NOT NULL,
	[Birthdate] [date] NOT NULL,
	[NumberOfKids] [int] NOT NULL,
 CONSTRAINT [PK_Survey] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Survey_HearSource]    Script Date: 08/08/2021 12:12:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Survey_HearSource](
	[SurveyId] [int] NOT NULL,
	[HearSourceId] [int] NOT NULL,
 CONSTRAINT [PK_Survey_HearSource\] PRIMARY KEY CLUSTERED 
(
	[SurveyId] ASC,
	[HearSourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[HearSource] ON 

INSERT [dbo].[HearSource] ([Id], [Title], [ChoiceOrder]) VALUES (1, N'Google', 1)
INSERT [dbo].[HearSource] ([Id], [Title], [ChoiceOrder]) VALUES (2, N'TV', 2)
INSERT [dbo].[HearSource] ([Id], [Title], [ChoiceOrder]) VALUES (3, N'Radio', 3)
INSERT [dbo].[HearSource] ([Id], [Title], [ChoiceOrder]) VALUES (4, N'Social Network', 4)
SET IDENTITY_INSERT [dbo].[HearSource] OFF
GO
SET IDENTITY_INSERT [dbo].[Survey] ON 

INSERT [dbo].[Survey] ([Id], [FullName], [Gender], [Birthdate], [NumberOfKids]) VALUES (1, N'1', 1, CAST(N'2020-01-01' AS Date), 5)
INSERT [dbo].[Survey] ([Id], [FullName], [Gender], [Birthdate], [NumberOfKids]) VALUES (2, N'Mohammed Gamal', 1, CAST(N'2021-08-06' AS Date), 10)
INSERT [dbo].[Survey] ([Id], [FullName], [Gender], [Birthdate], [NumberOfKids]) VALUES (3, N'Mohammed Gamal', 1, CAST(N'2021-08-06' AS Date), 10)
INSERT [dbo].[Survey] ([Id], [FullName], [Gender], [Birthdate], [NumberOfKids]) VALUES (4, N'Mohammed Gamal', 1, CAST(N'2021-08-06' AS Date), 0)
INSERT [dbo].[Survey] ([Id], [FullName], [Gender], [Birthdate], [NumberOfKids]) VALUES (5, N'Mgm Asdsa a das das d as', 1, CAST(N'2021-08-06' AS Date), 0)
SET IDENTITY_INSERT [dbo].[Survey] OFF
GO
INSERT [dbo].[Survey_HearSource] ([SurveyId], [HearSourceId]) VALUES (3, 1)
INSERT [dbo].[Survey_HearSource] ([SurveyId], [HearSourceId]) VALUES (3, 2)
INSERT [dbo].[Survey_HearSource] ([SurveyId], [HearSourceId]) VALUES (3, 3)
INSERT [dbo].[Survey_HearSource] ([SurveyId], [HearSourceId]) VALUES (5, 1)
INSERT [dbo].[Survey_HearSource] ([SurveyId], [HearSourceId]) VALUES (5, 4)
GO
ALTER TABLE [dbo].[Survey_HearSource]  WITH CHECK ADD  CONSTRAINT [FK_Survey_HearSource_HearSource] FOREIGN KEY([HearSourceId])
REFERENCES [dbo].[HearSource] ([Id])
GO
ALTER TABLE [dbo].[Survey_HearSource] CHECK CONSTRAINT [FK_Survey_HearSource_HearSource]
GO
ALTER TABLE [dbo].[Survey_HearSource]  WITH CHECK ADD  CONSTRAINT [FK_Survey_HearSource_Survey] FOREIGN KEY([SurveyId])
REFERENCES [dbo].[Survey] ([Id])
GO
ALTER TABLE [dbo].[Survey_HearSource] CHECK CONSTRAINT [FK_Survey_HearSource_Survey]
GO
