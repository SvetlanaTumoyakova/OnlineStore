using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;

namespace OnlineStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCategoryController : Controller
    {
        private OnlineStoreDBContext _dbContext;
        public ProductCategoryController(OnlineStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
