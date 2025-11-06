namespace MediTracker.Business.Dtos
{
    public class CaregiverReportDto
    {
        public IList<DoseDto> Doses { get; set; }
        public string Email { get; set; }
    }
}
