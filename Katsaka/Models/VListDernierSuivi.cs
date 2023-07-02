using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Npgsql;

namespace Katsaka.Models;

public partial class VListDernierSuivi
{
    [Key]
    public int? Id { get; set; }

    public int? Idparcelle { get; set; }

    public decimal? Longueurmoyenpousse { get; set; }

    public int? Couleurmoyenpousse { get; set; }

    public int? Nbrpousse { get; set; }

    public int? Nbrepismoyenparpousse { get; set; }

    public decimal? Longueurmoyenepis { get; set; }

    public DateTime? Datesuivi { get; set; }

    public string? Nomparcelle { get; set; }

    decimal paramLongueurCroissance { get; set; }




    public DiffSuivi GetDiff(NpgsqlConnection connection, VListDernierSuivi suiviPrecedent) {

        if (suiviPrecedent.Idparcelle != Idparcelle)
            throw new Exception("Idparcelle différent : \n\tsuiviPrecedent : "+ suiviPrecedent.Idparcelle + "\n\tsuivi actuel : "+this.Idparcelle);

        DiffSuivi diffSuivis = new();
        decimal paramLongueurCroissance = 0;
        try
        {
            // sélectionner des paramètres : nbr paramètre longueur moyen epis
            string queryString = "select * from parametrecroissance where nom='longueur epis' ;";
            var command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = queryString;
           
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                paramLongueurCroissance = (decimal)reader["valeur"];
            }
            reader.Close();
        }
        catch (Exception)
        {
            throw;
        }
        this.paramLongueurCroissance = paramLongueurCroissance;

        // diffSuivis = suiviActuel - suiviPrecedent
        //  id | idparcelle | longueurmoyenpousse | couleurmoyenpousse | nbrpousse | nbrepismoyenparpousse | longueurmoyenepis | datesuivi  | nomparcelle

        diffSuivis.Longueurmoyenpousse = Longueurmoyenpousse - suiviPrecedent.Longueurmoyenpousse;
        diffSuivis.Couleurmoyenpousse = Couleurmoyenpousse - suiviPrecedent.Couleurmoyenpousse;
        diffSuivis.Nbrpousse = Nbrpousse - suiviPrecedent.Nbrpousse;
        diffSuivis.Nbrepismoyenparpousse = Nbrepismoyenparpousse - suiviPrecedent.Nbrepismoyenparpousse;
        diffSuivis.Longueurmoyenepis = Longueurmoyenepis - suiviPrecedent.Longueurmoyenepis;
        diffSuivis.diffDate = (TimeSpan)(Datesuivi - suiviPrecedent.Datesuivi);


        return diffSuivis;
    }

    // Diffsuivis retourné par GetDiff
    public Dictionary<string, string> GetAnomalie(VListDernierSuivi diffSuivis) {
        Dictionary<string, string> listAnomalies = new();

        if(diffSuivis.Longueurmoyenpousse < 0) {
            listAnomalies.Add("Longueurmoyenpousse", "diminution");
        }

        if (diffSuivis.Couleurmoyenpousse < 0)
        {
            listAnomalies.Add("Couleurmoyenpousse", "diminution");
        }

        if (diffSuivis.Nbrpousse < 0)
        {
            listAnomalies.Add("Nbrpousse", "diminution");
        }

        if (diffSuivis.Nbrepismoyenparpousse < 0)
        {
            listAnomalies.Add("Nbrepismoyenparpousse", "diminution");
        }

        if(Longueurmoyenepis <= paramLongueurCroissance) {
            if (diffSuivis.Longueurmoyenepis <= 0)
            {
                listAnomalies.Add("Longueurmoyenepis", "maïs non-mature, l'épis n'a plus poussé");
            }
        }

        // Aucune anomalie détectée
        if (listAnomalies.Count == 0)
            listAnomalies.Add("aucun", "aucun");


        return listAnomalies;
    }
}
