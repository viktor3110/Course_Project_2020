use ComputerShop;
go

declare @ReferenceRecordsCount int = 500,
		@OperationalRecordsCount int = 20000


declare @Symbol char(52)= 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz',
		@Digit char(10) = '0123456789',
		@Position int,
		@i int,
		@NameLimit int,
		@RowCount int,
		@MinNumberSymbols int = 5,
		@MaxNumberSymbols int = 50


declare @ComponentTypesCount int = @ReferenceRecordsCount,
		@ManufacturersCount int = @ReferenceRecordsCount,
		@CountriesCount int = @ReferenceRecordsCount,
		@ComponentsCount int = @OperationalRecordsCount,
		@CustomersCount int = @ReferenceRecordsCount,
		@EmployeesCount int = @ReferenceRecordsCount,
		@ServicesCount int = @ReferenceRecordsCount,
		@OrdersCount int = @OperationalRecordsCount,
		@OrderComponentsCount int = @ReferenceRecordsCount,
		@OrderServicesCount int = @ReferenceRecordsCount

begin tran

-- Заполнение таблицы Countries
	declare @CountryName varchar(50)

	set @RowCount = 1
	while @RowCount <= @CountriesCount
	begin
		set @NameLimit=@MinNumberSymbols + rand() * (@MaxNumberSymbols-@MinNumberSymbols) -- имя от 5 до 50 символов
		set @i = 1
        set @CountryName = ''
		while @i <= @NameLimit
		begin
			set @Position = rand() * 52
			set @CountryName = @CountryName + substring(@Symbol, @Position, 1)
			set @i = @i + 1
		end
		exec InsertCountry @CountryName
		set @RowCount += 1
	end

-- Заполнение таблицы Manufacturers
	declare @ManufacturerName varchar(50)

	set @RowCount = 1
	while @RowCount <= @ManufacturersCount
	begin
		set @NameLimit = @MinNumberSymbols + rand() * (@MaxNumberSymbols-@MinNumberSymbols) -- имя от 5 до 50 символов
		set @i = 1
        set @ManufacturerName = ''
		while @i <= @NameLimit
		begin
			set @Position = rand() * 52
			set @ManufacturerName = @ManufacturerName + substring(@Symbol, @Position, 1)
			set @i = @i + 1
		end
		exec InsertManufacturer @ManufacturerName
		set @RowCount += 1
	end

-- Заполнение таблицы ComponentTypes
	declare @ComponentTypeName varchar(50),
			@ComponentTypeDescription varchar(200)

	set @RowCount = 1
	while @RowCount <= @ComponentTypesCount
	begin
		set @NameLimit = @MinNumberSymbols + rand() * (@MaxNumberSymbols-@MinNumberSymbols) -- имя от 5 до 50 символов
		set @i = 1
        set @ComponentTypeName = ''
		set @ComponentTypeDescription = ''
		while @i <= @NameLimit
		begin
			set @Position = rand() * 52
			set @ComponentTypeName = @ComponentTypeName + substring(@Symbol, @Position, 1)
			set @Position = rand() * 52
			set @ComponentTypeDescription = @ComponentTypeDescription + substring(@Symbol, @Position, 1)
			set @i = @i + 1
		end
		exec InsertComponentType @ComponentTypeName, @ComponentTypeDescription
		set @RowCount += 1
	end

-- Заполнение таблицы Components
	declare @ComponentTypeId int,
			@ComponentModel varchar(50),
			@ComponentManufacturerId int,
			@ComponentCountryId int,
			@ComponentReleaseDate date,
			@ComponentCharacteristics varchar(50),
			@ComponentWarrantyInMonths int,
			@ComponentDescription varchar(50),
			@ComponentPrice money

	set @RowCount = 1
	while @RowCount <= @ComponentsCount
	begin
		set @NameLimit = @MinNumberSymbols + rand() * (@MaxNumberSymbols-@MinNumberSymbols) -- имя от 5 до 50 символов
		set @i = 1
        set @ComponentTypeId = rand() * @ComponentTypesCount
		set @ComponentModel = ''
		set @ComponentManufacturerId = rand() * @ManufacturersCount
		set @ComponentCountryId = rand() * @CountriesCount
		set @ComponentReleaseDate = dateadd(day,-RAND()*15000,GETDATE())
		set @ComponentCharacteristics = ''
		set @ComponentWarrantyInMonths = 6 + rand() * 30 -- от 6 до 36
		set @ComponentDescription = ''
		set @ComponentPrice = 100 + rand() * 2900 -- от 100 до 3000
		while @i <= @NameLimit
		begin
			set @Position = rand() * 52
			set @ComponentModel = @ComponentModel + substring(@Symbol, @Position, 1)
			set @Position = rand() * 52
			set @ComponentCharacteristics = @ComponentCharacteristics + substring(@Symbol, @Position, 1)
			set @Position = rand() * 52
			set @ComponentDescription = @ComponentDescription + substring(@Symbol, @Position, 1)
			set @i = @i + 1
		end
		exec InsertComponent @ComponentTypeId, @ComponentModel, @ComponentManufacturerId, @ComponentCountryId,
							 @ComponentReleaseDate, @ComponentCharacteristics, @ComponentWarrantyInMonths, 
							 @ComponentDescription, @ComponentPrice
		set @RowCount += 1
	end

