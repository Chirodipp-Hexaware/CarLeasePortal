Use [UAT]

/****** Object:  Schema [CAR_LEASE]    Script Date: 8/24/2021 1:22:31 PM ******/
CREATE SCHEMA [CAR_LEASE]
;

CREATE SEQUENCE CAR_LEASE.seq_category_id
    START WITH 1 
    INCREMENT BY 1  
    MINVALUE 1  
    MAXVALUE 9999999999
    NO CYCLE  
    CACHE 10  
;  

CREATE SEQUENCE CAR_LEASE.seq_cust_id
    START WITH 1 
    INCREMENT BY 1  
    MINVALUE 1  
    MAXVALUE 9999999999999
    NO CYCLE  
    CACHE 10  
; 

CREATE SEQUENCE CAR_LEASE.seq_territory_id
    START WITH 1 
    INCREMENT BY 1  
    MINVALUE 1  
    MAXVALUE 9999999999999
    NO CYCLE  
    CACHE 10  
; 

CREATE SEQUENCE CAR_LEASE.seq_supplier_id
    START WITH 1 
    INCREMENT BY 1  
    MINVALUE 1  
    MAXVALUE 9999999999999
    NO CYCLE  
    CACHE 10  
; 

CREATE SEQUENCE CAR_LEASE.seq_order_id
    START WITH 1 
    INCREMENT BY 1  
    MINVALUE 1  
    MAXVALUE 9999999999
    NO CYCLE  
    CACHE 10  
;  

CREATE SEQUENCE CAR_LEASE.seq_product_id
    START WITH 1 
    INCREMENT BY 1  
    MINVALUE 1  
    MAXVALUE 9999999999999
    NO CYCLE  
    CACHE 10  
; 



Use [UAT]

/****** Object:  Schema [CAR_LEASE]    Script Date: 8/24/2021 1:22:31 PM ******/
/*

DROP TABLE [CAR_LEASE].[AAA] ;
DROP TABLE [CAR_LEASE].[CustomerCustomerDemo] ;
DROP TABLE [CAR_LEASE].[CustomerDemographics] ;

DROP TABLE [CAR_LEASE].[Order Details] ;
DROP TABLE [CAR_LEASE].[OrderDetailsArchive] ;
DROP TABLE [CAR_LEASE].[Orders] ;
DROP TABLE [CAR_LEASE].[Products] ;
DROP TABLE [CAR_LEASE].[Customers] ;
DROP TABLE [CAR_LEASE].[Cateries] ;
DROP TABLE [CAR_LEASE].[Emp] ;
DROP TABLE [CAR_LEASE].[EmployeeTerritories] ;
DROP TABLE [CAR_LEASE].[Employees] ;
DROP TABLE [CAR_LEASE].[Territories] ;
DROP TABLE [CAR_LEASE].[Region] ;
DROP TABLE [CAR_LEASE].[Shippers] ;
DROP TABLE [CAR_LEASE].[Suppliers] ;

DROP TABLE [CAR_LEASE].[Test] ;
DROP TABLE [CAR_LEASE].[TESTKEYVALUES] ;
DROP TABLE [CAR_LEASE].[TestTable] ;

*/

--select 'DROP TABLE [CAR_LEASE].['+TABLE_NAME+'] ;' from information_schema.tables where table_schema='CAR_LEASE';

