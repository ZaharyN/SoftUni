USE Minions
GO

--04.
INSERT INTO [Towns]([Id], [Name])
	VALUES 
(1, 'Sofia'),
(2, 'Plovdiv'),
(3, 'Varna')

INSERT INTO [Minions]([Id],[Name],[Age],[TownId])
	VALUES
	(1, 'Kevin', 22, 1),
	(2, 'Bob', 15, 3),
	(3, 'Steward',NULL ,2)
GO

--07.
CREATE TABLE [People](
	[Id] INT IDENTITY,
	[Name] NVARCHAR(200) NOT NULL,
	[Image] VARBINARY,
	CHECK ([Image] <= 2000000),
	[Height] DECIMAL(3,2),
	[Weight] DECIMAL(5,2),
	[Gender] CHAR(1),
	CHECK ([Gender] = 'm' OR [Gender] = 'f'),
	[Birthdate] DATE NOT NULL,
	[Biography] NVARCHAR(MAX),
	PRIMARY KEY (Id)
)


INSERT INTO [People]([Name],[Height],[Weight],[Gender],[Birthdate],[Biography])
	VALUES
	('Zahary', 1.82, 72, 'm', '1998-11-27', 'Hello, I am SoftUni student!'),
	('George', 1.75, 78, 'm', '2001-05-15', 'I love cats'),
	('Zdravka', 1.63, 62, 'f', '1998-11-30', 'I love dogs'),
	('Stinka', 1.72, 68, 'f', '1999-03-25', 'I love to code'),
	('Blagovest', 1.85, 87, 'm', '2001-08-11', 'I love do write in SQL')
GO

--08.
CREATE TABLE [Users](
	[Id] BIGINT PRIMARY KEY IDENTITY,
	[Username] VARCHAR(30) UNIQUE NOT NULL,
	[Password] VARCHAR(26) NOT NULL,
	[ProfilePicture] VARBINARY,
	CHECK ([ProfilePicture] <= 900000),
	[LastLoginTime] DATETIME2,
	[IsDeleted] VARCHAR(5),
	CHECK ([IsDeleted] = 'true' OR [IsDeleted] = 'false')
)  

INSERT INTO [Users]([Username],[Password],[LastLoginTime],[IsDeleted])
VALUES
('Zahary','1234567z','2023-09-18','true'),
('Sasho','74561232s', '2022-12-23', 'false'),
('Zdravko','568454fedfs', '2020-09-09', 'true'),
('Jivko','742vc561232s', '2022-02-23', 'false'),
('Niki', '568454fedfs', '2020-09-09', 'true')
GO

--13.
USE Movies
GO

CREATE TABLE [Directors](
	[Id] INT PRIMARY KEY,
	[DirectorName] VARCHAR(50) NOT NULL,
	[Notes] VARCHAR(MAX)
)
INSERT INTO [Directors]([Id],[DirectorName],[Notes])
	VALUES
	(1, 'Christopher Nolan', 'Fantastic movie!'),
	(2, 'Steven Spielberg', 'Stunning effects!'),
	(3, 'Quentin Tarantino', 'Brutal!'),
	(4, 'James Cameron', 'Long awaited!'),
	(5, 'Tim Burton', NULL)

CREATE TABLE  [Genres](
	[Id] INT PRIMARY KEY,
	[GenreName] VARCHAR(20) NOT NULL,
	[Notes] VARCHAR(MAX)
)
INSERT INTO [Genres]([Id],[GenreName],[Notes])
	VALUES
	(1, 'Action', NULL),
	(2, 'Thriller', NULL),
	(3, 'Horror', 'Not for children!'),
	(4, 'Sci-fi', NULL),
	(5, 'Drama', 'The best!')

CREATE TABLE [Categories](
	[Id] INT PRIMARY KEY,
	[CategoryName] VARCHAR(20) NOT NULL,
	[Notes] VARCHAR(MAX)
)
INSERT INTO [Categories]([Id],[CategoryName],[Notes])
	VALUES
	(1, 'War', '12+'),
	(2, 'Mystery', NULL),
	(3, 'Western', NULL),
	(4, 'Adventure', NULL),
	(5, 'Family', 'Great for children!')

