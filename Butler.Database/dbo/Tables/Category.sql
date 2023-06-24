CREATE TABLE [dbo].[Category]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[Name] nvarchar(max),
	[Description] nvarchar(max),
	[WebImageUrl] nvarchar(max),
	[ProfileImageUrl] nvarchar(max),
	[CreatedAt] datetime,
	[CreatedBy] nvarchar(max),
	[Date] datetime, 
	[IsAdded] bit not null default(0), 
	--IsACtive
	[Type] int not null default(0),
   

)
