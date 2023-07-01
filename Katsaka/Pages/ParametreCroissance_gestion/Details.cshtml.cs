using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Katsaka.Data;
using Katsaka.Models;

namespace Katsaka.Pages.ParametreCroissance_gestion
{
    public class DetailsModel : PageModel
    {
        private readonly Katsaka.Data.KatsakaContext _context;

        public DetailsModel(Katsaka.Data.KatsakaContext context)
        {
            _context = context;
        }

      public Parametrecroissance Parametrecroissance { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Parametrecroissances == null)
            {
                return NotFound();
            }

            var parametrecroissance = await _context.Parametrecroissances.FirstOrDefaultAsync(m => m.Id == id);
            if (parametrecroissance == null)
            {
                return NotFound();
            }
            else 
            {
                Parametrecroissance = parametrecroissance;
            }
            return Page();
        }
    }
}
