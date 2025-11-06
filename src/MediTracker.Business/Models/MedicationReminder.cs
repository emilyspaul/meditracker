using MediTracker.Business.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediTracker.Business.Models
{
    public class MedicationReminder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("MedicationId")]
        public Medication Medication { get; set; }

        public int ReminderTime { get; set; }
        public ReminderType ReminderType { get; set; }

    }
}
