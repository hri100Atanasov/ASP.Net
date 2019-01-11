using System;
using System.ComponentModel.DataAnnotations;

namespace ASP.NetMVC_Tutorial.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public DateTime DateAdded { get; set; } = DateTime.Now;
        [Required]
        public short NumberInStock { get; set; }
    }
}