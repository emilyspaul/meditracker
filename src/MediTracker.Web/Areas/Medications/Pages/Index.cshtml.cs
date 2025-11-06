using MediTracker.Business.Models;
using MediTracker.Web.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MediTracker.Web.Areas.Medications.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public List<Medication> Medications { get; set; }

        public IndexModel(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task OnGet()
        {
            var userId = GetUserId();

            Medications = await _context.Medications.Where(x => x.UserId == userId).ToListAsync();
        }

        internal string GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User?
                .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
