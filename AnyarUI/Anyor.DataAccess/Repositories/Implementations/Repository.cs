using Anyor.DataAccess.Contexts;
using Anyor.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Anyor.DataAccess.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;
        public Repository(AppDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();

        }
        public IQueryable<T> GetAll()
        {
           return  _table.AsQueryable();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
         return  _table.Where(expression);
        }

        public T GetById(int id)
        {
          return _table.Find(id);
        }
        public void Create(T entity)
        {
           _table.Add(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }


        public void Update(T entity)
        {
          _table.Update(entity);
        }

        public void SaveChanges()
        {
          _context.SaveChanges();
        }
    }
}
