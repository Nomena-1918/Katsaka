using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Katsaka.Data;
using Katsaka.Models;

namespace Katsaka.Pages.Responsable_gestion
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
        public Responsable Responsable { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (_context.Responsables == null || Responsable == null)
            {
                return Page();
            }

            _context.Responsables.Add(Responsable);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
