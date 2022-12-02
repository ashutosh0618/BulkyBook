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
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _iunitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ApplicationDbContext _context;
        

        // public object Guid { get; private set; }

        public ProductController(IUnitOfWork iunitOfWork, IWebHostEnvironment hostEnvironment, ApplicationDbContext context)
        {
            _iunitOfWork = iunitOfWork;
            _hostEnvironment = hostEnvironment;
            _context = context;

        }

        public IActionResult Index()
        {
            return View(); ;
        }

        //get action
        public IActionResult Create()
        {

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]//prevent cros req forgery
        public IActionResult Create(Product obj)
        {
            //server side

            

            if (ModelState.IsValid)
            { // handle validation exception

                _iunitOfWork.Product.Add(obj);//add record
                _iunitOfWork.Save();//push db

                TempData["success"] = "created success fully";

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //get action
        public IActionResult Upsert(int? id)
        {
            ////Product product = new();
            //////Projection using select
            ////// fetch all category
            ////IEnumerable<SelectListItem> CategoryList = _iunitOfWork.Category.GetAll().Select(
            ////    u => new SelectListItem
            ////    {
            ////        Text = u.name,
            ////        Value = u.Id.ToString()
            ////    });


            //////Projection using select
            ////// fetch all category
            ////IEnumerable<SelectListItem> CoverTypeList = _iunitOfWork.CoverType.GetAll().Select(
            ////    u => new SelectListItem
            ////    {
            ////        Text = u.Name,
            ////        Value = u.Id.ToString()
            ////    }); 


            //Titely binded
            ProductVM productVM = new()
            {
                product = new(),
                CategoryList = _iunitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.name,
                    Value = i.Id.ToString()
                }),

                CoverTypeList = _iunitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                // ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;@(ViewData["CoverTypeList"] as IEnumerable<SelectListItem>)
                //  ViewBag.CategoryList = CategoryList;

                return View(productVM);
            }
            else
            {
                //fetch data n show in edit product
                productVM.product = _iunitOfWork.Product.GetFirstOrDefault(i => i.Id == id);
                return View(productVM);
            }
            
           
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]//prevent cros req forgery
        public IActionResult Upsert(ProductVM obj,IFormFile file)
        {
            //server side


            if (ModelState.IsValid)
            { // handle validation exception

                //Add image to path product
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file!=null)
                {
                    string fileName = Guid.NewGuid().ToString(); 
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.product.Image != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.product.Image.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                    }
                    
                    using(var fileStrams = new FileStream (Path.Combine(uploads,fileName+extension),FileMode.Create))
                    {
                        file.CopyTo(fileStrams);
                    }
                    obj.product.Image = @"\images\products\" + fileName + extension;

                }
                //**

                if (obj.product.Id == 0)
                {

                    _iunitOfWork.Product.Add(obj.product);//add record
                }
                else
                {
                    _iunitOfWork.Product.Update(obj.product);//add record
                }

                _iunitOfWork.Save();//push db

                TempData["success"] = "Produuct Added success fully";

                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //get action
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    // var categoryFromDb = _db.Categories.FirstOrDefault();//Return 1st element
        //    // var categoryFromDb = _db.Categories.SingleOrDefault(); return exception when more than one ele
        //    //var categoryFromDb = _db.Categories.Find(id);//find through pk

        //    var ProductFromDbFirst = _iunitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
        //    // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u=>u.Id==id);

        //    if (ProductFromDbFirst == null)
        //    {
        //        return NotFound();

        //    }
        //    return View(ProductFromDbFirst);
        //}

        

        //Api
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _iunitOfWork.Product.GetAll(includeProperties:"Category,CoverType");
            return Json(new { data = productList });
        }


        //POST
        [HttpDelete]
        [ValidateAntiForgeryToken]//prevent cros req forgery
        public IActionResult Delete(int? id)
        {
            var obj = _iunitOfWork.Product.GetFirstOrDefault(u => u.Id == id);//find through pk


            if (obj == null)
            {
                return Json(new { success = false, message = "error while deleting" });

            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.Image.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _iunitOfWork.Product.Remove(obj);//delete record
            _iunitOfWork.Save();//push db

            return Json(new { success = true, message = " deleting..." });



        }
        #endregion

    }


}
