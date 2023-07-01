--database : katsaka
--user : nomena
--password : root
--connection string :  "KatsakaContext": "Host=localhost; Database=katsaka; Username=nomena; Password=root;"

DELETE FROM  parametrecroissance ;
DELETE FROM  parametrefrequence  ;
DELETE FROM  suivimais           ;
DELETE FROM  recolte             ;
DELETE FROM  parcelle            ;
DELETE FROM  responsable         ;
DELETE FROM  champ               ;




ALTER SEQUENCE  champ_id_seq                RESTART WITH 1;
ALTER SEQUENCE  parametrecroissance_id_seq  RESTART WITH 1;
ALTER SEQUENCE  parametrefrequence_id_seq   RESTART WITH 1;
ALTER SEQUENCE  parcelle_id_seq             RESTART WITH 1;
ALTER SEQUENCE  recolte_id_seq              RESTART WITH 1;
ALTER SEQUENCE  responsable_id_seq          RESTART WITH 1;
ALTER SEQUENCE  suivimais_id_seq            RESTART WITH 1;