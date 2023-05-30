using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReviews.Models
{
    public class Star : IEquatable<Star>
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Image { get; set; }

        public Star()
        {
            Id = Guid.NewGuid().ToString();
            Name = "Unknown";
            DateOfBirth = new DateTime(1900, 1, 1);
            Image = "";
        }

        public Star(string name, DateTime dateOfBirth, string image)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            DateOfBirth = dateOfBirth;
            Image = image;
        }
        public int GetAge()
        {
            DateTime now = DateTime.Today;
            int age = now.Year - DateOfBirth.Year;
            return age;
        }

        public override string ToString()
        {
            return $"{Name} | {Id} ";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Star otherUser = (Star)obj;

            return Id.Equals(otherUser.Id);
        }

        public bool Equals(Star? other)
        {
            if (other == null)
            {
                return false;
            }

            Star otherUser = (Star)other;

            return Id.Equals(otherUser.Id);
        }

        public override int GetHashCode() => (Id).GetHashCode();

    }
}
