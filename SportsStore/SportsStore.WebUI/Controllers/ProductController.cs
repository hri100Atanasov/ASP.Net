using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRespository repository;

        public ProductController(IProductRespository productRepository)
        {
            repository = productRepository;
        }

        public ViewResult List()
        {
            return View(repository.Products);
        }
    }
}