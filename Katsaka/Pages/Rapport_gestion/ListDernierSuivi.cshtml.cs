using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Katsaka.Data;
using Katsaka.Models;

namespace Katsaka.Pages.Rapport_gestion.Dernier_suivi
{
    public class ListDernierSuiviModel : PageModel
    {
        private readonly Katsaka.Data.KatsakaContext _context;

        public ListDernierSuiviModel(Katsaka.Data.KatsakaContext context)
        {
            _context = context;
        }

        public IList<VListDernierSuivi> VListDernierSuivi { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.VListDernierSuivis != null)
            {
                VListDernierSuivi = await _context.VListDernierSuivis.ToListAsync();
            }
        }
    }
}
