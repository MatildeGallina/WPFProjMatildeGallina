create database MockMechanicalComponentsDatabase
go

use MockMechanicalComponentsDatabase
go

create table Nodes
(
	Id int identity(1,1) primary key not null,
	Name nvarchar(max) not null,
	SerialCode nvarchar(10) unique not null,
	ParentId int,
	Type nvarchar(max) not null,
	Brand nvarchar(max),
	Model nvarchar(max),
	Price decimal(18, 2),
	FreeMaintenance int,
	WarrantyPeriod int,
	Material nvarchar(max),
	Year int
)

select * from Nodes

drop table Nodes
drop database MockMechanicalComponents

UPDATE Nodes
SET Brand = 'Mercedes'
WHERE Id = 1

update Nodes
set Brand = 'Fiat', Model = 'Punto', FreeMaintenance = 1
where Id = 2

select Brand, Model from Nodes where Id = 1