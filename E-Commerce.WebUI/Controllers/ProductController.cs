using E_Commerce.Business.Abstract;
using E_Commerce.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index(int page=1,int category=0,char alpha='A',char from='L')
        {
            var param = HttpContext.Request.Query["alpha"];
            var products = _productService.GetByCategory(category);
            if (param == "Z")
            {
                products = products.OrderByDescending(p=>p.ProductName).ToList();
            }
            else
            {
                products = products.OrderBy(p=>p.ProductName).ToList();
            }

            if (from == 'H')
            {
                products = products.OrderByDescending(p => p.UnitPrice).ToList();
            }
            else
            {
                products = products.OrderBy(p => p.UnitPrice).ToList();
            }

            int pageSize = 10;
            var vm = new ProductListViewModel()
            {
                Products =products.Skip((page-1)*pageSize).Take(pageSize).ToList() 
            };
            return View(vm);
        }
    }
}
