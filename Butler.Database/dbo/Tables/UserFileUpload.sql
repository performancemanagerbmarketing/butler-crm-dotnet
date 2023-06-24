CREATE TABLE [dbo].[UserFileUpload]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[UserId] int not null,
	[UserType] int,
	[FileUploadUrl] nvarchar(max),
	[FileType] int,
	[CreatedAt] datetime,
	[CreatedBy] nvarchar(max),
	[Date] datetime, 
    CONSTRAINT [FK_UserFileUpload_UserProfile] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserProfile]([Id])
)
