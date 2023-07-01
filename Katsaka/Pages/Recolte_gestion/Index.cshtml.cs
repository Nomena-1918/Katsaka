using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Katsaka.Data;
using Katsaka.Models;

namespace Katsaka.Pages.Recolte_gestion
{
    public class IndexModel : PageModel
    {
        private readonly Katsaka.Data.KatsakaContext _context;

        public IndexModel(Katsaka.Data.KatsakaContext context)
        {
            _context = context;
        }

        public IList<Recolte> Recolte { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Recoltes != null)
            {
                Recolte = await _context.Recoltes
                .Include(r => r.IdparcelleNavigation).ToListAsync();
            }
        }
    }
}
