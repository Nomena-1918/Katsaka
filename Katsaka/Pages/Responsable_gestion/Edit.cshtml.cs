using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Katsaka.Data;
using Katsaka.Models;

namespace Katsaka.Pages.Responsable_gestion
{
    public class EditModel : PageModel
    {
        private readonly Katsaka.Data.KatsakaContext _context;

        public EditModel(Katsaka.Data.KatsakaContext context)
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

            var responsable =  await _context.Responsables.FirstOrDefaultAsync(m => m.Id == id);
            if (responsable == null)
            {
                return NotFound();
            }
            Responsable = responsable;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Attach(Responsable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResponsableExists(Responsable.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ResponsableExists(int id)
        {
          return (_context.Responsables?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
