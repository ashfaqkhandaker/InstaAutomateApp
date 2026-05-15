-- =============================================================
--  InstaAutomate DB — Complete Setup Script
--  Copy and paste this entire file into SQL Server Management
--  Studio (SSMS) or Azure Data Studio and execute it.
--  It will create the database, all tables, all constraints,
--  all indexes, seed an admin account, and insert sample data.
-- =============================================================


-- =============================================================
--  STEP 1 — CREATE DATABASE
-- =============================================================

IF NOT EXISTS (
    SELECT name FROM sys.databases WHERE name = 'InstaAutomateDB'
)
BEGIN
    CREATE DATABASE InstaAutomateDB;
END
GO

USE InstaAutomateDB;
GO


-- =============================================================
--  STEP 2 — DROP TABLES (safe re-run order, FK children first)
-- =============================================================

IF OBJECT_ID('dbo.ActivityLogs',       'U') IS NOT NULL DROP TABLE dbo.ActivityLogs;
IF OBJECT_ID('dbo.DeliveryTracking',   'U') IS NOT NULL DROP TABLE dbo.DeliveryTracking;
IF OBJECT_ID('dbo.Payments',           'U') IS NOT NULL DROP TABLE dbo.Payments;
IF OBJECT_ID('dbo.Orders',             'U') IS NOT NULL DROP TABLE dbo.Orders;
IF OBJECT_ID('dbo.Cart',               'U') IS NOT NULL DROP TABLE dbo.Cart;
IF OBJECT_ID('dbo.AutomationFlows',    'U') IS NOT NULL DROP TABLE dbo.AutomationFlows;
IF OBJECT_ID('dbo.InstagramAccounts',  'U') IS NOT NULL DROP TABLE dbo.InstagramAccounts;
IF OBJECT_ID('dbo.Coupons',            'U') IS NOT NULL DROP TABLE dbo.Coupons;
IF OBJECT_ID('dbo.Services',           'U') IS NOT NULL DROP TABLE dbo.Services;
IF OBJECT_ID('dbo.Users',              'U') IS NOT NULL DROP TABLE dbo.Users;
GO


-- =============================================================
--  STEP 3 — CREATE TABLES
-- =============================================================


-- -------------------------------------------------------------
--  Users
--  Stores all accounts: Admin, Provider, Customer.
--  Role controls which dashboard the user sees after login.
-- -------------------------------------------------------------
CREATE TABLE Users (
    Id          INT             IDENTITY(1,1)   NOT NULL,
    FullName    NVARCHAR(100)                   NOT NULL,
    Email       NVARCHAR(150)                   NOT NULL,
    Password    NVARCHAR(255)                   NOT NULL,   -- plain text (hash before going to production)
    Role        NVARCHAR(20)                    NOT NULL,   -- 'Admin' | 'Provider' | 'Customer'

    CONSTRAINT PK_Users         PRIMARY KEY (Id),
    CONSTRAINT UQ_Users_Email   UNIQUE      (Email),
    CONSTRAINT CK_Users_Role    CHECK       (Role IN ('Admin', 'Provider', 'Customer'))
);
GO


-- -------------------------------------------------------------
--  Services
--  Both admin and providers can create services.
--  ProviderId = NULL  →  platform/admin service, available
--                         for direct purchase by customers.
--  ProviderId = <id>  →  provider resale service.
-- -------------------------------------------------------------
CREATE TABLE Services (
    Id            INT             IDENTITY(1,1)   NOT NULL,
    ServiceName   NVARCHAR(150)                   NOT NULL,
    Description   NVARCHAR(500)                   NULL,
    Price         DECIMAL(10, 2)                  NOT NULL,
    ProviderId    INT                             NULL,       -- NULL = platform service

    CONSTRAINT PK_Services          PRIMARY KEY (Id),
    CONSTRAINT FK_Services_Provider FOREIGN KEY (ProviderId) REFERENCES Users(Id)
                                    ON DELETE SET NULL
                                    ON UPDATE CASCADE
);
GO


