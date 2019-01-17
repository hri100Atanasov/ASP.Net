﻿using ASP.NetMVC_Tutorial.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NetMVC_Tutorial.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public byte GenreId { get; set; }

        public GenreDto Genre { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public DateTime DateAdded { get; set; } = DateTime.Now;

        [Required]
        [Range(1, 20)]
        public short NumberInStock { get; set; }
    }
}