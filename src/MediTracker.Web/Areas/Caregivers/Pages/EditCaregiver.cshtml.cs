using MediTracker.Business.Models;
using MediTracker.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MediTracker.Web.Areas.Caregivers.Pages
{
    [BindProperties(SupportsGet = true)]
    public class EditCaregiverModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditCaregiverModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Caregiver Caregiver { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Caregiver = _context.Find<Caregiver>(id);

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                OnGetAsync(Caregiver.Id); 
                return Page();
            }

            _context.Attach(Caregiver).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToPage("Index"); // Redirect to list page
        }
    }
}
