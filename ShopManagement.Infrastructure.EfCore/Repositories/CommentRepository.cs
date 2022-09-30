using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Infrastructure.EfCore.Repositories
{
    public class CommentRepository:RepositoryBase<long,Comment>,ICommentRepository
    {
        public CommentRepository(ShopContext context) : base(context)
        {
        }
    }
}
