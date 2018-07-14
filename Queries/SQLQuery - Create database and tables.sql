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
	IconId int
)

