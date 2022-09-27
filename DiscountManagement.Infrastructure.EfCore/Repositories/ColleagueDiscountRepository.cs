using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;

namespace DiscountManagement.Infrastructure.EfCore.Repositories
{
    public class ColleagueDiscountRepository:RepositoryBase<long, ColleagueDiscount>,IColleagueDiscountRepository
    {
        private readonly ShopContext _shopContext;

        public ColleagueDiscountRepository(DiscountContext context,ShopContext shopContext) : base(context)
        {
            _shopContext = shopContext;
        }

        public Dictionary<long, string> GetProducts()
        {
            var items = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();

            return items.ToDictionary(x => x.Id, x => x.Name);
        }
    }
}
