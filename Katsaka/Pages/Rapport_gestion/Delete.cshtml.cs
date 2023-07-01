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
    public class DeleteModel : PageModel
    {
        private readonly Katsaka.Data.KatsakaContext _context;

        public DeleteModel(Katsaka.Data.KatsakaContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Suivimai Suivimai { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Suivimais == null)
            {
                return NotFound();
            }

            var suivimai = await _context.Suivimais.FirstOrDefaultAsync(m => m.Id == id);

            if (suivimai == null)
            {
                return NotFound();
            }
            else 
            {
                Suivimai = suivimai;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Suivimais == null)
            {
                return NotFound();
            }
            var suivimai = await _context.Suivimais.FindAsync(id);

            if (suivimai != null)
            {
                Suivimai = suivimai;
                _context.Suivimais.Remove(Suivimai);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
