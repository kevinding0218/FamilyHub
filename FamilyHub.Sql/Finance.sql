CREATE SCHEMA Payment
GO

-- VISA/MASTER/DISCOVERY/CHECKING ACCOUNT/SAVING ACCOUNT...
CREATE TABLE Payment.PaymentMethodType
(
	[PaymentMethodTypeID] [INT] IDENTITY(1,1) NOT NULL,
	[PaymentMethodTypeName] NVARCHAR(30) NOT NULL,
	[PaymentMethodTypeDescription] NVARCHAR(250) NULL,
	[Timestamp] rowversion NULL,
	PRIMARY KEY (PaymentMethodTypeID),
);
GO
INSERT INTO Payment.PaymentMethodType
(
    PaymentMethodTypeName,
    PaymentMethodTypeDescription
)
VALUES
(   N'CASH', -- PaymentMethodTypeName - nvarchar(50)
    N'All CASH Type Such As CASH/MONEY ORDER'  -- PaymentMethodTypeDescription - nvarchar(250)
);
INSERT INTO Payment.PaymentMethodType
(
    PaymentMethodTypeName,
    PaymentMethodTypeDescription
)
VALUES
(   N'CREDIT CARD', -- PaymentMethodTypeName - nvarchar(50)
    N'All Credit Card Type Such As VISA/MASTER/AMERICAN EXPRESS/DISCOVERY'  -- PaymentMethodTypeDescription - nvarchar(250)
);
INSERT INTO Payment.PaymentMethodType
(
    PaymentMethodTypeName,
    PaymentMethodTypeDescription
)
VALUES
(   N'DEBIT CARD', -- PaymentMethodTypeName - nvarchar(50)
    N'All Debit Card Type Such As VISA/MASTER'  -- PaymentMethodTypeDescription - nvarchar(250)
);
INSERT INTO Payment.PaymentMethodType
(
    PaymentMethodTypeName,
    PaymentMethodTypeDescription
)
VALUES
(   N'BANK CHECKING ACCOUNT', -- PaymentMethodTypeName - nvarchar(50)
    N'All Bank Checking Account Type'  -- PaymentMethodTypeDescription - nvarchar(250)
);
INSERT INTO Payment.PaymentMethodType
(
    PaymentMethodTypeName,
    PaymentMethodTypeDescription
)
VALUES
(   N'BANK SAVING ACCOUNT', -- PaymentMethodTypeName - nvarchar(50)
    N'All Bank Saving Account Type'  -- PaymentMethodTypeDescription - nvarchar(250)
);
INSERT INTO Payment.PaymentMethodType
(
    PaymentMethodTypeName,
    PaymentMethodTypeDescription
)
VALUES
(   N'MOBILE PAYMENT', -- PaymentMethodTypeName - nvarchar(50)
    N'All Mobile Payment Type Such As ALIPAY/WEIXIN/ANDROID/APPLE PAY'  -- PaymentMethodTypeDescription - nvarchar(250)
);
GO
--SELECT * FROM Payment.PaymentMethodType

CREATE TABLE Payment.PaymentMethod
(
	[PaymentMethodID] [INT] IDENTITY(1,1) NOT NULL,
	[PaymentMethodName] NVARCHAR(50) NOT NULL,
	[PaymentMethodDescription] NVARCHAR(250) NULL,
	[Active] BIT NOT NULL,
	[PaymentMethodTypeID] INT NOT NULL,
	CreatedBy INT NULL,
	CreatedOn DATETIME NULL,
	LastUpdatedBy INT NULL,
	LastUpdatedOn DATETIME NULL,
	[Timestamp] rowversion NULL,
	PRIMARY KEY (PaymentMethodID),
	CONSTRAINT FK_PaymentMethodType FOREIGN KEY (PaymentMethodTypeID)
    REFERENCES Payment.PaymentMethodType(PaymentMethodTypeID)
);
GO

