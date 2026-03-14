CREATE DATABASE LibraryDB2;
GO

USE LibraryDB2;

CREATE TABLE Authors (
    AuthorId INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Country NVARCHAR(100)
);

CREATE TABLE Books (
    BookId INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(200),
    ISBN NVARCHAR(50),
    AuthorId INT,
    FOREIGN KEY (AuthorId) REFERENCES Authors(AuthorId)
);

CREATE TABLE Borrowers (
    BorrowerId INT PRIMARY KEY IDENTITY,
    FullName NVARCHAR(100),
    Email NVARCHAR(100)
);

CREATE TABLE BookBorrowers (
    BookId INT,
    BorrowerId INT,
    PRIMARY KEY (BookId, BorrowerId),
    FOREIGN KEY (BookId) REFERENCES Books(BookId),
    FOREIGN KEY (BorrowerId) REFERENCES Borrowers(BorrowerId)
);