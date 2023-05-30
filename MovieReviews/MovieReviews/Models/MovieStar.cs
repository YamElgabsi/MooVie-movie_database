using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReviews.Models
{
    public class MovieStar
    {
        public string MovieStarId { get; set; }

        public string StarId { get; set; }

        public string MovieId { get; set; }

        public MovieStar()
        {
            MovieStarId = Guid.NewGuid().ToString();
            StarId = "";
            MovieId = "";
        }

        public MovieStar(string starId, string movieId)
        {
            MovieStarId = Guid.NewGuid().ToString();
            StarId = starId;
            MovieId = movieId;
        }
    }


}
