using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Katsaka.Data;
using Katsaka.Models;

namespace Katsaka.Pages.Responsable_gestion
{
    public class IndexModel : PageModel
    {
        private readonly Katsaka.Data.KatsakaContext _context;

        public IndexModel(Katsaka.Data.KatsakaContext context)
        {
            _context = context;
        }

        public IList<Responsable> Responsable { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Responsables != null)
            {
                Responsable = await _context.Responsables.ToListAsync();
            }
        }
    }
}
