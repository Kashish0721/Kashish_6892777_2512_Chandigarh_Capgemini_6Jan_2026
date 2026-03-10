/* ===== DB RESET ===== */
USE master;
GO

IF DB_ID('bankdb') IS NOT NULL
BEGIN
 ALTER DATABASE bankdb SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
 DROP DATABASE bankdb;
END
GO

CREATE DATABASE bankdb;
GO
USE bankdb;
GO

/* ===== TABLE ===== */
CREATE TABLE Employeetb(
 EmpId INT PRIMARY KEY,
 EmpName VARCHAR(20),
 EmpDesg VARCHAR(50),
 EmpDOJ DATETIME,
 EmpSal INT,
 EmpDept INT
)
GO

/* ===== INSERT DATA ===== */
INSERT INTO Employeetb VALUES
(101,'Krishna','ProjManager','2010-07-08',45000,10),
(102,'Kumari','Manager','2010-06-08',50000,20),
(103,'Amit','Manager','2010-07-09',44000,30),
(104,'Riya','ProjManager','2010-07-05',55000,20)
GO

/* ===== DELETE PROCEDURE ===== */
CREATE PROC sp_DelRecP @PempId INT
AS
BEGIN
 DELETE FROM Employeetb WHERE EmpId=@PempId
END
GO

/* ===== INSERT PROCEDURE ===== */
CREATE PROC SPEmp_Insert
@PEmpId INT,@PEmpName VARCHAR(20),@PEmpDesg VARCHAR(50),
@PEmpDOJ DATETIME,@PEmpSal INT,@PEmpDept INT
AS
BEGIN
 INSERT INTO Employeetb VALUES
 (@PEmpId,@PEmpName,@PEmpDesg,@PEmpDOJ,@PEmpSal,@PEmpDept)
END
GO

/* ===== UPDATE PROCEDURE ===== */
CREATE PROC SPEmp_Update
@PEmpId INT,@PEmpName VARCHAR(20),@PEmpDesg VARCHAR(50),
@PEmpDOJ DATETIME,@PEmpSal INT,@PEmpDept INT
AS
BEGIN
 UPDATE Employeetb
 SET EmpName=@PEmpName,
     EmpDesg=@PEmpDesg,
     EmpDOJ=@PEmpDOJ,
     EmpSal=@PEmpSal,
     EmpDept=@PEmpDept
 WHERE EmpId=@PEmpId
END
GO

/* ===== DELETE PROCEDURE ===== */
CREATE PROC SPEmp_Del @PEmpId INT
AS
BEGIN
 DELETE FROM Employeetb WHERE EmpId=@PEmpId
END
GO

/* ===== OUTPUT PARAM PROCEDURE ===== */
CREATE PROC SPGetSal
@PEmpId INT,@PEmpSal INT OUTPUT
AS
BEGIN
 SELECT @PEmpSal=EmpSal FROM Employeetb WHERE EmpId=@PEmpId
END
GO

/* ===== MULTI OUTPUT PROCEDURE ===== */
CREATE PROC SPGetData
@PEmpId INT,
@PEmpName VARCHAR(50) OUTPUT,
@PEmpDesg VARCHAR(50) OUTPUT,
@PEmpDOJ DATETIME OUTPUT,
@PEmpSal INT OUTPUT,
@PEmpDept INT OUTPUT
AS
BEGIN
 SELECT
  @PEmpName=EmpName,
  @PEmpDesg=EmpDesg,
  @PEmpDOJ=EmpDOJ,
  @PEmpSal=EmpSal,
  @PEmpDept=EmpDept
 FROM Employeetb WHERE EmpId=@PEmpId
END
GO

/* ===== TEST ===== */
DECLARE @sal INT
EXEC SPGetSal 101,@sal OUTPUT
PRINT @sal

DECLARE @n VARCHAR(50),@d VARCHAR(50),@doj DATETIME,@s INT,@dept INT
EXEC SPGetData 101,@n OUTPUT,@d OUTPUT,@doj OUTPUT,@s OUTPUT,@dept OUTPUT
SELECT @n,@d,@doj,@s,@dept

SELECT * FROM Employeetb