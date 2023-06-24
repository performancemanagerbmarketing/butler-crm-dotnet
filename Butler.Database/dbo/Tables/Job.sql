﻿CREATE TABLE [dbo].[Job]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[Title] nvarchar(max),
	[Description] nvarchar(max),
	[CustomerId] int,
	[CustomerName] nvarchar(max),
	[CustomerContact] nvarchar(max),
	[CustomerEmail] nvarchar(max),
	[CustomerAddress] nvarchar(max),
	[Latitude] decimal,
	[Longitutde] decimal,
	[JobAddress] nvarchar(max),
	[CategoryId] int,
	[CategoryName] nvarchar(max),
	[ControlCenterId] int,
	[ControlCenterName] nvarchar(max),
	[AudioUrl] nvarchar(max),
	[ImageUrl] nvarchar(max),
	[ImageUrl2] nvarchar(max),
	[ImageUrl3] nvarchar(max),
	[VideoUrl] nvarchar(max),
	[Status] int,
	[TotalAmount] decimal,
	[CreatedAt] datetime,
	[CreatedBy] varchar(max),
	[UpdatedAt] datetime,
	[UpdateBy] varchar(max),
	[Date] datetime, 
	[BookingDate] datetime,
	[StartTime] datetime,
	[CompleteDateTime] datetime,
	[EndTime] datetime,
	[Duration] nvarchar(max),
	[PaymentStatus] int not null default(0),
	[IsAdded] bit not null default(0),
	[Discount] decimal(18,3),
	[DiscountPercentage] int,
    CONSTRAINT [FK_Job_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category]([Id]), 
    CONSTRAINT [FK_Job_ControlCenter] FOREIGN KEY ([ControlCenterId]) REFERENCES [dbo].[ControlCenter]([Id]), 
    CONSTRAINT [FK_Job_UserProfile] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[UserProfile]([Id]),

)