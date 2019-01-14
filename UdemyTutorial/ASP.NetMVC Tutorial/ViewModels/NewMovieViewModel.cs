using ASP.NetMVC_Tutorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NetMVC_Tutorial.ViewModels
{
    public class NewMovieViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public Movie Movie { get; set; }
    }
}