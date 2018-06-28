using System.Web.Mvc;
using XDetroit.WebFrontend.Dal;
using XDetroit.WebFrontend.Models;

namespace XDetroit.WebFrontend.Controllers
{
    public class ProductsController : Controller
    {
        private IDataLayer dataLayer;

        public ProductsController() : this(new DataLayer(new SqlDataProvider(new AppContext())))
        {
        }

        public ProductsController(IDataLayer dataLayer)
        {
            this.dataLayer = dataLayer;
        }


        public ActionResult Index(int categoryId)
        {
            var productsSummary = new VmProductsIndex
            {
                Products = dataLayer.GetProducts().Value
            };

            return View(productsSummary);
        }
    }
}