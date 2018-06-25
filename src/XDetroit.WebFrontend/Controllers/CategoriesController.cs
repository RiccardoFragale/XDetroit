using System.Collections.Generic;
using System.Web.Mvc;
using XDetroit.WebFrontend.Models;

namespace XDetroit.WebFrontend.Controllers
{
    public class CategoriesController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new VmCategoriesIndex
            {
                Categories = new List<ItemCategory>()
            };

            return View(viewModel);
        }

        public ActionResult Create()
        {
            throw new System.NotImplementedException();
        }
    }
}