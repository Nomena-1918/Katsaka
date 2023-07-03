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

                    // Sélection des 2 derniers rapports
                    listDerniersSuivi = VListDernierSuivi.GetDerniersRapports(connection, idparcelle);


                    if (listDerniersSuivi.Count > 1)
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

        }
    }
}
