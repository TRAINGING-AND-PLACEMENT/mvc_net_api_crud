using System.ComponentModel.DataAnnotations;

namespace WithImageWeb.Models
{
    public class ImageModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "please Enter name ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Select File ")]
        [Display(Name = "Choose File")]
        public string FileName { get; set; }
    }
}
