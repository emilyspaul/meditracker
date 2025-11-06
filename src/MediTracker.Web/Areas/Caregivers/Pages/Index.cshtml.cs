using MediTracker.Business.Dtos;
using MediTracker.Business.Models;
using MediTracker.Web.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MediTracker.Web.Areas.Caregivers.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public List<Caregiver> Caregivers { get; set; }
        public List<SharedForCaregiverDto> Shared { get; set; }

        public IndexModel(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task OnGet()
        {
            var userId = GetUserId();
            Caregivers = await _context.Caregivers.Where(x => x.UserId == userId).ToListAsync();

            var email = User.Identity?.Name;
            var rawShared = await _context.Caregivers.Where(x => x.Email == email && x.HasFullAccess).ToListAsync();
            var currentUsers = await _context.Users.ToListAsync();

            Shared = new List<SharedForCaregiverDto>();

            rawShared.ForEach(x => Shared.Add(new SharedForCaregiverDto
            {
                CaregiverId = x.Id,
                Email = currentUsers.SingleOrDefault(u => u.Id == x.UserId)?.UserName,
                HasFullAccess = x.HasFullAccess
            }));
        }

        internal string GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User?
                .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
