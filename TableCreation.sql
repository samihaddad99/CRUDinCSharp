IF OBJECT_ID ('dbo.repair', 'U') IS NOT NULL  
   DROP TABLE Repair;  
GO
IF OBJECT_ID ('dbo.inventory', 'U') IS NOT NULL  
   DROP TABLE Inventory;  
GO  
IF OBJECT_ID ('dbo.vehicle', 'U') IS NOT NULL  
   DROP TABLE Vehicle;  
GO 

CREATE TABLE Vehicle (
	ID int PRIMARY KEY IDENTITY(1,1),
	make Varchar(100),
	model Varchar(100),
	"year" INT, 
	condition Varchar(100)
);

CREATE TABLE Inventory (
	ID int PRIMARY KEY IDENTITY(1,1),
	vehicleID int,
	numberOnHand INT, 
	price Decimal(20, 2),
	cost Decimal(20, 2),
	CONSTRAINT FK_VehicleID FOREIGN KEY (vehicleID)
	REFERENCES Vehicle(ID)
);

CREATE TABLE Repair (
	ID int PRIMARY KEY IDENTITY(1,1),
	inventoryID INT,
	whatToRepair Varchar(100),
	CONSTRAINT FK_InventoryID FOREIGN KEY (inventoryID)
    REFERENCES Inventory(ID)
);