/****** Object:  Table [CAR_LEASE].[AAA]    Script Date: 8/24/2021 1:17:19 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [CAR_LEASE].[AAA](
	[Testkey] [nvarchar](128) NULL,
	[testvalue] [varbinary](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
/****** Object:  Table [CAR_LEASE].[Cateries]    Script Date: 8/24/2021 1:17:19 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [CAR_LEASE].[Cateries](
	[CateryID] [int] IDENTITY(1,1) NOT NULL,
	[CateryName] [nvarchar](15) NOT NULL,
	[Description] [ntext] NULL,
	[Picture] [image] NULL,
	[Sno] [int] NULL,
 CONSTRAINT [PK_Cateries] PRIMARY KEY CLUSTERED 
(
	[CateryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
/****** Object:  Table [CAR_LEASE].[CustomerCustomerDemo]    Script Date: 8/24/2021 1:17:19 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [CAR_LEASE].[CustomerCustomerDemo](
	[CustomerID] [nchar](5) NOT NULL,
	[CustomerTypeID] [nchar](10) NOT NULL,
 CONSTRAINT [PK_CustomerCustomerDemo] PRIMARY KEY NONCLUSTERED 
(
	[CustomerID] ASC,
	[CustomerTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
;
/****** Object:  Table [CAR_LEASE].[CustomerDemographics]    Script Date: 8/24/2021 1:17:19 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [CAR_LEASE].[CustomerDemographics](
	[CustomerTypeID] [nchar](10) NOT NULL,
	[CustomerDesc] [ntext] NULL,
 CONSTRAINT [PK_CustomerDemographics] PRIMARY KEY NONCLUSTERED 
(
	[CustomerTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
/****** Object:  Table [CAR_LEASE].[Customers]    Script Date: 8/24/2021 1:17:19 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [CAR_LEASE].[Customers](
	[CustomerID] [nchar](5) NOT NULL,
	[CompanyName] [nvarchar](40) NOT NULL,
	[ContactName] [nvarchar](30) NULL,
	[ContactTitle] [nvarchar](30) NULL,
	[Address] [nvarchar](60) NULL,
	[City] [nvarchar](15) NULL,
	[Region] [nvarchar](15) NULL,
	[PostalCode] [nvarchar](10) NULL,
	[Country] [nvarchar](15) NULL,
	[Phone] [nvarchar](24) NULL,
	[Fax] [nvarchar](24) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
;
/****** Object:  Table [CAR_LEASE].[Emp]    Script Date: 8/24/2021 1:17:19 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [CAR_LEASE].[Emp](
	[Emp_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Emp_Code] [varchar](20) NOT NULL,
	[Emp_Name] [varchar](200) NULL,
	[Emp_DOB] [datetime] NULL,
	[Emp_Salary] [decimal](18, 2) NULL,
	[Emp_Status] [bit] NULL,
	[Emp_Resume] [binary](50) NULL,
	[Emp_Photo] [image] NULL,
PRIMARY KEY CLUSTERED 
(
	[Emp_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
/****** Object:  Table [CAR_LEASE].[Employees]    Script Date: 8/24/2021 1:17:19 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [CAR_LEASE].[Employees](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [nvarchar](20) NOT NULL,
	[FirstName] [nvarchar](10) NOT NULL,
	[Title] [nvarchar](30) NULL,
	[TitleOfCourtesy] [nvarchar](25) NULL,
	[BirthDate] [datetime] NULL,
	[HireDate] [datetime] NULL,
	[Address] [nvarchar](60) NULL,
	[City] [nvarchar](15) NULL,
	[Region] [nvarchar](15) NULL,
	[PostalCode] [nvarchar](10) NULL,
	[Country] [nvarchar](15) NULL,
	[HomePhone] [nvarchar](24) NULL,
	[Extension] [nvarchar](4) NULL,
	[Photo] [image] NULL,
	[Notes] [ntext] NULL,
	[ReportsTo] [int] NULL,
	[PhotoPath] [nvarchar](255) NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
/****** Object:  Table [CAR_LEASE].[EmployeeTerritories]    Script Date: 8/24/2021 1:17:19 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [CAR_LEASE].[EmployeeTerritories](
	[EmployeeID] [int] NOT NULL,
	[TerritoryID] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_EmployeeTerritories] PRIMARY KEY NONCLUSTERED 
(
	[EmployeeID] ASC,
	[TerritoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
;
/****** Object:  Table [CAR_LEASE].[Order Details]    Script Date: 8/24/2021 1:17:19 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [CAR_LEASE].[Order Details](
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[UnitPrice] [money] NOT NULL,
	[Quantity] [smallint] NOT NULL,
	[Discount] [real] NOT NULL,
 CONSTRAINT [PK_Order_Details] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
;
/****** Object:  Table [CAR_LEASE].[OrderDetailsArchive]    Script Date: 8/24/2021 1:17:19 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [CAR_LEASE].[OrderDetailsArchive](
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[UnitPrice] [money] NOT NULL,
	[Quantity] [smallint] NOT NULL,
	[Discount] [real] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[UserId] [varchar](150) NOT NULL,
	[OperationType] [char](1) NOT NULL
) ON [PRIMARY]
;
/****** Object:  Table [CAR_LEASE].[Orders]    Script Date: 8/24/2021 1:17:19 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [CAR_LEASE].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [nchar](5) NULL,
	[EmployeeID] [int] NULL,
	[OrderDate] [datetime] NULL,
	[RequiredDate] [datetime] NULL,
	[ShippedDate] [datetime] NULL,
	[ShipVia] [int] NULL,
	[Freight] [money] NULL,
	[ShipName] [nvarchar](40) NULL,
	[ShipAddress] [nvarchar](60) NULL,
	[ShipCity] [nvarchar](15) NULL,
	[ShipRegion] [nvarchar](15) NULL,
	[ShipPostalCode] [nvarchar](10) NULL,
	[ShipCountry] [nvarchar](15) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
;
/****** Object:  Table [CAR_LEASE].[Products]    Script Date: 8/24/2021 1:17:19 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [CAR_LEASE].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](40) NOT NULL,
	[SupplierID] [int] NULL,
	[CateryID] [int] NULL,
	[QuantityPerUnit] [nvarchar](20) NULL,
	[UnitPrice] [money] NULL,
	[UnitsInStock] [smallint] NULL,
	[UnitsOnOrder] [smallint] NULL,
	[ReorderLevel] [smallint] NULL,
	[Discontinued] [bit] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
;
/****** Object:  Table [CAR_LEASE].[Region]    Script Date: 8/24/2021 1:17:19 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [CAR_LEASE].[Region](
	[RegionID] [int] NOT NULL,
	[RegionDescription] [nchar](50) NOT NULL,
 CONSTRAINT [PK_Region] PRIMARY KEY NONCLUSTERED 
(
	[RegionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
;
/****** Object:  Table [CAR_LEASE].[Shippers]    Script Date: 8/24/2021 1:17:19 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [CAR_LEASE].[Shippers](
	[ShipperID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](40) NOT NULL,
	[Phone] [nvarchar](24) NULL,
 CONSTRAINT [PK_Shippers] PRIMARY KEY CLUSTERED 
(
	[ShipperID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
;
/****** Object:  Table [CAR_LEASE].[Suppliers]    Script Date: 8/24/2021 1:17:19 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [CAR_LEASE].[Suppliers](
	[SupplierID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](40) NOT NULL,
	[ContactName] [nvarchar](30) NULL,
	[ContactTitle] [nvarchar](30) NULL,
	[Address] [nvarchar](60) NULL,
	[City] [nvarchar](15) NULL,
	[Region] [nvarchar](15) NULL,
	[PostalCode] [nvarchar](10) NULL,
	[Country] [nvarchar](15) NULL,
	[Phone] [nvarchar](24) NULL,
	[Fax] [nvarchar](24) NULL,
	[HomePage] [ntext] NULL,
 CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
/****** Object:  Table [CAR_LEASE].[Territories]    Script Date: 8/24/2021 1:17:19 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [CAR_LEASE].[Territories](
	[TerritoryID] [nvarchar](20) NOT NULL,
	[TerritoryDescription] [nchar](50) NOT NULL,
	[RegionID] [int] NOT NULL,
 CONSTRAINT [PK_Territories] PRIMARY KEY NONCLUSTERED 
(
	[TerritoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
;
/****** Object:  Table [CAR_LEASE].[Test]    Script Date: 8/24/2021 1:17:19 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [CAR_LEASE].[Test](
	[Photo] [binary](8000) NULL
) ON [PRIMARY]
;
/****** Object:  Table [CAR_LEASE].[TESTKEYVALUES]    Script Date: 8/24/2021 1:17:19 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [CAR_LEASE].[TESTKEYVALUES](
	[KEY] [decimal](9, 2) NOT NULL,
	[VALUE] [varchar](12) NOT NULL
) ON [PRIMARY]
;
/****** Object:  Table [CAR_LEASE].[TestTable]    Script Date: 8/24/2021 1:17:19 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [CAR_LEASE].[TestTable](
	[HashP] [nvarchar](128) NOT NULL,
	[UID] [nvarchar](128) NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[AppUID] [nvarchar](128) NULL
) ON [PRIMARY]
;
SET ANSI_PADDING ON

/****** Object:  Index [CateryName]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [CateryName] ON [CAR_LEASE].[Cateries]
(
	[CateryName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

SET ANSI_PADDING ON
;
/****** Object:  Index [City]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [City] ON [CAR_LEASE].[Customers]
(
	[City] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
SET ANSI_PADDING ON

/****** Object:  Index [CompanyName]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [CompanyName] ON [CAR_LEASE].[Customers]
(
	[CompanyName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
SET ANSI_PADDING ON

/****** Object:  Index [PostalCode]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [PostalCode] ON [CAR_LEASE].[Customers]
(
	[PostalCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
SET ANSI_PADDING ON

/****** Object:  Index [Region]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [Region] ON [CAR_LEASE].[Customers]
(
	[Region] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
SET ANSI_PADDING ON

/****** Object:  Index [LastName]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [LastName] ON [CAR_LEASE].[Employees]
(
	[LastName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
SET ANSI_PADDING ON

/****** Object:  Index [PostalCode]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [PostalCode] ON [CAR_LEASE].[Employees]
(
	[PostalCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
/****** Object:  Index [OrderID]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [OrderID] ON [CAR_LEASE].[Order Details]
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
/****** Object:  Index [OrdersOrder_Details]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [OrdersOrder_Details] ON [CAR_LEASE].[Order Details]
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
/****** Object:  Index [ProductID]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [ProductID] ON [CAR_LEASE].[Order Details]
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
/****** Object:  Index [ProductsOrder_Details]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [ProductsOrder_Details] ON [CAR_LEASE].[Order Details]
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
SET ANSI_PADDING ON

/****** Object:  Index [CustomerID]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [CustomerID] ON [CAR_LEASE].[Orders]
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
SET ANSI_PADDING ON

/****** Object:  Index [CustomersOrders]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [CustomersOrders] ON [CAR_LEASE].[Orders]
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
/****** Object:  Index [EmployeeID]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [EmployeeID] ON [CAR_LEASE].[Orders]
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
/****** Object:  Index [EmployeesOrders]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [EmployeesOrders] ON [CAR_LEASE].[Orders]
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
/****** Object:  Index [OrderDate]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [OrderDate] ON [CAR_LEASE].[Orders]
(
	[OrderDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
/****** Object:  Index [ShippedDate]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [ShippedDate] ON [CAR_LEASE].[Orders]
(
	[ShippedDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
/****** Object:  Index [ShippersOrders]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [ShippersOrders] ON [CAR_LEASE].[Orders]
(
	[ShipVia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
SET ANSI_PADDING ON

/****** Object:  Index [ShipPostalCode]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [ShipPostalCode] ON [CAR_LEASE].[Orders]
(
	[ShipPostalCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
/****** Object:  Index [CateriesProducts]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [CateriesProducts] ON [CAR_LEASE].[Products]
(
	[CateryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
/****** Object:  Index [CateryID]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [CateryID] ON [CAR_LEASE].[Products]
(
	[CateryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
SET ANSI_PADDING ON

/****** Object:  Index [ProductName]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [ProductName] ON [CAR_LEASE].[Products]
(
	[ProductName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
/****** Object:  Index [SupplierID]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [SupplierID] ON [CAR_LEASE].[Products]
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
/****** Object:  Index [SuppliersProducts]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [SuppliersProducts] ON [CAR_LEASE].[Products]
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
SET ANSI_PADDING ON

/****** Object:  Index [CompanyName]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [CompanyName] ON [CAR_LEASE].[Suppliers]
(
	[CompanyName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
SET ANSI_PADDING ON

/****** Object:  Index [PostalCode]    Script Date: 8/24/2021 1:17:19 PM ******/
CREATE NONCLUSTERED INDEX [PostalCode] ON [CAR_LEASE].[Suppliers]
(
	[PostalCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;

ALTER TABLE [CAR_LEASE].[Order Details] ADD  CONSTRAINT [DF_Order_Details_UnitPrice]  DEFAULT ((0)) FOR [UnitPrice]
;
ALTER TABLE [CAR_LEASE].[Order Details] ADD  CONSTRAINT [DF_Order_Details_Quantity]  DEFAULT ((1)) FOR [Quantity]
;
ALTER TABLE [CAR_LEASE].[Order Details] ADD  CONSTRAINT [DF_Order_Details_Discount]  DEFAULT ((0)) FOR [Discount]
;
ALTER TABLE [CAR_LEASE].[Orders] ADD  CONSTRAINT [DF_Orders_Freight]  DEFAULT ((0)) FOR [Freight]
;
ALTER TABLE [CAR_LEASE].[Products] ADD  CONSTRAINT [DF_Products_UnitPrice]  DEFAULT ((0)) FOR [UnitPrice]
;
ALTER TABLE [CAR_LEASE].[Products] ADD  CONSTRAINT [DF_Products_UnitsInStock]  DEFAULT ((0)) FOR [UnitsInStock]
;
ALTER TABLE [CAR_LEASE].[Products] ADD  CONSTRAINT [DF_Products_UnitsOnOrder]  DEFAULT ((0)) FOR [UnitsOnOrder]
;
ALTER TABLE [CAR_LEASE].[Products] ADD  CONSTRAINT [DF_Products_ReorderLevel]  DEFAULT ((0)) FOR [ReorderLevel]
;
ALTER TABLE [CAR_LEASE].[Products] ADD  CONSTRAINT [DF_Products_Discontinued]  DEFAULT ((0)) FOR [Discontinued]
;
ALTER TABLE [CAR_LEASE].[CustomerCustomerDemo]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCustomerDemo] FOREIGN KEY([CustomerTypeID])
REFERENCES [CAR_LEASE].[CustomerDemographics] ([CustomerTypeID])
;
ALTER TABLE [CAR_LEASE].[CustomerCustomerDemo] CHECK CONSTRAINT [FK_CustomerCustomerDemo]
;
ALTER TABLE [CAR_LEASE].[CustomerCustomerDemo]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCustomerDemo_Customers] FOREIGN KEY([CustomerID])
REFERENCES [CAR_LEASE].[Customers] ([CustomerID])
;
ALTER TABLE [CAR_LEASE].[CustomerCustomerDemo] CHECK CONSTRAINT [FK_CustomerCustomerDemo_Customers]
;
ALTER TABLE [CAR_LEASE].[Employees]  WITH NOCHECK ADD  CONSTRAINT [FK_Employees_Employees] FOREIGN KEY([ReportsTo])
REFERENCES [CAR_LEASE].[Employees] ([EmployeeID])
;
ALTER TABLE [CAR_LEASE].[Employees] CHECK CONSTRAINT [FK_Employees_Employees]
;
ALTER TABLE [CAR_LEASE].[EmployeeTerritories]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeTerritories_Employees] FOREIGN KEY([EmployeeID])
REFERENCES [CAR_LEASE].[Employees] ([EmployeeID])
;
ALTER TABLE [CAR_LEASE].[EmployeeTerritories] CHECK CONSTRAINT [FK_EmployeeTerritories_Employees]
;
ALTER TABLE [CAR_LEASE].[EmployeeTerritories]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeTerritories_Territories] FOREIGN KEY([TerritoryID])
REFERENCES [CAR_LEASE].[Territories] ([TerritoryID])
;
ALTER TABLE [CAR_LEASE].[EmployeeTerritories] CHECK CONSTRAINT [FK_EmployeeTerritories_Territories]
;
ALTER TABLE [CAR_LEASE].[Order Details]  WITH NOCHECK ADD  CONSTRAINT [FK_Order_Details_Orders] FOREIGN KEY([OrderID])
REFERENCES [CAR_LEASE].[Orders] ([OrderID])
;
ALTER TABLE [CAR_LEASE].[Order Details] CHECK CONSTRAINT [FK_Order_Details_Orders]
;
ALTER TABLE [CAR_LEASE].[Order Details]  WITH NOCHECK ADD  CONSTRAINT [FK_Order_Details_Products] FOREIGN KEY([ProductID])
REFERENCES [CAR_LEASE].[Products] ([ProductID])
;
ALTER TABLE [CAR_LEASE].[Order Details] CHECK CONSTRAINT [FK_Order_Details_Products]
;
ALTER TABLE [CAR_LEASE].[Orders]  WITH NOCHECK ADD  CONSTRAINT [FK_Orders_Customers] FOREIGN KEY([CustomerID])
REFERENCES [CAR_LEASE].[Customers] ([CustomerID])
;
ALTER TABLE [CAR_LEASE].[Orders] CHECK CONSTRAINT [FK_Orders_Customers]
;
ALTER TABLE [CAR_LEASE].[Orders]  WITH NOCHECK ADD  CONSTRAINT [FK_Orders_Employees] FOREIGN KEY([EmployeeID])
REFERENCES [CAR_LEASE].[Employees] ([EmployeeID])
;
ALTER TABLE [CAR_LEASE].[Orders] CHECK CONSTRAINT [FK_Orders_Employees]
;
ALTER TABLE [CAR_LEASE].[Orders]  WITH NOCHECK ADD  CONSTRAINT [FK_Orders_Shippers] FOREIGN KEY([ShipVia])
REFERENCES [CAR_LEASE].[Shippers] ([ShipperID])
;
ALTER TABLE [CAR_LEASE].[Orders] CHECK CONSTRAINT [FK_Orders_Shippers]
;
ALTER TABLE [CAR_LEASE].[Products]  WITH NOCHECK ADD  CONSTRAINT [FK_Products_Cateries] FOREIGN KEY([CateryID])
REFERENCES [CAR_LEASE].[Cateries] ([CateryID])
;
ALTER TABLE [CAR_LEASE].[Products] CHECK CONSTRAINT [FK_Products_Cateries]
;
ALTER TABLE [CAR_LEASE].[Products]  WITH NOCHECK ADD  CONSTRAINT [FK_Products_Suppliers] FOREIGN KEY([SupplierID])
REFERENCES [CAR_LEASE].[Suppliers] ([SupplierID])
;
ALTER TABLE [CAR_LEASE].[Products] CHECK CONSTRAINT [FK_Products_Suppliers]
;
ALTER TABLE [CAR_LEASE].[Territories]  WITH CHECK ADD  CONSTRAINT [FK_Territories_Region] FOREIGN KEY([RegionID])
REFERENCES [CAR_LEASE].[Region] ([RegionID])
;
ALTER TABLE [CAR_LEASE].[Territories] CHECK CONSTRAINT [FK_Territories_Region]
;
ALTER TABLE [CAR_LEASE].[Employees]  WITH NOCHECK ADD  CONSTRAINT [CK_Birthdate] CHECK  (([BirthDate]<getdate()))
;
ALTER TABLE [CAR_LEASE].[Employees] CHECK CONSTRAINT [CK_Birthdate]
;
ALTER TABLE [CAR_LEASE].[Order Details]  WITH NOCHECK ADD  CONSTRAINT [CK_Discount] CHECK  (([Discount]>=(0) AND [Discount]<=(1)))
;
ALTER TABLE [CAR_LEASE].[Order Details] CHECK CONSTRAINT [CK_Discount]
;
ALTER TABLE [CAR_LEASE].[Order Details]  WITH NOCHECK ADD  CONSTRAINT [CK_Quantity] CHECK  (([Quantity]>(0)))
;
ALTER TABLE [CAR_LEASE].[Order Details] CHECK CONSTRAINT [CK_Quantity]
;
ALTER TABLE [CAR_LEASE].[Order Details]  WITH NOCHECK ADD  CONSTRAINT [CK_UnitPrice] CHECK  (([UnitPrice]>=(0)))
;
ALTER TABLE [CAR_LEASE].[Order Details] CHECK CONSTRAINT [CK_UnitPrice]
;
ALTER TABLE [CAR_LEASE].[Products]  WITH NOCHECK ADD  CONSTRAINT [CK_Products_UnitPrice] CHECK  (([UnitPrice]>=(0)))
;
ALTER TABLE [CAR_LEASE].[Products] CHECK CONSTRAINT [CK_Products_UnitPrice]
;
ALTER TABLE [CAR_LEASE].[Products]  WITH NOCHECK ADD  CONSTRAINT [CK_ReorderLevel] CHECK  (([ReorderLevel]>=(0)))
;
ALTER TABLE [CAR_LEASE].[Products] CHECK CONSTRAINT [CK_ReorderLevel]
;
ALTER TABLE [CAR_LEASE].[Products]  WITH NOCHECK ADD  CONSTRAINT [CK_UnitsInStock] CHECK  (([UnitsInStock]>=(0)))
;
ALTER TABLE [CAR_LEASE].[Products] CHECK CONSTRAINT [CK_UnitsInStock]
;
ALTER TABLE [CAR_LEASE].[Products]  WITH NOCHECK ADD  CONSTRAINT [CK_UnitsOnOrder] CHECK  (([UnitsOnOrder]>=(0)))
;
ALTER TABLE [CAR_LEASE].[Products] CHECK CONSTRAINT [CK_UnitsOnOrder]
;

