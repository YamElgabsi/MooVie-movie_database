using System.ComponentModel.DataAnnotations;

namespace MovieReviews.Models
{
    public class Genre
    {
        private string name;

        [Key]
        public string Name
        {
            get { return name; }
            set { name = CapitalizeFirstLetter(value); }
        }

        public Genre()
        {
            Name = "";
        }

        public Genre(string name)
        {
            Name = name;
        }

        private string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var firstChar = input.Substring(0, 1).ToUpper();
            var restOfWord = input.Substring(1).ToLower();

            return firstChar + restOfWord;
        }
    }

}
