CREATE TABLE [dbo].[Reference]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[FullName] nvarchar(max),
	[CNIC] nvarchar(max),
	[CNICFrontImageUrl] nvarchar(max),
	[CNICBackImageUrl] nvarchar(max),
	[Notes] nvarchar(max),
	[Address] nvarchar(max),
	[IsAMember] bit not null default(0),
	[UserId] nvarchar(128),
	[ProfileImageUrl] nvarchar(max),
	[CreatedAt] datetime,
	[CreatedBy] nvarchar(max),
	[UpdateAt] datetime,
	[UpdateBy] nvarchar(max),
	[Date] datetime, 
   [IsAdded] bit not null default(0),
	
)
