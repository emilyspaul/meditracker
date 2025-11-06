using MediTracker.Business.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediTracker.Business.Models
{
    public class Medication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }


        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public Frequency DoseFrequency { get; set; }

        public bool NeedReminder { get; set;  }

        public IEnumerable<MedicationReminder>? Reminders { get; set; }
        public IEnumerable<DoseLog>? Doses { get; set; }

        public string UserId { get; set; }

    }
}
