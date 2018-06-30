create schema Logging
GO

CREATE table Logging.[EventLog]
(
	[EventLogID] uniqueidentifier not null,
	[EventType] int not null,
	[Key] varchar(255) not null,
	[Message] varchar(max) not null,
	[EntryDate] datetime not NULL,
	[Timestamp] rowversion NULL,
)

create table Logging.[ChangeLog]
(
	[ChangeLogID] int not null identity(1, 1),
	[ClassName] varchar(255) not null,
	[PropertyName] varchar(255) not null,
	[Key] varchar(255) not null,
	[OriginalValue] varchar(max) null,
	[CurrentValue] varchar(max) null,
	[UserID] int not null,
	[ChangeDate] datetime not NULL,
	[Timestamp] rowversion NULL,
)

create table Logging.[ChangeLogExclusion]
(
	[ChangeLogExclusionID] int not null identity(1, 1),
	[EntityName] varchar(128) not null,
	[PropertyName] varchar(128) not NULL,
	[Timestamp] rowversion NULL,
)

alter table Logging.[EventLog]
	add constraint [PK_EventLog] primary key (EventLogID)
go

alter table Logging.[ChangeLog]
	add constraint [PK_ChangeLog] primary key (ChangeLogID)
go

alter table Logging.[ChangeLogExclusion]
	add constraint [PK_ChangeLogExclusion] primary key(ChangeLogExclusionID)
go