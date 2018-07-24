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
	Type nvarchar(max) not null
)

select * from Nodes

drop table Nodes
drop database MockMechanicalComponents