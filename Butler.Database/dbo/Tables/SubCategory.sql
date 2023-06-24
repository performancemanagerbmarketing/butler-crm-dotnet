CREATE TABLE [dbo].[SubCategory]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[CategoryId] int,
	[Name] nvarchar(max),
	[Cost] decimal,
	[ImageUrl] nvarchar(max),
	[ConditionalKey] nvarchar(max),
	[CreatedAt] datetime,
	[CreatedBy] nvarchar(max),
	[Date] datetime, 
	[IsActive] bit not null default(1),
	[AdditionalInfoHeading] nvarchar(max),
	[AdditionalInfoKey] nvarchar(max),
    CONSTRAINT [FK_SubCategory_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category]([Id]),

)
