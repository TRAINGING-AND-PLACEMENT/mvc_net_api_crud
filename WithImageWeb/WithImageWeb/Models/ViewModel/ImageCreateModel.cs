using System.ComponentModel.DataAnnotations;

namespace WithImageWeb.Models.ViewModel
{
    public class ImageCreateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile FileName { get; set; }
    }
}
