using MediTracker.Business;
using MediTracker.Business.Constants;
using MediTracker.Business.Models;
using MediTracker.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MediTracker.Web.Areas.Caregivers.Pages
{
    public class NewCaregiverModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NewCaregiverModel(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public Caregiver Caregiver { get; set; }

        public void OnGet()
        {
            Caregiver = Caregiver ?? new Caregiver();
            Caregiver.UserId = GetUserId();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                OnGet(); 
                return Page();
            }

            _context.Caregivers.Add(Caregiver);
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
