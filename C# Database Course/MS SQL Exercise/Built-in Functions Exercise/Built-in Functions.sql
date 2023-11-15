USE SoftUni

--01.
SELECT 
	[FirstName],
	[LastName] 
FROM [Employees]
WHERE [FirstName] LIKE 'Sa%'
GO

--02.
SELECT 
	[FirstName],
	[LastName] 
FROM [Employees]
WHERE [LastName] LIKE '%ei%'
GO

--03.
SELECT 
	[FirstName]
FROM [Employees]
WHERE [DepartmentID] IN(3,10) AND (YEAR([HireDate]) BETWEEN 1995 AND 2005)
GO

--04.
SELECT 
	[FirstName], 
	[LastName]
FROM [Employees]
WHERE [JobTitle] NOT LIKE '%engineer%'
GO

--05.
SELECT 
	[Name] 
FROM [Towns]
WHERE LEN([Name]) IN (5,6)
ORDER BY [Name]
GO

--06.
SELECT 
	[TownID],
	[Name]
FROM [Towns]
WHERE [Name] LIKE '[MKBE]%'
ORDER BY [Name]
GO

--07.
SELECT 
	[TownID],
	[Name]
FROM [Towns]
WHERE [Name] NOT LIKE '[RBD]%'
ORDER BY [Name]
GO

--08.
CREATE VIEW V_EmployeesHiredAfter2000
AS
	SELECT 
		[FirstName],
		[LastName]
	FROM [Employees]
	WHERE DATEPART(YEAR,[HireDate]) > 2000
GO

--09.
SELECT 
	[FirstName],
	[LastName] 
FROM [Employees]
WHERE LEN([LastName]) = 5
GO

--10.
SELECT 
	[EmployeeID],
	[FirstName],
	[LastName],
	[Salary],
	DENSE_RANK () OVER(PARTITION BY [Salary] ORDER BY [EmployeeID]) AS [Rank]
FROM [Employees]
WHERE [Salary] BETWEEN 10000 AND 50000
ORDER BY [Salary] DESC
GO

--11.
SELECT 
	* 
FROM (SELECT 
		[EmployeeID],
		[FirstName],
		[LastName],
		[Salary],
		DENSE_RANK ()OVER(PARTITION BY [Salary] ORDER BY [EmployeeID]) AS [Rank]
	FROM [Employees]) AS [Subquery]
WHERE [Salary] BETWEEN 10000 AND 50000 
AND [Subquery].[Rank] = 2
ORDER BY [Salary] DESC
GO

--12.
USE Geography
GO

SELECT 
	[CountryName],
	[IsoCode]
FROM [Countries]
WHERE [CountryName] LIKE '%A%A%A%'
ORDER BY [IsoCode]
GO

--13.
SELECT 
	p.[PeakName],
	r.[RiverName],
	CONCAT(LOWER(p.PeakName), LOWER(RIGHT((r.RiverName),(LEN(r.RiverName)-1)))) AS [Mix]
FROM [Peaks] AS p, [Rivers] AS r
WHERE RIGHT(p.PeakName,1) = Left(r.RiverName,1)
ORDER BY [Mix]
GO

--14.
USE Diablo
GO

SELECT TOP(50)
	[Name],
	FORMAT([Start],'yyyy-MM-dd') AS [Start]
FROM [Games]
WHERE YEAR([Start]) BETWEEN 2011 AND 2012
ORDER BY [Start],[Name] 
GO

--15.
SELECT 
	[Username],
	SUBSTRING([Email], CHARINDEX('@',[Email])+1, LEN([Email])-1) AS [Email Provider]
FROM [Users]
ORDER BY [Email Provider], [Username]
GO

--16.
SELECT 
	[Username],
	[IpAddress] AS p
FROM [Users]
WHERE [IpAddress] LIKE '___.1%.%.___'
ORDER BY Username
GO

--17.
SELECT 
	[Name] AS [Game],	
	CASE
		WHEN DATEPART(HOUR, [Start]) BETWEEN 0 AND 11 THEN 'Morning'
		WHEN DATEPART(HOUR, [Start]) BETWEEN 12 AND 17 THEN 'Afternoon'
		WHEN DATEPART(HOUR, [Start]) BETWEEN 18 AND 23 THEN 'Evening'
	END AS [Part of the Day],
	CASE 
		WHEN [Duration] <= 3 THEN 'Extra Short'
		WHEN [Duration] BETWEEN 4 AND 6 THEN 'Short'
		WHEN [Duration] > 6 THEN 'Long'
		ELSE 'Extra Long'
	END AS [Duration]
FROM Games
ORDER BY [Name], [Duration], [Part of the Day]
GO

--18.
SELECT	
	[ProductName],
	[OrderDate],
	DATEADD(DAY,3,[OrderDate]) AS [Pay Due],
	DATEADD(MONTH,1, [OrderDate]) AS [Deliver Due]
FROM [Orders]
GO