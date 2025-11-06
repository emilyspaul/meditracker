using MediTracker.Business.Models;
using MediTracker.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MediTracker.Web.Areas.Medications.Pages
{
    [BindProperties(SupportsGet = true)]
    public class DoseModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DoseModel(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public DoseLog Dose { get; set; }

        public IList<SelectListItem> Medications { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id, int? medicationId)
        {
            var userId = GetUserId();
            var rawMedications = await _context.Medications.Where(x => x.UserId == userId).ToListAsync();

            rawMedications.ForEach(x => Medications.Add(new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }));

            if (id == null)
            {
                Dose = Dose ?? new DoseLog();
                Dose.UserId = userId;

                if (medicationId.HasValue)
                {
                    Dose.MedicationId = medicationId.Value;
                }
                return Page();
            }

            Dose = _context.Find<DoseLog>(id);

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var userId = GetUserId();
            var rawMedication = await _context.Medications.FindAsync(Dose.MedicationId);
            Dose.Medication = rawMedication;

            //if (!ModelState.IsValid)
            //{
            //    OnGetAsync(Dose.Id);
            //    return Page();
            //}

            if (Dose.Id > 0)
            {
                _context.Attach(Dose).State = EntityState.Modified;
            }
            else
            {
                _context.DoseLogs.Add(Dose);
            }

            _context.SaveChanges();

            return RedirectToPage("Index"); // Redirect to list page
        }

        internal string GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User?
                .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
