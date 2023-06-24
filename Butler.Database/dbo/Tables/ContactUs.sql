CREATE TABLE [dbo].[ContactUs]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[Name] nvarchar(max),
	[Email] nvarchar(max),
	[Number] varchar(max),
	[Description] nvarchar(max),
	[Date] datetime,
)
