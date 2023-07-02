--database : katsaka
--user : nomena
--password : root
--connection string :  "KatsakaContext": "Host=localhost; Database=katsaka; Username=nomena; Password=root;"


--========RESPONSABLE===============
insert into responsable(nom) values
('jean'),
('jean ba');

--========CHAMP===============
insert into champ(nom) values
('CHAMP');

--========PARCELLE===============
insert into parcelle(nom, idchamp, remarque, idresponsable) values
('P1', 1, null, 1),
('P2', 1, null, 1),
('P3', 1, null, 1),

('P4', 1, null, 2),
('P5', 1, null, 2),
('P6', 1, null, 2);


--========SUIVIMAIS===============

-- Scénarios : 
-- P1, 1 suivi, aucune anomalie
-- P2, 2 suivis, aucune anomalie
-- P3, 3 suivis, anomalies : couleur, nbrEpisMoyenParPousse

-- P4, 3 suivis, anomalies : longueurMoyenEpis < 15cm(paramètre croissance) mais n'ayant plus poussé, longueurMoyenPousse
-- P5, 2 suivis, aucune anomalie
-- P6, 2 suivis, aucune anomalie

-- P1
insert into suivimais(idparcelle ,longueurMoyenPousse, couleurMoyenPousse ,nbrPousse ,nbrEpisMoyenParPousse, longueurMoyenEpis,dateSuivi)
values (1, 20, 80, 300, 0, 0, now());

-- P2
insert into suivimais(idparcelle ,longueurMoyenPousse, couleurMoyenPousse ,nbrPousse ,nbrEpisMoyenParPousse, longueurMoyenEpis,dateSuivi)
values (2, 80, 70, 300, 0, 0, now()-'4 weeks'::interval);

insert into suivimais(idparcelle ,longueurMoyenPousse, couleurMoyenPousse ,nbrPousse ,nbrEpisMoyenParPousse, longueurMoyenEpis,dateSuivi)
values (2, 100, 90, 304, 3, 8, now()-'2 weeks'::interval);

-- P3
insert into suivimais(idparcelle ,longueurMoyenPousse, couleurMoyenPousse ,nbrPousse ,nbrEpisMoyenParPousse, longueurMoyenEpis,dateSuivi)
values (3, 20, 80, 300, 0, 0, now()-'4 weeks'::interval);

insert into suivimais(idparcelle ,longueurMoyenPousse, couleurMoyenPousse ,nbrPousse ,nbrEpisMoyenParPousse, longueurMoyenEpis,dateSuivi)
values (3, 100, 90, 304, 3, 8, now()-'2 weeks'::interval);

insert into suivimais(idparcelle ,longueurMoyenPousse, couleurMoyenPousse ,nbrPousse ,nbrEpisMoyenParPousse, longueurMoyenEpis,dateSuivi)
values (3, 100, 87, 304, 2, 14, now());

-- P4
insert into suivimais(idparcelle ,longueurMoyenPousse, couleurMoyenPousse ,nbrPousse ,nbrEpisMoyenParPousse, longueurMoyenEpis,dateSuivi)
values (4, 20, 80, 300, 0, 0, now()-'6 weeks'::interval);

insert into suivimais(idparcelle ,longueurMoyenPousse, couleurMoyenPousse ,nbrPousse ,nbrEpisMoyenParPousse, longueurMoyenEpis,dateSuivi)
values (4, 80, 95, 305, 3, 10, now()-'4 weeks'::interval);

insert into suivimais(idparcelle ,longueurMoyenPousse, couleurMoyenPousse ,nbrPousse ,nbrEpisMoyenParPousse, longueurMoyenEpis,dateSuivi)
values (4, 70, 95, 305, 3, 10, now()-'2 weeks'::interval);

-- P5
insert into suivimais(idparcelle ,longueurMoyenPousse, couleurMoyenPousse ,nbrPousse ,nbrEpisMoyenParPousse, longueurMoyenEpis,dateSuivi)
values (5, 80, 80, 300, 0, 0, now()-'4 weeks'::interval);

insert into suivimais(idparcelle ,longueurMoyenPousse, couleurMoyenPousse ,nbrPousse ,nbrEpisMoyenParPousse, longueurMoyenEpis,dateSuivi)
values (5, 150, 98, 310, 4, 15, now()-'2 weeks'::interval);

-- P6
insert into suivimais(idparcelle ,longueurMoyenPousse, couleurMoyenPousse ,nbrPousse ,nbrEpisMoyenParPousse, longueurMoyenEpis,dateSuivi)
values (6, 100, 80, 300, 0, 0, now()-'4 weeks'::interval);

insert into suivimais(idparcelle ,longueurMoyenPousse, couleurMoyenPousse ,nbrPousse ,nbrEpisMoyenParPousse, longueurMoyenEpis,dateSuivi)
values (6, 200, 99, 320, 6, 25, now()-'2 weeks'::interval);


--========RECOLTE===============
--Scénario :
-- P5 prêt pour la récolte
-- anomalie : nbrTotalEpis

--P6 prêt pour la récolte
-- anomalies : aucune

-- nbrTotalEpis : 1240 -> 1200
--P5
insert into recolte(idparcelle, poidsTotalGraine, nbrTotalEpis, longueurMoyenEpis, dateRecolte)
values (5, 1000, 1200, 16, now());

--P6
insert into recolte(idparcelle, poidsTotalGraine, nbrTotalEpis, longueurMoyenEpis, dateRecolte)
values (6, 3500, 1980, 26, now());



--========PARAMETRECROISSANCE===============
insert into parametreCroissance(nom, valeur) values
('longueur epis', 15);


--========PARAMETREFREQUENCE===============
insert into parametreFrequence(nom, valeur) values
('frequence suivi', '2 weeks');
