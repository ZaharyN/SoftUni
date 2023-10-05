SELECT * FROM Employees
GO

SELECT *FROM [Addresses]
GO

SELECT *FROM [Departments]
GO

SELECT *FROM [Projects]
GO

SELECT * FROM [EmployeesProjects]
GO

--01.
SELECT TOP(5) 
	[EmployeeID],
	[JobTitle],
	e.[AddressID],
	a.[AddressText]
FROM [Employees] AS e
JOIN [Addresses] AS a ON e.AddressID = a.AddressID
ORDER BY [AddressID] 
GO

--02.
SELECT TOP (50)
	[FirstName],
	[LastName],
	t.[Name],
	a.[AddressText]
FROM [Employees] AS e
JOIN [Addresses] AS a ON e.AddressID = a.AddressID
JOIN [Towns] AS t ON t.TownID = a.TownID
ORDER BY [FirstName],[LastName]
GO

--03.
SELECT 
	[EmployeeID],
	[FirstName],
	[LastName],
	d.[Name]
FROM [Employees] AS e
JOIN [Departments] AS d ON e.DepartmentID = d.DepartmentID
WHERE d.Name = 'Sales'
ORDER BY [EmployeeID]
GO

--04.
SELECT TOP (5)
	[EmployeeID],
	[FirstName],
	[Salary],
	d.[Name] AS [DepartmentName]
FROM [Employees] AS e
JOIN [Departments] AS d ON e.DepartmentID = d.DepartmentID
WHERE [Salary] > 15000
ORDER BY d.[DepartmentID]
GO

--05.
SELECT TOP(3)
	e.[EmployeeID],
	e.[FirstName]
FROM [Employees] AS e
LEFT JOIN [EmployeesProjects] AS ep ON ep.EmployeeID = e.EmployeeID
WHERE [ProjectID] IS NULL
ORDER BY [EmployeeID]
GO

--06.
SELECT
	[FirstName],
	[LastName],
	[HireDate],
	d.[Name]
FROM [Employees] AS e
JOIN [Departments] AS d ON e.DepartmentID = d.DepartmentID
WHERE [HireDate] > '1999-01-01' AND
d.Name = 'Sales' OR d.Name = 'Finance'
ORDER BY [HireDate]
GO

--07.
SELECT TOP(5)
	e.[EmployeeID],
	[FirstName],
	p.[Name]
FROM [Employees] AS e
JOIN [EmployeesProjects] AS ep ON ep.EmployeeID = e.EmployeeID
JOIN [Projects] AS p ON p.ProjectID = ep.ProjectID
WHERE p.[StartDate] > '2002-08-13' AND p.[EndDate] IS NULL
ORDER BY [EmployeeID]
GO

--08.
SELECT
	e.[EmployeeID],
	[FirstName],
	CASE
		WHEN p.[StartDate] >= '2005-01-01' THEN NULL
		ELSE p.[Name]
	END AS [ProjectName]
FROM [Employees] AS e 
JOIN [EmployeesProjects] AS ep ON e.EmployeeID = ep.EmployeeID
JOIN [Projects] AS p ON ep.ProjectID = p.ProjectID
WHERE e.[EmployeeID] = 24
GO

--09.
SELECT
	e.[EmployeeID],
	e.[FirstName],
	e.[ManagerID],
	m.[FirstName] AS [ManagerName]
FROM [Employees] AS e
JOIN [Employees] AS m ON m.EmployeeID = e.ManagerID 
WHERE e.ManagerID IN (3,7)
ORDER BY e.[EmployeeID]
GO

--10.
SELECT TOP(50)
	e.[EmployeeID],
	CONCAT_WS(' ',e.[FirstName],e.[LastName]) AS [EmployeeName],
	CONCAT_WS(' ',m.[FirstName], m.[LastName]) AS [ManagerName],
	d.[Name] AS [DepartmentName]
FROM [Employees] AS e
JOIN [Employees] AS m ON m.EmployeeID = e.ManagerID
JOIN [Departments] AS d ON e.DepartmentID = d.DepartmentID 
ORDER BY [EmployeeID]
GO

