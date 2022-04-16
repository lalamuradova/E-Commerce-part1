using E_Commerce.Business.Abstract;
using E_Commerce.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.WebUI.ViewComponents
{
    public class PageListViewComponent:ViewComponent
    {
        private IProductService _productService;

        public PageListViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public ViewViewComponentResult Invoke()
        {
            var current = HttpContext.Request.Query["page"];
            var intResult = int.TryParse(current, out int DataResult);
            var count = _productService.GetAll().Count();
            var pages = count / 10;
            var pagesNumbers = new List<int>();
            for (int i = 1; i <=pages; i++)
            {
                pagesNumbers.Add(i);
            }
            var model = new PageListViewModel
            {
                Pages = pagesNumbers,
                CurrentPage = intResult? DataResult:1
            };
            return View(model);
        }
    }
}
