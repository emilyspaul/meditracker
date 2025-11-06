using MediTracker.Business;
using MediTracker.Business.Constants;
using MediTracker.Business.Models;
using MediTracker.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MediTracker.Web.Areas.Medications.Pages
{
    [BindProperties(SupportsGet = true)]
    public class EditMedicationModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditMedicationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Medication Medication { get; set; }

        public IEnumerable<SelectListItem> FrequencyOptions { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Medication = _context.Find<Medication>(id);

            FrequencyOptions = Enum.GetValues(typeof(Frequency))
                .Cast<Frequency>()
                .Select(c => new SelectListItem
                {
                    Value = c.ToString(),
                    Text = c.GetDescription()
                });

            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                OnGetAsync(Medication.Id); 
                return Page();
            }

            _context.Attach(Medication).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToPage("Index"); // Redirect to list page
        }
    }
}
