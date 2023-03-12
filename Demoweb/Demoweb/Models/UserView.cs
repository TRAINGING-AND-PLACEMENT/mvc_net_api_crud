using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Demoweb.Models
{
    public class UserView
    {
        [Key]
        [Index(0)]
        public int Id { get; set; }
        [Index(1)]
        public string Name { get; set; }
        [Index(2)]
        public string Email { get; set; }
    }
}
