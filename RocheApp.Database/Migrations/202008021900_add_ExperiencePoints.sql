ALTER TABLE [dbo].[User] ADD [ExperiencePoints] INT NULL;
GO
UPDATE [dbo].[User] SET [ExperiencePoints] = 0;
GO
ALTER TABLE [dbo].[User] ALTER COLUMN [ExperiencePoints] INT NOT NULL;