Use [UAT]

/****** Object:  View [CAR_LEASE].[Product Sales for 1997]    Script Date: 8/24/2021 1:19:16 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


CREATE OR ALTER VIEW   [CAR_LEASE].[Product Sales for 1997] AS
SELECT Cateries.CateryName, Products.ProductName, 
Sum(CONVERT(money,("Order Details".UnitPrice*Quantity*(1-Discount)/100))*100) AS ProductSales
FROM (Cateries INNER JOIN Products ON Cateries.CateryID = Products.CateryID) 
	INNER JOIN (Orders 
		INNER JOIN "Order Details" ON Orders.OrderID = "Order Details".OrderID) 
	ON Products.ProductID = "Order Details".ProductID
WHERE (((Orders.ShippedDate) Between '19970101' And '19971231'))
GROUP BY Cateries.CateryName, Products.ProductName
;
/****** Object:  View [CAR_LEASE].[Catery Sales for 1997]    Script Date: 8/24/2021 1:19:16 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


CREATE OR ALTER VIEW   [CAR_LEASE].[Catery Sales for 1997] AS
SELECT "Product Sales for 1997".CateryName, Sum("Product Sales for 1997".ProductSales) AS CaterySales
FROM "Product Sales for 1997"
GROUP BY "Product Sales for 1997".CateryName
;
/****** Object:  View [CAR_LEASE].[Order Details Extended]    Script Date: 8/24/2021 1:19:16 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


CREATE OR ALTER VIEW   [CAR_LEASE].[Order Details Extended] AS
SELECT "Order Details".OrderID, "Order Details".ProductID, Products.ProductName, 
	"Order Details".UnitPrice, "Order Details".Quantity, "Order Details".Discount, 
	(CONVERT(money,("Order Details".UnitPrice*Quantity*(1-Discount)/100))*100) AS ExtendedPrice
FROM Products INNER JOIN "Order Details" ON Products.ProductID = "Order Details".ProductID
--ORDER BY "Order Details".OrderID
;
/****** Object:  View [CAR_LEASE].[Sales by Catery]    Script Date: 8/24/2021 1:19:16 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


CREATE OR ALTER VIEW   [CAR_LEASE].[Sales by Catery] AS
SELECT Cateries.CateryID, Cateries.CateryName, Products.ProductName, 
	Sum("Order Details Extended".ExtendedPrice) AS ProductSales
FROM 	Cateries INNER JOIN 
		(Products INNER JOIN 
			(Orders INNER JOIN "Order Details Extended" ON Orders.OrderID = "Order Details Extended".OrderID) 
		ON Products.ProductID = "Order Details Extended".ProductID) 
	ON Cateries.CateryID = Products.CateryID
WHERE Orders.OrderDate BETWEEN '19970101' And '19971231'
GROUP BY Cateries.CateryID, Cateries.CateryName, Products.ProductName
--ORDER BY Products.ProductName
;
/****** Object:  View [CAR_LEASE].[Order Subtotals]    Script Date: 8/24/2021 1:19:16 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


CREATE OR ALTER VIEW   [CAR_LEASE].[Order Subtotals] AS
SELECT "Order Details".OrderID, Sum(CONVERT(money,("Order Details".UnitPrice*Quantity*(1-Discount)/100))*100) AS Subtotal
FROM "Order Details"
GROUP BY "Order Details".OrderID
;
/****** Object:  View [CAR_LEASE].[Sales Totals by Amount]    Script Date: 8/24/2021 1:19:16 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


CREATE OR ALTER VIEW   [CAR_LEASE].[Sales Totals by Amount] AS
SELECT "Order Subtotals".Subtotal AS SaleAmount, Orders.OrderID, Customers.CompanyName, Orders.ShippedDate
FROM 	Customers INNER JOIN 
		(Orders INNER JOIN "Order Subtotals" ON Orders.OrderID = "Order Subtotals".OrderID) 
	ON Customers.CustomerID = Orders.CustomerID
WHERE ("Order Subtotals".Subtotal >2500) AND (Orders.ShippedDate BETWEEN '19970101' And '19971231')
;
/****** Object:  View [CAR_LEASE].[Summary of Sales by Quarter]    Script Date: 8/24/2021 1:19:16 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


CREATE OR ALTER VIEW   [CAR_LEASE].[Summary of Sales by Quarter] AS
SELECT Orders.ShippedDate, Orders.OrderID, "Order Subtotals".Subtotal
FROM Orders INNER JOIN "Order Subtotals" ON Orders.OrderID = "Order Subtotals".OrderID
WHERE Orders.ShippedDate IS NOT NULL
--ORDER BY Orders.ShippedDate
;
/****** Object:  View [CAR_LEASE].[Summary of Sales by Year]    Script Date: 8/24/2021 1:19:16 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


CREATE OR ALTER VIEW   [CAR_LEASE].[Summary of Sales by Year] AS
SELECT Orders.ShippedDate, Orders.OrderID, "Order Subtotals".Subtotal
FROM Orders INNER JOIN "Order Subtotals" ON Orders.OrderID = "Order Subtotals".OrderID
WHERE Orders.ShippedDate IS NOT NULL
--ORDER BY Orders.ShippedDate
;
/****** Object:  View [CAR_LEASE].[Current Product List]    Script Date: 8/24/2021 1:19:16 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


CREATE OR ALTER VIEW   [CAR_LEASE].[Current Product List] AS
SELECT Product_List.ProductID, Product_List.ProductName
FROM Products AS Product_List
WHERE (((Product_List.Discontinued)=0))
--ORDER BY Product_List.ProductName
;
/****** Object:  View [CAR_LEASE].[Customer and Suppliers by City]    Script Date: 8/24/2021 1:19:16 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


CREATE OR ALTER VIEW   [CAR_LEASE].[Customer and Suppliers by City] AS
SELECT City, CompanyName, ContactName, 'Customers' AS Relationship 
FROM Customers
UNION SELECT City, CompanyName, ContactName, 'Suppliers'
FROM Suppliers
--ORDER BY City, CompanyName
;
/****** Object:  View [CAR_LEASE].[Invoices]    Script Date: 8/24/2021 1:19:16 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


CREATE OR ALTER VIEW   [CAR_LEASE].[Invoices] AS
SELECT Orders.ShipName, Orders.ShipAddress, Orders.ShipCity, Orders.ShipRegion, Orders.ShipPostalCode, 
	Orders.ShipCountry, Orders.CustomerID, Customers.CompanyName AS CustomerName, Customers.Address, Customers.City, 
	Customers.Region, Customers.PostalCode, Customers.Country, 
	(FirstName + ' ' + LastName) AS Salesperson, 
	Orders.OrderID, Orders.OrderDate, Orders.RequiredDate, Orders.ShippedDate, Shippers.CompanyName As ShipperName, 
	"Order Details".ProductID, Products.ProductName, "Order Details".UnitPrice, "Order Details".Quantity, 
	"Order Details".Discount, 
	(CONVERT(money,("Order Details".UnitPrice*Quantity*(1-Discount)/100))*100) AS ExtendedPrice, Orders.Freight
FROM 	Shippers INNER JOIN 
		(Products INNER JOIN 
			(
				(Employees INNER JOIN 
					(Customers INNER JOIN Orders ON Customers.CustomerID = Orders.CustomerID) 
				ON Employees.EmployeeID = Orders.EmployeeID) 
			INNER JOIN "Order Details" ON Orders.OrderID = "Order Details".OrderID) 
		ON Products.ProductID = "Order Details".ProductID) 
	ON Shippers.ShipperID = Orders.ShipVia
;
/****** Object:  View [CAR_LEASE].[IV_ORDER_DETAILS]    Script Date: 8/24/2021 1:19:16 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE OR ALTER VIEW [CAR_LEASE].[IV_ORDER_DETAILS]
	WITH SCHEMABINDING AS
SELECT i.UnitPrice as max_unit_price, i.Quantity, i.Discount, i.ProductID, i.OrderID
FROM [CAR_LEASE].[Order Details] i  
GROUP BY i.OrderID, i.ProductID, i.Quantity, i.Discount, i.UnitPrice
;
/****** Object:  View [CAR_LEASE].[IV_ORDERS]    Script Date: 8/24/2021 1:19:16 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE OR ALTER VIEW   [CAR_LEASE].[IV_ORDERS]
	WITH SCHEMABINDING AS
SELECT o.OrderID, i.UnitPrice, i.Quantity, i.Discount, i.ProductID, o.ShipName, o.CustomerID, o.EmployeeID
FROM [CAR_LEASE].[Order Details] i INNER JOIN [CAR_LEASE].[Orders] o ON i.OrderID = o.OrderID 
;
/****** Object:  View [CAR_LEASE].[Orders Qry]    Script Date: 8/24/2021 1:19:16 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


CREATE OR ALTER VIEW   [CAR_LEASE].[Orders Qry] AS
SELECT Orders.OrderID, Orders.CustomerID, Orders.EmployeeID, Orders.OrderDate, Orders.RequiredDate, 
	Orders.ShippedDate, Orders.ShipVia, Orders.Freight, Orders.ShipName, Orders.ShipAddress, Orders.ShipCity, 
	Orders.ShipRegion, Orders.ShipPostalCode, Orders.ShipCountry, 
	Customers.CompanyName, Customers.Address, Customers.City, Customers.Region, Customers.PostalCode, Customers.Country
FROM Customers INNER JOIN Orders ON Customers.CustomerID = Orders.CustomerID
;
/****** Object:  View [CAR_LEASE].[Products Above Average Price]    Script Date: 8/24/2021 1:19:16 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


CREATE OR ALTER VIEW   [CAR_LEASE].[Products Above Average Price] AS
SELECT Products.ProductName, Products.UnitPrice
FROM Products
WHERE Products.UnitPrice>(SELECT AVG(UnitPrice) From Products)
--ORDER BY Products.UnitPrice DESC
;
/****** Object:  View [CAR_LEASE].[Products by Catery]    Script Date: 8/24/2021 1:19:16 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


CREATE OR ALTER VIEW   [CAR_LEASE].[Products by Catery] AS
SELECT Cateries.CateryName, Products.ProductName, Products.QuantityPerUnit, Products.UnitsInStock, Products.Discontinued
FROM Cateries INNER JOIN Products ON Cateries.CateryID = Products.CateryID
WHERE Products.Discontinued <> 1
--ORDER BY Cateries.CateryName, Products.ProductName
;
/****** Object:  View [CAR_LEASE].[Quarterly Orders]    Script Date: 8/24/2021 1:19:16 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


CREATE OR ALTER VIEW   [CAR_LEASE].[Quarterly Orders] AS
SELECT DISTINCT Customers.CustomerID, Customers.CompanyName, Customers.City, Customers.Country
FROM Customers RIGHT JOIN Orders ON Customers.CustomerID = Orders.CustomerID
WHERE Orders.OrderDate BETWEEN '19970101' And '19971231'
;
/****** Object:  View [CAR_LEASE].[V_CUST_DETAIL]    Script Date: 8/24/2021 1:19:16 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE OR ALTER VIEW   [CAR_LEASE].[V_CUST_DETAIL] WITH SCHEMABINDING AS SELECT CustomerID, CompanyName, City, PostalCode, Country Phone FROM CAR_LEASE.CUSTOMERS
;


Use [UAT]

/****** Object:  UserDefinedFunction [CAR_LEASE].[resign_employee]    Script Date: 8/24/2021 1:21:35 PM ******/
SET ANSI_NULLS ON 