CREATE TABLE [Movies](
	[Id] INT PRIMARY KEY,
	[Title] NVARCHAR(MAX) NOT NULL,
	[DirectorId] INT FOREIGN KEY REFERENCES [Directors](Id) NOT NULL,
	[CopyrightYear] DATE NOT NULL,
	[Length] DECIMAL(3) NOT NULL,
	[GenreId] INT FOREIGN KEY REFERENCES [Genres](Id) NOT NULL,
	[CategoryId] INT FOREIGN KEY REFERENCES [Categories](Id) NOT NULL,
	[Rating] DECIMAL (1),
	[Notes] NVARCHAR (MAX)
)
INSERT INTO [Movies]([Id],[Title],[DirectorId],[CopyrightYear],[Length],[GenreId],[CategoryId]
,[Rating],[Notes])
	VALUES
	(1, 'Inception', 1, '2010-11-10', 180, 2, 2, 5, 'One of the best movies ever ecreated!'),
	(2, 'BLALA', 2, '2020-10-22', 125,3,4,4, 'Average'),
	(3, 'Dwdawd', 3, '1998-05-12', 93, 5, 2, 3, NULL),
	(4, 'DWADAW', 2, '2001-10-17', 112, 3, 1, 3, null),
	(5, 'fefg', 2, '2007-01-16', 125, 5, 5, 5, 'Excellent movie!')

GO

--14.
USE CarRental
GO

CREATE TABLE [Categories](
	[Id] INT PRIMARY KEY IDENTITY,
	[CategoryName] VARCHAR(30) NOT NULL,
	[DailyRate] DECIMAL(7,2),
	[WeeklyRate] DECIMAL(7,2),
	[MonthlyRate] DECIMAL(7,2),
	[WeekendRate] DECIMAL(7,2) 
	)
INSERT INTO [Categories]
	([CategoryName],[DailyRate],[WeeklyRate],[MonthlyRate],[WeekendRate])
	VALUES
	('Sedan', 49.99, 300.00, 13000.00, 200.00),
	('Coupe', 39.99, 250.00, 12000.00, 180.00),
	('Sports', 69.99, 400.00, 15000.00, 300.00)

CREATE TABLE [Cars](
	[Id] INT PRIMARY KEY IDENTITY,
	[PlateNumber] NVARCHAR(20) NOT NULL,
	[Manufacturer] VARCHAR(50) NOT NULL,
	[Model] VARCHAR(50) NOT NULL,
	[CarYear] DATE NOT NULL,
	[CategoryId] INT FOREIGN KEY REFERENCES [Categories](Id),
	[Doors] TINYINT NOT NULL,
	[Picture] VARBINARY,
	[Condition] VARCHAR(20),
	[Available] BIT NOT NULL
	)
INSERT INTO [Cars]
([PlateNumber],[Manufacturer],[Model],[CarYear],[CategoryId],[Doors],[Picture],[Condition],[Available])
	VALUES
	('ัย 4554 ะฬ', 'Volvo', 'Long', '1999-10-13', 1, 4, NULL, 'Excellent', 1),
	('ัย 9554 ะฬ', 'Folkswagen', 'Golf', '1997-10-13', 3, 4, NULL, 'Good', 0),
	('ัย 2554 ะฬ', 'Opel', 'Corsa', '2010-10-13', 2, 4, NULL, 'Poor', 1)

CREATE TABLE [Employees](
	[Id] INT PRIMARY KEY IDENTITY,
	[FirstName] NVARCHAR(20) NOT NULL,
	[LastName] NVARCHAR(20) NOT NULL,
	[Title] VARCHAR(20) NOT NULL,
	[Notes] VARCHAR(MAX)
	)
INSERT INTO [Employees]([FirstName],[LastName],[Title],[Notes])
	VALUES
	('Zahary', 'Nyagolob', 'Owner', 'Best owner ever!'),
	('Sasho', 'hfhob', 'Manager', 'Best!'),
	('ddad', 'hfhfh', 'Salesman', 'Best ever!')

CREATE TABLE [Customers](
	[Id] INT PRIMARY KEY IDENTITY,
	[DriverLicenceNumber] INT NOT NULL,
	[FullName] NVARCHAR (50) NOT NULL,
	[Address] NVARCHAR(MAX) NOT NULL,
	[City] NVARCHAR(50),
	[ZIPCode] INT,
	[Notes] VARCHAR(MAX)
	)
INSERT INTO [Customers]([DriverLicenceNumber],[FullName],[Address],[City],[ZIPCode],[Notes])
	VALUES
	('14856326', 'Blagoy Vasilev', 'dawdawd', 'Sofia', 1000, 'Best customer!'),
	('23568546', 'Blagoy Georgiev', 'dwdadwrfa', 'Plovdiv', 2000, 'Good customer!'),
	('89564878', 'Blagoy Stoyanov', 'wfrgrgs', 'Pleven', 5800, 'Great customer!')

