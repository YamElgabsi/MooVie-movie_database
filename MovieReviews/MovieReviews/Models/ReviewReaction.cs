using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReviews.Models
{
    public class ReviewReaction
    {
        [Key]
        public string ReviewReaction_Id { get; set; }

        public string Username_Review { get; set; }

        public string Username_React { get; set; }

        public string MovieId { get; set; }

        public bool IsLike { get; set; }

        public ReviewReaction()
        {
            ReviewReaction_Id = Guid.NewGuid().ToString();
            Username_Review = "";
            Username_React = "";
            MovieId = "";
            IsLike = false;
        }

        public ReviewReaction(string username_review, string username_react, string movieId, bool isLike)
        {
            ReviewReaction_Id = Guid.NewGuid().ToString();
            Username_Review = username_review;
            Username_React = username_react;
            MovieId = movieId;
            IsLike = isLike;
        }
    }


}
