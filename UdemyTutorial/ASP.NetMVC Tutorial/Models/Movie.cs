using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ASP.NetMVC_Tutorial.Models
{
    [Bind(Exclude = "Id")]
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public byte GenreId { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public DateTime DateAdded { get; set; } = DateTime.Now;

        [Required]
        [Range(1,20)]
        [Display(Name = "Number in Stock")]
        public short NumberInStock { get; set; }

        [Required]
        public Genre Genre { get; set; }
    }
}