-- -------------------------------------------------------------
--  Cart
--  Holds items a customer has selected but not yet purchased.
--  If the same service is added again, Quantity is incremented
--  (handled in application code).
-- -------------------------------------------------------------
CREATE TABLE Cart (
    Id          INT     IDENTITY(1,1)   NOT NULL,
    UserId      INT                     NOT NULL,
    ServiceId   INT                     NOT NULL,
    Quantity    INT                     NOT NULL    DEFAULT 1,

    CONSTRAINT PK_Cart          PRIMARY KEY (Id),
    CONSTRAINT FK_Cart_User     FOREIGN KEY (UserId)    REFERENCES Users(Id)    ON DELETE CASCADE,
    CONSTRAINT FK_Cart_Service  FOREIGN KEY (ServiceId) REFERENCES Services(Id) ON DELETE CASCADE,
    CONSTRAINT CK_Cart_Qty      CHECK (Quantity > 0)
);
GO


-- -------------------------------------------------------------
--  Orders
--  One row per service per checkout transaction.
--  ProviderId is nullable — platform services have no provider.
--  Status lifecycle: Pending → Completed | Cancelled
-- -------------------------------------------------------------
CREATE TABLE Orders (
    Id            INT             IDENTITY(1,1)   NOT NULL,
    CustomerId    INT                             NOT NULL,
    ServiceId     INT                             NOT NULL,
    ProviderId    INT                             NULL,       -- NULL for platform/admin services
    Quantity      INT                             NOT NULL,
    TotalAmount   DECIMAL(10, 2)                  NOT NULL,
    Status        NVARCHAR(50)                    NOT NULL    DEFAULT 'Pending',
    CreatedAt     DATETIME                        NOT NULL    DEFAULT GETDATE(),

    CONSTRAINT PK_Orders            PRIMARY KEY (Id),
    CONSTRAINT FK_Orders_Customer   FOREIGN KEY (CustomerId) REFERENCES Users(Id),
    CONSTRAINT FK_Orders_Service    FOREIGN KEY (ServiceId)  REFERENCES Services(Id),
    CONSTRAINT FK_Orders_Provider   FOREIGN KEY (ProviderId) REFERENCES Users(Id),
    CONSTRAINT CK_Orders_Status     CHECK (Status IN ('Pending', 'Completed', 'Cancelled')),
    CONSTRAINT CK_Orders_Qty        CHECK (Quantity > 0),
    CONSTRAINT CK_Orders_Amount     CHECK (TotalAmount >= 0)
);
GO


-- -------------------------------------------------------------
--  Payments
--  One payment record is created per order at checkout.
--  PaymentStatus is always 'Completed' on initial insert
--  (payment is collected before the order is confirmed).
-- -------------------------------------------------------------
CREATE TABLE Payments (
    Id              INT             IDENTITY(1,1)   NOT NULL,
    OrderId         INT                             NOT NULL,
    UserId          INT                             NOT NULL,   -- the customer who paid
    Amount          DECIMAL(10, 2)                  NOT NULL,
    PaymentMethod   NVARCHAR(50)                    NOT NULL,   -- 'Credit Card' | 'bKash' | 'PayPal' | etc.
    PaymentStatus   NVARCHAR(50)                    NOT NULL    DEFAULT 'Completed',
    PaymentDate     DATETIME                        NOT NULL    DEFAULT GETDATE(),

    CONSTRAINT PK_Payments          PRIMARY KEY (Id),
    CONSTRAINT FK_Payments_Order    FOREIGN KEY (OrderId) REFERENCES Orders(Id),
    CONSTRAINT FK_Payments_User     FOREIGN KEY (UserId)  REFERENCES Users(Id),
    CONSTRAINT CK_Payments_Amount   CHECK (Amount >= 0)
);
GO


