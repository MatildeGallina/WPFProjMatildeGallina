use MechanicalComponentsDatabase
go

insert into Nodes
(Name, SerialCode, Type)
values
('29CV 2 cilindri', '1020304056', 'MultiChildrenNode')

insert into Nodes
(Name, SerialCode, Type)
values
('54CV 4 cilindri', '1002003004', 'MultiChildrenNode')

insert into Nodes
(Name, SerialCode, Type)
values
('190 CV 4 cilindri', '1012023034', 'MultiChildrenNode')

insert into Nodes
(Name, SerialCode, ParentId, Type)
values
('Candela Glow', 'AAA000111B', 1, 'MultiChildrenNode')

insert into Nodes
(Name, SerialCode, ParentId, Type)
values
('Carburatore', 'AAA111222B', 1, 'SingleChildrenNode')

insert into Nodes
(Name, SerialCode, ParentId, Type)
values
('Gruppo termico', 'AAA222333B', 1, 'NullChildrenNode')

insert into Nodes
(Name, SerialCode, ParentId, Type)
values
('Spinotto', 'BBB000111C', 2, 'NullChildrenNode')

insert into Nodes
(Name, SerialCode, ParentId, Type)
values
('Monoblocco', 'BBB111222C', 2, 'SingleChildrenNode')

insert into Nodes
(Name, SerialCode, ParentId, Type)
values
('iniettore', 'CCC000111D', 3, 'SingleChildrenNode')

insert into Nodes
(Name, SerialCode, ParentId, Type)
values
('Cilindro ruotante', 'CCC111222D', 3, 'MultiChildrenNode')

insert into Nodes
(Name, SerialCode, ParentId, Type)
values
('Albero di trasmissione', 'CCC222333D', 3, 'NullChildrenNode')

insert into Nodes
(Name, SerialCode, ParentId, Type)
values
('Cambio', 'CCC333444D', 3, 'MultiChildrenNode')

insert into Nodes
(Name, SerialCode, ParentId, Type)
values
('Spinterogeno', 'CCC444555D', 3, 'NullChildrenNode')


update Nodes
set Brand = 'Mercedes'
where Id = 1

update Nodes
set Brand = 'Fiat', Model = 'Punto', FreeMaintenance = 1
where Id = 2