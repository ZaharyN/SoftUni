CREATE DATABASE NationalTouristSitesOfBulgaria
GO

USE NationalTouristSitesOfBulgaria
GO

--01.DDL
CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Locations(
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL,
	Municipality VARCHAR(50),
	Province VARCHAR(50)
)

CREATE TABLE Sites(
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(100) NOT NULL,
	LocationId INT FOREIGN KEY REFERENCES Locations(Id) NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
	Establishment VARCHAR(15) 
)

CREATE TABLE Tourists(
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50)	NOT NULL,
	Age INT NOT NULL CHECK(Age >= 0 AND Age <=120),
	PhoneNumber VARCHAR(20) NOT NULL,
	Nationality VARCHAR(30) NOT NULL,
	Reward VARCHAR(20)
)

CREATE TABLE SitesTourists(
	TouristId INT FOREIGN KEY REFERENCES Tourists(Id) NOT NULL,
	SiteId INT FOREIGN KEY REFERENCES Sites(Id) NOT NULL,
	PRIMARY KEY(TouristId,SiteId)
)

CREATE TABLE BonusPrizes(
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL
)

CREATE TABLE TouristsBonusPrizes(
	TouristId INT FOREIGN KEY REFERENCES Tourists(Id) NOT NULL,
	BonusPrizeId INT FOREIGN KEY REFERENCES BonusPrizes(Id) NOT NULL,
	PRIMARY KEY (TouristId,BonusPrizeId)
)
GO

--02.Insert
INSERT INTO Tourists([Name],Age,PhoneNumber,Nationality,Reward)
	VALUES
	('Borislava Kazakova',52, '+359896354244', 'Bulgaria', NULL),
	('Peter Bosh', 48, '+447911844141', 'UK', NULL),
	('Martin Smith', 29, '+353863818592', 'Ireland', 'Bronze badge'),
	('Svilen Dobrev', 49, '+359986584786', 'Bulgaria', 'Silver badge'),
	('Kremena Popova', 38, '+359893298604', 'Bulgaria', NULL)

INSERT INTO Sites([Name],LocationId, CategoryId, Establishment)
	VALUES
	('Ustra fortress', 90, 7, 'X'),
	('Karlanovo Pyramids', 65, 7, NULL),
	('The Tomb of Tsar Sevt', 63, 8, 'V BC'),
	('Sinite Kamani Natural Park', 17, 1, NULL),
	('St. Petka of Bulgaria – Rupite', 92, 6, '1994')
GO

--03.Update
UPDATE Sites
SET Establishment = '(not defined)'
WHERE Establishment IS NULL
GO

--04.Delete
DELETE FROM TouristsBonusPrizes
WHERE BonusPrizeId = 5
  
DELETE FROM BonusPrizes
WHERE [Name] = 'Sleeping bag'
GO

--05.Tourists
SELECT
	[Name],
	Age,
	PhoneNumber,
	Nationality
FROM Tourists
ORDER BY Nationality, Age DESC, [Name]
GO

--06.Sites with Their Location and Category
SELECT 
	s.[Name] AS [Site],
	l.[Name] AS [Location],
	Establishment,
	c.[Name] AS Catergory
FROM Sites AS s
JOIN Locations AS l ON s.LocationId = l.Id
JOIN Categories AS c ON c.Id = s.CategoryId
ORDER BY Catergory DESC,[Location], [Site]
GO

--07.Count of Sites in Sofia Province
SELECT
	Province,
	Municipality,
	l.[Name] AS [Location],
	COUNT(s.[Name]) AS CountOfSites
FROM Locations AS l
JOIN Sites AS s ON l.Id = s.LocationId
WHERE Province = 'Sofia'
GROUP BY Province, Municipality, l.Name
ORDER BY CountOfSites DESC, [Location]
GO

--08.Tourist Sites established BC
SELECT
	s.[Name] AS [Site],
	l.[Name] AS [Location],
	Municipality,
	Province,
	Establishment
FROM Sites AS s
JOIN Locations AS l ON s.LocationId = l.Id
WHERE l.[Name] NOT LIKE '[BMD]%' AND
	Establishment LIKE '%BC'
ORDER BY s.[Name]
GO

--09.Tourists with their Bonus Prizes
SELECT
	t.[Name] AS [Name],
	Age,
	PhoneNumber,
	Nationality,
	CASE
		WHEN tbp.BonusPrizeId IS NULL THEN '(no bonus prize)'
		ELSE bp.[Name]
		END AS Reward
FROM Tourists AS t
LEFT JOIN TouristsBonusPrizes AS tbp ON t.Id = tbp.TouristId
LEFT JOIN BonusPrizes AS bp ON tbp.BonusPrizeId = bp.Id
ORDER BY t.[Name]
GO

--10.Tourists visiting History & Archaeology sites
SELECT 
	RIGHT(t.[Name], LEN(t.[Name]) - CHARINDEX(' ', t.[Name])) AS LastName,
	Nationality,
	Age,
	PhoneNumber
FROM Tourists AS t 
JOIN SitesTourists AS st ON t.Id = st.TouristId
JOIN Sites AS s ON s.Id = st.SiteId
JOIN Categories AS c ON c.Id = s.CategoryId
WHERE c.[Name] = 'History and archaeology'
GROUP BY RIGHT(t.[Name], LEN(t.[Name]) - CHARINDEX(' ', t.[Name])), Nationality, Age, PhoneNumber
ORDER BY LastName--RIGHT(t.[Name], LEN(t.[Name]) - CHARINDEX(' ', t.[Name]))
GO

--11.Tourists Count on a Tourist Site
CREATE FUNCTION udf_GetTouristsCountOnATouristSite (@Site VARCHAR(100))
RETURNS INT
AS
	BEGIN
	DECLARE @result INT = (SELECT
		COUNT(t.Id)
		FROM SitesTourists AS st
		JOIN Tourists AS t ON t.Id = st.TouristId
		JOIN Sites AS s ON s.Id = st.SiteId
		WHERE s.[Name] = @Site)
	RETURN @result
	END
GO

--12.Annual Reward Lottery
CREATE OR ALTER PROC usp_AnnualRewardLottery(@TouristName VARCHAR(50))
AS
	BEGIN
	DECLARE @rewardType INT = (SELECT 
		COUNT(st.SiteId)
		FROM Tourists AS t 
		LEFT JOIN SitesTourists AS st ON t.Id = st.TouristId
		WHERE t.[Name] = @TouristName
		GROUP BY t.[Name])

		IF @rewardType >= 100
			UPDATE Tourists
			SET Reward = 'Gold badge'
		ELSE IF @rewardType >= 50
			UPDATE Tourists
			SET Reward = 'Silver badge'
		ELSE IF @rewardType >= 25
			UPDATE Tourists
			SET Reward = 'Bronze badge'

		SELECT 
			[Name],
			Reward
		FROM Tourists
		WHERE [Name] = @TouristName
	END

EXEC usp_AnnualRewardLottery 'Teodor Petrov'
EXEC usp_AnnualRewardLottery 'Zac Walsh'

GO