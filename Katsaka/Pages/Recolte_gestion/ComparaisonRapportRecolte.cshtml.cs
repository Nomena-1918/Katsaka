using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Katsaka.Data;
using Katsaka.Models;

namespace Katsaka.Pages.Rapport_gestion
{
    public class ComparaisonRapportRecolteModel : PageModel
    {
        private readonly KatsakaContext _context;
        
        public ComparaisonRapportRecolteModel(KatsakaContext context)
        {
            _context = context;
        }

        public IList<VDerniersuiviAvantRecolte> VDerniersuiviAvantRecolte { get;set; } = default!;
        public DiffSuiviRecolte diffSuiviRecolte { get; set; } = default!;
        public Dictionary<string, string> listAnomalies { get; set; } = default!;

        public async Task OnGetAsync(int idparcelle)
        {
            if (_context.VDerniersuiviAvantRecoltes != null)
            {
                VDerniersuiviAvantRecolte = await _context.VDerniersuiviAvantRecoltes
                    .Where(v => v.Idparcelle == idparcelle)
                    .ToListAsync();

                // Différence de résultats
                diffSuiviRecolte = VDerniersuiviAvantRecolte[0].GetDiffSuiviRecolte();

                // Liste des anomalies
                listAnomalies = VDerniersuiviAvantRecolte[0].GetAnomalie(diffSuiviRecolte);

            }
        }
    }
}
