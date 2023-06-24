CREATE TABLE [dbo].[Quotations]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[Name] nvarchar(max),
	[Number] nvarchar(max),
	[Service] nvarchar(max),
	[CreatedAt] datetime,
	[CreatedBy] nvarchar(max),
	[UpdatedAt] datetime,
	[UpdatedBy] nvarchar(max),
	[Date] datetime,

)
