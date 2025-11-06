using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediTracker.Business.Models
{
    public class DoseLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey("MedicationId")]
        public Medication Medication { get; set; }
        public int MedicationId { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime TakenAt { get; set; } = DateTime.Now;
        public string Amount { get; set;  }
    }
}