SET QUOTED_IDENTIFIER ON 

CREATE FUNCTION [CAR_LEASE].[resign_employee](@num CHAR(6))
  RETURNS @delEmpDetail TABLE   
  (
	empno  CHAR(6),
	salary DECIMAL,
	dept   CHAR(3)
	)
  AS
  BEGIN 
    DECLARE @l_salary DECIMAL;
    DECLARE @l_job CHAR(3);
    SELECT @l_salary=salary, @l_job=job FROM [CAR_LEASE].[EMPLOYEE] WHERE employee.empno = @num;
	DELETE FROM [CAR_LEASE].[EMPLOYEE] WHERE employee.empno = @num;
	INSERT into @delEmpDetail values (@num, @l_salary, @l_job);
    RETURN 
  END;
/
/****** Object:  UserDefinedFunction [CAR_LEASE].[udfNetSale]    Script Date: 8/24/2021 1:21:35 PM ******/

CREATE FUNCTION [CAR_LEASE].[udfNetSale](
    @quantity INT,
    @list_price DEC(10,2),
    @discount DEC(4,2)
)
RETURNS DEC(10,2)
AS 
BEGIN
    RETURN @quantity * @list_price * (1 - @discount);
END
;
/
/****** Object:  UserDefinedFunction [CAR_LEASE].[GetSuplierByCity]    Script Date: 8/24/2021 1:21:35 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


--select * from [CAR_LEASE].[Suppliers]


Create Function [CAR_LEASE].[GetSuplierByCity]
(
@City nvarchar(200)
)
Returns Table 
As

Return
(
	select * from [CAR_LEASE].[Suppliers] Where City=@City 
)
;
/


--
Use [UAT]
/*SELECT 'DROP PROCEDURE [CAR_LEASE].['+ROUTINE_NAME+'] ;', ROUTINE_SCHEMA,  ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
WHERE ROUTINE_TYPE = 'PROCEDURE' and routine_schema='CAR_LEASE';
*/

