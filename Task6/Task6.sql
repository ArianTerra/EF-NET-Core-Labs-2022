-- reconfigure DB
-- make sure to enable FileStream in MSSQL instance settings
EXEC sp_configure filestream_access_level, 2
RECONFIGURE

-- Create database
CREATE DATABASE [Task6.SQL]
ON
PRIMARY (NAME = [Task6.SQL], FILENAME = N'C:\Temp\DB\Task6.SQL.mdf'),
    FILEGROUP FileStreamGroup CONTAINS FILESTREAM (NAME = [Task6.SQL_fs],
        FILENAME = N'C:\Temp\DB\Task6_fs')
LOG ON (NAME = Task6Log, FILENAME = N'C:\Temp\DB\Task6.SQL_log.ldf')
WITH FILESTREAM( NON_TRANSACTED_ACCESS = FULL, DIRECTORY_NAME = 'Task6_storage')
GO

-- Create table
USE [Task6.SQL]
CREATE TABLE DocumentsStore as FileTable

-- Procedure for loading file to storage
CREATE PROC LoadFile
	@FileName NVARCHAR(MAX),
	@Path NVARCHAR(MAX)
AS
	DECLARE @sql NVARCHAR(MAX)
	SET @sql = 'INSERT INTO DocumentsStore(name, file_stream)
	SELECT ''' +
		@FileName +''',
		* FROM OPENROWSET(BULK N''' + @Path + ''', SINGLE_BLOB)
		AS FileData'

	EXEC(@sql)
GO

-- execute procedure
EXEC LoadFile '2.txt', 'C:\Temp\DB\2.txt'

-- create index for fulltext search

CREATE UNIQUE INDEX UQ_StreamID ON DocumentsStore(stream_id)
GO

CREATE FULLTEXT CATALOG DocumentsStoreFT AS DEFAULT
GO
-- check if fulltext is installed
SELECT SERVERPROPERTY('IsFullTextInstalled');  
GO

CREATE FULLTEXT INDEX ON DocumentsStore(file_stream TYPE COLUMN file_type)
	KEY INDEX PK__Document__5A5B77D511BA4C86 ON DocumentsStoreFT
GO

-- search examples

SELECT name FROM DocumentsStore WHERE CONTAINS(file_stream, '"Lorem"')

SELECT name FROM DocumentsStore WHERE CONTAINS(file_stream, '"Lorem" OR "ipsum"')

SELECT name FROM DocumentsStore WHERE CONTAINS(file_stream, 'NEAR((Lorem, ipsum), 2, TRUE)')