-- -------------------------------------------------------------
--  DeliveryTracking
--  One row per order, updated by the provider.
--  When DeliveryStatus is set to 'Delivered', the application
--  also sets Orders.Status = 'Completed'.
-- -------------------------------------------------------------
CREATE TABLE DeliveryTracking (
    Id              INT             IDENTITY(1,1)   NOT NULL,
    OrderId         INT                             NOT NULL,
    DeliveryStatus  NVARCHAR(50)                    NOT NULL    DEFAULT 'Processing',
    UpdatedAt       DATETIME                        NOT NULL    DEFAULT GETDATE(),

    CONSTRAINT PK_DeliveryTracking          PRIMARY KEY (Id),
    CONSTRAINT FK_DeliveryTracking_Order    FOREIGN KEY (OrderId) REFERENCES Orders(Id),
    CONSTRAINT CK_DeliveryTracking_Status   CHECK (DeliveryStatus IN (
                                                'Processing', 'Shipped', 'Delivered', 'Cancelled'
                                            ))
);
GO


-- -------------------------------------------------------------
--  Coupons
--  Admin creates discount codes.
--  Discount is a percentage value (e.g. 10.00 = 10% off).
--  IsActive = 0 disables the coupon without deleting it.
-- -------------------------------------------------------------
CREATE TABLE Coupons (
    Id          INT             IDENTITY(1,1)   NOT NULL,
    Code        NVARCHAR(50)                    NOT NULL,
    Discount    DECIMAL(5, 2)                   NOT NULL,   -- percentage, e.g. 10.00
    ExpiryDate  DATETIME                        NOT NULL,
    IsActive    BIT                             NOT NULL    DEFAULT 1,

    CONSTRAINT PK_Coupons           PRIMARY KEY (Id),
    CONSTRAINT UQ_Coupons_Code      UNIQUE      (Code),
    CONSTRAINT CK_Coupons_Discount  CHECK (Discount > 0 AND Discount <= 100)
);
GO


-- -------------------------------------------------------------
--  InstagramAccounts
--  Each customer registers their own Instagram accounts.
--  UserId scopes accounts to the individual customer.
-- -------------------------------------------------------------
CREATE TABLE InstagramAccounts (
    Id          INT             IDENTITY(1,1)   NOT NULL,
    UserId      INT                             NOT NULL,   -- the customer who owns this account
    Username    NVARCHAR(100)                   NOT NULL,
    AccessToken NVARCHAR(500)                   NULL,
    AddedAt     DATETIME                        NOT NULL    DEFAULT GETDATE(),

    CONSTRAINT PK_InstagramAccounts         PRIMARY KEY (Id),
    CONSTRAINT FK_InstagramAccounts_User    FOREIGN KEY (UserId) REFERENCES Users(Id)
                                            ON DELETE CASCADE
);
GO


-- -------------------------------------------------------------
--  AutomationFlows
--  Each customer creates their own keyword-triggered flows.
--  UserId scopes flows so customers only see/edit their own.
--
--  Keywords format: comma-separated string
--  e.g. "buy,interested,price,how much"
--
--  Engine logic (AutomationEngine.cs):
--    If PostUrl matches AND comment contains any keyword:
--      → Send ReplyMessage as comment reply
--      → Send DmMessage as direct message
--      If comment also contains "done":
--        → Send SpecialLink (e.g. payment or download link)
-- -------------------------------------------------------------
CREATE TABLE AutomationFlows (
    Id              INT             IDENTITY(1,1)   NOT NULL,
    UserId          INT                             NOT NULL,   -- the customer who owns this flow
    PostUrl         NVARCHAR(500)                   NOT NULL,
    Keywords        NVARCHAR(500)                   NOT NULL,   -- comma-separated trigger words
    ReplyMessage    NVARCHAR(500)                   NULL,
    DmMessage       NVARCHAR(500)                   NULL,
    SpecialLink     NVARCHAR(500)                   NULL,

    CONSTRAINT PK_AutomationFlows       PRIMARY KEY (Id),
    CONSTRAINT FK_AutomationFlows_User  FOREIGN KEY (UserId) REFERENCES Users(Id)
                                        ON DELETE CASCADE
);
GO


