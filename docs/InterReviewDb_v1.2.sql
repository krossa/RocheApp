CREATE DATABASE [InterReview]
GO

ALTER DATABASE [InterReview] SET QUERY_STORE=ON
GO

USE [InterReview]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pet](
	[PetId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[TimeStampValue] [timestamp] NOT NULL,
 CONSTRAINT [PK_Pet] PRIMARY KEY CLUSTERED 
(
	[PetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[Status] [bit] NULL,
	[TimeStampValue] [timestamp] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'95c61165-ec78-424b-8c26-00fc44f53583', N'Hugo', N'9fcd56c9-5c6f-4c9c-98e3-a049a4a21ca2')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'715f0df4-e7a1-44ea-a37e-0d5ff5e03b5a', N'Hektor', N'07b6e09c-ae2d-423f-90bb-7066af9df1c8')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'594dc71c-af82-4371-824e-0ddd2fdef83e', N'Roki', N'd4ee366c-b9ca-4cac-9c32-06a46179ecf8')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'e5680cce-bdf3-4a33-829c-14d93685d84f', N'Misza', N'92ff2fb6-3e57-473d-a096-1b041968457f')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'0ba15478-6e42-4fb6-ac5f-17e72d9f483f', N'Diego', N'3de248a7-f5e6-4a26-a512-918d237c249e')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'e0860cdb-8112-4b4a-b4b8-2227fa6f29e5', N'Leo', N'3af55f98-9b6b-4324-bb7a-6e3e3a049399')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'3d3dab71-4db5-405a-98ff-25ac1ef4986a', N'Fuks', N'3de248a7-f5e6-4a26-a512-918d237c249e')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'e965fb11-6c35-47b4-bae7-2e67ca02ab37', N'Emi', N'9fcd56c9-5c6f-4c9c-98e3-a049a4a21ca2')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'7ae826df-94be-4a2d-bcbd-317acf55c6d2', N'Rico', N'3de248a7-f5e6-4a26-a512-918d237c249e')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'963c5655-e2c7-4fad-97a4-3c627406d902', N'Fado', N'07b6e09c-ae2d-423f-90bb-7066af9df1c8')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'e6596a4a-3aa7-4f36-a400-42cff52770a0', N'Toffik', N'd4ee366c-b9ca-4cac-9c32-06a46179ecf8')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'4b2e02a4-1e66-47de-9a7d-4d67dda0e274', N'Charli', N'9fcd56c9-5c6f-4c9c-98e3-a049a4a21ca2')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'6537bfa2-b39f-49fb-adfe-5f7c52de9c56', N'Maks', N'92ff2fb6-3e57-473d-a096-1b041968457f')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'2f8a0acf-ab65-4736-be3b-61b22ccc0558', N'Fafik', N'92ff2fb6-3e57-473d-a096-1b041968457f')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'082ca532-98aa-427a-b874-6dc24e620850', N'Biszkopt', N'592e1fcf-aace-4902-8b9b-c1f499cd09df')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'53a4d7dd-f4eb-4b68-b0be-731eccf0d5e1', N'Hades', N'3de248a7-f5e6-4a26-a512-918d237c249e')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'82bf9c1e-2efb-4fac-9d5c-7ff94478a7ea', N'Shadow', N'592e1fcf-aace-4902-8b9b-c1f499cd09df')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'9d7c3e4c-7aec-425d-ba4e-90b78ffed967', N'Puszek', N'4575f73a-5695-4688-b2ec-80c7c6ae82ad')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'bbd4af3b-c2b6-462c-abb3-a04578bff096', N'Max', N'3af55f98-9b6b-4324-bb7a-6e3e3a049399')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'2f27c54f-4f92-4b58-a190-a3bef028200b', N'Harry', N'3de248a7-f5e6-4a26-a512-918d237c249e')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'8a13ad6a-0447-4258-b0cd-b1bd372cab86', N'Riko', N'07b6e09c-ae2d-423f-90bb-7066af9df1c8')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'a9512ced-891e-4fdd-ab88-b9c6d98a425f', N'Kajtek', N'3de248a7-f5e6-4a26-a512-918d237c249e')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'fa0cdb8c-7e0e-43f1-866d-ba62a0454ec7', N'Laki', N'07b6e09c-ae2d-423f-90bb-7066af9df1c8')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'fb67091a-e942-4c55-a151-d396d9a0c437', N'Teddy', N'd4ee366c-b9ca-4cac-9c32-06a46179ecf8')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'47e0e34b-2fd7-4b37-ae01-de4a8eb70fd6', N'Pimpek', N'3de248a7-f5e6-4a26-a512-918d237c249e')
INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'eab04a6e-00a1-4ea9-9ff3-f826b73b8918', N'Alex', N'd4ee366c-b9ca-4cac-9c32-06a46179ecf8')
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status]) VALUES (N'd4ee366c-b9ca-4cac-9c32-06a46179ecf8', N'Mateusz', N'Wróblewski', 1)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status]) VALUES (N'92ff2fb6-3e57-473d-a096-1b041968457f', N'Daniel', N'Ziółkowski', 0)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status]) VALUES (N'1bbfc782-932b-4d57-87a8-3a54bed25974', N'Przemysław', N'Dąbrowski', 1)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status]) VALUES (N'1e6ff8d8-4133-4d76-bc9f-558bfde8feec', N'Krzysztof', N'Kołodziej', 0)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status]) VALUES (N'3af55f98-9b6b-4324-bb7a-6e3e3a049399', N'Cezary', N'Chmielewski', 1)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status]) VALUES (N'07b6e09c-ae2d-423f-90bb-7066af9df1c8', N'Krystian', N'Jakubowski', 0)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status]) VALUES (N'4575f73a-5695-4688-b2ec-80c7c6ae82ad', N'Oktawian', N'Zieliński', 1)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status]) VALUES (N'd2495e99-cb65-4779-a2d1-87a40b9bda14', N'Dorian', N'Brzeziński', 0)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status]) VALUES (N'3de248a7-f5e6-4a26-a512-918d237c249e', N'Alexander', N'Mazur', 1)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status]) VALUES (N'9fcd56c9-5c6f-4c9c-98e3-a049a4a21ca2', N'Mikołaj', N'Kozłowski', 0)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status]) VALUES (N'490f8791-2b37-46c3-85c9-a4ba9bf8de0f', N'Roman', N'Tomaszewski', 1)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status]) VALUES (N'592e1fcf-aace-4902-8b9b-c1f499cd09df', N'Łukasz', N'Sikorska', 0)
GO
ALTER TABLE [dbo].[Pet] ADD  CONSTRAINT [DF_Pet_PetId]  DEFAULT (newid()) FOR [PetId]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_UserId]  DEFAULT (newid()) FOR [UserId]
GO
