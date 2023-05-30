using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReviews.Models
{
    public class Company
    {
        [Key]
        public string Name { get; set; }
        public string Logo { get; set; }
        public DateTime Founded { get; set; }
        public Company()
            : this("Default Company", "", new DateTime(1900, 1, 1))
        {
            // Default constructor calls parameterized constructor with default values
        }

        public Company(string name, string logo, DateTime founded)
        {
            Name = name;
            Logo = logo;
            Founded = founded;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}

