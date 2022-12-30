CREATE DATABASE [Task4.SQL]
USE [Task4.SQL]

-- Example for prime numbers. See /SQL/PrimeNumbers.sql for implementation.
-- This example uses table variables, cycles and if statements.

EXEC PrimeNumbers 100

-- Cursor usage example. See ShopItemsInfo.sql for implementation.

CREATE TABLE ShopItems(Id INT PRIMARY KEY, Name VARCHAR(100), Price DECIMAL)

INSERT INTO ShopItems(Id, Name, Price) VALUES
                                     (0, 'Apple', 15.3),
                                     (1, 'Watermelon', 28.4),
                                     (2, 'Cucumber', 12.0),
                                     (3, 'Strawberry', 45.1),
                                     (4, 'Peach', 40.0)

EXEC ShopItemsInfo

INSERT INTO ShopItems(Id, Name, Price) VALUES
                                     (5, 'Negative', -1)

EXEC ShopItemsInfo