CREATE DATABASE UniversityDB;
GO
USE UniversityDB;
GO

-- Departments
CREATE TABLE Departments (
    DeptId INT PRIMARY KEY IDENTITY(1,1),
    DeptName NVARCHAR(100) NOT NULL
);

-- Courses
CREATE TABLE Courses (
    CourseId INT PRIMARY KEY IDENTITY(1,1),
    CourseName NVARCHAR(100),
    DeptId INT FOREIGN KEY REFERENCES Departments(DeptId)
);

-- Students
CREATE TABLE Students (
    StudentId INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Email NVARCHAR(100),
    DeptId INT FOREIGN KEY REFERENCES Departments(DeptId)
);

-- Enrollments
CREATE TABLE Enrollments (
    EnrollmentId INT PRIMARY KEY IDENTITY(1,1),
    StudentId INT FOREIGN KEY REFERENCES Students(StudentId),
    CourseId INT FOREIGN KEY REFERENCES Courses(CourseId),
    Grade CHAR(2)
);

-- Sample Data
INSERT INTO Departments VALUES ('Computer Science'),('Math');
INSERT INTO Students (FirstName,LastName,Email,DeptId)
VALUES ('Alice','Johnson','alice@uni.com',1),
       ('Bob','Smith','bob@uni.com',2);


       -- INSERT
CREATE PROC sp_InsertStudent
@FirstName NVARCHAR(50),
@LastName NVARCHAR(50),
@Email NVARCHAR(100),
@DeptId INT
AS
INSERT INTO Students VALUES (@FirstName,@LastName,@Email,@DeptId);
GO

-- SELECT
CREATE PROC sp_GetStudents
AS
SELECT * FROM Students;
GO

-- UPDATE
CREATE PROC sp_UpdateStudent
@StudentId INT,
@FirstName NVARCHAR(50),
@LastName NVARCHAR(50),
@Email NVARCHAR(100),
@DeptId INT
AS
UPDATE Students
SET FirstName=@FirstName,LastName=@LastName,Email=@Email,DeptId=@DeptId
WHERE StudentId=@StudentId;
GO

-- DELETE
CREATE PROC sp_DeleteStudent
@StudentId INT
AS
DELETE FROM Students WHERE StudentId=@StudentId;
GO