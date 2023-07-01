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

namespace Katsaka.Pages.Parcelle_gestion
{
    public class EditModel : PageModel
    {
        private readonly Katsaka.Data.KatsakaContext _context;

        public EditModel(Katsaka.Data.KatsakaContext context)
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

            var parcelle =  await _context.Parcelles.FirstOrDefaultAsync(m => m.Id == id);
            if (parcelle == null)
            {
                return NotFound();
            }
            Parcelle = parcelle;
           ViewData["Idchamp"] = new SelectList(_context.Champs, "Id", "Id");
           ViewData["Idresponsable"] = new SelectList(_context.Responsables, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {


            _context.Attach(Parcelle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParcelleExists(Parcelle.Id))
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

        private bool ParcelleExists(int id)
        {
          return (_context.Parcelles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
