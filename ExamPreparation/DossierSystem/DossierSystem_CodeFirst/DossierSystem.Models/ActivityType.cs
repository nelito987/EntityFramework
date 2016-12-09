using System.ComponentModel.DataAnnotations;
namespace DossierSystem.Models
{
    public class ActivityType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }
    }
}
