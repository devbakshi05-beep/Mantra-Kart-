using Ecom_Project_2026.Data;
using Ecom_Project_2026.DataAccess.Repository.IRepository;
using Ecom_Project_2026.Models;
using Ecom_Project_2026.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecom_Project_2024.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public UserController(IUnitOfWork unitOfWork,
            ApplicationDbContext context)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            var userList = _context.ApplicationUsers.ToList(); //AspNetUser
            var roles = _context.Roles.ToList(); //AspNetRoles
            var userRoles = _context.UserRoles.ToList(); //AspNetUserRole
            foreach (var user in userList)
            {
                var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(r => r.Id == roleId).Name;
                if (user.CompanyId == null)
                {
                    user.Company = new Company()
                    {
                        Name = ""
                    };
                }
                if (user.CompanyId != null)
                {
                    user.Company = new Company()
                    {
                        Name = _unitOfWork.Company.Get(Convert.ToInt32(user.CompanyId)).Name,
                    };
                }
            }
            //Remove Admin User
            var adminUser = userList.FirstOrDefault(u => u.Role == SD.Role_Admin);
            if (adminUser != null)
            {
                userList.Remove(adminUser);
            }
            return Json(new { data = userList });
        }
        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            bool isLocked = false;
            var userInDb = _context.ApplicationUsers.FirstOrDefault(au => au.Id == id);
            if (userInDb == null)
            {
                return Json(new
                {
                    sucess = false,
                    message = "Something went wrong while lock and unlock user !!!"
                });
            }
            if (userInDb != null && userInDb.LockoutEnd > DateTime.Now)
            {
                userInDb.LockoutEnd = DateTime.Now;
                isLocked = false;
            }
            else
            {
                userInDb.LockoutEnd = DateTime.Now.AddYears(100);
                isLocked = true;
            }
            _context.SaveChanges();
            return Json(new
            {
                success = true,
                message = isLocked == true ?
                "User successfully Locked " : "User successfully Unlocked "
            });
        }
        #endregion
    }
}


