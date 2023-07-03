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
        public VDerniersuiviAvantRecolte suivi_recolte_ref { get; set; }
        public List<Recolte> listRecoltePrevu { get; set; }


        public PrevisionRecolteModelModel(KatsakaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task OnGetAsync(int idparcelle)
        {
            listRecoltePrevu = new();

            var listSuivi = await _context.VDerniersuiviAvantRecoltes
                    .Where(v => v.Idparcelle == idparcelle)
                    .ToListAsync();

            // DerniersuiviAvantRecolte de référence
            suivi_recolte_ref = listSuivi[0];

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

                    Recolte r;
                    PrevisionRecolte prevision;

                    foreach(VListDernierSuivi suiviPourPrev in derniersSuivi) {
                        r = new();
                        prevision = new();

                        prevision.setListDerniersSuivi(connection, suiviPourPrev);
                        prevision.setCoeffs();
                        
                        r = prevision.getPrevisionRecolte(suivi_recolte_ref);

                        listRecoltePrevu.Add(r);
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
