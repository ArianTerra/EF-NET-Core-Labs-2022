# Task 2

## Task details

Show HierarchyId usage examples in two variants:
1. SQL script
2. EF Core program

What should be in the program:
1. Element insertions to the tree hierarchy
2. Get tree hierarchy from given node (get all descendants)
3. Get all ancestors
4. Delete tree branch (delete all descendants of a node)

## Description

There are two projects:
1. Task2.SQL
2. Task2.Console

Task2.SQL contains a simple SQL script, that creates simple procedures for finding descendants and ancestors of a node.

Task2.Console does basically the same, but with simple LINQ. The project uses migrations. To apply them, execute next command:

```shell
dotnet ef database update --project .\Task2\Task2.Console\
```