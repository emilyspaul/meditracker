using MediTracker.Business.Dtos;
using MediTracker.Web.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MediTracker.Web.Areas.Reports.Pages
{
    public class HistoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HistoryModel(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<DoseDto> DoseHistory { get; set; }

        public async Task OnGet()
        {
            var userId = GetUserId();

            var rawDoseLog = await _context.DoseLogs.Include(x => x.Medication).Where(x => x.UserId == userId)
                                        .OrderByDescending(x => x.TakenAt).ToListAsync();

            DoseHistory = new List<DoseDto>();
            rawDoseLog.ForEach(x => DoseHistory.Add(new DoseDto
            {
                Amount = x.Amount,
                DoseId = x.Id,
                MedicationId = x.MedicationId,
                MedicationName = x.Medication.Name,
                TakenAt = x.TakenAt
            }));
        }

        internal string GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User?
                .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
