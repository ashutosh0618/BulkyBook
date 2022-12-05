using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Model;
using BulkyBook.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _iunitOfWork;
        private readonly ApplicationDbContext _context;
        

        // public object Guid { get; private set; }

        public CompanyController(IUnitOfWork iunitOfWork,  ApplicationDbContext context)
        {
            _iunitOfWork = iunitOfWork;
            _context = context;

        }

        public IActionResult Index()
        {
            return View(); ;
        }

      

        //get action
        public IActionResult Upsert(int? id)
        {

            //Titely binded
            Company company = new();
                

            if (id == null || id == 0)
            {
                
                return View(company);
            }
            else
            {
                //fetch data n show in edit product
                company = _iunitOfWork.Company.GetFirstOrDefault(i => i.Id == id);
                return View(company);
            }
            
           
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]//prevent cros req forgery
        public IActionResult Upsert(Company obj)
        {
            //server side


            if (ModelState.IsValid)
            { // handle validation exception

                
                
                //**

                if (obj.Id == 0)
                {

                    _iunitOfWork.Company.Add(obj);//add record
                    TempData["success"] = "Company Added success fully";
                }
                else
                {
                    _iunitOfWork.Company.Update(obj);//add record
                    TempData["success"] = "Company updayed success fully";
                }

                _iunitOfWork.Save();//push db

                

                return RedirectToAction("Index");
            }
            return View(obj);
        }


       
        

        //Api
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var CompanyList = _iunitOfWork.Company.GetAll();
            return Json(new { data = CompanyList });
        }


        //POST
        [HttpDelete]
        //prevent cros req forgery
        public IActionResult Delete(int? id)
        {
            var obj = _iunitOfWork.Company.GetFirstOrDefault(u => u.Id == id);//find through pk


            if (obj == null)
            {
                return Json(new { success = false, message = "error while deleting" });

            }

           

            _iunitOfWork.Company.Remove(obj);//delete record
            _iunitOfWork.Save();//push db

            return Json(new { success = true, message = " deleting..." });



        }
        #endregion

    }


}
