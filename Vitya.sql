-- Создание бд
USE master;

CREATE DATABASE VideoRental;

GO
-- Создание таблиц
USE VideoRental;

GO

CREATE TABLE [Employees](
	Id INT PRIMARY KEY,
	FIO NVARCHAR(100) NOT NULL,
	Position NVARCHAR(50) NOT NULL,
	DateOfWorkStart DATE NOT NULL
)

CREATE TABLE [Clients](
	Id INT PRIMARY KEY,
	FIO NVARCHAR(100) NOT NULL,
	Number INT NOT NULL,
	Pasport NVARCHAR(9) NOT NULL
)

CREATE TABLE [Genres](
	Id INT PRIMARY KEY,
	[Name] NVARCHAR(20) NOT NULL,
	[Description] NVARCHAR(200) NOT NULL
)

CREATE TABLE [Discs](
	Id INT PRIMARY KEY,
	[Name] NVARCHAR(20) NOT NULL,
	DateOfCreation DATE NOT NULL,
	Creater NVARCHAR(20) NOT NULL,
	Country NVARCHAR(50) NOT NULL,
	MainActor NVARCHAR(100) NOT NULL,
	DateOfRecord DATE NOT NULL,
	Genre INT NOT NULL FOREIGN KEY REFERENCES [Genres] (Id),
	TypeOfDisc NVARCHAR(7) NOT NULL,
	Price DECIMAL NOT NULL
)

CREATE TABLE [RentalRecords](
	Id INT PRIMARY KEY,
	ClientId INT NOT NULL FOREIGN KEY REFERENCES [Clients] (Id),
	DateOfRent DATE NOT NULL,
	DateOfReturn DATE NOT NULL,
	PaymentCheck INT NOT NULL,
	ReturnCheck INT NOT NULL,
	DiscId INT NOT NULL FOREIGN KEY REFERENCES [Discs] (Id),
	EmployeeId INT NOT NULL FOREIGN KEY REFERENCES [Employees] (Id)
)

GO

--Создание процедур

CREATE PROCEDURE [dbo].[AddEmployee]
    @Id INT,
	@FIO NVARCHAR(100),
	@Position NVARCHAR(50),
	@DateOfWorkStart DATE
AS
INSERT INTO Employees (Id, FIO, Position, DateOfWorkStart) 
	VALUES(@Id, @FIO, @Position, @DateOfWorkStart)

GO

CREATE PROCEDURE [dbo].[UpdateEmployee]
    @Id INT,
	@FIO NVARCHAR(100),
	@Position NVARCHAR(50),
	@DateOfWorkStart DATE
AS
UPDATE Employees SET FIO = @FIO, Position = @Position, DateOfWorkStart = @DateOfWorkStart
	WHERE Id = @Id

GO 

CREATE PROCEDURE [dbo].[AddClient]
    @Id INT,
	@FIO NVARCHAR(100),
	@Number INT,
	@Pasport NVARCHAR(9)
AS
INSERT INTO [Clients] (Id, FIO, Number, Pasport) 
	VALUES(@Id, @FIO, @Number, @Pasport)

GO

CREATE PROCEDURE [dbo].[UpdateClient]
    @Id INT,
	@FIO NVARCHAR(100),
	@Number INT,
	@Pasport NVARCHAR(9)
AS
UPDATE Clients SET FIO = @FIO, Number = @Number, Pasport = @Pasport
	WHERE Id = @Id

GO

-- Создание View

CREATE VIEW RentalView AS
SELECT RentalRecords.Id, Clients.FIO AS Client, RentalRecords.DateOfRent, RentalRecords.DateOfReturn, RentalRecords.PaymentCheck, RentalRecords.ReturnCheck, Discs.[Name], Employees.FIO As Employee
	FROM RentalRecords
	JOIN Clients ON Clients.Id = RentalRecords.ClientId
	JOIN Discs ON Discs.Id = RentalRecords.DiscId
	JOIN Employees ON Employees.Id = RentalRecords.EmployeeId

GO

CREATE VIEW DiscView AS
SELECT Discs.Id, Discs.[Name], Discs.DateOfCreation, Discs.Creater, Discs.Country, Discs.MainActor, Discs.DateOfRecord, Genres.[Name] As Genre, Discs.TypeOfDisc, Discs.Price
	FROM Discs
	JOIN Genres ON Genres.Id = Discs.Genre

GO

DECLARE @step INT = 1;
DECLARE @Symbol CHAR(52) = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';

