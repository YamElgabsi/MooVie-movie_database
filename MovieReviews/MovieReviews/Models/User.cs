using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReviews.Models
{
    public class User
    {
        private string username;

        [Key]
        public string Username
        {
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

        public override string ToString()
        {
            return this.Username;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            User otherUser = (User)obj;

            return Username.Equals(otherUser.Username);
        }
    }

}