/****** Object:  StoredProcedure [CAR_LEASE].[delete_employee]    Script Date: 8/24/2021 1:20:27 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE PROCEDURE [CAR_LEASE].[delete_employee] (@p_empno CHAR(6))
AS
BEGIN
    DECLARE @v_leavingreason varchar(25);--
    DECLARE @v_ledgerid int;--

    SET @v_leavingreason = 'Resigned';--
    SET @v_ledgerid = 1;--

    INSERT INTO [CAR_LEASE].prev_employee
    (BIRTHDATE,BONUS,COMM,EDLEVEL,EMPNO,EMP_STATUS,FIRSTNME,HIREDATE,JOB,LASTNAME,LAST_DATE,LEAVING_REASON,LEDGER_ID,MIDINIT,PHONENO,SALARY,SEX,WORKDEPT)
       SELECT BIRTHDATE,BONUS,COMM,EDLEVEL,@p_empno,'D',FIRSTNME,HIREDATE,JOB,LASTNAME,CURRENT_TIMESTAMP,@v_leavingreason,@v_ledgerid,MIDINIT,PHONENO,SALARY,SEX,WORKDEPT
        FROM EMPLOYEE where EMPNO = @p_empno;--

    DELETE FROM employee where EMPNO = @p_empno;--

END;
/


/****** Object:  StoredProcedure [CAR_LEASE].[CateryDetails_By_Id]    Script Date: 8/24/2021 1:20:27 PM ******/