--Spouse/Children/Parent/Relative/Friends/Colleague
CREATE TABLE Payment.PaymentPayorRelationship
(
	[PaymentPayorRelationshipID] [INT] IDENTITY(1,1) NOT NULL,
	[PaymentPayorRelationshipName] NVARCHAR(50) NOT NULL,
	[PaymentPayorRelationshipDescription] NVARCHAR(250) NULL,
	[Timestamp] rowversion NULL,
	PRIMARY KEY (PaymentPayorRelationshipID),
);
GO

INSERT INTO Payment.PaymentPayorRelationship
(
    PaymentPayorRelationshipName,
    PaymentPayorRelationshipDescription
)
VALUES
(   N'Owner', -- PaymentPayorRelationshipName - nvarchar(50)
    N'Family Member'  -- PaymentPayorRelationshipDescription - nvarchar(250)
);
INSERT INTO Payment.PaymentPayorRelationship
(
    PaymentPayorRelationshipName,
    PaymentPayorRelationshipDescription
)
VALUES
(   N'Parent', -- PaymentPayorRelationshipName - nvarchar(50)
    N'Family Member'  -- PaymentPayorRelationshipDescription - nvarchar(250)
);
INSERT INTO Payment.PaymentPayorRelationship
(
    PaymentPayorRelationshipName,
    PaymentPayorRelationshipDescription
)
VALUES
(   N'Spouse', -- PaymentPayorRelationshipName - nvarchar(50)
    N'Family Member'  -- PaymentPayorRelationshipDescription - nvarchar(250)
);
INSERT INTO Payment.PaymentPayorRelationship
(
    PaymentPayorRelationshipName,
    PaymentPayorRelationshipDescription
)
VALUES
(   N'Children', -- PaymentPayorRelationshipName - nvarchar(50)
    N'Family Member'  -- PaymentPayorRelationshipDescription - nvarchar(250)
);
INSERT INTO Payment.PaymentPayorRelationship
(
    PaymentPayorRelationshipName,
    PaymentPayorRelationshipDescription
)
VALUES
(   N'Relative', -- PaymentPayorRelationshipName - nvarchar(50)
    N'Family Member'  -- PaymentPayorRelationshipDescription - nvarchar(250)
);
INSERT INTO Payment.PaymentPayorRelationship
(
    PaymentPayorRelationshipName,
    PaymentPayorRelationshipDescription
)
VALUES
(   N'Friends', -- PaymentPayorRelationshipName - nvarchar(50)
    N'Family Member'  -- PaymentPayorRelationshipDescription - nvarchar(250)
);
INSERT INTO Payment.PaymentPayorRelationship
(
    PaymentPayorRelationshipName,
    PaymentPayorRelationshipDescription
)
VALUES
(   N'Colleague', -- PaymentPayorRelationshipName - nvarchar(50)
    N'Family Member'  -- PaymentPayorRelationshipDescription - nvarchar(250)
);
GO

--SELECT * FROM Payment.PaymentPayorRelationship

CREATE TABLE Payment.PaymentPayor
(
	[PaymentPayorID] [INT] IDENTITY(1,1) NOT NULL,
	[PaymentPayorName] NVARCHAR(50) NOT NULL,
	[PaymentPayorDescription] NVARCHAR(250) NULL,
	[Active] BIT NOT NULL,
	[PaymentSplit] BIT NOT NULL,
	[PaymentSplitFactor] FLOAT NULL,
	[PaymentPayorRelationshipID] INT NOT NULL,
	CreatedBy INT NULL,
	CreatedOn DATETIME NULL,
	LastUpdatedBy INT NULL,
	LastUpdatedOn DATETIME NULL,
	[Timestamp] rowversion NULL,
	PRIMARY KEY (PaymentPayorID),
	CONSTRAINT PaymentPayorPaymentPayorRelationship FOREIGN KEY (PaymentPayorRelationshipID)
    REFERENCES Payment.PaymentPayorRelationship(PaymentPayorRelationshipID)
);
GO

