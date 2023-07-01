using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Katsaka.Data;
using Katsaka.Models;

namespace Katsaka.Pages.Parcelle_gestion
{
    public class CreateModel : PageModel
    {
        private readonly Katsaka.Data.KatsakaContext _context;

        public CreateModel(Katsaka.Data.KatsakaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["Idchamp"] = new SelectList(_context.Champs, "Id", "Id");
        ViewData["Idresponsable"] = new SelectList(_context.Responsables, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Parcelle Parcelle { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if ( _context.Parcelles == null || Parcelle == null)
            {
                return Page();
            }

            _context.Parcelles.Add(Parcelle);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
