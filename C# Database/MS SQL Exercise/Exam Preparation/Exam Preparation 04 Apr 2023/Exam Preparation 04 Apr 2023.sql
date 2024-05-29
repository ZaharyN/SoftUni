--01.CREATE
CREATE DATABASE Accounting
GO
USE Accounting
GO

CREATE TABLE Countries(
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(10) NOT NULL
	)

CREATE TABLE Addresses(
	Id INT PRIMARY KEY IDENTITY,
	StreetName NVARCHAR(20) NOT NULL,
	StreetNumber INT,
	PostCode INT NOT NULL,
	City VARCHAR(25) NOT NULL,
	CountryId INT FOREIGN KEY REFERENCES Countries(Id) NOT NULL
)

CREATE TABLE Vendors(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(25) NOT NULL,
	NumberVAT nvarchar(15) NOT NULL,
	AddressId INT FOREIGN KEY REFERENCES Addresses(Id) NOT NULL
	)

CREATE TABLE Clients (
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(25) NOT NULL,
	NumberVAT NVARCHAR(15) NOT NULL,
	AddressId INT FOREIGN KEY REFERENCES Addresses(Id) NOT NULL
)

CREATE TABLE Invoices(
	Id INT PRIMARY KEY IDENTITY,
	Number INT UNIQUE NOT NULL,
	IssueDate DATETIME2 NOT NULL,
	DueDate DATETIME2 NOT NULL,
	Amount DECIMAL(18,2) NOT NULL,
	Currency VARCHAR(5) NOT NULL,
	ClientId INT FOREIGN KEY REFERENCES Clients(Id) NOT NULL
)

CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(10) NOT NULL,
)

CREATE TABLE Products(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(35) NOT NULL,
	Price DECIMAL(18,2) NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
	VendorId INT FOREIGN KEY REFERENCES Vendors(Id) NOT NULL,
)

CREATE TABLE ProductsClients(
	ProductId INT FOREIGN KEY REFERENCES Products(Id),
	ClientId INT FOREIGN KEY REFERENCES Clients(Id),
	PRIMARY KEY (ProductId,ClientId) 
)
GO

--02.INSERT
INSERT INTO Products ([Name],Price,CategoryId,VendorId)
	VALUES
	('SCANIA Oil Filter XD01', 78.69, 1, 1),
	('MAN Air Filter XD01', 97.38, 1, 5),
	('DAF Light Bulb 05FG87', 55.00, 2, 13),
	('ADR Shoes 47-47.5', 49.85, 3, 5),
	('Anti-slip pads S', 5.87, 5, 7)

INSERT INTO Invoices (Number, IssueDate, DueDate, Amount, Currency, ClientId)
	VALUES
	(1219992181, '2023-03-01', '2023-04-30', 180.96, 'BGN', 3),
	(1729252340, '2022-11-06', '2023-01-04', 158.18, 'EUR', 13),
	(1950101013, '2023-02-17', '2023-04-18', 615.15, 'USD', 19)
GO

--03.UPDATE
SELECT * FROM Invoices

SELECT * FROM Clients

UPDATE Invoices
SET DueDate = '2023-04-01'
WHERE YEAR(IssueDate) = 2022 AND MONTH(IssueDate) = 11

-- ADDRESS ID = 3

UPDATE Clients
SET AddressId = 3
WHERE [Name] LIKE '%CO%'
GO

--04.DELETE
DELETE FROM ProductsClients
WHERE [ClientId] = 11

DELETE FROM Invoices
WHERE ClientId = 11

DELETE FROM Clients
WHERE NumberVAT LIKE 'IT%'

SELECT * FROM Clients
WHERE [Name] LIKE 'IT%'
GO

--05.Invoices by Amount and Date
SELECT 
	Number,
	Currency
FROM [Invoices]
ORDER BY Amount DESC, DueDate 
GO

--06.Products by Category
SELECT 
	p.Id,
	p.[Name],
	p.Price,
	c.[Name]
FROM Products AS p
JOIN Categories AS c ON c.Id = p.CategoryId
WHERE c.[Name] LIKE 'ADR' OR c.[Name] = 'Others'
ORDER BY p.Price DESC
GO

--07.Clients without Products
SELECT 
	c.Id,
	c.[Name],
	CONCAT(a.StreetName, ' ', a.StreetNumber, ', ', a.City,', ', a.PostCode,', ',cntr.[Name]) AS Address 
FROM Clients AS c
LEFT JOIN ProductsClients AS pc ON c.Id = pc.ClientId
JOIN Addresses AS a ON c.AddressId = a.Id
JOIN Countries AS cntr ON cntr.Id = a.CountryId
WHERE pc.ProductId IS NULL
GO

--08.First 7 Invoices
SELECT TOP(7)
	i.Number,
	Amount,
	c.[Name]
FROM Invoices AS i
JOIN Clients AS c ON i.ClientId = c.Id
WHERE i.IssueDate < '2023-01-01' AND Currency = 'EUR' OR
	i.Amount > 500.00 AND c.NumberVAT LIKE 'DE%'
ORDER BY i.Number, Amount DESC
GO

--09.Clients with VAT
SELECT * FROM Clients
SELECT * FROM Vendors

SELECT
	c.[Name] AS Client,
	MAX(p.Price) AS Price,
	c.NumberVAT AS [VAT Number]
FROM Clients AS c
JOIN ProductsClients AS pc ON pc.ClientId = c.Id
JOIN Products AS p ON p.Id = pc.ProductId
WHERE c.[Name] NOT LIKE '%KG'
GROUP BY c.[Name], c.NumberVAT
ORDER BY MAX(p.Price) DESC
GO

--10.Clients by Price
SELECT 
	c.[Name] AS Client,
	FLOOR(AVG(P.Price)) AS [Average Price]
FROM Clients AS c
JOIN ProductsClients AS pc ON pc.ClientId = c.Id
JOIN Products AS p ON p.Id = pc.ProductId
JOIN Vendors AS v ON v.Id = p.VendorId
WHERE v.NumberVAT LIKE '%FR%'
GROUP BY c.[Name]
ORDER BY AVG(P.Price), Client DESC
GO

--11.Product with Clients
CREATE OR ALTER FUNCTION udf_ProductWithClients (@name nvarchar(35))
RETURNS INT
AS
	BEGIN
	DECLARE @result INT = (SELECT
		COUNT(pc.ProductId) 
		FROM ProductsClients AS pc
		JOIN Products AS p ON p.Id = pc.ProductId
		WHERE pc.ProductId = (
			SELECT 
			Id
			FROM Products 
			WHERE Name = @name))
		RETURN @result
	END
GO

--12.Search for Vendors from a Specific Country
CREATE OR ALTER PROC usp_SearchByCountry(@country NVARCHAR(30))
AS
	BEGIN
		SELECT 
			v.[Name] AS Vendor,
			v.NumberVAT AS VAT,
			CONCAT_WS(' ', A.StreetName, A.StreetNumber) AS [Street Info],
			CONCAT_WS(' ', a.City, a.PostCode) AS [City Info]
		FROM Countries AS c
		JOIN Addresses AS a ON c.Id = a.CountryId
		JOIN Vendors AS v ON v.AddressId = a.Id
	WHERE c.Name = @country
	ORDER BY v.[Name], a.City
	END
GO

EXEC usp_SearchByCountry 'France'