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

namespace Katsaka.Pages.Recolte_gestion
{
    public class EditModel : PageModel
    {
        private readonly Katsaka.Data.KatsakaContext _context;

        public EditModel(Katsaka.Data.KatsakaContext context)
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

            var recolte =  await _context.Recoltes.FirstOrDefaultAsync(m => m.Id == id);
            if (recolte == null)
            {
                return NotFound();
            }
            Recolte = recolte;
           ViewData["Idparcelle"] = new SelectList(_context.Parcelles, "Id", "Nom");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {


            _context.Attach(Recolte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecolteExists(Recolte.Id))
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

        private bool RecolteExists(int id)
        {
          return (_context.Recoltes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
