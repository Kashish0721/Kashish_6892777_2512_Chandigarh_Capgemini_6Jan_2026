use TransactionDB;

INSERT INTO Users (Username, Password)
VALUES ('test', '1234')

INSERT INTO Transactions (Amount, Date, Type, UserId)
VALUES 
(2000, GETDATE(), 'Credit', 1),
(500, GETDATE(), 'Debit', 1),
(1000, GETDATE(), 'Credit', 1),
(350, GETDATE(), 'Debit', 1);

CREATE LOGIN [IIS APPPOOL\TransactionApi] FROM WINDOWS;

CREATE USER [IIS APPPOOL\TransactionApi] FOR LOGIN [IIS APPPOOL\TransactionApi];

ALTER ROLE db_owner ADD MEMBER [IIS APPPOOL\TransactionApi];