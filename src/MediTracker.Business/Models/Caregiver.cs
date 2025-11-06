using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediTracker.Business.Models
{
    public class Caregiver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Email { get; set; }
        public bool HasFullAccess { get; set; }
    }
}