-- Заполнение таблицы клиентов
DECLARE @FIO NVARCHAR(100);
DECLARE @secondStep INT;
DECLARE @Position INT;
DECLARE @Length INT;
DECLARE @Pass NVARCHAR(9);

WHILE @step <= 500
	BEGIN
		SET @Length = 5 + RAND()*(100-5)
		SET @FIO = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @FIO = @FIO + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END

		SET @Length = 5 + RAND()*(9 - 5)
		SET @Pass = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @Pass = @Pass + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END

		INSERT INTO Clients (Id, FIO, Number, Pasport) VALUES (@step, @FIO, 1 + RAND() * 1000000, @Pass)
		SET @step = @step + 1;
	END;

-- Заполнение таблицы сотрудников
SET @step = 501;
DECLARE @Pos NVARCHAR(50);

WHILE @step <= 1000
	BEGIN
		SET @Length = 5 + RAND()*(100-5)
		SET @FIO = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @FIO = @FIO + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END

		SET @Length = 5 + RAND()*(50-5)
		SET @Pos = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @Pos = @Pos + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END

		INSERT INTO Employees (Id, FIO, Position, DateOfWorkStart) VALUES (@step, @FIO, @Pos, DATEADD(d, RAND() * 100, GETDATE()))
		SET @step = @step + 1;
	END;

-- Заполнение таблицы жанров
SET @step = 1001;
DECLARE @Name NVARCHAR(20);
DECLARE @Descr NVARCHAR(200);

WHILE @step <= 1500
	BEGIN
		SET @Length = 5 + RAND()*(20-5)
		SET @Name = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @Name = @Name + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END

		SET @Length = 5 + RAND()*(200-5)
		SET @Descr = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @Descr = @Descr + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END

		INSERT INTO Genres(Id, [Name], [Description]) VALUES (@step, @Name, @Descr);
		SET @step = @step + 1;
	END;

-- Заполнение таблицы дисков
SET @step = 1501;
DECLARE @Creator NVARCHAR(20);
DECLARE @Country NVARCHAR(50);
DECLARE @MainActor NVARCHAR(100);
DECLARE @Genre INT;
DECLARE @type NVARCHAR(7);
WHILE @step <= 21500
	BEGIN
		SET @Genre = 1001 + RAND() * 10000;
		WHILE @Genre > 1500
		BEGIN
			SET @Genre = 1001 + RAND() * 10000;
		END

		SET @Length = 5 + RAND()*(20-5)
		SET @Creator = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @Creator = @Creator + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END

		SET @Length = 5 + RAND()*(50-5)
		SET @Country = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @Country = @Country + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END

		SET @Length = 5 + RAND()*(100-5)
		SET @MainActor = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @MainActor = @MainActor + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END

		SET @Length = 5 + RAND()*(7-5)
		SET @type = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @type = @type + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END

		SET @Length = 5 + RAND()*(20-5)
		SET @Name = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @Name = @Name + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END

		INSERT INTO Discs(Id, [Name], DateOfCreation ,Creater, Country, MainActor, DateOfRecord, Genre, TypeOfDisc, Price) VALUES (@step, @Name, DATEADD(d, RAND() * 100, GETDATE()), @Creator, @Country, @MainActor, DATEADD(d, RAND() * 100, GETDATE()), @Genre, @type, 1 + RAND() * 1000);
		SET @step = @step + 1;
	END;

-- Заполнение таблицы источников
SET @step = 21501;
DECLARE @Client INT;
DECLARE @Empl INT;
DECLARE @Disc INT;
WHILE @step <= 41500
	BEGIN
		SET @Client = 1 + RAND() * 1000;
		SET @Empl = 501 + RAND() * 1000;
		SET @Disc = 1501 + RAND() * 10000;
		WHILE @Client > 500
		BEGIN
			SET @Client = 1 + RAND() * 1000;
		END

		WHILE @Empl > 1000
		BEGIN
			SET @Empl = 501 + RAND() * 1000;
		END

		WHILE @Disc > 21500
		BEGIN
			SET @Disc = 1501 + RAND() * 10000;
		END

		INSERT INTO RentalRecords (Id, ClientId, DateOfRent ,DateOfReturn, PaymentCheck, ReturnCheck, DiscId, EmployeeId) VALUES (@step, @Client, DATEADD(d, RAND() * 100, GETDATE()), DATEADD(d, RAND() * 100, GETDATE()), ROUND(RAND(), 1), ROUND(RAND(), 1), @Disc, @Empl);
		SET @step = @step + 1;
	END;