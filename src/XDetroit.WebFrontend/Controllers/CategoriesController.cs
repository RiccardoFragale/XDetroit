using System.Web.Mvc;
using XDetroit.WebFrontend.Dal;
using XDetroit.WebFrontend.Models;

namespace XDetroit.WebFrontend.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IDataLayer dataLayer;

        public CategoriesController() : this(new DataLayer(new SqlDataProvider(new AppContext())))
        {
        }

        public CategoriesController(IDataLayer dataLayer)
        {
            this.dataLayer = dataLayer;
        }

        public ActionResult Index()
        {
            var viewModel = new VmCategoriesIndex
            {
                Categories = dataLayer.GetCategories(10, 0).Value
            };

            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(VmCategoryCreate model)
        {
            if (ModelState.IsValid)
            {
                dataLayer.CreateCategory(model.Category);
            }

            return RedirectToAction("Index");
        }

    }
}