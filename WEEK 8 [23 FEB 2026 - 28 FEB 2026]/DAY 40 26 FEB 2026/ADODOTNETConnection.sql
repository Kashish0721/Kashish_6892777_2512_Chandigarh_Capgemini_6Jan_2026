CREATE DATABASE EmployeeDB;
use EmployeeDB;

SET ANSI_NULLS ON
go

set quoted_identifier on
go

create table[dbo].[Address](
[AddressID][int] IDENTITY (1,1) NOT NULL,
[Street][varchar](255) NULL,
[City][varchar] (100) NULL,
[State][varchar](100) NULL,
[postalCode][varchar](20) NULL,

Primary key CLUSTERED(
[AddressID] asc
)
);
create table[dbo].[Employee](
[EmployeeID][int] IDENTITY (1,1) NOT NULL,
[FirstName][varchar](100) NULL,
[LatName][varchar] (100) NULL,
[Email][varchar](100) NULL,
[AddressID][int] NULL,

Primary key CLUSTERED(
[EmployeeID] asc
)
)ON [PRIMARY];
GO

SELECT TOP(1000)[AddressID]
 ,[Street]
 ,[City]
 ,[State]
 ,[postalCode]
 from[EmployeeDB].[dbo].[Address]

INSERT INTO [EmployeeDB].[dbo].[Address]
([Street], [City], [State], [postalCode])
VALUES
('Park Street', 'Kolkata', 'West Bengal', '700016'),

('Salt Lake Sector V', 'Kolkata', 'West Bengal', '700091'),

('Anna Salai', 'Chennai', 'Tamil Nadu', '600002');


SELECT TOP(1000)[EmployeeID]
 ,[FirstName]
 ,[LatName]
 ,[Email]
 ,[AddressID]
 from[EmployeeDB].[dbo].[Employee]

INSERT INTO [EmployeeDB].[dbo].[Employee]
([FirstName], [LatName], [Email], [AddressID])
VALUES
('Rahul', 'Sharma', 'rahul.sharma@gmail.com', 1),

('Priya', 'Verma', 'priya.verma@gmail.com', 2),

('Amit', 'Kumar', 'amit.kumar@gmail.com', 3);

SELECT * 
FROM [EmployeeDB].[dbo].[Employee];

SELECT *
FROM [EmployeeDB].[dbo].[Address];


go
CREATE PROCEDURE [dbo].[CreateEmployeeWithAddress]
    @FirstName VARCHAR(100),
    @LastName VARCHAR(100),
    @Email VARCHAR(100),
    @Street VARCHAR(255),
    @City VARCHAR(100),
    @State VARCHAR(100),
    @PostalCode VARCHAR(20)
AS
BEGIN
    DECLARE @AddressID INT;

    -- Insert into Address table and get the AddressID
    INSERT INTO Address (Street, City, State, PostalCode)
    VALUES (@Street, @City, @State, @PostalCode);

    SET @AddressID = SCOPE_IDENTITY();

    -- Insert into Employee table with the new AddressID
    INSERT INTO Employee (FirstName, LatName, Email, AddressID)
    VALUES (@FirstName, @LastName, @Email, @AddressID);
END;

go
CREATE PROCEDURE [dbo].[DeleteEmployee]
    @EmployeeID INT
AS
BEGIN
    DECLARE @AddressID INT;

    -- Start transaction
    BEGIN TRANSACTION;

    -- Get the AddressID for rollback purposes
    SELECT @AddressID = AddressID
    FROM Employee
    WHERE EmployeeID = @EmployeeID;

    -- Delete the Employee record
    DELETE FROM Employee
    WHERE EmployeeID = @EmployeeID;

    -- Delete the Address record
    DELETE FROM Address
    WHERE AddressID = @AddressID;

    -- Commit transaction
    COMMIT TRANSACTION;
END;
GO



SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllEmployees]
AS
BEGIN
    SELECT e.EmployeeID,
           e.FirstName,
           e.LatName,
           e.Email,
           a.Street,
           a.City,
           a.State,
           a.PostalCode
    FROM Employee e
    INNER JOIN Address a 
        ON e.AddressID = a.AddressID
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetEmployeeByID]
    @EmployeeID INT
AS
BEGIN
    SELECT  e.EmployeeID,
            e.FirstName,
            e.LatName,
            e.Email,
            a.Street,
            a.City,
            a.State,
            a.PostalCode
    FROM Employee e
    INNER JOIN Address a 
        ON e.AddressID = a.AddressID
    WHERE e.EmployeeID = @EmployeeID;
END
GO


CREATE PROCEDURE [dbo].[UpdateEmployeeWithAddress]
    @EmployeeID INT,
    @FirstName VARCHAR(100),
    @LastName VARCHAR(100),
    @Email VARCHAR(100),
    @Street VARCHAR(255),
    @City VARCHAR(100),
    @State VARCHAR(100),
    @PostalCode VARCHAR(20),
    @AddressID INT
AS
BEGIN
    -- Update Address table
    UPDATE Address
    SET Street = @Street,
        City = @City,
        State = @State,
        PostalCode = @PostalCode
    WHERE AddressID = @AddressID;

    -- Update Employee table
    UPDATE Employee
    SET FirstName = @FirstName,
        LatName = @LastName,
        Email = @Email,
        AddressID = @AddressID
    WHERE EmployeeID = @EmployeeID;
END;
GO


-- Create Employee
EXEC dbo.CreateEmployeeWithAddress
    @FirstName = 'Aman',
    @LastName = 'Verma',
    @Email = 'aman@example.com',
    @Street = '45 Sector 17',
    @City = 'Chandigarh',
    @State = 'Punjab',
    @PostalCode = '160017';
GO

-- Get All Employees
EXEC dbo.GetAllEmployees;
GO

-- Get Employee By ID
EXEC dbo.GetEmployeeByID 
    @EmployeeID = 1;
GO

-- Update Employee
EXEC dbo.UpdateEmployeeWithAddress
    @EmployeeID = 1,
    @FirstName = 'Rahul',
    @LastName = 'Sharma',
    @Email = 'rahul@example.com',
    @Street = 'New Street 45',
    @City = 'Delhi',
    @State = 'Delhi',
    @PostalCode = '110001',
    @AddressID=1;
   
GO
-- Delete Employee
EXEC dbo.DeleteEmployee 
    @EmployeeID = 1;
GO

select*from Employee;
select*from Address;