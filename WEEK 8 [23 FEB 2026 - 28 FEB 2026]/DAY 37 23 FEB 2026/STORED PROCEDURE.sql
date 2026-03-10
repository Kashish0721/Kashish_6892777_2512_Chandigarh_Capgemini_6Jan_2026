/* ===== DATABASE RESET ===== */
USE master;
GO

IF DB_ID('kashdb') IS NOT NULL
BEGIN
    ALTER DATABASE kashdb SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE kashdb;
END
GO

CREATE DATABASE kashdb;
GO
USE kashdb;
GO

/* ===== FUNCTIONS ===== */
CREATE FUNCTION Total(@A INT,@B INT,@C INT)
RETURNS INT
AS
BEGIN
 RETURN @A+@B+@C
END
GO

CREATE FUNCTION multiplys(@num1 INT,@num2 INT)
RETURNS INT
AS
BEGIN
 RETURN @num1*@num2
END
GO

/* ===== PROCEDURES ===== */
CREATE PROC MySum
@A INT,@B INT,@C INT=0,@D INT=0,@E INT=0
AS
BEGIN
 PRINT @A+@B+@C+@D+@E
END
GO

CREATE PROC callingfunct
@firstnum1 INT,@secnum2 INT
AS
BEGIN
 DECLARE @setval INT
 SET @setval=dbo.multiplys(@firstnum1,@secnum2)
 PRINT @setval
END
GO

CREATE PROC Swap @X INT OUTPUT,@Y INT OUTPUT
AS
BEGIN
 DECLARE @T INT
 SET @T=@X
 SET @X=@Y
 SET @Y=@T
END
GO

/* ===== TABLES ===== */
CREATE TABLE Employee
(
 EmpID INT PRIMARY KEY,
 FirstName VARCHAR(50),
 LastName VARCHAR(50),
 Salary INT,
 Address VARCHAR(100)
)
GO

INSERT INTO Employee VALUES
(1,'Mohan','Chauahn',22000,'Delhi'),
(2,'Asif','Khan',15000,'Delhi'),
(3,'Bhuvnesh','Shakya',19000,'Noida'),
(4,'Deepak','Kumar',19000,'Noida'),
(5,'Deepika','Kumari',25000,'Noida')
GO

CREATE FUNCTION fnGetEmpFullName(@FirstName VARCHAR(50),@LastName VARCHAR(50))
RETURNS VARCHAR(101)
AS
BEGIN
 RETURN @FirstName+' '+@LastName
END
GO

CREATE FUNCTION fnGetEmployee()
RETURNS TABLE
AS
RETURN (SELECT * FROM Employee)
GO

CREATE TABLE EmployeeCursor
(
 EmpID INT PRIMARY KEY,
 EmpName VARCHAR(50),
 Salary INT,
 Address VARCHAR(200)
)
GO

INSERT INTO EmployeeCursor VALUES
(1,'Mohan',12000,'Noida'),
(2,'Pavan',25000,'Delhi'),
(3,'Amit',22000,'Dehradun'),
(4,'Sonu',22000,'Noida'),
(5,'Deepak',28000,'Gurgaon')
GO

/* ===== CURSOR ===== */
DECLARE @Id INT,@name VARCHAR(50),@salary INT
DECLARE cur_emp CURSOR STATIC FOR
SELECT EmpID,EmpName,Salary FROM EmployeeCursor
OPEN cur_emp
FETCH NEXT FROM cur_emp INTO @Id,@name,@salary
WHILE @@FETCH_STATUS=0
BEGIN
 PRINT 'ID:'+CAST(@Id AS VARCHAR)+' Name:'+@name+' Salary:'+CAST(@salary AS VARCHAR)
 FETCH NEXT FROM cur_emp INTO @Id,@name,@salary
END
CLOSE cur_emp
DEALLOCATE cur_emp
GO

/* ===== TRIGGER TABLES ===== */
CREATE TABLE Employee_Demo
(
 Emp_ID INT IDENTITY,
 Emp_Name VARCHAR(55),
 Emp_Sal DECIMAL(10,2)
)
GO

CREATE TABLE Employee_Demo_Audit
(
 Emp_ID INT,
 Emp_Name VARCHAR(55),
 Emp_Sal DECIMAL(10,2),
 Audit_Action VARCHAR(100),
 Audit_Timestamp DATETIME
)
GO

/* ===== TRIGGERS ===== */
CREATE TRIGGER trgAfterInsert ON Employee_Demo
AFTER INSERT
AS
BEGIN
 INSERT INTO Employee_Demo_Audit
 SELECT Emp_ID,Emp_Name,Emp_Sal,'Inserted',GETDATE() FROM inserted
END
GO

CREATE TRIGGER trgAfterUpdate ON Employee_Demo
AFTER UPDATE
AS
BEGIN
 INSERT INTO Employee_Demo_Audit
 SELECT Emp_ID,Emp_Name,Emp_Sal,'Updated',GETDATE() FROM inserted
END
GO

CREATE TRIGGER trgAfterDelete ON Employee_Demo
AFTER DELETE
AS
BEGIN
 INSERT INTO Employee_Demo_Audit
 SELECT Emp_ID,Emp_Name,Emp_Sal,'Deleted',GETDATE() FROM deleted
END
GO

/* ===== TEST ===== */
SELECT dbo.Total(3,5,4)
EXEC MySum 10,20
EXEC callingfunct 3,4
SELECT * FROM Employee
SELECT * FROM fnGetEmployee()