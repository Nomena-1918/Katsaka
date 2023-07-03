using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Katsaka.Data;
using Katsaka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Katsaka.Pages.Recolte_gestion
{
	public class PrevisionRecolteModelModel : PageModel
    {

        private readonly KatsakaContext _context;
        private readonly IConfiguration _configuration;
        public PrevisionRecolte prevision { get; set; }
        public Recolte recolte_ref { get; set; }
        public List<Recolte> listRecoltePrevuMin { get; set; }
        public List<Recolte> listRecoltePrevuLinear { get; set; }


        public PrevisionRecolteModelModel(KatsakaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task OnGetAsync(int idparcelle)
        {
            listRecoltePrevuMin = new();
            listRecoltePrevuLinear = new();

            var listSuivi = await _context.Recoltes
                    .Where(v => v.Idparcelle == idparcelle)
                    .ToListAsync();

            // DerniersuiviAvantRecolte de référence
            recolte_ref = listSuivi[0];

            // Sélection des derniers suivis des récoltes
            var derniersSuivi = await _context.VListDernierSuivis
                    .Where(v => v.Idparcelle != idparcelle)
                    .ToListAsync();

            string connectionString = _configuration.GetConnectionString("KatsakaContext");


            using (var connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Set CoeffKgCmEpis de maïs
                    PrevisionRecolte prevision = new(recolte_ref);
                    Recolte r, r1;
                    VListDernierSuivi avantDernierSuivi;

                    foreach (VListDernierSuivi dernierSuivi in derniersSuivi) {

                        // Récolte prévue à partir d'un suivi maïs
                        r = prevision.getPrevisionRecolteMin(dernierSuivi);

                        // Récolte calculée de manière linéaire
                        avantDernierSuivi = null;
                        List<VListDernierSuivi> listDernierSuivi = VListDernierSuivi.GetDerniersRapports(connection, idparcelle);
                        if (listDernierSuivi.Count > 0)
                            avantDernierSuivi = listDernierSuivi[1];

                        prevision.setEvolutionLinear(dernierSuivi, avantDernierSuivi);
                        r1 = prevision.getPrevisionRecolteLinear();
                        
                        listRecoltePrevuMin.Add(r);
                        listRecoltePrevuLinear.Add(r1);
                    }

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
        }
    }
}
