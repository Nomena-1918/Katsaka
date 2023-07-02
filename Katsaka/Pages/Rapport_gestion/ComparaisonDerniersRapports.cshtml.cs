using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Katsaka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Katsaka.Pages.Rapport_gestion
{
	public class ComparaisonDerniersRapportsModel : PageModel
    {
        int idparcelle;
        private readonly IConfiguration _configuration;
        public List<VListDernierSuivi> listDerniersSuivi { get; set; } = default!;
        public Dictionary<string, string> listAnomalies { get; set; } = default!;
        public DiffSuivi diffSuivis { get; set; } = default!;


        public ComparaisonDerniersRapportsModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet(int idparcelle)
        {
            this.idparcelle = idparcelle;
            listDerniersSuivi = new();
            diffSuivis = new();

            string connectionString = _configuration.GetConnectionString("KatsakaContext");

            using (var connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Sélection derniers suivis
                    string queryString = "select suivimais.*, parcelle.nom as nomparcelle from suivimais join parcelle on parcelle.id = suivimais.idparcelle where idparcelle=@idparcelle order by datesuivi desc limit 2;";
                    var command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText = queryString;
                    command.Parameters.AddWithValue("@idparcelle", this.idparcelle);

                    NpgsqlDataReader reader = command.ExecuteReader();

                    VListDernierSuivi suivimais;
                    while (reader.Read())
                    {
                        suivimais = new();

    //  id | idparcelle | longueurmoyenpousse | couleurmoyenpousse | nbrpousse | nbrepismoyenparpousse | longueurmoyenepis | datesuivi  | nomparcelle 

                        suivimais.Id = (int)reader["id"];
                        suivimais.Idparcelle = (int)reader["idparcelle"];
                        suivimais.Longueurmoyenpousse = (decimal)reader["longueurmoyenpousse"];
                        suivimais.Couleurmoyenpousse = (int)reader["couleurmoyenpousse"];
                        suivimais.Nbrpousse = (int)reader["nbrpousse"];
                        suivimais.Nbrepismoyenparpousse = (int)reader["nbrepismoyenparpousse"];
                        suivimais.Longueurmoyenepis = (decimal)reader["longueurmoyenepis"];
                        suivimais.Datesuivi = (DateTime)reader["datesuivi"];
                        suivimais.Nomparcelle = (string)reader["nomparcelle"];

                        listDerniersSuivi.Add(suivimais);
                    }
                    reader.Close();


                    if(listDerniersSuivi.Count > 1)
                        // Différence entre les 2 suivis
                        diffSuivis = listDerniersSuivi[0].GetDiff(connection, listDerniersSuivi[1]);

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }

            // Liste des anomalies
            listAnomalies = listDerniersSuivi[0].GetAnomalie(diffSuivis);

            // Boucle Dictionnaire C#
            /*
             foreach(KeyValuePair<int, string> kvp in flowerNames)
                Console.WriteLine("Key: {0}, Value: {1}", kvp.Key, kvp.Value);
             */
        }
    }
}
