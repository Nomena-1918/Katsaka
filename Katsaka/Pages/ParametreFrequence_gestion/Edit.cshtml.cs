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

namespace Katsaka.Pages.ParametreFrequence_gestion
{
    public class EditModel : PageModel
    {
        private readonly Katsaka.Data.KatsakaContext _context;

        public EditModel(Katsaka.Data.KatsakaContext context)
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

            var parametrefrequence =  await _context.Parametrefrequences.FirstOrDefaultAsync(m => m.Id == id);
            if (parametrefrequence == null)
            {
                return NotFound();
            }
            Parametrefrequence = parametrefrequence;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            _context.Attach(Parametrefrequence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParametrefrequenceExists(Parametrefrequence.Id))
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

        private bool ParametrefrequenceExists(int id)
        {
          return (_context.Parametrefrequences?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
