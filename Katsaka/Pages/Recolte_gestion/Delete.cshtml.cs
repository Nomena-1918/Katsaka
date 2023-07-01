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
    public class DeleteModel : PageModel
    {
        private readonly Katsaka.Data.KatsakaContext _context;

        public DeleteModel(Katsaka.Data.KatsakaContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Recolte Recolte { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Recoltes == null)
            {
                return NotFound();
            }

            var recolte = await _context.Recoltes.FirstOrDefaultAsync(m => m.Id == id);

            if (recolte == null)
            {
                return NotFound();
            }
            else 
            {
                Recolte = recolte;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Recoltes == null)
            {
                return NotFound();
            }
            var recolte = await _context.Recoltes.FindAsync(id);

            if (recolte != null)
            {
                Recolte = recolte;
                _context.Recoltes.Remove(Recolte);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
