using Anyar.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Anyor.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T> where T : class,new()
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
       
        void Delete(T entity);
        void SaveChanges();
      
    }
}
