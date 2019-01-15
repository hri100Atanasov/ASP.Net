using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NetMVC_Tutorial.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }
    }
}