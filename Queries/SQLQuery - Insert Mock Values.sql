use MechanicalComponentsDatabase

-- insertion of some engines

insert into Engines
(Name, SerialCode)
values
('29CV 2 cilindri', '102030405')

insert into Engines
(Name, SerialCode)
values
('54CV 4 cilindri', '100200300')

insert into Engines
(Name, SerialCode)
values
('190 CV 4 cilindri', '101202303')


-- insertion of some icons
insert into Icons(Image) values (NULL)
insert into Icons(Image) values (NULL)
insert into Icons(Image) values (NULL) 


-- insertion of some components

insert into Components
(Name, SerialCode, EngineId, IconId)
values
('Candela Glow', 'AAA000111', 1, 1)


insert into Components
(Name, SerialCode, EngineId, IconId)
values
('Carburatore', 'AAA111222', 1, 2)


insert into Components
(Name, SerialCode, EngineId, IconId)
values
('Gruppo termico', 'AAA222333', 1, 3)


insert into Components
(Name, SerialCode, EngineId, IconId)
values
('Spinotto', 'BBB000111', 2, 3)


insert into Components
(Name, SerialCode, EngineId, IconId)
values
('Monoblocco', 'BBB111222', 2, 1) 

insert into Components
(Name, SerialCode, EngineId, IconId)
values
('iniettore', 'CCC000111', 3, 1)


insert into Components
(Name, SerialCode, EngineId, IconId)
values
('Cilindro ruotante', 'CCC111222', 3, 2)


insert into Components
(Name, SerialCode, EngineId, IconId)
values
('Albero di trasmissione', 'CCC222333', 3, 1)


insert into Components
(Name, SerialCode, EngineId, IconId)
values
('Cambio', 'CCC333444', 3, 3)


insert into Components
(Name, SerialCode, EngineId, IconId)
values
('Spinterogeno', 'CCC444555', 3, 3)
