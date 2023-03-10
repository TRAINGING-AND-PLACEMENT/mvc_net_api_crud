using System.ComponentModel.DataAnnotations;

namespace Demoweb.Models
{
    public class Demo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 50, ErrorMessage = "Rank must be in range 1 to 50 only!!")]
        public int Rank { get; set; }
        public string Performance { get; set; }
    }
}

