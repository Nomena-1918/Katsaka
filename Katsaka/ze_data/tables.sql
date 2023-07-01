--database : katsaka
--user : nomena
--password : root
--connection string :  "KatsakaContext": "Host=localhost; Database=katsaka; Username=nomena; Password=root;"


/*
Scaffolding :
dotnet ef dbcontext scaffold "Name=ConnectionStrings:KatsakaContext" Npgsql.EntityFrameworkCore.PostgreSQL --context-dir Data --output-dir Models --table 
*/

create table responsable(
    id serial primary key,
    nom varchar(100) not null
);

create table champ(
    id serial primary key,
    nom varchar(100) not null
);

create table parcelle(
    id serial primary key,
    idchamp int not null,
    nom varchar(100) not null,
    remarque varchar(200),
    idresponsable int not null,
    foreign key (idchamp) references champ(id),
    foreign key (idresponsable) references responsable(id)
);

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

create table recolte(
    id serial primary key,
    idparcelle int not null,
    poidsTotalGraine numeric(10,2) check(poidsTotalGraine >= 0),
    nbrTotalEpis int check(nbrTotalEpis >= 0),
    longueurMoyenEpis numeric(10,2) check(longueurMoyenEpis >= 0),
    dateRecolte timestamp not null,
    foreign key (idparcelle) references parcelle(id)
);


create table parametreCroissance(
    id serial primary key,
    nom varchar(100) not null,
    valeur numeric(10,2) check(valeur > 0)
);

create table parametreFrequence(
    id serial primary key,
    nom varchar(100) not null,
    valeur interval check(valeur > '00:00:00')
);


