using MediTracker.Business.Dtos;
using MediTracker.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MediTracker.Web.Areas.Caregivers.Pages
{
    [BindProperties(SupportsGet = true)]
    public class ReportModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ReportModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public CaregiverReportDto Report { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregiver = _context.Caregivers.Find(id);
            if (caregiver == null)
            {
                return NotFound();
            }

            var reporteeUser = _context.Users.Find(caregiver.UserId);
            Report.Email = reporteeUser.Email;

            var rawDoseLog = await _context.DoseLogs.Include(x => x.Medication).Where(x => x.UserId == caregiver.UserId)
                                    .OrderByDescending(x => x.TakenAt).ToListAsync();

            Report.Doses = new List<DoseDto>();
            rawDoseLog.ForEach(x => Report.Doses.Add(new DoseDto
            {
                Amount = x.Amount,
                DoseId = x.Id,
                MedicationId = x.MedicationId,
                MedicationName = x.Medication.Name,
                TakenAt = x.TakenAt
            }));


            return Page();
        }
    }
}
