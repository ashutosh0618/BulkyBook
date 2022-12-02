using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }


        public void Update(Product obj)
        {
            //_db.Products.Update(obj);//On table
            var objFromDb = _db.Products.FirstOrDefault(u=>u.Id==obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title= obj.Title;// to restrict to update all fields
                objFromDb.ISBN= obj.ISBN;// to restrict to update all fields
                objFromDb.Price = obj.Price;// to restrict to update all fields
                objFromDb.Price50 = obj.Price50;// to restrict to update all fields
                objFromDb.Price100= obj.Price100;// to restrict to update all fields
                objFromDb.ListPrice= obj.ListPrice;// to restrict to update all fields
                objFromDb.Description= obj.Description;// to restrict to update all fields
                objFromDb.CategoryId= obj.CategoryId;// to restrict to update all fields
                objFromDb.Author= obj.Author;// to restrict to update all fields
                objFromDb.CategoryId= obj.CoverTypeId;// to restrict to update all fields
                        
                //if(obj.Image != null)
                //{
                //    objFromDb.Image= obj.Image;
                //}
            }
        }
    }
}
