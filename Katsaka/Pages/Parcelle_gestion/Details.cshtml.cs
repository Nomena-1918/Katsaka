using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Katsaka.Data;
using Katsaka.Models;

namespace Katsaka.Pages.Parcelle_gestion
{
    public class DetailsModel : PageModel
    {
        private readonly Katsaka.Data.KatsakaContext _context;

        public DetailsModel(Katsaka.Data.KatsakaContext context)
        {
            _context = context;
        }

      public Parcelle Parcelle { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Parcelles == null)
            {
                return NotFound();
            }

            var parcelle = await _context.Parcelles.FirstOrDefaultAsync(m => m.Id == id);
            if (parcelle == null)
            {
                return NotFound();
            }
            else 
            {
                Parcelle = parcelle;
            }
            return Page();
        }
    }
}
