USE SoftUni
GO

--01.Employees with Salary Above 35000
CREATE PROC usp_GetEmployeesSalaryAbove35000
AS
BEGIN
	SELECT 
		[FirstName] AS [First Name],
		[LastName] AS [Last Name]
	FROM [Employees]
	WHERE [Salary] > 35000
END
GO

--02.Employees with Salary Above Number
CREATE PROC usp_GetEmployeesSalaryAboveNumber (@salary DECIMAL(18,4))
AS
SELECT
	[FirstName] AS [First Name],
	[LastName] AS [Last Name]
FROM [Employees] 
WHERE [Salary] >= @salary
GO

--03.Town Names Starting With
CREATE OR ALTER PROC usp_GetTownsStartingWith (@string NVARCHAR(20))
AS
SELECT
	[Name]
FROM [Towns]
WHERE [Name] LIKE  @string + '%'
GO

--04.Employees from Town
CREATE PROC usp_GetEmployeesFromTown (@townName NVARCHAR(50))
AS
SELECT 
	[FirstName] AS [First Name],
	[LastName] AS [Last Name]
FROM [Employees] AS e
JOIN [Addresses] AS d ON d.AddressID = e.AddressID
JOIN [Towns] AS t ON t.TownID = d.TownID
WHERE t.[Name] = @townName
GO

--05.Salary Level Function
CREATE FUNCTION ufn_GetSalaryLevel (@salary DECIMAL(18,4)) 
RETURNS VARCHAR(20)
AS
BEGIN
	DECLARE @salaryType VARCHAR(20)
	IF (@salary < 30000)
		SET @salaryType = 'Low'
	ELSE IF (@salary <= 50000)
		SET @salaryType = 'Average'
	ELSE
		SET @salaryType = 'High'
	RETURN @salaryType
END
GO

--06.Employees by Salary Level
CREATE PROCEDURE usp_EmployeesBySalaryLevel (@salaryLevel VARCHAR(20))
AS
BEGIN
	SELECT
		[FirstName] AS [First Name],
		[LastName] AS [Last Name]
	FROM [Employees]
	WHERE ((SELECT dbo.ufn_GetSalaryLevel([Salary])) = @salaryLevel)
END
GO

--07.Define Function
CREATE OR ALTER FUNCTION ufn_IsWordComprised (@setOfLetters VARCHAR(20), @word VARCHAR(20))
RETURNS BIT
AS
BEGIN
	DECLARE @i INT = 1
	WHILE @i <= LEN(@word)
		BEGIN
			DECLARE @char VARCHAR(5) = SUBSTRING(@word, @i, 1)  
			IF CHARINDEX(@char, @setOfLetters) = 0
				RETURN 0
			ELSE
				SET @i += 1
		END
	RETURN 1
END 
GO
