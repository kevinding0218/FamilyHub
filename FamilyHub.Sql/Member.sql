CREATE SCHEMA Member
GO

CREATE TABLE [Member].[MemberRelationship]
(
	[MemberRelationshipID] [INT] IDENTITY(1,1) NOT NULL,
	[MemberRelationshipName] NVARCHAR(50) NOT NULL,
	[MemberRelationshipDescription] NVARCHAR(250) NULL,
	[Timestamp] rowversion NULL,
	PRIMARY KEY ([MemberRelationshipID]),
);
GO

INSERT INTO [Member].[MemberRelationship]
(
    MemberRelationshipName,
    MemberRelationshipDescription
)
VALUES
(   N'Owner', -- MemberRelationshipName - nvarchar(50)
    N'Family Member'  -- MemberRelationshipDescription - nvarchar(250)
);
INSERT INTO [Member].[MemberRelationship]
(
    MemberRelationshipName,
    MemberRelationshipDescription
)
VALUES
(   N'Parent', -- MemberRelationshipName - nvarchar(50)
    N'Family Member'  -- MemberRelationshipDescription - nvarchar(250)
);
INSERT INTO [Member].[MemberRelationship]
(
    MemberRelationshipName,
    MemberRelationshipDescription
)
VALUES
(   N'Spouse', -- MemberRelationshipName - nvarchar(50)
    N'Family Member'  -- MemberRelationshipDescription - nvarchar(250)
);
INSERT INTO [Member].[MemberRelationship]
(
    MemberRelationshipName,
    MemberRelationshipDescription
)
VALUES
(   N'Child', -- MemberRelationshipName - nvarchar(50)
    N'Family Member'  -- MemberRelationshipDescription - nvarchar(250)
);
INSERT INTO [Member].[MemberRelationship]
(
    MemberRelationshipName,
    MemberRelationshipDescription
)
VALUES
(   N'Relative', -- MemberRelationshipName - nvarchar(50)
    N'Family Member'  -- MemberRelationshipDescription - nvarchar(250)
);
INSERT INTO [Member].[MemberRelationship]
(
    MemberRelationshipName,
    MemberRelationshipDescription
)
VALUES
(   N'Friend', -- MemberRelationshipName - nvarchar(50)
    N'Friends'  -- MemberRelationshipDescription - nvarchar(250)
);
INSERT INTO [Member].[MemberRelationship]
(
    MemberRelationshipName,
    MemberRelationshipDescription
)
VALUES
(   N'Colleague', -- MemberRelationshipName - nvarchar(50)
    N'Work/Office'  -- MemberRelationshipDescription - nvarchar(250)
);
GO

CREATE TABLE [Member].[MemberContact](
	[MemberContactID] [INT] IDENTITY(1,1) NOT NULL,	
	[FirstName] NVARCHAR(30) NOT NULL,
	[MiddleInitial] NVARCHAR(10) NULL,
	[LastName] NVARCHAR(30) NOT NULL,
	[HomePhone] NVARCHAR(20) NULL,
	[MobilePhone] NVARCHAR(20) NULL,
	[Location] NVARCHAR(50) NULL,
	[EmailAddress] [NVARCHAR](100) NULL,
	[MemberRelationshipID] INT NOT NULL,
	[ImageSourceID] INT NULL,
	--[ContactAddressID] INT NULL,
	[CreatedBy] INT NULL,
	[CreatedOn] DATETIME NULL,
	[LastUpdatedBy] INT NULL,
	[LastUpdatedOn] DATETIME NULL,
	[Timestamp] rowversion NULL,
	PRIMARY KEY (MemberContactID),
	CONSTRAINT FK_MemberContactRelationship FOREIGN KEY (MemberRelationshipID)
    REFERENCES [Member].[MemberRelationship](MemberRelationshipID),
	CONSTRAINT FK_MemberContactImageSource FOREIGN KEY ([ImageSourceID])
    REFERENCES [Common].[ImageSource]([ImageSourceID]),
	--CONSTRAINT FK_MemberContactAddress FOREIGN KEY (ContactAddressID)
 --   REFERENCES Common.ContactAddress(ContactAddressID)
)
GO