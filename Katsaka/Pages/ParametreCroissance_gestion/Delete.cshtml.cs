using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Katsaka.Data;
using Katsaka.Models;

namespace Katsaka.Pages.ParametreCroissance_gestion
{
    public class DeleteModel : PageModel
    {
        private readonly Katsaka.Data.KatsakaContext _context;

        public DeleteModel(Katsaka.Data.KatsakaContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Parametrecroissance Parametrecroissance { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Parametrecroissances == null)
            {
                return NotFound();
            }

            var parametrecroissance = await _context.Parametrecroissances.FirstOrDefaultAsync(m => m.Id == id);

            if (parametrecroissance == null)
            {
                return NotFound();
            }
            else 
            {
                Parametrecroissance = parametrecroissance;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Parametrecroissances == null)
            {
                return NotFound();
            }
            var parametrecroissance = await _context.Parametrecroissances.FindAsync(id);

            if (parametrecroissance != null)
            {
                Parametrecroissance = parametrecroissance;
                _context.Parametrecroissances.Remove(Parametrecroissance);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
