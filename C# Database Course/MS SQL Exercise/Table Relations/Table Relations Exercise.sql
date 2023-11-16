--01.
CREATE TABLE [Passports](
	[PassportID] INT IDENTITY(101,1) PRIMARY KEY,
	[PassportNumber] VARCHAR(50) UNIQUE NOT NULL
	)

INSERT INTO [Passports]
([PassportNumber])
	VALUES
	('N34FG21B'),
	('K65LO4R7'),
	('ZE657QP2')


CREATE TABLE [Persons](
	[PersonID] INT PRIMARY KEY IDENTITY,
	[FirstName] VARCHAR(50) NOT NULL,
	[Salary] DECIMAL(7,2) NOT NULL,
	[PassportID] INT FOREIGN KEY REFERENCES [Passports](PassportId)
	)

INSERT INTO [Persons]
([FirstName],[Salary],[PassportID])
	VALUES
	('Roberto', 43300.00,102),
	('Tom', 56100.00,103),
	('Yana', 60200.00,101)
GO

--02.
CREATE TABLE [Manufacturers](
	[ManufacturerID] INT IDENTITY PRIMARY KEY,
	[Name] VARCHAR (50) NOT NULL,
	[EstablishedOn] DATE NOT NULL,
	)
INSERT INTO [Manufacturers]([Name],[EstablishedOn])
	VALUES
	('BMW', '1916-03-07'),
	('Tesla', '2003-01-01'),
	('Lada', '1966-05-01')

CREATE TABLE [Models](
	[ModelId] INT PRIMARY KEY IDENTITY(101,1),
	[Name] VARCHAR(50) NOT NULL,
	[ManufacturerID] INT FOREIGN KEY REFERENCES [Manufacturers]([ManufacturerID])
	)
INSERT INTO [Models]([Name],[ManufacturerID])
	VALUES
	('X1',1),
	('i6',1),
	('Model S',2),
	('Model X',2),
	('Model 3',2),
	('Nova',3)
GO

--03.
CREATE TABLE [Exams](
	[ExamID] INT PRIMARY KEY IDENTITY(101,1),
	[Name] VARCHAR(50) NOT NULL
	)
INSERT INTO [Exams](Name)
	VALUES
	('SpringMVC'),
	('Neo4j'),
	('Oracle 11g')

CREATE TABLE [Students](
	[StudentID] INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL
	)
INSERT INTO [Students]([Name])
	VALUES
	('Mila'),
	('Toni'),
	('Ron')

CREATE TABLE [StudentsExams](
	[StudentID] INT,
	[ExamID] INT,
	PRIMARY KEY ([StudentID],[ExamID]),
	FOREIGN KEY (StudentID) REFERENCES [Students](StudentID),
	FOREIGN KEY (ExamID) REFERENCES [Exams]([ExamID])
	)

INSERT INTO [StudentsExams]([StudentID],[ExamID])
	VALUES
	(1,101),
	(1,102),
	(2,101),
	(3,103),
	(2,102),
	(2,103)
GO

--04.
CREATE TABLE [Cities](
	[CityID] INT PRIMARY KEY,
	[Name] NVARCHAR (50) NOT NULL
	)

CREATE TABLE [Customers](
	[CustomerID] INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR (50) NOT NULL,
	[Birthday] DATE NOT NULL,
	[CityID] INT FOREIGN KEY REFERENCES [Cities](CityID)
	)

CREATE TABLE [Orders](
	[OrderID] INT PRIMARY KEY IDENTITY,
	[CustomerID] INT FOREIGN KEY REFERENCES [Customers](CustomerID)
	)

CREATE TABLE [ItemTypes](
	[ItemTypeID] INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL
	)

CREATE TABLE [Items](
	[ItemID] INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL,
	[ItemTypeID] INT FOREIGN KEY REFERENCES [ItemTypes](ItemTypeID)
	)

CREATE TABLE [OrderItems](
	[OrderID] INT,
	[ItemID] INT,
	PRIMARY KEY ([OrderID],[ItemID]),
	FOREIGN KEY ([OrderID]) REFERENCES [Orders](OrderID),
	FOREIGN KEY ([ItemID]) REFERENCES [Items](ItemID)
	)
GO

--05.
CREATE TABLE [Cities](
	[CityID] INT PRIMARY KEY,
	[Name] NVARCHAR (50) NOT NULL
	)

CREATE TABLE [Customers](
	[CustomerID] INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR (50) NOT NULL,
	[Birthday] DATE NOT NULL,
	[CityID] INT FOREIGN KEY REFERENCES [Cities](CityID)
	)

CREATE TABLE [Orders](
	[OrderID] INT PRIMARY KEY IDENTITY,
	[CustomerID] INT FOREIGN KEY REFERENCES [Customers](CustomerID)
	)

CREATE TABLE [ItemTypes](
	[ItemTypeID] INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL
	)

CREATE TABLE [Items](
	[ItemID] INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL,
	[ItemTypeID] INT FOREIGN KEY REFERENCES [ItemTypes](ItemTypeID)
	)

CREATE TABLE [OrderItems](
	[OrderID] INT,
	[ItemID] INT,
	PRIMARY KEY ([OrderID],[ItemID]),
	FOREIGN KEY ([OrderID]) REFERENCES [Orders](OrderID),
	FOREIGN KEY ([ItemID]) REFERENCES [Items](ItemID)
	)
GO

--06.
CREATE TABLE [Majors](
	[MajorID] INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL
	)

CREATE TABLE [Students](
	[StudentID] INT PRIMARY KEY IDENTITY,
	[StudentNumber] NVARCHAR(10) NOT NULL,
	[StudentName] NVARCHAR(50) NOT NULL,
	[MajorID] INT FOREIGN KEY REFERENCES [Majors](MajorID)
	)

CREATE TABLE [Payments](
	[PaymentID] INT PRIMARY KEY IDENTITY,
	[PaymentDate] DATE NOT NULL,
	[PaymentAmount] DECIMAL (7,2) NOT NULL,
	[StudentID] INT FOREIGN KEY REFERENCES [Students](StudentID)
	)

CREATE TABLE [Subjects](
	[SubjectID] INT PRIMARY KEY IDENTITY,
	[Subjectname] VARCHAR(50) NOT NULL
	)

CREATE TABLE [Agenda](
	[StudentID] INT,
	[SubjectID] INT,
	PRIMARY KEY ([StudentID],[SubjectID]),
	FOREIGN KEY ([StudentID]) REFERENCES [Students]([StudentID]),
	FOREIGN KEY ([SubjectID]) REFERENCES [Subjects]([SubjectID])
	)
GO

--09.
SELECT 
	m.MountainRange,
	[PeakName],
	[Elevation]
FROM [Peaks] AS p
JOIN [Mountains] AS m on m.Id = p.MountainId
WHERE m.Id = 17
ORDER BY [Elevation] DESC
GO