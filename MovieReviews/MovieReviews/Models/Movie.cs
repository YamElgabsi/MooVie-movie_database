using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MovieReviews.Models

{
    public class CompanyUpdateRequest
    {
        public string OldCompanyName { get; set; }
        public string NewCompanyName { get; set; }
    }

    public class Movie
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Poster { get; set; }
        public string Director_Id { get; set; }
        public string Company { get; set; }
        public DateTime ReleaseDate { get; set; }

        public Movie()
        {
            Id = Guid.NewGuid().ToString();
            Title = string.Empty;
            Poster = string.Empty;
            Director_Id = string.Empty;
            Company = string.Empty;
            ReleaseDate = DateTime.MinValue;
        }

        public Movie(string title, string poster, string director_id, string company, DateTime releaseDate)
        {
            Id = Guid.NewGuid().ToString();
            Title = title;
            Poster = poster;
            Director_Id = director_id;
            Company = company;
            ReleaseDate = releaseDate;
        }

        public override string ToString()
        {
            return $"{Title} | {Company} | {Id} ";
        }
    }

}



