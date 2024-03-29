﻿using ShopManagement.Application.Contracts.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository:IRepository<long,ProductCategory>
    {

        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> Search(SearchProductCategory command);
    }
}
