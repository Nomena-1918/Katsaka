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

namespace Katsaka.Pages.ParametreCroissance_gestion
{
    public class EditModel : PageModel
    {
        private readonly Katsaka.Data.KatsakaContext _context;

        public EditModel(Katsaka.Data.KatsakaContext context)
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

            var parametrecroissance =  await _context.Parametrecroissances.FirstOrDefaultAsync(m => m.Id == id);
            if (parametrecroissance == null)
            {
                return NotFound();
            }
            Parametrecroissance = parametrecroissance;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Parametrecroissance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParametrecroissanceExists(Parametrecroissance.Id))
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

        private bool ParametrecroissanceExists(int id)
        {
          return (_context.Parametrecroissances?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
