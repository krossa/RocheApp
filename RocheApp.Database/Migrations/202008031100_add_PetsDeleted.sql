ALTER TABLE [dbo].[User] ADD [PetsDeleted] BIT NULL;
GO
UPDATE [dbo].[User] SET [PetsDeleted] = 0;
GO
ALTER TABLE [dbo].[User] ALTER COLUMN [PetsDeleted] BIT NOT NULL;