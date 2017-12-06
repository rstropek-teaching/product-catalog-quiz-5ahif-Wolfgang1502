using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Services;

namespace ProductCatalog.Model
{
    public class ProductsController : Controller
    {

        private IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View(productRepository.GetAll());
        }

        public IActionResult Details(int id)
        {
            var proddetails = this.productRepository.GetById(id);
            if (proddetails == null)
            {
                return NotFound();
            }
            return View(proddetails);
        }

        public IActionResult Create(Product prod)
        {
            if (ModelState.IsValid)
            {
                if (prod != null)
                {
                    this.productRepository.Add(prod);
                }
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            var productdelete = productRepository.GetById(id);
            if (productdelete == null)
            {
                return NotFound();
            }
            return View(productdelete);
        }

        public IActionResult DeletePermanently(int id)
        {
            productRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult FoundProducts(string name)
        {
            return View(productRepository.GetByName(name));
        }
    }
}