drop table Nodes
drop database MockMechanicalComponentsDatabase


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