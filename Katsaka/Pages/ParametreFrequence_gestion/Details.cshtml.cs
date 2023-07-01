using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Katsaka.Data;
using Katsaka.Models;

namespace Katsaka.Pages.ParametreFrequence_gestion
{
    public class DetailsModel : PageModel
    {
        private readonly Katsaka.Data.KatsakaContext _context;

        public DetailsModel(Katsaka.Data.KatsakaContext context)
        {
            _context = context;
        }

      public Parametrefrequence Parametrefrequence { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Parametrefrequences == null)
            {
                return NotFound();
            }

            var parametrefrequence = await _context.Parametrefrequences.FirstOrDefaultAsync(m => m.Id == id);
            if (parametrefrequence == null)
            {
                return NotFound();
            }
            else 
            {
                Parametrefrequence = parametrefrequence;
            }
            return Page();
        }
    }
}