-- -------------------------------------------------------------
--  ActivityLogs
--  Written by AutomationEngine.cs every time an automation
--  action fires. Also used for general system event auditing.
-- -------------------------------------------------------------
CREATE TABLE ActivityLogs (
    Id      INT             IDENTITY(1,1)   NOT NULL,
    Action  NVARCHAR(500)                   NOT NULL,
    Status  NVARCHAR(50)                    NOT NULL    DEFAULT 'Success',
    Time    DATETIME                        NOT NULL    DEFAULT GETDATE(),

    CONSTRAINT PK_ActivityLogs PRIMARY KEY (Id)
);
GO


-- =============================================================
--  STEP 4 — INDEXES
--  These improve query performance for the most common lookups
--  used throughout the application.
-- =============================================================

-- Fast lookup of services by provider (ProviderDashboard)
CREATE INDEX IX_Services_ProviderId
    ON Services (ProviderId);

-- Fast cart lookup by user (CustomerDashboard)
CREATE INDEX IX_Cart_UserId
    ON Cart (UserId);

-- Fast order lookup by customer (CustomerDashboard)
CREATE INDEX IX_Orders_CustomerId
    ON Orders (CustomerId);

-- Fast order lookup by provider (ProviderDashboard)
CREATE INDEX IX_Orders_ProviderId
    ON Orders (ProviderId);

-- Fast order status filtering (AdminOrdersForm, revenue stats)
CREATE INDEX IX_Orders_Status
    ON Orders (Status);

-- Fast payment lookup per order (AdminPaymentsForm)
CREATE INDEX IX_Payments_OrderId
    ON Payments (OrderId);

-- Fast delivery lookup per order (AdminDeliveryForm, ProviderDashboard)
CREATE INDEX IX_DeliveryTracking_OrderId
    ON DeliveryTracking (OrderId);

-- Fast coupon lookup by code (CustomerDashboard, CheckoutForm)
CREATE INDEX IX_Coupons_Code
    ON Coupons (Code);

-- Fast flow lookup by user (CustomerDashboard)
CREATE INDEX IX_AutomationFlows_UserId
    ON AutomationFlows (UserId);

-- Fast account lookup by user (CustomerDashboard)
CREATE INDEX IX_InstagramAccounts_UserId
    ON InstagramAccounts (UserId);

-- Fast log retrieval newest-first (ActivityLogs grid)
CREATE INDEX IX_ActivityLogs_Time
    ON ActivityLogs (Time DESC);
GO


-- =============================================================
--  STEP 5 — SEED: ADMIN ACCOUNT
--  The app has no admin registration screen.
--  This inserts the default admin directly.
--  IMPORTANT: Change the password before going to production.
-- =============================================================

INSERT INTO Users (FullName, Email, Password, Role)
VALUES ('Administrator', 'admin@instautomate.com', 'admin123', 'Admin');
GO


-- =============================================================
--  STEP 6 — SEED: SAMPLE DATA
--  Realistic sample records so the app is usable immediately
--  after setup without needing to create everything manually.
-- =============================================================


-- ── Sample Provider account
INSERT INTO Users (FullName, Email, Password, Role)
VALUES ('Sarah Johnson', 'sarah@provider.com', 'provider123', 'Provider');
GO

-- ── Sample Customer accounts
INSERT INTO Users (FullName, Email, Password, Role)
VALUES
    ('Ahmed Rahman',   'ahmed@customer.com',   'customer123', 'Customer'),
    ('Maria Santos',   'maria@customer.com',   'customer123', 'Customer');
GO

