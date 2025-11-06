namespace MediTracker.Business.Dtos
{
    public class SharedForCaregiverDto
    {
        public int CaregiverId { get; set; }
        public string Email { get; set; }
        public bool HasFullAccess { get; set; }
    }
}
