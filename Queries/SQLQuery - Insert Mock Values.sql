use MechanicalComponentsDatabase
go

insert into Nodes
(Name, SerialCode)
values
('29CV 2 cilindri', '1020304056')

insert into Nodes
(Name, SerialCode)
values
('54CV 4 cilindri', '1002003004')

insert into Nodes
(Name, SerialCode)
values
('190 CV 4 cilindri', '1012023034')

insert into Nodes
(Name, SerialCode, ParentId)
values
('Candela Glow', 'AAA000111B', 1)

insert into Nodes
(Name, SerialCode, ParentId)
values
('Carburatore', 'AAA111222B', 1)

insert into Nodes
(Name, SerialCode, ParentId)
values
('Gruppo termico', 'AAA222333B', 1)

insert into Nodes
(Name, SerialCode, ParentId)
values
('Spinotto', 'BBB000111C', 2)

insert into Nodes
(Name, SerialCode, ParentId)
values
('Monoblocco', 'BBB111222C', 2)

insert into Nodes
(Name, SerialCode, ParentId)
values
('iniettore', 'CCC000111D', 3)

insert into Nodes
(Name, SerialCode, ParentId)
values
('Cilindro ruotante', 'CCC111222D', 3)

insert into Nodes
(Name, SerialCode, ParentId)
values
('Albero di trasmissione', 'CCC222333D', 3)

insert into Nodes
(Name, SerialCode, ParentId)
values
('Cambio', 'CCC333444D', 3)

insert into Nodes
(Name, SerialCode, ParentId)
values
('Spinterogeno', 'CCC444555D', 3)

