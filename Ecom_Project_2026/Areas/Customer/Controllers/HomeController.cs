using System.Diagnostics;
using System.Security.Claims;
using Ecom_Project_2026.DataAccess.Repository.IRepository;
using Ecom_Project_2026.Models;
using Ecom_Project_2026.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecom_Project_2026.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                var count = _unitOfWork.ShoppingCart.GetAll(sc => sc.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.Ss_CartSessionCount, count);
            }
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return View(productList);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ShippingReturns()
        {
            return View();
        }
        public IActionResult FAQa()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Details(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                var count = _unitOfWork.ShoppingCart.GetAll(sc => sc.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.Ss_CartSessionCount, count);
            }
            //***
            var productInDb = _unitOfWork.Product.FirstOrDefault(p => p.Id == id,
                includeProperties: "Category,CoverType");
            if (productInDb == null) return NotFound();
            var shoppingCart = new ShoppingCart()
            {
                Product = productInDb,
                ProductId = productInDb.Id,
            };
            return View(shoppingCart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingcart)
        {
            shoppingcart.Id = 0;
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                if (claims == null) return NotFound();
                shoppingcart.ApplicationUserId = claims.Value;

                var shoppingCartInDb = _unitOfWork.ShoppingCart.FirstOrDefault(sc => sc.ApplicationUserId == claims.Value &&
                sc.ProductId == shoppingcart.ProductId);

                if (shoppingCartInDb == null)
                    _unitOfWork.ShoppingCart.Add(shoppingcart);
                else
                    shoppingCartInDb.Count += shoppingcart.Count;
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));

            }
            else
            {
                {
                    var productInDb = _unitOfWork.Product.FirstOrDefault(p => p.Id == shoppingcart.ProductId,
                        includeProperties: "Category,CoverType");
                    if (productInDb == null) return NotFound();
                    var shoppingCartEdit = new ShoppingCart()
                    {
                        Product = productInDb,
                        ProductId = productInDb.Id,
                    };
                    return View(shoppingCartEdit);
                }
            }
            //return View();
        }
    }
}

