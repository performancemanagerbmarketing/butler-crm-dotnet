CREATE TABLE [dbo].[Others]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[JobId] int not null,
	[OthersName] nvarchar(max),
	[Cost] decimal,
	[CreatedAt] datetime,
	[CreatedBy] nvarchar(max),
	[Date] datetime, 
    CONSTRAINT [FK_Others_Job] FOREIGN KEY ([JobId]) REFERENCES [dbo].[Job]([Id]),
)
