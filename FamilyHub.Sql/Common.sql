create schema Common
GO

CREATE TABLE [Common].[ContactAddress](
	[ContactAddressID] [INT] IDENTITY(1,1) NOT NULL,
	[Address1] [NVARCHAR](100) NULL,
	[Address2] [NVARCHAR](100) NULL,
	[City] [NVARCHAR](30) NULL,
	[State] [NVARCHAR](5) NULL,
	[ZipCode] [NVARCHAR](15) NULL,
	CreatedBy INT NOT NULL,
	CreatedOn DATETIME NOT NULL,
	LastUpdatedBy INT NULL,
	LastUpdatedOn DATETIME NULL,
	[Timestamp] rowversion NULL,
	PRIMARY KEY (ContactAddressID),
);
GO

CREATE TABLE [Common].[Users]
(
	[UserID] [INT] IDENTITY(1,1) NOT NULL,
	[Email] [NVARCHAR](100) NOT NULL,
	FirstName NVARCHAR(30) NOT NULL,
	MiddleInitial NVARCHAR(10) NULL,
	LastName NVARCHAR(30) NOT NULL,
	[Active] [BIT] NOT NULL DEFAULT ((1)),
	[IsCoreUser] [BIT] NULL,
	[LastLogIn] DATETIME NOT NULL,
	CreatedBy INT NULL,
	CreatedOn DATETIME NULL,
	LastUpdatedBy INT NULL,
	LastUpdatedOn DATETIME NULL,
	[Note] [NVARCHAR](255) NULL,
	[RefreshToken] NVARCHAR(300) NULL,
	ContactAddressID INT NULL,
	[Timestamp] rowversion NULL,
	PRIMARY KEY (UserID),
	CONSTRAINT FK_UsersContactAddress FOREIGN KEY (ContactAddressID)
    REFERENCES Common.ContactAddress(ContactAddressID)
);
GO

CREATE TABLE [Common].[UserPassword]
(
	[UserPasswordID] [INT] IDENTITY(1,1) NOT NULL,
	[Active] [BIT] NOT NULL,
	[IsTemporary] [BIT] NOT NULL,
	[Password] [NVARCHAR](MAX) NOT NULL,
	[PasswordCreated] [DATETIME2] NOT NULL,
	[PasswordUpdated] [DATETIME2] NULL,
	[UserID] [INT] NOT NULL,
	[Timestamp] rowversion NULL,
	PRIMARY KEY (UserPasswordID),
	CONSTRAINT FK_UsersUserPassword FOREIGN KEY (UserId)
    REFERENCES Common.Users(UserId)
);
GO

INSERT INTO Logging.ChangeLogExclusion
(
    EntityName,
    PropertyName
)
VALUES
(   'User', -- EntityName - varchar(128)
    'ContactAddressFk'  -- PropertyName - varchar(128)
)
GO
INSERT INTO Logging.ChangeLogExclusion
(
    EntityName,
    PropertyName
)
VALUES
(   'User', -- EntityName - varchar(128)
    'UserPasswords'  -- PropertyName - varchar(128)
)
GO
INSERT INTO Logging.ChangeLogExclusion
(
    EntityName,
    PropertyName
)
VALUES
(   'UserPassword', -- EntityName - varchar(128)
    'UserFk'  -- PropertyName - varchar(128)
)
GO
INSERT INTO Logging.ChangeLogExclusion
(
    EntityName,
    PropertyName
)
VALUES
(   'ContactAddress', -- EntityName - varchar(128)
    'Users'  -- PropertyName - varchar(128)
)
GO

SELECT * FROM Common.Users u
SELECT * FROM Common.UserPassword
SELECT * FROM Logging.ChangeLog

TRUNCATE TABLE common.ContactAddress
go
TRUNCATE TABLE common.Users
go
TRUNCATE TABLE common.UserPassword
go

