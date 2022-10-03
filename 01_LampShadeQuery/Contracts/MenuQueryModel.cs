using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_LampShadeQuery.Contracts.ArticleCategory;
using _01_LampShadeQuery.Contracts.ProductCategory;

namespace _01_LampShadeQuery.Contracts
{
    
    public class MenuQueryModel
    {
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }

        public List<ProductCategoryQueryModel> ProductCategories { get; set; }
    }
}
