USE Gringotts
GO

SELECT * FROM [WizzardDeposits]
GO

--01.
SELECT 
COUNT(*)
FROM [WizzardDeposits]
GO

--02.
SELECT 
MAX([MagicWandSize]) AS [LongesMagicWand]
FROM [WizzardDeposits]
GO

--03.
SELECT
	[DepositGroup],
	MAX([MagicWandSize]) AS LongestMagicWand
FROM [WizzardDeposits]
GROUP BY [DepositGroup]
GO

--04.
SELECT TOP(2)
	[DepositGroup]
FROM [WizzardDeposits]
GROUP BY [DepositGroup]
ORDER BY AVG([MagicWandSize])
GO

--05.
SELECT 
	[DepositGroup],
	SUM([DepositAmount])
FROM [WizzardDeposits]
GROUP BY [DepositGroup]
GO

--06.
SELECT
	[DepositGroup],
	SUM([DepositAmount])
FROM [WizzardDeposits]
WHERE [MagicWandCreator] = 'Ollivander family'
GROUP BY [DepositGroup]
GO

--07.
SELECT
	[DepositGroup],
	SUM([DepositAmount])
FROM [WizzardDeposits]
WHERE [MagicWandCreator] = 'Ollivander family'
GROUP BY [DepositGroup]
HAVING SUM([DepositAmount]) < 150000
ORDER BY SUM([DepositAmount]) DESC
GO

--08.
SELECT 
	[DepositGroup],
	[MagicWandCreator],
	MIN([DepositCharge]) AS [MinDepositCharge]
FROM [WizzardDeposits]
GROUP BY [DepositGroup], [MagicWandCreator]
ORDER BY [MagicWandCreator],[DepositGroup]
GO

--09.
SELECT 
	[AgeGroup],
	COUNT(*)
FROM
	(SELECT 
		CASE
			WHEN [Age] >= 0 AND [Age] < 11 THEN '[0-10]'
			WHEN [Age] >= 11 AND [Age] < 21 THEN '[11-20]'
			WHEN [Age] >= 21 AND [Age] < 31 THEN '[21-30]'
			WHEN [Age] >= 31 AND [Age] < 41 THEN '[31-40]'
			WHEN [Age] >= 41 AND [Age] < 51 THEN '[41-50]'
			WHEN [Age] >= 51 AND [Age] < 61 THEN '[51-60]'
			ELSE '[61+]'
		END AS [AgeGroup]
	FROM [WizzardDeposits]) AS Core
GROUP BY [AgeGroup]
GO

--10.
SELECT
	LEFT([FirstName],1) AS [FirstLetter]
FROM [WizzardDeposits]
WHERE [DepositGroup] = 'Troll Chest'
GROUP BY LEFT([FirstName],1)
ORDER BY [FirstLetter]
GO

--11.
SELECT 
	[DepositGroup],
	[IsDepositExpired],
	AVG([DepositInterest])
FROM [WizzardDeposits]
WHERE [DepositStartDate] > '1985-01-01'
GROUP BY [DepositGroup], [IsDepositExpired]
ORDER BY [DepositGroup] DESC, [IsDepositExpired]
GO

--12.
Select 
	SUM([Difference]) AS [SumDifference]
FROM
(SELECT 
	[FirstName],
	[DepositAmount],
	LEAD([DepositAmount]) OVER(ORDER BY [Id]) AS [NexAmount],
	([DepositAmount] - LEAD([DepositAmount]) OVER(ORDER BY [Id])) AS [Difference]
FROM [WizzardDeposits]) AS Core
GO

--------------------------------------------------------------------------------------------------------------------------
USE SoftUni
GO

SELECT * FROM [Employees]
GO

--13.
SELECT 
	[DepartmentID],
	SUM([Salary])
FROM [Employees]
GROUP BY [DepartmentID]
ORDER BY [DepartmentID]
GO

--14.
SELECT
	[DepartmentID],
	MIN([Salary]) AS [MinimumSalary]
FROM [Employees]
WHERE [DepartmentID] IN (2,5,7)
GROUP BY [DepartmentID]
GO

--15.
SELECT * FROM [NewTableEmployees]

SELECT 
	*
INTO [NewTableEmployees]
FROM [Employees]
WHERE [Salary] > 30000

DELETE  
FROM [NewTableEmployees]
WHERE [ManagerID] = 42

UPDATE [NewTableEmployees]
SET [Salary] += 5000
WHERE [DepartmentID] = 1

SELECT 
	[DepartmentID],
	AVG([Salary])
FROM [NewTableEmployees]
GROUP BY [DepartmentID]
GO

--16.
SELECT 
	[DepartmentID],
	MAX([Salary]) AS [MaxSalary]
FROM [Employees]
GROUP BY [DepartmentID]
HAVING MAX([Salary]) NOT BETWEEN 30000 AND 70000
GO

--17.
SELECT 
	COUNT([Salary]) AS [Count]
FROM [Employees]
WHERE [ManagerID] IS NULL
GO
 
--18.
SELECT 
	[DepartmentID],
	[Salary]
FROM
	(SELECT 
		DISTINCT [DepartmentID],
		[Salary],
		DENSE_RANK() OVER(PARTITION BY [DepartmentID] ORDER BY [Salary] DESC) AS [Ranked]
	FROM [Employees]) AS Core
WHERE [Ranked] = 3
GO

--19.
SELECT TOP(10)
	[FirstName],
	[LastName],
	[DepartmentID]
FROM [Employees] AS e
	WHERE [Salary] > (SELECT 		
		AVG([Salary]) AS [AverageSalaryPerDepartment]
	FROM [Employees] as eOut
	WHERE eOut.DepartmentID = e.DepartmentID
	GROUP BY [DepartmentID])
ORDER BY [DepartmentID]
GO