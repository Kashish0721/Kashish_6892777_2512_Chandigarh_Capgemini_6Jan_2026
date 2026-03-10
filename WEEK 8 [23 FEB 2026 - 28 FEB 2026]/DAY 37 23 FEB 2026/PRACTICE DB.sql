/* ===== DB CREATE ===== */
USE master;
GO

IF DB_ID('PracticeDB') IS NOT NULL
BEGIN
 ALTER DATABASE PracticeDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
 DROP DATABASE PracticeDB;
END
GO

CREATE DATABASE PracticeDB;
GO
USE PracticeDB;
GO

/* ===== TABLE ===== */
CREATE TABLE Employee
(
 EmpID INT PRIMARY KEY,
 FirstName VARCHAR(50),
 LastName VARCHAR(50),
 Salary INT
)
GO

INSERT INTO Employee VALUES
(1,'Mohan','Kumar',20000),
(2,'Amit','Sharma',25000),
(3,'Riya','Singh',30000)
GO

/* ===== STORED PROCEDURE ===== */
CREATE PROC MySum
@A INT,@B INT,@Result INT OUTPUT
AS
BEGIN
 SET @Result=@A+@B
END
GO

/* ===== SCALAR FUNCTION ===== */
CREATE FUNCTION AddTwoNumbers(@a INT,@b INT)
RETURNS INT
AS
BEGIN
 RETURN @a+@b
END
GO

/* ===== INLINE TVF ===== */
CREATE FUNCTION fnGetEmployee()
RETURNS TABLE
AS
RETURN (SELECT * FROM Employee)
GO

/* ===== MULTI TVF ===== */
CREATE FUNCTION fnGetEmpSalary()
RETURNS @Emp TABLE(EmpID INT,Salary INT)
AS
BEGIN
 INSERT INTO @Emp SELECT EmpID,Salary FROM Employee
 RETURN
END
GO

/* ===== TEST ===== */
DECLARE @res INT
EXEC MySum 10,20,@res OUTPUT
PRINT @res

SELECT dbo.AddTwoNumbers(5,6)

SELECT * FROM fnGetEmployee()

SELECT * FROM fnGetEmpSalary()