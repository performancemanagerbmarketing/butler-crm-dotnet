CREATE TABLE [dbo].[ControlCenter]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[Name] nvarchar(max),
	[Latitude] decimal,
	[Longitude] decimal,
	[LocationId] int,
	[LocationName] nvarchar(max), 
    [IsAdded] bit not null default(0),
	[IsActive] bit not null default(1),
)
