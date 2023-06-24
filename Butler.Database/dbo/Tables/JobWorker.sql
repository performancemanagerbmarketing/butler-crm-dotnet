CREATE TABLE [dbo].[JobWorker]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[JobId] int not null,
	[WorkerId] int not null,
	[WorkerName] nvarchar(max),
	[JobStatus] bit,
	[CreatedAt] datetime,
	[CreatedBy] nvarchar(max),
	[Date] datetime, 
    CONSTRAINT [FK_JobWorker_Job] FOREIGN KEY ([JobId]) REFERENCES [dbo].[Job]([Id]),

)
