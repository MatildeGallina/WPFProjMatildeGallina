create database MechanicalComponentsDatabase
go

use MechanicalComponentsDB
go

-- tabella engines
create table Engines
(
	Id int identity(1,1) primary key not null,
	Name nvarchar(max) not null,
    SerialNumber nvarchar(max) unique not null
)

-- tabella components
create table Components
(
	Id int identity(1,1) primary key not null,
	Name nvarchar(max) not null,
	SerialNumber nvarchar(max) unique not null,
	EngineId int not null,
	IconId int not null
)

alter table Components
add constraint FK_Components_Engines foreign key (EngineId)
    references Engines (Id) 
    on delete cascade
    on update cascade

alter table Components
add constraint FK_Components_Icons foreign key (IconId)
    references Icons (Id) 
    on delete cascade
    on update cascade


-- tabella elements
create table Children
(
	Id int identity(1,1) primary key not null,
	Name nvarchar(max) not null,
	SerialNumber nvarchar(max) unique not null,
	ComponentId int not null
)

alter table Children
add constraint FK_Children_Components foreign key (ComponentId)
    references Components (Id) 
    on delete cascade
    on update cascade


-- tabella properties
create table Properties
(
	Id int identity(1,1) primary key not null,
	ComponentId int,
	ChildId int,
	Material nvarchar(max),
	Height int,
	Length int,
	Depth int,
	Diameter int,
	Power int,
	Ampere int,
	Voltage int,
	Capacity int,
)

alter table Properties
add constraint FK_Properties_Components foreign key (ComponentId)
    references Components (Id) 
    on delete cascade
    on update cascade

alter table Properties
add constraint FK_Properties_Children foreign key (ChildId)
    references Children (Id) 
    on delete cascade
    on update cascade


-- tabella icons
create table Icons
(
	Id int identity(1,1) primary key not null,
	Image image default(0x0)
)