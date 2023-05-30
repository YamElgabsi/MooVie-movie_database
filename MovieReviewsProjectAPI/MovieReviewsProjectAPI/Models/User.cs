using System.ComponentModel.DataAnnotations;

namespace MovieReviewsProjectAPI.Models
{
    public class User
    {
        private string username;

        [Key]
        public string Username {
            get { return username; }
            set { username = value.ToLower(); }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Image { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsAdmin { get; set; }

        public User()
        {
            Username = "default";
            FirstName = "Def";
            LastName = "Fault";
            Email = "default@example.com";
            Image = "";
            Password = "password";
            DateOfBirth = DateTime.Now;
            IsAdmin = false;
        }

        public User(string username, string firstName, string lastName, string email, string password, DateTime dateOfBirth, bool isAdmin)
        {
            Username = username.ToLower();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Image = "";
            DateOfBirth = dateOfBirth;
            IsAdmin = isAdmin;
        }

        public int GetAge()
        {
            DateTime now = DateTime.Today;
            int age = now.Year - DateOfBirth.Year;
            return age;
        }
    }

}
