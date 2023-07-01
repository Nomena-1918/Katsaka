
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Katsaka.Models;

namespace Katsaka.Pages.Parcelle_gestion
{
    public class IndexModel : PageModel
    {
        private readonly Data.KatsakaContext _context;

        public IndexModel(Data.KatsakaContext context)
        {
            _context = context;
        }

        public IList<Parcelle> Parcelle { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Parcelles != null)
            {
                Parcelle = await _context.Parcelles
                .Include(p => p.IdchampNavigation)
                .Include(p => p.IdresponsableNavigation).ToListAsync();
            }
        }
    }
}
