using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Domain
{
    public interface IRepository<in TKey, T> where T:class
    {
        T Get(TKey key);

        IQueryable<T> GetAll();

        void Create(T entity);

        void SaveChanges();

        bool Exists(Expression<Func<T, bool>> expression);
    }
}
