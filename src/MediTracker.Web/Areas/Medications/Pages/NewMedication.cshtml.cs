using MediTracker.Business;
using MediTracker.Business.Constants;
using MediTracker.Business.Models;
using MediTracker.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MediTracker.Web.Areas.Medications.Pages
{
    public class NewMedicationModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NewMedicationModel(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public Medication Medication { get; set; }

        public IEnumerable<SelectListItem> FrequencyOptions { get; set; }

        public void OnGet()
        {
            Medication = Medication ?? new Medication();
            Medication.UserId = GetUserId();
            Medication.Description = Medication.Description ?? string.Empty;

            FrequencyOptions = Enum.GetValues(typeof(Frequency))
                .Cast<Frequency>()
                .Select(c => new SelectListItem
                {
                    Value = c.ToString(),
                    Text = c.GetDescription()
                });
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                OnGet(); 
                return Page();
            }

            _context.Medications.Add(Medication);
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