CREATE TABLE [RentalOrders](
	[Id] INT PRIMARY KEY IDENTITY,
	[EmployeeId] INT FOREIGN KEY REFERENCES [Employees](Id),
	[CustomerId] INT FOREIGN KEY REFERENCES [Customers](Id),
	[CarId] INT FOREIGN KEY REFERENCES [Cars](Id),
	[TankLevel] TINYINT NOT NULL,
	[KilometrageStart] INT NOT NULL,
	[KilometrageEnd] INT NOT NULL,
	[TotalKilometrage] INT NOT NULL,
	[StartDate] DATE NOT NULL,
	[EndDate] DATE NOT NULL,
	[TotalDays] TINYINT NOT NULL,
	[RateApplied] DECIMAL (7,2),
	[TaxRate] DECIMAL (5,2),
	[OrderStatus] VARCHAR (50),
	[Notes] VARCHAR(MAX)
	) 

	ALTER TABLE [RentalOrders]
	ALTER COLUMN [OrderStatus] VARCHAR (50)

INSERT INTO [RentalOrders]
([EmployeeId],[CustomerId],[CarId],[TankLevel],[KilometrageStart],
[KilometrageEnd],[TotalKilometrage],[StartDate],[EndDate],[TotalDays],[RateApplied],[TaxRate],
[OrderStatus],[Notes])
	VALUES
	(2, 1, 1, 100, 2500, 3000, 500,'2023-08-15','2023-08-22', 7, 1, 22.2, 'Pending', NULL),
	(3, 2, 2, 100, 10000, 13000, 2000,'2023-08-15','2023-08-22', 7, 1, 17.2, 'Ongoing', NULL),
	(1, 3, 3, 100, 1500, 3000, 1500,'2023-08-15','2023-08-22', 7, 1, 28.2, 'Denied', NULL)

GO

--15.
USE Hotel
GO

CREATE TABLE [Employees](
	[Id] INT IDENTITY PRIMARY KEY,
	[FirstName] VARCHAR(20) NOT NULL,
	[LastName] VARCHAR(20) NOT NULL,
	[Title] VARCHAR(50) NOT NULL,
	[Notes] VARCHAR(MAX) 
)
INSERT INTO [Employees]([FirstName],[LastName],[Title],[Notes])
	VALUES
	('Zahary','Nyagolov','Owner','The best!'),
	('Blagoy','Vasilev','Salesman','The worst!'),
	('Blagovest','Nikolov','Manager','Not the best!')

CREATE TABLE [Customers](
	[AccountNumber] INT IDENTITY PRIMARY KEY,
	[FirstName] VARCHAR(20) NOT NULL,
	[LastName] VARCHAR(20) NOT NULL,
	[PhoneNumber] VARCHAR(15) NOT NULL,
	[EmergencyName] NVARCHAR(40),
	[EmergencyNumber] VARCHAR(15),
	[Notes] VARCHAR(MAX)
	)
INSERT INTO [Customers]
([FirstName],[LastName],[PhoneNumber],[EmergencyName],[EmergencyNumber],[Notes])
	VALUES
	('Nikolay','Hristov', '0896554488', 'Nikoleta', '0998989898', NULL),
	('Nikolay','Nikolov', '0896214487', 'Suzana', '0987986598', NULL),
	('Nikolay','Svetlinov', '0896551128', 'Violeta', '0995689898', NULL)

CREATE TABLE [RoomStatus](
	[RoomStatus] VARCHAR PRIMARY KEY NOT NULL,
	[Notes] VARCHAR(MAX)
	)
INSERT INTO [RoomStatus]
([RoomStatus],[Notes])
	VALUES
	(1, 'Most beautiful room!'),
	(2, 'OK'),
	(3, 'Good for its price!')

CREATE TABLE [RoomTypes](
	[RoomType] VARCHAR PRIMARY KEY NOT NULL,
	[Notes] VARCHAR (MAX)
)
INSERT INTO [RoomTypes]([RoomType],[Notes])
	VALUES
	(1,NULL),
	(2, NULL),
	(3, NULL)

CREATE TABLE [BedTypes](
	[BedType] VARCHAR PRIMARY KEY NOT NULL,
	[Notes] VARCHAR (MAX)
)
	INSERT INTO [BedTypes]([BedType],[Notes])
	VALUES
	(1, 'Comfy'),
	(2, 'Smelled good!'),
	(3, 'Not recommended')

