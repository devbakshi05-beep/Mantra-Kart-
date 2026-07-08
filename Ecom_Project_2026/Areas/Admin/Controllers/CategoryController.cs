    using Dapper;
using Ecom_Project_2026.DataAccess.Repository.IRepository;
using Ecom_Project_2026.Models;
using Ecom_Project_2026.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecom_Project_2026.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]

    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
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
            //var categoryList = _unitOfWork.Category.GetAll();
            //return Json(new { data = categoryList });
            return Json(new { data = _unitOfWork.SP_Call.List<Category>(SD.SP_GetCategories)});
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            //var categoryInDb = _unitOfWork.Category.Get(id);
            DynamicParameters param = new DynamicParameters();
            param.Add("id", id);
            var categoryInDb = _unitOfWork.SP_Call.OneRecord<Category>(SD.SP_GetCategory, param);
            if (categoryInDb == null)
                return Json(new { success = false, message = "Something went wrong while delete data !!!" });
            if (categoryInDb == null)
                return Json(new { success = false,
                    message = "Something went wrong while Delete Data" });
            //_unitOfWork.Category.Remove(categoryInDb);
            //_unitOfWork.Save();
            _unitOfWork.SP_Call.Execute(SD.SP_DeleteCategory, param);
            return Json(new { success = true, message = "data deleted successfully !!!" });
         }
        #endregion
        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            //Create
            if (id == null) return View(category);
            //Edit
            category = _unitOfWork.Category.Get(id.GetValueOrDefault());
            //DynamicParameters param = new DynamicParameters();
            //param.Add("id", id.GetValueOrDefault());
            //category = _unitOfWork.SP_Call.OneRecord<Category>
            //    (SD.SP_GetCategory, param);

            if (category == null) return NotFound();
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (category == null) return NotFound();    
            if (!ModelState.IsValid) return View(category);
            //DynamicParameters param= new DynamicParameters();
            //param.Add("name", category.Name);
            if (category.Id == 0)
                //_unitOfWork.SP_Call.Execute(SD.SP_CreateCategory, param);
            _unitOfWork.Category.Add(category);
            else
            {
                //param.Add("id", category.Id);
                //_unitOfWork.SP_Call.Execute(SD.SP_UpdateCategory, param);
                _unitOfWork.Category.Update(category);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
