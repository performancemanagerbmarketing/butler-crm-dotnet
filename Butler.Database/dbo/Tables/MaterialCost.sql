CREATE TABLE [dbo].[MaterialCost]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[JobId] int not null,
	[MaterialName] nvarchar(max),
	[Cost] decimal,
	[CreatedAt] datetime,
	[CreatedBy] nvarchar(max),
	[Date] datetime, 
    CONSTRAINT [FK_MaterialCost_Job] FOREIGN KEY ([JobId]) REFERENCES [dbo].[Job]([Id]),
)
