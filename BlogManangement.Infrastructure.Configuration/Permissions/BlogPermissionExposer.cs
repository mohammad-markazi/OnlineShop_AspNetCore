using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;

namespace BlogManagement.Infrastructure.Configuration.Permissions
{
    public class BlogPermissionExposer:IPermissionExposer

    {
        public Dictionary<string, List<PermissionDto>> Expose()
        { 
            return new Dictionary<string, List<PermissionDto>>()
            {
                {"ArticleCategory",new List<PermissionDto>(){
                        new PermissionDto("ListArticleCategory",BlogPermissions.ListArticleCategory),
                        new PermissionDto("SearchArticleCategory",BlogPermissions.SearchArticleCategory),
                        new PermissionDto("CreateArticleCategory",BlogPermissions.CreateArticleCategory),
                        new PermissionDto("EditArticleCategory",BlogPermissions.EditArticleCategory),


                    }
                },
                {"Article",new List<PermissionDto>(){
                        new PermissionDto("ListArticle",BlogPermissions.ListArticle),
                        new PermissionDto("SearchArticle",BlogPermissions.SearchArticle),
                        new PermissionDto("CreateArticle",BlogPermissions.CreateArticle),
                        new PermissionDto("EditArticle",BlogPermissions.EditArticle),

                    }
                },
            };
        }
    }
}
