using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demoweb.Models
{
    public class UserView
    {
        [Key]
        [ValidateNever]
        [CsvHelper.Configuration.Attributes.Index(0)]
        public int id { get; set; }

        [CsvHelper.Configuration.Attributes.Index(1)]
        [ValidateNever]
        public string names { get; set; }

        [CsvHelper.Configuration.Attributes.Index(2)]
        public string email { get; set; }

        [CsvHelper.Configuration.Attributes.Index(3)]
        public string password { get; set; }

        [CsvHelper.Configuration.Attributes.Index(4)]
        [ValidateNever]
        public string status { get; set; }

        [ValidateNever]
        [CsvHelper.Configuration.Attributes.Index(5)]
        public string role { get; set; }
    }
}
