-- This is an example for hierarchyid usage
-- Launch code blocks one by one

USE [Task2.SQL]
GO

-- Create table
CREATE TABLE GeographyData(
    Path hierarchyid PRIMARY KEY,
    GeographicName nvarchar(30)
)

-- Insert some geography data
INSERT GeographyData(Path, GeographicName)
VALUES
    ('/', 'Earth'),
    ('/0/', 'Africa'),
    ('/1/', 'Asia'),
    ('/2/', 'Europe'),
    ('/3/', 'North America'),
    ('/4/', 'South America'),
    ('/5/', 'Australia'),
    ('/1/0/', 'Japan'),
    ('/1/0/0/', 'Kyoto'),
    ('/1/0/1/', 'Tokyo'),
    ('/2/0/', 'France'),
    ('/2/0/0/', 'Paris'),
    ('/2/1/', 'Germany'),
    ('/2/1/0/', 'Berlin'),
    ('/2/1/1/', 'Munchen'),
    ('/2/2/', 'Ukraine'),
    ('/2/2/0/', 'Kyiv'),
    ('/2/2/1/', 'Kharkiv'),
    ('/2/2/2/', 'Lviv')

-- Make different table for storing level names
CREATE TABLE Levels(
    LevelId INT PRIMARY KEY,
    Name nvarchar(30) UNIQUE NOT NULL
)

INSERT Levels(LevelId, Name)
VALUES
    (0, 'Planet'),
    (1, 'Continent'),
    (2, 'Country'),
    (3, 'City')

-- Show result
CREATE PROC ShowResult
    AS
        SELECT Path, Path.ToString() as 'PathString', Path.GetLevel() as 'Level', L.Name as 'LevelName', GeographicName
        FROM GeographyData
        INNER JOIN Levels L on GeographyData.Path.GetLevel() = L.LevelId
        ORDER BY GeographyData.Path.GetLevel()
GO

EXEC ShowResult

-- Get geographic object data
-- In this example we'll get Tokyo as a variable and try to get info about ancestor node
CREATE PROC GetNodeInfo @NodeName nvarchar(30)
AS
    DECLARE @Node hierarchyid
    SELECT @Node = Path
    FROM GeographyData
    WHERE GeographicName = @NodeName

    SELECT @NodeName as 'Name', Type.Name as 'Type', Ancestor.GeographicName as 'AncestorName'
    FROM Levels
    INNER JOIN Levels Type on @Node.GetLevel() = Type.LevelId
    INNER JOIN GeographyData Ancestor on @Node.GetAncestor(1) = Ancestor.Path
    WHERE @Node.GetLevel() = Levels.LevelId
GO

EXEC GetNodeInfo 'Tokyo'

-- Get all ancestors of a node, f. e. Kharkiv
CREATE PROC GetAncestors @NodeName nvarchar(30)
AS
    WITH Ancestors(Id, [Name], AncestorId) AS
    (
          SELECT Path, GeographicName, Path.GetAncestor(1)
          FROM GeographyData
          WHERE GeographicName = @NodeName
          UNION ALL

          SELECT data.Path, data.GeographicName, data.Path.GetAncestor(1)
          FROM GeographyData data
          INNER JOIN Ancestors a ON data.Path = a.AncestorId
    )
    SELECT *, Id.ToString() as 'Path' FROM Ancestors
GO

EXEC GetAncestors 'Kharkiv'

-- Get all descendants of a node, f. e. Europe
CREATE PROC GetDescendants @NodeName nvarchar(30)
AS
    DECLARE @Node hierarchyid
    SELECT @Node = Path
    FROM GeographyData
    WHERE GeographicName = @NodeName

    SELECT *, Path.ToString() as 'Path'
    FROM GeographyData
    WHERE Path.IsDescendantOf(@Node) = 1
GO

EXEC GetDescendants 'Europe'

-- Remove hierarchy branch with all descendants
-- Let's remove France, yay
CREATE PROC RemoveNodeTree @NodeName nvarchar(30)
AS
    DECLARE @France hierarchyid
    SELECT @France = Path
    FROM GeographyData
    WHERE GeographicName = @NodeName

    DELETE FROM GeographyData
    WHERE Path.IsDescendantOf(@France) = 1
GO

EXEC RemoveNodeTree 'France'