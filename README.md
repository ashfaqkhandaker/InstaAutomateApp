# InstaAutomate

> A Windows desktop application for Instagram automation management, service marketplace, and order tracking — built with C# WinForms and SQL Server.

---

## Table of Contents

- [Overview](#overview)
- [Screenshots](#screenshots)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Database Setup](#database-setup)
- [Configuration](#configuration)
- [Running the App](#running-the-app)
- [Role System](#role-system)
- [Dashboard Breakdown](#dashboard-breakdown)
- [Automation Engine](#automation-engine)
- [Contributing](#contributing)

---

## Overview

InstaAutomate is a three-role desktop platform that combines an Instagram automation engine with a service marketplace. Admins manage the platform catalog and monitor all activity. Providers list and resell services to customers. Customers browse services, place orders, manage their Instagram accounts, and build automation flows — all from a single dashboard.

---

## Screenshots

### Login & Signup

> : LoginForm_<img width="795" height="489" alt="image" src="https://github.com/user-attachments/assets/65560d60-f80a-471a-ba80-2b246c2295ef" />


> : SignupForm_<img width="796" height="495" alt="image" src="https://github.com/user-attachments/assets/dddf08ef-7ed1-4f94-99dd-f949f9060cea" />


---

### Admin Dashboard

> _<img width="984" height="745" alt="image" src="https://github.com/user-attachments/assets/88a6ec40-e9b6-40a3-875d-2354c959862b" />
: Admin Dashboard — Services grid + Users grid + action buttons_

> _<img width="792" height="483" alt="image" src="https://github.com/user-attachments/assets/1c8bbeb5-7a10-43a9-bbc6-4a906f75ae72" />
: Admin — Add Service form_

> _<img width="992" height="547" alt="image" src="https://github.com/user-attachments/assets/9bdefe2c-cd1c-4a4f-a11e-3ecbb7fb110c" />
: Admin — All Orders panel_

> _<img width="880" height="511" alt="image" src="https://github.com/user-attachments/assets/203b4483-5f43-452a-b57c-0208db70ae12" />
: Admin — Payment Overview panel_

> _<img width="788" height="513" alt="image" src="https://github.com/user-attachments/assets/a7aa1d0d-43a6-470e-8a59-a797f96e7b5d" />
: Admin — Coupon Manager_

> _<img width="884" height="517" alt="image" src="https://github.com/user-attachments/assets/71186419-6fb9-453a-9e2a-766601f75c7f" />
: Admin — Delivery Monitor_

> _<img width="422" height="330" alt="image" src="https://github.com/user-attachments/assets/f937ceda-2c03-4c77-8864-bb0dcae0f509" />
: Admin — Revenue Stats popup_

---

### Provider Dashboard

> _<img width="779" height="321" alt="image" src="https://github.com/user-attachments/assets/6f4b413b-6d89-4f1f-a9e3-589cff809324" />
— My Services grid_

> _<img width="792" height="256" alt="image" src="https://github.com/user-attachments/assets/c5522c5d-7d2c-48ae-ae46-8d657f9df808" />
: Provider Dashboard — Customer Orders grid_

> _<img width="793" height="488" alt="image" src="https://github.com/user-attachments/assets/406f310c-0717-43d2-924c-95e8e0daaa46" />
 — Add Service form_

> _<img width="368" height="219" alt="image" src="https://github.com/user-attachments/assets/cf15c3e9-fd7d-4f95-b49d-6f1824635b91" />
: Provider — Update Delivery Status dialog_

---

### Customer Dashboard

> _<img width="1008" height="315" alt="image" src="https://github.com/user-attachments/assets/2479c966-28c9-4078-99f9-93aaa56571f8" />
: Customer Dashboard — Available Services grid_

> _<img width="1005" height="202" alt="image" src="https://github.com/user-attachments/assets/c6321b57-80ab-4092-9075-f2adbe54ca81" />
: Customer Dashboard — My Cart_

> _<img width="1021" height="177" alt="image" src="https://github.com/user-attachments/assets/9ad38f5b-39cb-40c5-870a-7f6d03675ddb" />
: Customer Dashboard — My Orders & Tracking_

> _<img width="1011" height="215" alt="image" src="https://github.com/user-attachments/assets/6b993a18-ff2a-4030-bb22-9cda5fb9eae7" />
: Customer Dashboard — My Instagram Accounts_

> _<img width="1024" height="268" alt="image" src="https://github.com/user-attachments/assets/931c9c90-1d45-4748-a699-8cee3ef8b3f7" />
: Customer Dashboard — Automation Flows_

> _<img width="695" height="538" alt="image" src="https://github.com/user-attachments/assets/2c20df02-b34e-4a8a-aa8c-be8bb2d10823" />
: Customer — Checkout form_

---

## Features

### Admin
- Add, view, and delete **platform services** — services with no assigned provider, available for direct customer purchase
- View all registered users across all roles
- Monitor **all orders** platform-wide
- View **payment history** and total revenue collected
- Manage **discount coupons** — create codes, set percentage discount, set expiry date, activate/deactivate
- **Delivery monitor** — track delivery status across all orders regardless of provider
- **Revenue analytics** — total revenue, order count, customer count, provider count, service count

### Provider
- Add, edit, and delete their own **resale services**
- View **incoming orders** from customers for their services
- Update **delivery status** per order (Processing → Shipped → Delivered → Cancelled)
- Auto-marks orders as `Completed` when delivery status is set to `Delivered`
- Earnings tracker — total earnings from completed orders

### Customer
- Browse **all services** — both platform services and provider-listed resale services
- **Add to cart**, adjust quantity, remove items
- Apply **discount coupons** at cart level or at checkout
- **Checkout** with payment method selection — creates order, payment record, and delivery tracking in one transaction
- View **order history** with live delivery status
- Manage personal **Instagram accounts**
- Build and manage **automation flows** scoped to their own account
- **Simulate** a selected flow directly from the dashboard
- Search flows by keyword

---

## Tech Stack

| Layer | Technology |
|---|---|
| Language | C# (.NET 10) |
| UI Framework | Windows Forms (WinForms) |
| Database | Microsoft SQL Server |
| DB Driver | `Microsoft.Data.SqlClient` 7.0.1 |
| Config | `System.Configuration` / `App.config` |
| Target OS | Windows |

---

## Project Structure

```
InstaAutomateApp/
│
├── App.config                          # Connection string config
├── Program.cs                          # App entry point
├── UserSession.cs                      # Static session (UserId, UserName, UserRole)
│
├── Database/
│   └── DbHelper.cs                     # SQL connection factory
│
├── Services/
│   └── AutomationEngine.cs             # Keyword-matching automation logic + activity logging
│
├── Forms/
│   ├── LoginForm.cs / .Designer.cs     # Login + role-based routing
│   ├── SignupForm.cs / .Designer.cs    # Registration (Customer or Provider)
│   │
│   ├── Form1.cs / .Designer.cs         # Admin Dashboard
│   ├── AdminUsersForm.cs               # Admin — User management
│   ├── AdminCouponsForm.cs             # Admin — Coupon management
│   ├── AdminOrdersPaymentsDelivery.cs  # Admin — Orders, Payments, Delivery monitor (3 forms in one file)
│   │
│   ├── ProviderDashboard.cs            # Provider Dashboard
│   ├── AddServiceForm.cs               # Add service (Admin and Provider both use this)
│   ├── EditServiceForm.cs              # Edit existing service (Provider only)
│   │
│   ├── CustomerDashboard.cs            # Customer Dashboard
│   ├── CheckoutForm.cs                 # Checkout + order placement
│   │
│   ├── AddAccountForm.cs               # Add Instagram account
│   └── AddFlowForm.cs                  # Add / edit automation flow
```

---

## Database Setup

Run the following SQL against your SQL Server instance to create the database and all required tables.

### Step 1 — Create Database

```sql
CREATE DATABASE InstaAutomateDB;
GO

USE InstaAutomateDB;
GO
```

---

### Step 2 — Create Tables

#### Users

```sql
CREATE TABLE Users (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    FullName    NVARCHAR(100)   NOT NULL,
    Email       NVARCHAR(150)   NOT NULL UNIQUE,
    Password    NVARCHAR(255)   NOT NULL,
    Role        NVARCHAR(20)    NOT NULL    -- 'Admin', 'Provider', 'Customer'
);
```

---

#### Services

```sql
CREATE TABLE Services (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    ServiceName   NVARCHAR(150)   NOT NULL,
    Description   NVARCHAR(500)   NULL,
    Price         DECIMAL(10,2)   NOT NULL,
    ProviderId    INT             NULL,      -- NULL = platform/admin service (direct purchase)
    CONSTRAINT FK_Services_Provider FOREIGN KEY (ProviderId) REFERENCES Users(Id)
);
```

> `ProviderId` is **nullable**. `NULL` means the service was created by an admin and is available for direct purchase by customers. When set, it is a provider-listed resale service.

---

#### Cart

```sql
CREATE TABLE Cart (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    UserId      INT     NOT NULL,
    ServiceId   INT     NOT NULL,
    Quantity    INT     NOT NULL DEFAULT 1,
    CONSTRAINT FK_Cart_User    FOREIGN KEY (UserId)    REFERENCES Users(Id),
    CONSTRAINT FK_Cart_Service FOREIGN KEY (ServiceId) REFERENCES Services(Id)
);
```

---

#### Orders

```sql
CREATE TABLE Orders (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId    INT             NOT NULL,
    ServiceId     INT             NOT NULL,
    ProviderId    INT             NULL,      -- NULL for platform/admin services
    Quantity      INT             NOT NULL,
    TotalAmount   DECIMAL(10,2)   NOT NULL,
    Status        NVARCHAR(50)    NOT NULL DEFAULT 'Pending',   -- 'Pending', 'Completed', 'Cancelled'
    CreatedAt     DATETIME        NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Orders_Customer FOREIGN KEY (CustomerId) REFERENCES Users(Id),
    CONSTRAINT FK_Orders_Service  FOREIGN KEY (ServiceId)  REFERENCES Services(Id),
    CONSTRAINT FK_Orders_Provider FOREIGN KEY (ProviderId) REFERENCES Users(Id)
);
```

---

#### Payments

```sql
CREATE TABLE Payments (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    OrderId         INT             NOT NULL,
    UserId          INT             NOT NULL,
    Amount          DECIMAL(10,2)   NOT NULL,
    PaymentMethod   NVARCHAR(50)    NOT NULL,   -- e.g. 'Credit Card', 'bKash', 'PayPal'
    PaymentStatus   NVARCHAR(50)    NOT NULL DEFAULT 'Completed',
    PaymentDate     DATETIME        NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Payments_Order FOREIGN KEY (OrderId) REFERENCES Orders(Id),
    CONSTRAINT FK_Payments_User  FOREIGN KEY (UserId)  REFERENCES Users(Id)
);
```

---

#### DeliveryTracking

```sql
CREATE TABLE DeliveryTracking (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    OrderId         INT             NOT NULL,
    DeliveryStatus  NVARCHAR(50)    NOT NULL DEFAULT 'Processing',  -- 'Processing', 'Shipped', 'Delivered', 'Cancelled'
    UpdatedAt       DATETIME        NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Delivery_Order FOREIGN KEY (OrderId) REFERENCES Orders(Id)
);
```

---

#### Coupons

```sql
CREATE TABLE Coupons (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    Code        NVARCHAR(50)    NOT NULL UNIQUE,
    Discount    DECIMAL(5,2)    NOT NULL,   -- Percentage value, e.g. 10.00 = 10%
    ExpiryDate  DATETIME        NOT NULL,
    IsActive    BIT             NOT NULL DEFAULT 1
);
```

---

#### InstagramAccounts

```sql
CREATE TABLE InstagramAccounts (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    UserId      INT             NOT NULL,   -- The customer who owns this account
    Username    NVARCHAR(100)   NOT NULL,
    AccessToken NVARCHAR(500)   NULL,
    AddedAt     DATETIME        NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_IGAccounts_User FOREIGN KEY (UserId) REFERENCES Users(Id)
);
```

---

#### AutomationFlows

```sql
CREATE TABLE AutomationFlows (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    UserId          INT             NOT NULL,   -- The customer who owns this flow
    PostUrl         NVARCHAR(500)   NOT NULL,
    Keywords        NVARCHAR(500)   NOT NULL,   -- Comma-separated, e.g. "buy,interested,price"
    ReplyMessage    NVARCHAR(500)   NULL,
    DmMessage       NVARCHAR(500)   NULL,
    SpecialLink     NVARCHAR(500)   NULL,
    CONSTRAINT FK_Flows_User FOREIGN KEY (UserId) REFERENCES Users(Id)
);
```

---

#### ActivityLogs

```sql
CREATE TABLE ActivityLogs (
    Id      INT IDENTITY(1,1) PRIMARY KEY,
    Action  NVARCHAR(500)   NOT NULL,
    Status  NVARCHAR(50)    NOT NULL DEFAULT 'Success',
    Time    DATETIME        NOT NULL DEFAULT GETDATE()
);
```

---

### Step 3 — Seed an Admin User

The app has no built-in admin creation screen. Insert the admin account directly into the database:

```sql
INSERT INTO Users (FullName, Email, Password, Role)
VALUES ('Administrator', 'admin@instautomate.com', 'admin123', 'Admin');
```

> **Note:** Passwords are stored as plain text in the current version. Hash them before deploying to any production environment.

---

### Step 4 — Sample Data (Optional)

```sql
-- Sample discount coupon
INSERT INTO Coupons (Code, Discount, ExpiryDate, IsActive)
VALUES ('WELCOME10', 10.00, '2027-12-31', 1);

-- Sample platform service (admin-listed, no provider, available for direct purchase)
INSERT INTO Services (ServiceName, Description, Price, ProviderId)
VALUES ('Instagram Followers Pack', '1000 real followers delivered in 3 days', 29.99, NULL);

-- Sample provider service (replace 2 with the actual provider UserId)
INSERT INTO Services (ServiceName, Description, Price, ProviderId)
VALUES ('Story Views Boost', '500 story views within 24 hours', 9.99, 2);
```

## Configuration

Open `App.config` and update the connection string to match your SQL Server setup:

```xml
<configuration>
  <connectionStrings>
    <add name="db"
         connectionString="Server=.;Database=InstaAutomateDB;Trusted_Connection=True;TrustServerCertificate=True;"
         providerName="Microsoft.Data.SqlClient" />
  </connectionStrings>
</configuration>
```

| Parameter | Description |
|---|---|
| `Server=.` | Local SQL Server instance. Change to your server name or IP address if remote. |
| `Database=InstaAutomateDB` | The database name. Must match what you created in Step 1. |
| `Trusted_Connection=True` | Windows Authentication. Replace with `User Id=sa;Password=...` for SQL auth. |
| `TrustServerCertificate=True` | Required for local dev with self-signed certificates. Remove in production. |

---

## Running the App

### Requirements

- Windows 10 or later
- [.NET 10 SDK](https://dotnet.microsoft.com/download) (or .NET 10 Runtime if only running)
- Microsoft SQL Server (any edition — Express works fine)
- Visual Studio 2022 or later (recommended)

### Steps

1. Clone the repository
   ```bash
   git clone https://github.com/ashfaqkhandaker/InstaAutomateApp.git
   ```

2. Open `InstaAutomateApp.slnx` in Visual Studio 2022

3. Create the database and run all SQL from the [Database Setup](#database-setup) section

4. Update the connection string in `App.config`

5. Build and run (`F5`)

6. Log in with the admin credentials you seeded, or register a new Customer or Provider account from the signup screen

---

## Role System

| Role | How to Create | Dashboard Opened |
|---|---|---|
| **Admin** | Insert directly into `Users` with `Role = 'Admin'` | `Form1` (Admin Dashboard) |
| **Provider** | Self-register via Signup, select "Provider" | `ProviderDashboard` |
| **Customer** | Self-register via Signup, select "Customer" | `CustomerDashboard` |

Role routing happens in `LoginForm.cs` immediately after a successful login. The static `UserSession` class holds `UserId`, `UserName`, and `UserRole` for the lifetime of the session.

```csharp
// LoginForm.cs — role-based dashboard routing
if (UserSession.UserRole == "Admin")
    new Form1().Show();
else if (UserSession.UserRole == "Provider")
    new ProviderDashboard().Show();
else if (UserSession.UserRole == "Customer")
    new CustomerDashboard().Show();
```

---

## Dashboard Breakdown

### Admin Dashboard (`Form1`)

| UI Element | What it does |
|---|---|
| Stats bar | Live counts — Services, Orders, Customers, Revenue |
| Platform Services grid | All services. Admin can add a new service (ProviderId = NULL) or delete any |
| All Users grid | Quick read-only view of all registered users grouped by role |
| All Orders button | Opens `AdminOrdersForm` — full order history with customer, provider, and service info |
| Payment Overview button | Opens `AdminPaymentsForm` — all payment records + running total received |
| Manage Coupons button | Opens `AdminCouponsForm` — create and manage discount codes |
| Delivery Monitor button | Opens `AdminDeliveryForm` — all delivery tracking records across all providers |
| Revenue Stats button | Popup showing revenue, order count, customer count, provider count, service count |
| Manage Users button | Opens `AdminUsersForm` — edit roles or remove users |

---

### Provider Dashboard (`ProviderDashboard`)

| UI Element | What it does |
|---|---|
| Stats bar | Services count, total orders received, pending orders, total earnings |
| My Services grid | Only this provider's own service listings |
| Add Service | Opens `AddServiceForm` — inserts new service with `ProviderId = UserSession.UserId` |
| Edit Service | Opens `EditServiceForm` — updates name, description, price (provider-scoped) |
| Delete Service | Deletes the selected service from the provider's own listings |
| Customer Orders grid | All orders placed for this provider's services, with customer name and delivery status |
| Update Delivery Status | Dialog to set order delivery status. Setting `Delivered` automatically sets `Orders.Status = 'Completed'` |

---

### Customer Dashboard (`CustomerDashboard`)

| UI Element | What it does |
|---|---|
| Stats bar | Cart item count, total orders, total amount spent |
| Available Services grid | All services — platform and provider — with provider name shown (`Platform` if admin-listed) |
| Add to Cart | Adds selected service; increments quantity if already in cart |
| My Cart grid | Current cart items with per-item subtotal |
| Remove from Cart | Deletes selected cart row |
| Apply Coupon | Validates coupon code, checks expiry and active status, applies percentage discount |
| Checkout | Opens `CheckoutForm` — review cart, optionally apply another coupon, select payment method, place order |
| My Orders & Tracking | Order history joined with `DeliveryTracking` for live delivery status |
| My Instagram Accounts | Personal IG accounts, add new via `AddAccountForm` |
| Automation Flows | Full CRUD on personal flows, keyword search, simulate selected flow |

---

### Checkout Flow (`CheckoutForm`)

When a customer confirms an order, the following runs per cart item inside a single open connection:

```sql
-- 1. Create the order
INSERT INTO Orders (CustomerId, ServiceId, ProviderId, Quantity, TotalAmount, Status, CreatedAt)
VALUES (@customerId, @serviceId, @providerId, @qty, @itemTotal, 'Pending', GETDATE());
-- ProviderId is NULL for platform/admin services

-- 2. Record the payment
INSERT INTO Payments (OrderId, UserId, Amount, PaymentMethod, PaymentStatus, PaymentDate)
VALUES (@orderId, @customerId, @itemTotal, @paymentMethod, 'Completed', GETDATE());

-- 3. Create delivery tracking record
INSERT INTO DeliveryTracking (OrderId, DeliveryStatus, UpdatedAt)
VALUES (@orderId, 'Processing', GETDATE());

-- 4. Clear the customer's cart after all items are processed
DELETE FROM Cart WHERE UserId = @customerId;
```

---

## Automation Engine

`AutomationEngine.cs` handles keyword-based comment automation. It scans automation flows owned by the current user and matches against a post URL and incoming comment text.

### Flow of Logic

```
Given: postUrl, commentText

For each AutomationFlow where UserId = current user:
    If flow.PostUrl == postUrl
    AND commentText contains any word from flow.Keywords (comma-separated, case-insensitive):

        Log → "Comment Reply Sent: {ReplyMessage}"
        Log → "DM Sent: {DmMessage}"

        If commentText contains "done":
            Log → "User Follow Verified"
            Log → "Link Sent: {SpecialLink}"

        Stop (first matching flow wins)
```

### Activity Logging Query

Every action triggered by the engine writes to `ActivityLogs`:

```sql
INSERT INTO ActivityLogs (Action, Status)
VALUES (@action, 'Success');
-- Time is auto-set by the DEFAULT GETDATE() constraint
```

### Keywords Format

Keywords are stored as a comma-separated string in the `AutomationFlows.Keywords` column:

```
buy,interested,price,how much,dm me
```

The engine splits on `,`, trims whitespace, and does a case-insensitive `Contains` check against the full comment text.

---


## License

This project is licensed under the MIT License. See `LICENSE` for details.