CREATE SCHEMA [Transactions]
GO

--Purchase/Refund
CREATE TABLE [Transactions].[TransactionType]
(
	[TransactionTypeID] [INT] IDENTITY(1,1) NOT NULL, 
	[TransactionTypeName] NVARCHAR(50) NOT NULL,
	[TransactionTypeDescription] NVARCHAR(250) NULL,
	[Timestamp] rowversion NULL,
	PRIMARY KEY (TransactionTypeID),
);
GO
INSERT INTO Transactions.TransactionType
(
    TransactionTypeName,
    TransactionTypeDescription
)
VALUES
(   N'PURCHASE', -- TransactionTypeName - nvarchar(50)
    N''  -- TransactionTypeDescription - nvarchar(250)
);
INSERT INTO Transactions.TransactionType
(
    TransactionTypeName,
    TransactionTypeDescription
)
VALUES
(   N'REFUND', -- TransactionTypeName - nvarchar(50)
    N''  -- TransactionTypeDescription - nvarchar(250)
);
--SELECT * FROM Transactions.TransactionType

--Mortgage/Insurance/Food/Market/Gas
CREATE TABLE [Transactions].[TransactionCategory]
(
	[TransactionCategoryID] [INT] IDENTITY(1,1) NOT NULL, 
	[TransactionCategoryName] NVARCHAR(50) NOT NULL,
	[TransactionCategoryDescription] NVARCHAR(250) NULL,
	[IsFixed] BIT NOT NULL,
	[IsRecurring] BIT NULL,
	[Timestamp] rowversion NULL,
	PRIMARY KEY (TransactionCategoryID),
);
GO

CREATE TABLE [Transactions].TransactionDetail
(
	[TransactionDetailID] [INT] IDENTITY(1,1) NOT NULL,
	[PostedDate] DATETIME NULL, 
	[TransactionTypeID] [INT] NOT NULL,
	[PaymentPayorID] [INT] NOT NULL,
	[TransactionCategoryID] [INT] NOT NULL,
	[PaymentMethodID] [INT] NOT NULL,
	[Timestamp] rowversion NULL,
	PRIMARY KEY (TransactionDetailID),

	CONSTRAINT TransactionDetailDetailType FOREIGN KEY (TransactionTypeID)
    REFERENCES [Transactions].TransactionType(TransactionTypeID),
	CONSTRAINT TransactionDetailDetailPayor FOREIGN KEY (PaymentPayorID)
    REFERENCES Payment.PaymentPayor(PaymentPayorID),
	CONSTRAINT TransactionDetailDetailCategory FOREIGN KEY (TransactionCategoryID)
    REFERENCES [Transactions].TransactionCategory(TransactionCategoryID),
	CONSTRAINT TransactionDetailDetailPaymentMethod FOREIGN KEY (PaymentMethodID)
    REFERENCES Payment.PaymentMethod(PaymentMethodID)
 );
GO

CREATE TABLE [Transactions].[Transaction]
(
	[TransactionID] [INT] IDENTITY(1,1) NOT NULL, 
	[TransactionDate] DATETIME NOT NULL,
	[TransactionDescription] NVARCHAR(250) NOT NULL,
	[Amount] FLOAT NOT NULL,
	[Active] BIT NOT NULL,
	[TransactionDetailID] [INT] NOT NULL,
	CreatedBy INT NULL,
	CreatedOn DATETIME NULL,
	LastUpdatedBy INT NULL,
	LastUpdatedOn DATETIME NULL,
	[Timestamp] rowversion NULL,
	PRIMARY KEY (TransactionID),
	CONSTRAINT TransactionTransactionDetail FOREIGN KEY (TransactionDetailID)
    REFERENCES [Transactions].TransactionDetail(TransactionDetailID),
);
GO