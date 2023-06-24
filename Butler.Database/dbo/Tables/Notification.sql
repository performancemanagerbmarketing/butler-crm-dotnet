CREATE TABLE [dbo].[Notification]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[AdminId] int,
	[CustomerId] int,
	[Title] nvarchar(max),
	[Content] nvarchar(max),
	[IsRead] bit not null default(0),
	[Link] nvarchar(max),
	[CreatedAt] datetime,
	[CreatedBy] nvarchar(max),
	[Date] datetime,
)
