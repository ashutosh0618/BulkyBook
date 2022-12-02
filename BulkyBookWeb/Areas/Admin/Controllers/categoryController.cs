using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class categoryController : Controller
    {
        private readonly IUnitOfWork _iunitOfWork;
        private readonly ApplicationDbContext _context;
       
        public categoryController(IUnitOfWork iunitOfWork, ApplicationDbContext context)
        {
            _iunitOfWork = iunitOfWork;
            _context = context;
          
        }

        public IActionResult Index()
        {
            IEnumerable<category> objCategoryList = _iunitOfWork.Category.GetAll();//retrive all datan asign
            return View(objCategoryList);
        }

        //get action
        public IActionResult Create()
        {

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]//prevent cros req forgery
        public IActionResult Create(category obj)
        {
            //server side

            //custome validation 
            if (obj.name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Display cant match exactly match the name");//for name
                //ModelState.AddModelError("cust", "Display cant match exactly match the name");//summery
            }

            if (ModelState.IsValid)
            { // handle validation exception

                _iunitOfWork.Category.Add(obj);//add record
                _iunitOfWork.Save();//push db

                TempData["success"] = "created success fully";

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //get action
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // var categoryFromDb = _db.Categories.FirstOrDefault();//Return 1st element
            // var categoryFromDb = _db.Categories.SingleOrDefault(); return exception when more than one ele
            // var categoryFromDb = _db.Categories.Find(id);//find through pk

            var categoryFromDbFirst = _iunitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u=>u.Id==id);

            if (categoryFromDbFirst == null)
            {
                return NotFound();

            }
            return View(categoryFromDbFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]//prevent cros req forgery
        public IActionResult Edit(category obj)
        {
            //server side

            //custome validation 
            if (obj.name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Display cant match exactly match the name");//for name
                //ModelState.AddModelError("cust", "Display cant match exactly match the name");//summery
            }

            if (ModelState.IsValid)
            { // handle validation exception

                _iunitOfWork.Category.Update(obj);//update record
                _iunitOfWork.Save();//push db

                TempData["success"] = "Edited success fully";

                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //get action
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // var categoryFromDb = _db.Categories.FirstOrDefault();//Return 1st element
            // var categoryFromDb = _db.Categories.SingleOrDefault(); return exception when more than one ele
            //var categoryFromDb = _db.Categories.Find(id);//find through pk

            var categoryFromDbFirst = _iunitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u=>u.Id==id);

            if (categoryFromDbFirst == null)
            {
                return NotFound();

            }
            return View(categoryFromDbFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]//prevent cros req forgery
        public IActionResult DeletePost(int? id)
        {
            var obj = _iunitOfWork.Category.GetFirstOrDefault(u => u.Id == id);//find through pk


            if (obj == null)
            {
                return NotFound();

            }

            _iunitOfWork.Category.Remove(obj);//delete record
            _iunitOfWork.Save();//push db
            TempData["success"] = "Deleted success fully";
            return RedirectToAction("Index");


        }

    }
}
