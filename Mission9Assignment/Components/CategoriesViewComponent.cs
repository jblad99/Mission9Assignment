using Microsoft.AspNetCore.Mvc;
using Mission9Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9Assignment.Components
{
    public class CategoriesViewComponent : ViewComponent
    {
        //Separate view for the categories component class
        private IBookStoreRepository repo { get; set; }
        public CategoriesViewComponent(IBookStoreRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedType = RouteData?.Values["category"];

            //Filters by selected category
            var categories = repo.Books
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return View(categories);
        }
    }
}