-- ── Platform services (ProviderId = NULL — created by admin, direct purchase)
INSERT INTO Services (ServiceName, Description, Price, ProviderId)
VALUES
    ('Instagram Followers Pack — 1K',
     '1,000 real, active followers delivered within 72 hours. Safe and gradual delivery.',
     29.99, NULL),

    ('Instagram Followers Pack — 5K',
     '5,000 real, active followers delivered within 7 days. High retention guaranteed.',
     99.99, NULL),

    ('Instagram Likes Boost — 500',
     '500 likes on any post of your choice. Delivered within 24 hours.',
     9.99, NULL),

    ('Instagram Story Views — 1K',
     '1,000 story views per story. Applied to your next 3 stories automatically.',
     14.99, NULL),

    ('Instagram Reel Views — 10K',
     '10,000 reel views delivered within 48 hours. Helps with Explore page ranking.',
     19.99, NULL);
GO

-- ── Provider resale services (ProviderId = Sarah Johnson, Id = 2)
INSERT INTO Services (ServiceName, Description, Price, ProviderId)
VALUES
    ('Story Views Boost — 500',
     '500 targeted story views from real accounts in your niche. Delivered within 12 hours.',
     9.99, 2),

    ('Comment Engagement Pack',
     '25 genuine comments on a post of your choice. Real accounts, varied messages.',
     24.99, 2),

    ('Instagram DM Outreach — 50 Accounts',
     'Manual DM outreach to 50 targeted accounts in your niche. Custom message included.',
     49.99, 2);
GO

-- ── Sample coupons
INSERT INTO Coupons (Code, Discount, ExpiryDate, IsActive)
VALUES
    ('WELCOME10',   10.00, '2027-12-31', 1),   -- 10% off for new users
    ('SUMMER20',    20.00, '2026-08-31', 1),   -- 20% summer promo
    ('PROVIDER15',  15.00, '2027-06-30', 1),   -- 15% for provider upsells
    ('EXPIRED5',     5.00, '2024-01-01', 0);   -- expired/inactive (for testing)
GO

-- ── Sample Instagram accounts (linked to Ahmed, UserId = 3)
INSERT INTO InstagramAccounts (UserId, Username, AccessToken)
VALUES
    (3, 'ahmed_official',   'tok_abc123demo'),
    (3, 'ahmed_business',   NULL);
GO

-- ── Sample automation flows (linked to Ahmed, UserId = 3)
INSERT INTO AutomationFlows (UserId, PostUrl, Keywords, ReplyMessage, DmMessage, SpecialLink)
VALUES
    (3,
     'https://www.instagram.com/p/ABC123/',
     'buy,price,how much,interested,cost',
     'Thanks for your interest! Check your DMs for details 📩',
     'Hi! Thanks for commenting. Here is our pricing link: https://instautomate.com/pricing',
     'https://instautomate.com/buy'),

    (3,
     'https://www.instagram.com/p/XYZ789/',
     'link,more info,details,send me,dm me',
     'DM sent! Check your inbox 📬',
     'Hey! Here is the info you requested. Feel free to reply if you have questions.',
     'https://instautomate.com/info');
GO

-- ── Sample cart items (Ahmed has 2 items in cart)
INSERT INTO Cart (UserId, ServiceId, Quantity)
VALUES
    (3, 1, 1),   -- Ahmed: 1x Followers Pack 1K
    (3, 3, 2);   -- Ahmed: 2x Likes Boost 500
GO

-- ── Sample completed orders (Ahmed has previous orders)
DECLARE @OrderId1 INT;
DECLARE @OrderId2 INT;

INSERT INTO Orders (CustomerId, ServiceId, ProviderId, Quantity, TotalAmount, Status, CreatedAt)
VALUES (3, 4, NULL, 1, 14.99, 'Completed', DATEADD(DAY, -10, GETDATE()));
SET @OrderId1 = SCOPE_IDENTITY();

