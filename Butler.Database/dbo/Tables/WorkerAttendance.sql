CREATE TABLE [dbo].[WorkerAttendance]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[WorkerId] int not null,
	[IsPresent] bit not null default(0),
	[IsLate] bit not null default(0),
	[IsAbsent] bit not null default(0),
	[IsDayOff] bit not null default(0),
	[StartDateTime] datetime,
	[EndDateTime] datetime,
	[StartDate] datetime,
	[EndDate] datetime,
	[StartStatus] bit not null default(0),
	[EndStatus] bit not null default(0),
	[CreatedBy] nvarchar(max),
	[CreatedAt] datetime,
	[UpdatedAt] datetime,
	[UpdateBy] nvarchar(max),
	[Date] datetime, 
    CONSTRAINT [FK_WorkerAttendance_UserProfile] FOREIGN KEY ([WorkerId]) REFERENCES [dbo].[UserProfile]([Id]),
	
)

