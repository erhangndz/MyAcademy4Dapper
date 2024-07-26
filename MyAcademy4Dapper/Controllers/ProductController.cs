using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAcademy4Dapper.Dtos.ProductDtos;
using MyAcademy4Dapper.Services.CategoryServices;
using MyAcademy4Dapper.Services.ProductServices;

namespace MyAcademy4Dapper.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task CategoryDropdownAsync()
        {
            var categoryList = await _categoryService.GetAllCategoriesAsync();

            List<SelectListItem> categories = (from x in categoryList
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.CategoryId.ToString()
                                               }).ToList();
            ViewBag.categories = categories;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _productService.GetAllProductsAsync();
            return View(values);
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CreateProduct()
        {
            await CategoryDropdownAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            await _productService.CreateProductAsync(createProductDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateProduct(int id)
        {
            await CategoryDropdownAsync();
            var value = await _productService.GetProductByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            await _productService.UpdateProductAsync(updateProductDto);
            return RedirectToAction("Index");
        }
    }
}
