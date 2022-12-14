# Task 3

## Task details

Write a script in SQL:

1. Save XML to MSSQL and access it in 3 different ways
2. Transform a table to XML or JSON
3. Transform XML or JSON to a table

Create a similar program in C#.

## Description

SQL script contains some example of using XML in MSSQL.

You need to change the path for the [example.xml](Task3.SQL/example.xml) file inside the script.

Before launching the C# program, apply migrations:

```shell
dotnet ef database update --project .\Task3\Task3.Console
```