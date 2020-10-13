use ComputerShop;
go

create procedure InsertCountry
	@CountryName varchar(50)
as
	insert into Countries(CountryName)
	values(@CountryName)
go

create procedure InsertManufacturer
	@ManufacturerName varchar(50)
as
	insert into Manufacturers(ManufacturerName)
	values(@ManufacturerName)
go

create procedure InsertComponentType
	@ComponentTypeName varchar(50),
	@ComponentTypeDescription varchar(200)
as
	insert into ComponentTypes(ComponentTypeName,
	ComponentTypeDescription)
	values(@ComponentTypeName,
	@ComponentTypeDescription)
go

create procedure InsertComponent
	@ComponentTypeId int,
	@ComponentModel varchar(50),
	@ComponentManufacturerId int,
	@ComponentCountryId int,
	@ComponentReleaseDate date,
	@ComponentCharacteristics varchar(200),
	@ComponentWarrantyInMonths int,
	@ComponentDescription varchar(200),
	@ComponentPrice money
as
	begin
	if exists(select ComponentTypeId from ComponentTypes where ComponentTypeId = @ComponentTypeId) and
	   exists(select ManufacturerId from Manufacturers where ManufacturerId = @ComponentManufacturerId) and
	   exists(select CountryId from Countries where CountryId = @ComponentCountryId)
	insert into Components(ComponentTypeId,
	ComponentModel,
	ComponentManufacturerId,
	ComponentCountryId,
	ComponentReleaseDate,
	ComponentCharacteristics,
	ComponentWarrantyInMonths,
	ComponentDescription,
	ComponentPrice)
	values(@ComponentTypeId,
	@ComponentModel,
	@ComponentManufacturerId,
	@ComponentCountryId,
	@ComponentReleaseDate,
	@ComponentCharacteristics,
	@ComponentWarrantyInMonths,
	@ComponentDescription,
	@ComponentPrice)
	end
go

create procedure InsertCustomer
	@CustomerFullName varchar(50),
	@CustomerAddress varchar(100),
	@CustomerPhoneNumber varchar(10),
	@CustomerDiscount int
as
	insert into Customers(CustomerFullName,
	CustomerAddress,
	CustomerPhoneNumber,
	CustomerDiscount)
	values(@CustomerFullName,
	@CustomerAddress,
	@CustomerPhoneNumber,
	@CustomerDiscount)
go

create procedure InsertEmployee
	@EmployeeFullName varchar(50),
	@EmployeeWorkExperienceInMonth int
as
	insert into Employees(EmployeeFullName,
	EmployeeWorkExperienceInMonth)
	values(@EmployeeFullName,
	@EmployeeWorkExperienceInMonth)
go

create procedure InsertService
	@ServiceName varchar(50),
	@ServiceDescription varchar(200),
	@ServicePrice money
as
	insert into Services(ServiceName,
	ServiceDescription,
	ServicePrice)
	values(@ServiceName,
	@ServiceDescription,
	@ServicePrice)
go

create procedure InsertOrder
	@OrderStartDate date,
	@OrderExecutionDate date,
	@OrderCustomerId int,
	@OrderPrepayment money,
	@OrderPaid bit,
	@OrderFinished bit,
	@OrderExecutingEmployeeId int
as
	begin
	if exists(select CustomerId from Customers where CustomerId = @OrderCustomerId) and
	   exists(select EmployeeId from Employees where EmployeeId = @OrderExecutingEmployeeId)
	insert into Orders(OrderStartDate,
	OrderExecutionDate,
	OrderCustomerId,
	OrderPrepayment,
	OrderPaid,
	OrderFinished,
	OrderExecutingEmployeeId)
	values(@OrderStartDate,
	@OrderExecutionDate,
	@OrderCustomerId,
	@OrderPrepayment,
	@OrderPaid,
	@OrderFinished,
	@OrderExecutingEmployeeId)
	end
go

create procedure InsertOrderComponent
	@OrderId int,
	@ComponentId int
as
	begin
	if exists(select OrderId from Orders where OrderId = @OrderId) and
	   exists(select ComponentId from Components where ComponentId = @ComponentId)
	insert into OrderComponents(OrderId,
	ComponentId)
	values(@OrderId,
	@ComponentId)
	end
go

create procedure InsertOrderService
	@OrderId int,
	@ServiceId int
as
	begin
	if exists(select OrderId from Orders where OrderId = @OrderId) and
	   exists(select ServiceId from Services where ServiceId = @ServiceId)
	insert into OrderServices(OrderId,
	ServiceId)
	values(@OrderId,
	@ServiceId)
	end