INSERT INTO Orders (CustomerId, ServiceId, ProviderId, Quantity, TotalAmount, Status, CreatedAt)
VALUES (3, 6, 2, 1, 9.99, 'Completed', DATEADD(DAY, -5, GETDATE()));
SET @OrderId2 = SCOPE_IDENTITY();

-- ── Sample pending order (Maria, UserId = 4)
DECLARE @OrderId3 INT;
INSERT INTO Orders (CustomerId, ServiceId, ProviderId, Quantity, TotalAmount, Status, CreatedAt)
VALUES (4, 2, NULL, 1, 99.99, 'Pending', DATEADD(DAY, -1, GETDATE()));
SET @OrderId3 = SCOPE_IDENTITY();
GO

-- ── Sample payments (one per order above)
-- Re-fetch the order IDs since GO resets DECLARE scope
INSERT INTO Payments (OrderId, UserId, Amount, PaymentMethod, PaymentStatus, PaymentDate)
SELECT o.Id, o.CustomerId, o.TotalAmount, 'bKash', 'Completed', o.CreatedAt
FROM Orders o
WHERE o.Status IN ('Completed', 'Pending')
  AND o.CreatedAt >= DATEADD(DAY, -11, GETDATE());
GO

-- ── Sample delivery tracking records
INSERT INTO DeliveryTracking (OrderId, DeliveryStatus, UpdatedAt)
SELECT o.Id,
       CASE WHEN o.Status = 'Completed' THEN 'Delivered'
            ELSE 'Processing' END,
       GETDATE()
FROM Orders o
WHERE o.CreatedAt >= DATEADD(DAY, -11, GETDATE());
GO

-- ── Sample activity logs (simulated automation events)
INSERT INTO ActivityLogs (Action, Status, Time)
VALUES
    ('Comment Reply Sent: Thanks for your interest! Check your DMs for details 📩',
     'Success', DATEADD(MINUTE, -120, GETDATE())),

    ('DM Sent: Hi! Thanks for commenting. Here is our pricing link.',
     'Success', DATEADD(MINUTE, -119, GETDATE())),

    ('User Follow Verified',
     'Success', DATEADD(MINUTE, -118, GETDATE())),

    ('Link Sent: https://instautomate.com/buy',
     'Success', DATEADD(MINUTE, -118, GETDATE())),

    ('Comment Reply Sent: DM sent! Check your inbox 📬',
     'Success', DATEADD(MINUTE, -45, GETDATE())),

    ('DM Sent: Hey! Here is the info you requested.',
     'Success', DATEADD(MINUTE, -44, GETDATE()));
GO


-- =============================================================
--  STEP 7 — VERIFY SETUP
--  Run these SELECT statements to confirm everything was
--  created and seeded correctly.
-- =============================================================

SELECT 'Users'              AS TableName, COUNT(*) AS RowCount FROM Users
UNION ALL
SELECT 'Services',                        COUNT(*)             FROM Services
UNION ALL
SELECT 'Cart',                            COUNT(*)             FROM Cart
UNION ALL
SELECT 'Orders',                          COUNT(*)             FROM Orders
UNION ALL
SELECT 'Payments',                        COUNT(*)             FROM Payments
UNION ALL
SELECT 'DeliveryTracking',                COUNT(*)             FROM DeliveryTracking
UNION ALL
SELECT 'Coupons',                         COUNT(*)             FROM Coupons
UNION ALL
SELECT 'InstagramAccounts',               COUNT(*)             FROM InstagramAccounts
UNION ALL
SELECT 'AutomationFlows',                 COUNT(*)             FROM AutomationFlows
UNION ALL
SELECT 'ActivityLogs',                    COUNT(*)             FROM ActivityLogs;
GO

PRINT '============================================';
PRINT ' InstaAutomateDB setup complete.';
PRINT ' Default admin login:';
PRINT '   Email   : admin@instautomate.com';
PRINT '   Password: admin123';
PRINT ' Change the admin password before deploying.';
PRINT '============================================';
GO
