using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReviewsProjectAPI.Models
{
    public class Star
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

        public Star(string name, string bio, DateTime dateOfBirth, string image)
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
    }
}
