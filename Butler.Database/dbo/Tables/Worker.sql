CREATE TABLE [dbo].[Worker]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[Name] nvarchar(max),
	[Description] nvarchar(max),
	[ProfileImageUrl] nvarchar(max),
	[Contact] nvarchar(max),
	[CNIC] nvarchar(max),
	[CNICFrontImageUrl] nvarchar(max),
	[CNICBackImageUrl] nvarchar(max),
	[ControlCenterId] int,
	[ControllerName] nvarchar(max),
	[ControlCenterName] nvarchar(max),
	[CreatedAt] datetime,
	[CreatedBy] nvarchar(max),
	[Date] datetime,
	[IsAdded] bit not null default(0),
)
