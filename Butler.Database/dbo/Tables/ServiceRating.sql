CREATE TABLE [dbo].[ServiceRating]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[CustomerId] int,
	[CustomerName] varchar(max),
	[JobId] int,
	[NoOfRating] int,
	[Remarks] nvarchar(max),
	[JobTitle] nvarchar(max),
	[CreatedAt] datetime,
	[CreatedBy] nvarchar(max),
	[Date] datetime, 
    CONSTRAINT [FK_ServiceRating_Job] FOREIGN KEY ([JobId]) REFERENCES [dbo].[Job]([Id]),

)
