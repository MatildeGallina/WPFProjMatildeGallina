use MockMechanicalComponentsDatabase
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

