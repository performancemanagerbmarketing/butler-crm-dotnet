CREATE TABLE [dbo].[ControllerCategory]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[ControllerId] int not null,
	[CategoryId] int not null,
	[ControllerName] nvarchar(max),
	[CategoryName] nvarchar(max),
	[CreatedAt] datetime,
	[CreatedBy] nvarchar(max),
	[UpdatedAt] datetime,
	[UpdatedBy] nvarchar(max),
	[Date] datetime, 
    CONSTRAINT [FK_ControllerCategory_UserProfile] FOREIGN KEY ([ControllerId]) REFERENCES [dbo].[UserProfile]([Id]), 
    CONSTRAINT [FK_ControllerCategory_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category]([Id]),
)
