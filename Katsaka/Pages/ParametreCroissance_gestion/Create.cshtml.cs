using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Katsaka.Data;
using Katsaka.Models;

namespace Katsaka.Pages.ParametreCroissance_gestion
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
            return Page();
        }

        [BindProperty]
        public Parametrecroissance Parametrecroissance { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Parametrecroissances == null || Parametrecroissance == null)
            {
                return Page();
            }

            _context.Parametrecroissances.Add(Parametrecroissance);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
