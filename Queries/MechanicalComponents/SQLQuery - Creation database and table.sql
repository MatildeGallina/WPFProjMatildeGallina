drop table Nodes
drop database MechanicalComponentsDatabase


create database MechanicalComponentsDatabase
go

use MechanicalComponentsDatabase
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
	Price float,
	FreeMaintenance int,
	WarrantyPeriod int,
	Material nvarchar(max),
	Year int
)