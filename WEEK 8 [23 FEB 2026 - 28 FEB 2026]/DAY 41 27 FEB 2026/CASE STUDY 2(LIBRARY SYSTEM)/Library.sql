CREATE DATABASE LibraryDB;
GO
USE LibraryDB;
GO

CREATE TABLE Authors(
 AuthorId INT PRIMARY KEY IDENTITY(1,1),
 AuthorName NVARCHAR(100)
);

CREATE TABLE Books(
 BookId INT PRIMARY KEY IDENTITY(1,1),
 Title NVARCHAR(200),
 AuthorId INT FOREIGN KEY REFERENCES Authors(AuthorId),
 PublishedYear INT
);

INSERT INTO Authors VALUES ('J.K. Rowling'),('George Orwell');
INSERT INTO Books VALUES ('Harry Potter',1,1997),('1984',2,1949);

CREATE PROC sp_GetBooks
AS SELECT * FROM Books;
GO

CREATE PROC sp_InsertBook
@Title NVARCHAR(200),@AuthorId INT,@PublishedYear INT
AS INSERT INTO Books VALUES (@Title,@AuthorId,@PublishedYear);
GO

CREATE PROC sp_UpdateBook
@BookId INT,@Title NVARCHAR(200),@AuthorId INT,@PublishedYear INT
AS UPDATE Books SET Title=@Title,AuthorId=@AuthorId,PublishedYear=@PublishedYear
WHERE BookId=@BookId;
GO

CREATE PROC sp_DeleteBook
@BookId INT
AS DELETE FROM Books WHERE BookId=@BookId;
GO