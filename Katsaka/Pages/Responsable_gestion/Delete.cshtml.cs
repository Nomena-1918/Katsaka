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
    public class DeleteModel : PageModel
    {
        private readonly Katsaka.Data.KatsakaContext _context;

        public DeleteModel(Katsaka.Data.KatsakaContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Responsable Responsable { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Responsables == null)
            {
                return NotFound();
            }

            var responsable = await _context.Responsables.FirstOrDefaultAsync(m => m.Id == id);

            if (responsable == null)
            {
                return NotFound();
            }
            else 
            {
                Responsable = responsable;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Responsables == null)
            {
                return NotFound();
            }
            var responsable = await _context.Responsables.FindAsync(id);

            if (responsable != null)
            {
                Responsable = responsable;
                _context.Responsables.Remove(Responsable);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
