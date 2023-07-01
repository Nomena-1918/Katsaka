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
    public class DeleteModel : PageModel
    {
        private readonly Katsaka.Data.KatsakaContext _context;

        public DeleteModel(Katsaka.Data.KatsakaContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Parcelles == null)
            {
                return NotFound();
            }
            var parcelle = await _context.Parcelles.FindAsync(id);

            if (parcelle != null)
            {
                Parcelle = parcelle;
                _context.Parcelles.Remove(Parcelle);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
