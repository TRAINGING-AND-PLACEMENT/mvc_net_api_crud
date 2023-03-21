using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demoweb.Models
{
    public class UserView
    {
        [Key]
        [CsvHelper.Configuration.Attributes.Index(0)]
        public int Id { get; set; }

        [CsvHelper.Configuration.Attributes.Index(1)]
        public string Names { get; set; }

        [CsvHelper.Configuration.Attributes.Index(2)]
        public string Email { get; set; }
    }
}
