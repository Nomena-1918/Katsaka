using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Katsaka.Data;
using Katsaka.Models;

namespace Katsaka.Pages.Recolte_gestion
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
        ViewData["Idparcelle"] = new SelectList(_context.Parcelles, "Id", "Nom");
            return Page();
        }

        [BindProperty]
        public Recolte Recolte { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if ( _context.Recoltes == null || Recolte == null)
            {
                return Page();
            }

            _context.Recoltes.Add(Recolte);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
