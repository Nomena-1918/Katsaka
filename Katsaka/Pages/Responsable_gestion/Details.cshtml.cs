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
    public class DetailsModel : PageModel
    {
        private readonly Katsaka.Data.KatsakaContext _context;

        public DetailsModel(Katsaka.Data.KatsakaContext context)
        {
            _context = context;
        }

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
    }
}