CREATE PROCEDURE [CAR_LEASE].[CateryDetails_By_Id]
(
@catoryId char(5)
)
As

BEGIN
select * from Cateries where 
CateryID = @catoryId
END;
/


/****** Object:  StoredProcedure [CAR_LEASE].[CateryDetails_By_Id_1]    Script Date: 8/24/2021 1:20:27 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE PROCEDURE [CAR_LEASE].[CateryDetails_By_Id_1]
(
@catoryId char(5)
)
AS
BEGIN
select * from Cateries where 
CateryID = @catoryId
END;
/


/****** Object:  StoredProcedure [CAR_LEASE].[CustOrderHist]    Script Date: 8/24/2021 1:20:27 PM ******/


CREATE PROCEDURE [CAR_LEASE].[CustOrderHist] @CustomerID nchar(5)
AS
SELECT ProductName, Total=SUM(Quantity)
FROM Products P, [Order Details] OD, Orders O, Customers C
WHERE C.CustomerID = @CustomerID
AND C.CustomerID = O.CustomerID AND O.OrderID = OD.OrderID AND OD.ProductID = P.ProductID
GROUP BY ProductName;
/

/****** Object:  StoredProcedure [CAR_LEASE].[CustOrdersDetail]    Script Date: 8/24/2021 1:20:27 PM ******/



