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

    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
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
            //return Json(new { data = _unitOfWork.CoverType.GetAll() });
            return Json(new { data = _unitOfWork.SP_Call.List<CoverType>(SD.SP_GetCoverTypes) });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            //var coverTypeInDb = _unitOfWork.CoverType.Get(id);
            DynamicParameters param = new DynamicParameters();
            param.Add("id", id);
            var coverTypeInDb = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.SP_GetCoverType, param);
            if (coverTypeInDb == null)
                return Json(new { success = false, message =  "Something went wrong while delete data !!!" });
            //_unitOfWork.CoverType.Remove(coverTypeInDb);
            //_unitOfWork.Save();
            _unitOfWork.SP_Call.Execute(SD.SP_DeleteCoverType, param);
            return Json(new {success=true,message="data deleted successfully !!!" });
        }
        #endregion
        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if(id == null) return View(coverType);
            coverType = _unitOfWork.CoverType.Get(id.GetValueOrDefault());
            //DynamicParameters param = new DynamicParameters();
            //param.Add("id", id.GetValueOrDefault());
            //coverType = _unitOfWork.SP_Call.OneRecord<CoverType>
            //    (SD.SP_GetCoverType,param);

            if(coverType==null) return NotFound();
            return View(coverType);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if (coverType == null) return BadRequest();
            if (!ModelState.IsValid) return View(coverType);
            //DynamicParameters param = new DynamicParameters();
            //param.Add("name", coverType.Name);
            if (coverType.Id == 0)
                _unitOfWork.CoverType.Add(coverType);
            //_unitOfWork.SP_Call.Execute(SD.SP_CreateCoverType, param);
            else
            {
                _unitOfWork.CoverType.Update(coverType);
            }
            _unitOfWork.Save();
            //{
            //    param.Add("id", coverType.Id);
            //    _unitOfWork.SP_Call.Execute(SD.SP_UpdateCoverType, param);
            //}
            return RedirectToAction("Index");
        }
    }
}
    