using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _iunitOfWork;
        

        private readonly ApplicationDbContext _context;
        public CoverTypeController(IUnitOfWork iunitOfWork, ApplicationDbContext context)
        {
            _iunitOfWork = iunitOfWork;
            _context = context;
          
            
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _iunitOfWork.CoverType.GetAll();//retrive all datan asign
            return View(objCoverTypeList);
        }

        //get action
        public IActionResult Create()
        {

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]//prevent cros req forgery
        public IActionResult Create(CoverType obj)
        {
            //server side

            

            if (ModelState.IsValid)
            { // handle validation exception

                _iunitOfWork.CoverType.Add(obj);//add record
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

            var CoverTypeFromDbFirst = _iunitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u=>u.Id==id);

            if (CoverTypeFromDbFirst == null)
            {
                return NotFound();

            }
            return View(CoverTypeFromDbFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]//prevent cros req forgery
        public IActionResult Edit(CoverType obj)
        {
            //server side


            if (ModelState.IsValid)
            { // handle validation exception

                _iunitOfWork.CoverType.Update(obj);//update record
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

            var CoverTypeFromDbFirst = _iunitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u=>u.Id==id);

            if (CoverTypeFromDbFirst == null)
            {
                return NotFound();

            }
            return View(CoverTypeFromDbFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]//prevent cros req forgery
        public IActionResult DeletePost(int? id)
        {
            var obj = _iunitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);//find through pk


            if (obj == null)
            {
                return NotFound();

            }

            _iunitOfWork.CoverType.Remove(obj);//delete record
            _iunitOfWork.Save();//push db
            TempData["success"] = "Deleted success fully";
            return RedirectToAction("Index");


        }

    }
}