--11.
SELECT TOP(1)
	AVG([Salary]) AS [MinAverageSalary]
FROM [Employees] AS e
JOIN [Departments] AS d ON e.DepartmentID = d.DepartmentID
GROUP BY d.Name
ORDER BY [MinAverageSalary]
GO

------------------------

SELECT * FROM [Countries]
GO
SELECT * FROM [Mountains]
GO
SELECT * FROM [MountainsCountries]
GO
SELECT * FROM [Peaks]
GO
SELECT * FROM [Rivers]
GO
SELECT * FROM [CountriesRivers]
GO
SELECT * FROM [CountriesRivers]
GO

--12.
SELECT 
	c.[CountryCode],
	m.[MountainRange],
	p.[PeakName],
	p.Elevation
FROM [Countries] AS c
JOIN [MountainsCountries] AS mc ON c.CountryCode = mc.CountryCode
JOIN [Mountains] AS m ON mc.MountainId = m.Id
JOIN [Peaks] AS p ON p.MountainId = m.Id
WHERE c.[CountryName] = 'Bulgaria' AND p.[Elevation] > 2835
ORDER BY p.Elevation DESC
GO

--13.
SELECT
	c.[CountryCode],
	COUNT([MountainRange]) AS [MountainRanges]
FROM [Countries] AS c
JOIN [MountainsCountries] as MC on C.CountryCode= mc.CountryCode
JOIN [Mountains] AS m ON m.Id = mc.MountainId
WHERE mc.[CountryCode] IN('BG','RU','US')
GROUP BY c.[CountryCode]
GO

-- 14.
SELECT TOP (5)
	[CountryName],
	r.[RiverName]
FROM [Countries] AS c
LEFT JOIN [CountriesRivers] AS cr ON c.CountryCode = cr.CountryCode 
LEFT JOIN [Rivers] AS r ON r.Id = cr.RiverId
WHERE c.ContinentCode = 'AF' 
ORDER BY CountryName
GO

--15.
SELECT
	[ContinentCode],
	[CurrencyCode],
	[CurrencyUsage]
FROM
	(SELECT 
		[ContinentCode],
		[CurrencyCode],
		[CurrencyUsage],
		DENSE_RANK() OVER(PARTITION BY [ContinentCode] ORDER BY [CurrencyUsage] desc) AS [Ranked]
	FROM
		(SELECT
			[ContinentCode],
			[CurrencyCode],
			COUNT([CurrencyCode]) AS [CurrencyUsage]
		FROM [Countries]
		GROUP BY [ContinentCode], [CurrencyCode]) AS CoreQuery
	WHERE [CurrencyUsage] > 1) AS SecondQuery
WHERE [Ranked] = 1
GO

--16.
SELECT 
	COUNT(c.CountryName) AS [Count]
FROM [Countries] AS c
LEFT JOIN [MountainsCountries] AS mc ON c.CountryCode = mc.CountryCode
WHERE mc.[MountainId] IS NULL
GO

--17.
SELECT TOP(5)
	[CountryName],
	[Elevation],
	[Length]
FROM
	(SELECT 
		[CountryName],
		[Subquery].[Elevation],
		[Subquery].[Length],
			RANK() OVER(PARTITION BY [Subquery].CountryName
			ORDER BY 
			Subquery.[Elevation] DESC,
			Subquery.[Length] DESC,
			[Subquery].CountryName) AS 
		[Rank]
	FROM
		(SELECT  
			CountryName,
			p.[Elevation],
			r.[Length]
		FROM [Countries] AS c
		JOIN [MountainsCountries] AS mc ON mc.CountryCode = c.CountryCode
		JOIN [Peaks] AS p ON p.MountainId = mc.MountainId
		JOIN [CountriesRivers] AS cr ON cr.CountryCode = c.CountryCode
		JOIN [Rivers] AS r ON r.Id = cr.RiverId)
		AS [Subquery]) 
	AS Query
WHERE [Rank] = 1
ORDER BY [Elevation] DESC, [Length] DESC, [CountryName]
GO



