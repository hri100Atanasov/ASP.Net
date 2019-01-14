using System;
using System.ComponentModel.DataAnnotations;

namespace ASP.NetMVC_Tutorial.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public byte GenreId { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public DateTime DateAdded { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Number in Stock")]
        public short NumberInStock { get; set; }

        public Genre Genre { get; set; }
    }
}