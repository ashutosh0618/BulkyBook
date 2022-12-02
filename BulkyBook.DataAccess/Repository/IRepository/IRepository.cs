using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T- category
        T GetFirstOrDefault(Expression<Func <T, bool>> filter,string? includeProperties =null);
        IEnumerable<T> GetAll(string? includeProperties = null);//get all category
        void Add(T entity);//add category
        void Remove(T entity);//delete category
        void RemoveRamge(IEnumerable<T> entity);//collection of entity

    
    }
}
