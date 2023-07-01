--database : katsaka
--user : nomena
--password : root
--connection string :  "KatsakaContext": "Host=localhost; Database=katsaka; Username=nomena; Password=root;"

/*
Scaffolding :
dotnet ef dbcontext scaffold "Name=ConnectionStrings:SamboContext" Npgsql.EntityFrameworkCore.PostgreSQL --context-dir Data --output-dir Models --table 
*/

/*
create table responsable(
    id serial primary key,
    nom varchar(100) not null
);
*/
/*
create table champ(
    id serial primary key,
    nom varchar(100) not null
);
*/
/*
create table parcelle(
    id serial primary key,
    idchamp int not null,
    nom varchar(100) not null,
    remarque varchar(200),
    idresponsable int not null,
    foreign key (idchamp) references champ(id),
    foreign key (idresponsable) references responsable(id)
);
*/
/*
create table suivimais(
    id serial primary key,
    idparcelle int not null,
    longueurMoyenPousse numeric(10,2) check(longueurMoyenPousse >= 0),
    couleurMoyenPousse int check(couleurMoyenPousse >= 0 and couleurMoyenPousse <= 100),
    nbrPousse int check(nbrPousse >= 0),
    nbrEpisMoyenParPousse int check(nbrEpisMoyenParPousse >= 0),
    longueurMoyenEpis numeric(10,2) check(longueurMoyenEpis >= 0),
    dateSuivi timestamp not null,
    foreign key (idparcelle) references parcelle(id)
);
*/
/*
create table recolte(
    id serial primary key,
    idparcelle int not null,
    poidsTotalGraine numeric(10,2) check(poidsTotalGraine >= 0),
    nbrTotalEpis int check(nbrTotalEpis >= 0),
    dateRecolte timestamp not null,
    foreign key (idparcelle) references parcelle(id)
);
*/
/*
create table parametreCroissance(
    id serial primary key,
    nom varchar(100) not null,
    valeur numeric(10,2) check(valeur > 0)
);
*/
/*
create table parametreFrequence(
    id serial primary key,
    nom varchar(100) not null,
    valeur interval check(valeur > '00:00:00')
);
*/


-- DEVELOPPEMENT/POUSSE MAIS
--Suivis à comparer pour détecter les anomalies (les deux derniers par défaut)
select * from suivimais where idparcelle=4 order by datesuivi desc limit 2;



-- RECOLTE
-- Dernière récolte et dernier suivi à comparer pour détecter les anomalies

--Dernier suivi avant récoltes

create view v_suivi_recolte as
select id, idparcelle, longueurmoyenpousse, couleurmoyenpousse, nbrpousse*nbrepismoyenparpousse as nbrepistotalsuivi,  longueurmoyenepis, datesuivi
from suivimais;

---------------
---Dates derniers suivi par parcelle
create view v_list_date_dernier_suivi as 
select idparcelle, max(datesuivi) as maxdatesuivi
from suivimais
group by idparcelle
order by idparcelle;

--Derniers suivi par parcelle
create view v_list_dernier_suivi as 
select suivimais.*, parcelle.nom as nomparcelle from suivimais
join v_list_date_dernier_suivi
on v_list_date_dernier_suivi.maxdatesuivi = suivimais.datesuivi
join parcelle on parcelle.id = v_list_date_dernier_suivi.idparcelle;


---------------------------------------------------------
create view v_derniersuivi_avant_recolte as 
select v_suivi_recolte.id as idsuivi, v_suivi_recolte.idparcelle as idparcelle, v_suivi_recolte.longueurmoyenpousse, v_suivi_recolte.couleurmoyenpousse, v_suivi_recolte.nbrepistotalsuivi,  v_suivi_recolte.longueurmoyenepis as longueurmoyenepis_suivi , v_suivi_recolte.datesuivi,
recolte.id as idrecolte, recolte.poidstotalgraine,  recolte.nbrtotalepis, recolte.longueurmoyenepis as longueurmoyenepis_recolte, recolte.daterecolte        

from
v_suivi_recolte

join recolte
on recolte.idparcelle = v_suivi_recolte.idparcelle

join v_list_date_dernier_suivi
on v_list_date_dernier_suivi.maxdatesuivi = v_suivi_recolte.datesuivi

where v_suivi_recolte.datesuivi <= recolte.daterecolte
order by recolte.daterecolte;



