using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Katsaka.Data;
using Katsaka.Models;

namespace Katsaka.Pages.ParametreFrequence_gestion
{
    public class DeleteModel : PageModel
    {
        private readonly Katsaka.Data.KatsakaContext _context;

        public DeleteModel(Katsaka.Data.KatsakaContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Parametrefrequence Parametrefrequence { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Parametrefrequences == null)
            {
                return NotFound();
            }

            var parametrefrequence = await _context.Parametrefrequences.FirstOrDefaultAsync(m => m.Id == id);

            if (parametrefrequence == null)
            {
                return NotFound();
            }
            else 
            {
                Parametrefrequence = parametrefrequence;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Parametrefrequences == null)
            {
                return NotFound();
            }
            var parametrefrequence = await _context.Parametrefrequences.FindAsync(id);

            if (parametrefrequence != null)
            {
                Parametrefrequence = parametrefrequence;
                _context.Parametrefrequences.Remove(Parametrefrequence);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
