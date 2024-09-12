CREATE DATABASE NET1720_231_2_FENGSHUIKOI

CREATE TABLE Element (
    ID INT IDENTITY(1,1) PRIMARY KEY, 
    Name NVARCHAR(15) NOT NULL, 
    Description NTEXT, 
    TabooTime DATETIME, 
    ImageUrl NVARCHAR(MAX), 
    Status BIT DEFAULT 1, 
    LuckyNumbers NVARCHAR(MAX),
    UpdatedAt DATETIME, 
    CreatedAt DATETIME, 
    CreatedBy NVARCHAR(MAX), 
    UpdateBy NVARCHAR(MAX)
);

CREATE TABLE Member (
    ID INT IDENTITY(1,1) PRIMARY KEY,  
    Name NVARCHAR(15) NOT NULL, 
    Username NVARCHAR(15) NOT NULL,  
    Password NVARCHAR(15) NOT NULL,  
    Status BIT DEFAULT 1,  
    Avatar NVARCHAR(MAX),  
    Phone NVARCHAR(15),  
    StoreName NVARCHAR(15),  
    X NVARCHAR(15),  
    Y NVARCHAR(15),  
    Address NVARCHAR(MAX),  
    Description NVARCHAR(MAX) 
);

CREATE TABLE Account (
    ID INT IDENTITY(1,1) PRIMARY KEY,  
    Name NVARCHAR(255) NOT NULL,  
    Username NVARCHAR(15) NOT NULL,  
    Password NVARCHAR(15) NOT NULL,  
    Status BIT DEFAULT 1  
);

CREATE TABLE Type (
    ID INT IDENTITY(1,1) PRIMARY KEY,  
    Name NVARCHAR(255) NOT NULL,  
    Description NTEXT,  
    IsActive BIT DEFAULT 1  
);

CREATE TABLE ProductDetail (
    ID INT IDENTITY(1,1) PRIMARY KEY,           
    Description NTEXT,                            
    ComboID INT,   
    TypeID INT,
    Color VARCHAR(50),                           
    Name VARCHAR(100) NOT NULL,                  
    Quantity INT DEFAULT 0,                      
    Type VARCHAR(50),                            
    Status bit default 1,     
    CreateDate DATETIME, 
    Size VARCHAR(50),
    Origin VARCHAR(100),
    FOREIGN KEY (TypeID) REFERENCES Type(ID),
);

CREATE TABLE ProductImage (
    ID INT IDENTITY(1,1) PRIMARY KEY,           
    ProductDetailID INT NOT NULL,               
    Name VARCHAR(100) NOT NULL,                 
    Status bit default 1, 
    Description NTEXT,                           
    Url VARCHAR(255) NOT NULL,                  
    FOREIGN KEY (ProductDetailID) REFERENCES ProductDetail(ID) 
);

CREATE TABLE Combo (
    ID INT IDENTITY(1,1) PRIMARY KEY,  
    MemberID INT,  
    ElementID INT, 
    ProductDetailID INT,  
    ComboName NVARCHAR(255) NOT NULL,  
    ComboPrice DECIMAL(10, 2) NOT NULL,  
    Discount DECIMAL(5, 2),  
    Status BIT DEFAULT 1,  
    CreatedBy NVARCHAR(255),  
    CreatedAt DATETIME,  
    UpdatedAt DATETIME,  
    FOREIGN KEY (MemberID) REFERENCES Member(ID),  
    FOREIGN KEY (ElementID) REFERENCES Element(ID),
    FOREIGN KEY (ProductDetailID) REFERENCES ProductDetail(ID)
);


CREATE TABLE SuitableObject (
    ID INT IDENTITY(1,1) PRIMARY KEY,  
    ElementID INT,
    TypeID INT,  
    Color NVARCHAR(50),  
    Size NVARCHAR(50),  
    Direction NVARCHAR(50),  
    Position NVARCHAR(50),  
    Shape NVARCHAR(50),  
    Volume NVARCHAR(50),  
    WaterQuality NVARCHAR(50),  
    WaterTemperature NVARCHAR(50),
    InformationDirection NVARCHAR(MAX),
    Flag NVARCHAR(MAX),
    FOREIGN KEY (ElementID) REFERENCES Element(ID),
    FOREIGN KEY (TypeID) REFERENCES Type(ID)
);

CREATE TABLE Package (
    ID INT IDENTITY(1,1) PRIMARY KEY,  
    Name NVARCHAR(255) NOT NULL,  
    Description NVARCHAR(MAX),  
    Price FLOAT NOT NULL, 
    FeatureType NVARCHAR(255),  
    StartDate DATETIME,  
    EndDate DATETIME,  
    Status BIT DEFAULT 1,  
    Discount FLOAT,  
    Highlight BIT DEFAULT 0,  
    CreatedBy NVARCHAR(255),  
    CreatedAt DATETIME,  
    UpdatedBy NVARCHAR(255),  
    UpdatedAt DATETIME 
);

CREATE TABLE Member_Package (
    ID INT IDENTITY(1,1) PRIMARY KEY,  
    MemberID INT,  
    PackageID INT,  
    ExpiredDate DATETIME,
    BoughtAt DATETIME,  
    Status bit default 1,
    FOREIGN KEY (MemberID) REFERENCES Member(ID),  
    FOREIGN KEY (PackageID) REFERENCES Package(ID)  
);
