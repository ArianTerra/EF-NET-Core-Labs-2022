create database [Task3.SQL]

use [Task3.SQL]
go

create table xml(IntCol int, XmlCol xml)
go

insert into xml(XmlCol)
select * from openrowset(
        bulk 'C:\Users\Arian\OneDrive\Документи\Study\KHAI 5.1\C#\KHAI .NET Labs 2022\Task3\Task3.SQL\example.xml',
        single_blob
    ) as x;

-- show all
select * from xml

-- show some nodes inside a node
select XmlCol.query('weatherdata/forecast/time')
from xml

-- show node attribute value by another value
select XmlCol.value('(weatherdata/forecast/time/windDirection[@code="N"]/@deg)[1]', 'varchar(50)')
from xml

-- example 2: XML to relational form

DECLARE @XML XML
SET @XML = '<root>
    <row>
        <Id>8</Id>
        <IdProduct>3</IdProduct>
        <Cantidate>25</Cantidate>
        <Folio>4568457</Folio>
    </row>
    <row>
        <Id>3</Id>
        <IdProduct>3</IdProduct>
        <Cantidate>72</Cantidate>
        <Folio>4463223</Folio>
    </row>
</root>'

DECLARE @handle INT  
DECLARE @PrepareXmlStatus INT  

EXEC @PrepareXmlStatus= sp_xml_preparedocument @handle OUTPUT, @XML  

SELECT  *
FROM    OPENXML(@handle, '/root/row', 2)  
    WITH (
    Id INT,
    IdProduct INT,
    Cantidate INT,
    Folio INT
    )  

EXEC sp_xml_removedocument @handle 

-- example 3: table to XML

CREATE TABLE Students(
	Id INT,
	FullName VARCHAR(50),
	Class VARCHAR(10),
	Address VARCHAR(50),
	ZipCode INT
)

INSERT INTO Students(Id, FullName, Class, Address, ZipCode) 
VALUES
(0, 'Hamud Gamibi', '111-3', '35, Tree str.', 12345),
(1, 'Abob Bibi', '111-2', '12, Star str.', 12341),
(2, 'Migi Hidari', '111-2', '11, Seven Eleven str.', 12341),
(3, 'Dracula Bob', '100-A', '3, Forest ave.', 12342),
(4, 'John Smith', '110-A', '22, Human str.', 12345)

SELECT 
   Id as "@StudentID",
   FullName,
   Address as "address/street",
   ZipCode as "address/zip"
FROM Students
FOR XML PATH('Student'), ROOT('Students')