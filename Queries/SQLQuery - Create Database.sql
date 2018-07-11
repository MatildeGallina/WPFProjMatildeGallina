create database MechanicalComponentsDatabase
go


use MechanicalComponentsDatabase
go

-- tabella engines
create table Engines
(
	Id int identity(1,1) primary key not null,
	Name nvarchar(max) not null,
    SerialCode nvarchar(9) unique not null
)



-- tabella icons
create table Icons
(
	Id int identity(1,1) primary key not null,
	Image image default(0x0)
)


-- tabella components
create table Components
(
	Id int identity(1,1) primary key not null,
	Name nvarchar(max) not null,
	SerialCode nvarchar(9) unique not null,
	EngineId int not null,
	IconId int not null,
    ComponentId int
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

---- non posso creare una FK verso la stessa tabella (?)
--alter table Components
--add constraint FK_Components_Components foreign key (ComponentId)
--    references Components (Id) 
--    on delete cascade
--    on update cascade


-- tabella properties
create table Properties
(
	Id int identity(1,1) primary key not null,
	ComponentId int,
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