CREATE TABLE [Rooms](
	[RoomNumber] INT IDENTITY PRIMARY KEY,
	[RoomType] VARCHAR FOREIGN KEY REFERENCES [RoomTypes]([RoomType]) NOT NULL,
	[BedType] VARCHAR FOREIGN KEY REFERENCES [BedTypes]([BedType]) NOT NULL,
	[Rate] TINYINT,
	--[RoomStatus] VARCHAR FOREIGN KEY REFERENCES [RoomStatus]([RoomStatus]),
	[Notes] VARCHAR(MAX)
	)
INSERT INTO [Rooms]
	([RoomType],[BedType],[Rate],[Notes])
	VALUES
	(1,2,3,null),
	(1,2,3,null),
	(1,2,3,null)

CREATE TABLE [Payments](
	[Id] INT IDENTITY PRIMARY KEY,
	[EmployeeId] INT FOREIGN KEY REFERENCES [Employees]([Id]) NOT NULL,
	[PaymentDate] DATE NOT NULL,
	[AccountNumber] INT FOREIGN KEY REFERENCES [Customers]([AccountNumber]),
	[FirstDateOccupied] DATE,
	[LastDateOccupied] DATE,
	[TotalDays] TINYINT,
	[AmountCharged] DECIMAL (7,2),
	[TaxRate] DECIMAL (5,2),
	[TaxAmount] DECIMAL (5,2),
	[PaymentTotal] DECIMAL (7,2),
	[Notes] VARCHAR(MAX)
	)
INSERT INTO [Payments]
([EmployeeId],[PaymentDate],[AccountNumber],[FirstDateOccupied],[LastDateOccupied],[TotalDays],
[AmountCharged],[TaxRate],[TaxAmount],[PaymentTotal],[Notes])
	VALUES
	(1, '2010-10-10', 1, '2010-11-20', '2010-11-30', 10, 1500, 200, 200, 1500, 'Perfect place!'),
	(2, '2012-07-10', 2, '2012-08-20', '2012-09-10', 10, 3500, 200, 200, 1500, 'Beautiful place!'),
	(3, '2010-10-10', 3, '2020-11-20', '2020-12-31', 10, 1500, 200, 200, 1500, 'Perfect place!')

CREATE TABLE [Occupancies](
	[Id] INT IDENTITY PRIMARY KEY,
	[EmployeeId] INT FOREIGN KEY REFERENCES [Employees]([Id]) NOT NULL,
	[DateOccupied] DATE,
	[AccountNumber] INT FOREIGN KEY REFERENCES [Customers]([AccountNumber]),
	[RoomNumber] INT FOREIGN KEY REFERENCES [Rooms]([Roomnumber]),
	[RateApplied] TINYINT,
	[PhoneCharge] VARCHAR(15),
	[Notes] VARCHAR(MAX)
	)
INSERT INTO [Occupancies]
	([EmployeeId],[DateOccupied],[AccountNumber],[RoomNumber],[RateApplied],[PhoneCharge],[Notes])
	VALUES
	(1,'2020-10-10', 2, 1, 10, '0895442278', 'Good place!'),
	(2,'2020-10-10', 1, 1, 10, '0895465278', 'Best place!'),
	(3,'2020-10-10', 3, 1, 10, '0885442278', 'Better place!')

GO

--19.
SELECT * FROM [Towns]
SELECT * FROM [Departments]
SELECT * FROM [Employees]
GO

--20.
SELECT * FROM [Towns]
ORDER BY [Name] ASC

SELECT * FROM [Departments]
ORDER BY [Name] ASC

SELECT * FROM [Employees]
ORDER BY [Salary] DESC
GO

--21.
SELECT [Name]
FROM [Towns]
ORDER BY [Name] ASC

SELECT [Name] FROM [Departments] ORDER BY [Name] ASC

SELECT [FirstName],[LastName],[JobTitle],[Salary] FROM [Employees]
ORDER BY [Salary] DESC
GO

--22.
UPDATE [Employees]
SET [Salary] = [Salary] * 1.1

SELECT [Salary] 
FROM [Employees]
GO

--23.
UPDATE [Payments]
SET [TaxRate] = TaxRate - TaxRate * 0.03

SELECT [TaxRate] 
FROM [Payments]
GO

--24.
DELETE [Occupancies]
GO

