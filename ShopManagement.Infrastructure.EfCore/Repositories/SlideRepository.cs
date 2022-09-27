using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.EfCore.Repositories
{
    public class SlideRepository:RepositoryBase<long,Slide>,ISlideRepository
    {
        public SlideRepository(ShopContext context) : base(context)
        {
        }
    }
}
