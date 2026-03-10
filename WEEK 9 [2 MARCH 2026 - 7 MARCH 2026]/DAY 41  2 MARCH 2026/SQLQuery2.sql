CREATE DATABASE EmployeeWindowdb;
use EmployeeWindowdb;
GO
CREATE TABLE Employeetb
(
 EmpId INT PRIMARY KEY,
 EmpName VARCHAR(20),
 EmpDesg VARCHAR(50),
 EmpDOJ DATETIME,
 EmpSal INT,
 EmpDept INT
)
GO

INSERT INTO Employeetb VALUES
(101,'krishna','ProjManager','2010-08-07',45000,10)

INSERT INTO Employeetb VALUES
(102,'kumari','Manager','2010-08-06',50000,20)

INSERT INTO Employeetb VALUES
(103,'Amit','Manager','2010-09-07',44000,30)

INSERT INTO Employeetb VALUES
(104,'krishna','ProjManager','2010-05-07',55000,20)
GO



CREATE PROC sp_DelRecP 
 @PEmpId INT
AS
BEGIN
 DELETE FROM Employeetb 
 WHERE EmpId = @PEmpId
END
GO

CREATE PROC SPEmp_Insert
(
 @PEmpId INT,
 @PEmpName VARCHAR(20),
 @PEmpDesg VARCHAR(50),
 @PEmpDOJ DATETIME,
 @PEmpSal INT,
 @PEmpDept INT
)
AS
BEGIN
 INSERT INTO Employeetb
 VALUES (@PEmpId,@PEmpName,@PEmpDesg,
         @PEmpDOJ,@PEmpSal,@PEmpDept)
END
GO



CREATE PROC SPEmp_Update
(
 @PEmpId INT,
 @PEmpName VARCHAR(20),
 @PEmpDesg VARCHAR(50),
 @PEmpDOJ DATETIME,
 @PEmpSal INT,
 @PEmpDept INT
)
AS
BEGIN
 UPDATE Employeetb
 SET EmpName = @PEmpName,
     EmpDesg = @PEmpDesg,
     EmpDOJ  = @PEmpDOJ,
     EmpSal  = @PEmpSal,
     EmpDept = @PEmpDept
 WHERE EmpId = @PEmpId
END
GO



CREATE PROC SPEmp_Del 
 @PEmpId INT
AS
BEGIN
 DELETE FROM Employeetb 
 WHERE EmpId = @PEmpId
END
GO



CREATE PROC SPGetSal
(
 @PEmpId INT,
 @PEmpSal INT OUTPUT
)
AS
BEGIN
 SELECT @PEmpSal = EmpSal
 FROM Employeetb
 WHERE EmpId = @PEmpId
END
GO



CREATE PROC SPGetData
(
 @PEmpId INT,
 @PEmpName VARCHAR(50) OUTPUT,
 @PEmpDesg VARCHAR(50) OUTPUT,
 @PEmpDOJ DATETIME OUTPUT,
 @PEmpSal INT OUTPUT,
 @PEmpDept INT OUTPUT
)
AS
BEGIN
 SELECT
  @PEmpName = EmpName,
  @PEmpDesg = EmpDesg,
  @PEmpDOJ  = EmpDOJ,
  @PEmpSal  = EmpSal,
  @PEmpDept = EmpDept
 FROM Employeetb
 WHERE EmpId = @PEmpId
END
GO

Select * from Employeetb;