namespace MediTracker.Business.Dtos
{
    public class DoseDto
    {
        public int DoseId { get; set; }
        public int MedicationId { get; set; }
        public string MedicationName { get;set; }
        public string Amount { get; set; }
        public DateTime TakenAt { get; set; }
    }
}
