CREATE DATABASE Task5

USE Task5

-- Create test table
CREATE PROCEDURE fill_test_table
AS
	DROP TABLE IF EXISTS Users

	CREATE TABLE Users(
		Id INT PRIMARY KEY,
		Username VARCHAR(MAX),
		Email VARCHAR(MAX),
		Gender INT, --https://en.wikipedia.org/wiki/ISO/IEC_5218
		Age INT
	)

	INSERT INTO Users(Id, Username, Email, Gender, Age) VALUES
	(0, 'Alex', 'alex@mail.com', 1, 25),
	(1, 'Ivan', 'ivan123@mail.com', 1, 22),
	(2, 'Diana', 'diana@mail.com', 2, 22),
	(3, 'Lana', 'lana@mail.com', 2, 30),
	(4, 'John', 'john.smith@mail.com', 1, 27),
	(5, 'Leila', 'leila@mail.com', 2, 19),
	(6, 'Dio', 'dio@mail.com', 0, 50),
	(7, 'Sam', 'sam@mail.com', 2, 26),
	(8, 'Ronald', 'rld@mail.com', 1, 31),
	(9, 'Kiriko', 'kko@mail.com', 2, 18)
GO

EXEC fill_test_table

SELECT * FROM Users

-- Simple transaction example

BEGIN TRAN
	DELETE FROM Users WHERE Gender = 0
	SELECT * FROM Users
COMMIT

SELECT * FROM Users

-- Rollback example

EXEC fill_test_table

BEGIN TRAN
	DELETE FROM Users WHERE Gender = 1
	SELECT * FROM Users
ROLLBACK

SELECT * FROM Users

-- Savepoint and rollback example

EXEC fill_test_table

BEGIN TRAN
	INSERT INTO Users(Id, Username, Email, Gender, Age) VALUES
		(10, 'Emilia', 'emi@mail.com', 2, 23)
	SAVE TRANSACTION Inserted

	DELETE FROM Users WHERE Id = 1
	SELECT * FROM Users

	ROLLBACK TRAN Inserted
COMMIT

SELECT * FROM Users

-- Isolation level example
---- Read uncommited. Launch second transaction in another query (should return 23).

EXEC fill_test_table

BEGIN TRANSACTION;
	UPDATE Users SET Age = 23 WHERE Username = 'Diana'
	WAITFOR DELAY '00:00:15'
ROLLBACK;

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
BEGIN TRANSACTION;
	SELECT Age FROM Users WHERE Username = 'Diana'
COMMIT;

---- NOLOCK example. Launch second transaction in another query (should return 'kirakira@mail.jp').

EXEC fill_test_table

BEGIN TRANSACTION;
	UPDATE Users SET Email = 'kirakira@mail.jp' WHERE Username = 'Kiriko'
	WAITFOR DELAY '00:00:15'
ROLLBACK;

BEGIN TRANSACTION;
	SELECT Email FROM Users WITH (NOLOCK) WHERE Username = 'Kiriko'
COMMIT;