-- Заполнение таблицы Customers
	declare @CustomerFullName varchar(50),
			@CustomerAddress varchar(100),
			@CustomerPhoneNumber varchar(10),
			@CustomerDiscount int

	set @RowCount = 1	
	while @RowCount <= @CustomersCount
	begin
		set @NameLimit = @MinNumberSymbols + rand() * (@MaxNumberSymbols-@MinNumberSymbols) -- имя от 5 до 50 символов
		set @i = 1
        set @CustomerFullName = ''
		set @CustomerAddress = ''
		set @CustomerPhoneNumber = ''
		set @CustomerDiscount = rand() * 50
		while @i <= @NameLimit
		begin
			set @Position = rand() * 52
			set @CustomerFullName = @CustomerFullName + substring(@Symbol, @Position, 1)
			set @Position = rand() * 52
			set @CustomerAddress = @CustomerAddress + substring(@Symbol, @Position, 1)
			set @i = @i + 1
		end
		set @i = 1
		while @i <= 10
		begin
			set @Position = rand() * 10
			set @CustomerPhoneNumber = @CustomerPhoneNumber + substring(@Digit, @Position, 1)
			set @i = @i + 1
		end
		exec InsertCustomer @CustomerFullName, @CustomerAddress, @CustomerPhoneNumber, @CustomerDiscount
		set @RowCount += 1
	end

-- Заполнение таблицы Employees
	declare @EmployeeFullName varchar(50),
			@EmployeeWorkExperienceInMonth int

	set @RowCount = 1
	while @RowCount <= @EmployeesCount
	begin
		set @NameLimit = @MinNumberSymbols + rand() * (@MaxNumberSymbols-@MinNumberSymbols) -- имя от 5 до 50 символов
		set @i = 1
        set @EmployeeFullName = ''
		set @EmployeeWorkExperienceInMonth = rand() * 480
		while @i <= @NameLimit
		begin
			set @Position = rand() * 52
			set @EmployeeFullName = @EmployeeFullName + substring(@Symbol, @Position, 1)
			set @i = @i + 1
		end
		exec InsertEmployee @EmployeeFullName, @EmployeeWorkExperienceInMonth
		set @RowCount += 1
	end

-- Заполнение таблицы Services
	declare @ServiceName varchar(50),
			@ServiceDescription varchar(200),
			@ServicePrice money

	set @RowCount = 1
	while @RowCount <= @ServicesCount
	begin
		set @NameLimit = @MinNumberSymbols + rand() * (@MaxNumberSymbols-@MinNumberSymbols) -- имя от 5 до 50 символов
		set @i = 1
        set @ServiceName = ''
		set @ServiceDescription = ''
		set @ServicePrice = 300 + rand() * 2700 -- от 300 до 3000
		while @i <= @NameLimit
		begin
			set @Position = rand() * 52
			set @ServiceName = @ServiceName + substring(@Symbol, @Position, 1)
			set @Position = rand() * 52
			set @ServiceDescription = @ServiceDescription + substring(@Symbol, @Position, 1)
			set @i = @i + 1
		end
		exec InsertService @ServiceName, @ServiceDescription, @ServicePrice
		set @RowCount += 1
	end

-- Заполнение таблицы Orders
	declare @OrderStartDate date,
			@OrderExecutionDate date,
			@OrderCustomerId int,
			@OrderPrepayment money,
			@OrderPaid bit,
			@OrderFinished bit,
			@OrderExecutingEmployeeId int

	set @RowCount = 1
	while @RowCount <= @OrdersCount
	begin
		set @OrderStartDate = dateadd(day,-(15000 + RAND()*5000),GETDATE()) -- 15000 - 20000
		set @OrderExecutionDate = dateadd(day,-(5000 + RAND()*10000),GETDATE()) -- 5000 - 15000
		set @OrderCustomerId = rand() * @CustomersCount
		set @OrderPrepayment = 300 + rand() * 2700
		set @OrderPaid = cast(round(rand(), 0) as bit)
		set @OrderFinished = cast(round(rand(), 0) as bit)
		set @OrderExecutingEmployeeId = rand() * @EmployeesCount
		exec InsertOrder @OrderStartDate, @OrderExecutionDate, @OrderCustomerId, @OrderPrepayment, @OrderPaid, @OrderFinished,
		@OrderExecutingEmployeeId
		set @RowCount += 1
	end

-- Заполнение таблицы OrderComponents
	declare @OrderId int,
			@ComponentId int

	set @RowCount = 1
	while @RowCount <= @OrderComponentsCount
	begin
		set @OrderId  = rand() * @OrdersCount
		set @ComponentId = rand() * @ComponentsCount
		exec InsertOrderComponent @OrderId, @ComponentId
		set @RowCount += 1
	end

-- Заполнение таблицы OrderServices
	declare @ServiceId int

	set @RowCount = 1
	while @RowCount <= @OrderServicesCount
	begin
		set @OrderId  = rand() * @OrdersCount
		set @ServiceId = rand() * @ServicesCount
		exec InsertOrderService @OrderId, @ServiceId
		set @RowCount += 1
	end

commit tran