CREATE PROCEDURE [CAR_LEASE].[CustOrdersDetail] @OrderID int
AS
SELECT ProductName,
    UnitPrice=ROUND(Od.UnitPrice, 2),
    Quantity,
    Discount=CONVERT(int, Discount * 100), 
    ExtendedPrice=ROUND(CONVERT(money, Quantity * (1 - Discount) * Od.UnitPrice), 2)
FROM Products P, [Order Details] Od
WHERE Od.ProductID = P.ProductID and Od.OrderID = @OrderID;
/


/****** Object:  StoredProcedure [CAR_LEASE].[CustOrdersOrders]    Script Date: 8/24/2021 1:20:27 PM ******/



CREATE PROCEDURE [CAR_LEASE].[CustOrdersOrders] @CustomerID nchar(5)
AS
SELECT OrderID, 
	OrderDate,
	RequiredDate,
	ShippedDate
FROM Orders
WHERE CustomerID = @CustomerID
ORDER BY OrderID;
/


/****** Object:  StoredProcedure [CAR_LEASE].[Employee Sales by Country]    Script Date: 8/24/2021 1:20:27 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


create procedure [CAR_LEASE].[Employee Sales by Country] 
@Beginning_Date DateTime, @Ending_Date DateTime AS
SELECT Employees.Country, Employees.LastName, Employees.FirstName, Orders.ShippedDate, Orders.OrderID, "Order Subtotals".Subtotal AS SaleAmount
FROM Employees INNER JOIN 
	(Orders INNER JOIN "Order Subtotals" ON Orders.OrderID = "Order Subtotals".OrderID) 
	ON Employees.EmployeeID = Orders.EmployeeID
WHERE Orders.ShippedDate Between @Beginning_Date And @Ending_Date;
/


/****** Object:  StoredProcedure [CAR_LEASE].[GetOrderByCursor]    Script Date: 8/24/2021 1:20:27 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


--Exec GetOrderByCursor 'USA'
Create Procedure [CAR_LEASE].[GetOrderByCursor]
(
@ShipCountry VARCHAR(150) = null
)
As
Begin

DECLARE @CustomerID VARCHAR(150) -- database name 
DECLARE @ShipName VARCHAR(250) -- path for backup files 
DECLARE @ShipAddress VARCHAR(250) -- filename for backup 


DECLARE orderCursor CURSOR FOR 
Select  CustomerID,ShipName,ShipAddress  from Orders Where [ShipCountry]=@ShipCountry

OPEN orderCursor  
FETCH NEXT FROM orderCursor INTO @CustomerID,@ShipName,@ShipAddress  

WHILE @@FETCH_STATUS = 0  
BEGIN  
    
		Select @CustomerID,@ShipName,@ShipAddress 
      FETCH NEXT FROM orderCursor INTO @CustomerID,@ShipName,@ShipAddress  
END 

CLOSE orderCursor  
DEALLOCATE orderCursor 
End;
/


/****** Object:  StoredProcedure [CAR_LEASE].[Sales by Year]    Script Date: 8/24/2021 1:20:27 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


create procedure [CAR_LEASE].[Sales by Year] 
	@Beginning_Date DateTime, @Ending_Date DateTime AS
SELECT Orders.ShippedDate, Orders.OrderID, "Order Subtotals".Subtotal, DATENAME(yy,ShippedDate) AS Year
FROM Orders INNER JOIN "Order Subtotals" ON Orders.OrderID = "Order Subtotals".OrderID
WHERE Orders.ShippedDate Between @Beginning_Date And @Ending_Date;
/

/****** Object:  StoredProcedure [CAR_LEASE].[SalesByCatery]    Script Date: 8/24/2021 1:20:27 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE PROCEDURE [CAR_LEASE].[SalesByCatery]
    @CateryName nvarchar(15), @OrdYear nvarchar(4) = '1998'
AS
IF @OrdYear != '1996' AND @OrdYear != '1997' AND @OrdYear != '1998' 
BEGIN
	SELECT @OrdYear = '1998'
END

SELECT ProductName,
	TotalPurchase=ROUND(SUM(CONVERT(decimal(14,2), OD.Quantity * (1-OD.Discount) * OD.UnitPrice)), 0)
FROM [Order Details] OD, Orders O, Products P, Cateries C
WHERE OD.OrderID = O.OrderID 
	AND OD.ProductID = P.ProductID 
	AND P.CateryID = C.CateryID
	AND C.CateryName = @CateryName
	AND SUBSTRING(CONVERT(nvarchar(22), O.OrderDate, 111), 1, 4) = @OrdYear
GROUP BY ProductName
ORDER BY ProductName;
/


/****** Object:  StoredProcedure [CAR_LEASE].[StringAndDateFuntions]    Script Date: 8/24/2021 1:20:27 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE PROCEDURE [CAR_LEASE].[StringAndDateFuntions] (
@EmployeeID nchar(5),
@funtionsType varchar(10) = Date
)
AS
BEGIN
IF (@funtionsType = 'Date')
 BEGIN
    SELECT 
    ET.EmployeeID, ET.TerritoryID,employee.EmployeeID, employee.LastName, employee.FirstName, employee.Title, employee.TitleOfCourtesy, 
    employee.BirthDate,
    DATEADD(year, 1, employee.BirthDate) AS DateAdd,
    DATEDIFF(year, '2017/08/25', '2011/08/25') AS DateDiff, 
    DATEFROMPARTS(2018, 10, 31) AS DateFromParts,
    DATENAME(year, '2017/08/25') AS DatePartString,
    DAY('2017/08/25') AS DayOfMonth,
    GETDATE(),
    GETUTCDATE(),
    ISDATE('2017-08-25'),
    MONTH('2017/08/25') AS Month,
    SYSDATETIME() AS SysDateTime,
    YEAR('2017/08/25') AS Year
    from EmployeeTerritories ET JOIN Employees employee on ET.EmployeeID = employee.EmployeeID
    where ET.EmployeeID =@EmployeeID
END
ELSE IF (@funtionsType = 'string')
   BEGIN 

   SELECT
    ET.EmployeeID, ET.TerritoryID,employee.EmployeeID, employee.LastName, employee.FirstName, employee.Title, employee.TitleOfCourtesy, 
    employee.BirthDate,
    ASCII(employee.FirstName) AS NumCodeOfFirstChar,
    CHAR(65) AS CodeToCharacter,
    CHARINDEX('t', 'Customer') AS MatchPosition,
    CONCAT('W3Schools', '.com'),
    'W3Schools' + '.com',
    CONCAT_WS('.', 'www', 'W3Schools', 'com'),
    DATALENGTH('W3Schools.com'),
    DIFFERENCE('Juice', 'Jucy'),
    LEFT('SQL Tutorial', 3) AS ExtractString,
    LEN('W3Schools.com'),
    LOWER('SQL Tutorial is FUN!'),
    LTRIM('     SQL Tutorial') AS LeftTrimmedString,
    NCHAR(65) AS NumberCodeToUnicode,
    PATINDEX('%schools%', 'W3Schools.com'),
    QUOTENAME('abcdef'),
    REPLACE('SQL Tutorial', 'T', 'M'),
    REPLICATE('SQL Tutorial', 5),
    REVERSE('SQL Tutorial'),
    RIGHT('SQL Tutorial', 3) AS ExtractString,
    RTRIM('SQL Tutorial     ') AS RightTrimmedString,
    SOUNDEX('Juice'), SOUNDEX('Jucy'),
    SPACE(10),
    STR(185),
    STUFF('SQL Tutorial', 1, 3, 'HTML'),
    SUBSTRING('SQL Tutorial', 1, 3) AS ExtractString,
    TRANSLATE('Monday', 'Monday', 'Sunday'),
    TRIM('     SQL Tutorial!     ') AS TrimmedString,
    UNICODE('Atlanta'),
    UPPER('SQL Tutorial is FUN!')
    from EmployeeTerritories ET JOIN Employees employee on ET.EmployeeID = employee.EmployeeID
    where ET.EmployeeID =@EmployeeID
    END
END;
/


/****** Object:  StoredProcedure [CAR_LEASE].[Ten Most Expensive Products]    Script Date: 8/24/2021 1:20:27 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


create procedure [CAR_LEASE].[Ten Most Expensive Products] AS
SET ROWCOUNT 10
SELECT Products.ProductName AS TenMostExpensiveProducts, Products.UnitPrice
FROM Products
ORDER BY Products.UnitPrice DESC;
/

CREATE TRIGGER [CAR_LEASE].[TRG_Del_Customer] ON [CAR_LEASE].[Customers]
FOR DELETE  
NOT FOR REPLICATION 
AS
 
BEGIN
  INSERT INTO [CAR_LEASE].[Customers_hist]
  SELECT [CustomerID]
    ,getdate()
  FROM Deleted 
END;
/
