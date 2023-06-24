CREATE TABLE [dbo].[JobDetail]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[JobId] int not null,
	[SubCategoryId] int not null,
	[SubCategoryName] nvarchar(max),
	[Amount] decimal,
	[MaterialName] nvarchar(max),
	[MaterialCost] decimal,
	[Description] nvarchar(max),
	[Discount] decimal,
	[CreatedAt] datetime,
	[CreatedBy] nvarchar(max),
	[Date] datetime, 
    CONSTRAINT [FK_JobDetail_SubCategory] FOREIGN KEY ([SubCategoryId]) REFERENCES [dbo].[SubCategory]([Id]), 
    CONSTRAINT [FK_JobDetail_Job] FOREIGN KEY ([JobId]) REFERENCES [dbo].[Job]([Id]),
 )
