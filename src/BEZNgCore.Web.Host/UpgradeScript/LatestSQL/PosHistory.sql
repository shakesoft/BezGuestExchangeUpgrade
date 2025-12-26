CREATE TABLE PosHistory (
    PosHistoryKey UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),   -- Unique key for each POS record
    PosDate DATETIME NULL,                                     -- Date of POS transaction
    PosTime DATETIME NULL,                                     -- Time of POS transaction
    Room VARCHAR(12) NULL,                                     -- Room or table name
    Charge DECIMAL(18,4) NULL,                                 -- Charge amount
    DocketNo VARCHAR(50) NULL,                                 -- Docket or receipt number
    Outlet VARCHAR(100) NULL,                                  -- Outlet name or ID
    Revenue VARCHAR(100) NULL,                                 -- Revenue category
    Period VARCHAR(50) NULL,                                   -- Time period (e.g., Breakfast, Dinner)
    Covers VARCHAR(50) NULL,                                   -- Number of covers/guests
    Amount DECIMAL(18,4) NULL,                                 -- Total amount
    Payment VARCHAR(50) NULL,                                  -- Payment type (e.g., Cash, Card)
    Seq INT IDENTITY(1,1) NOT NULL,  
    CONSTRAINT PK_PosHistory PRIMARY KEY (PosHistoryKey)        -- Primary key
);

GO


INSERT INTO [dbo].[GeneralProfile]
           ([SetupKey]
           ,[ProfileName]
           ,[ProfileValue]          
           ,[Description]
           ,[DataType]
           ,[TenantId])
     VALUES
           ('65A8328A-3E78-43EB-AA23-06453E0044CF'
           ,'Default POS Paymenttype'
           ,'767002CA-21CB-4CB6-8FC2-7009D6E65224'
           ,'Defatult Paymenttype'
           ,'GUID'
           ,1)
