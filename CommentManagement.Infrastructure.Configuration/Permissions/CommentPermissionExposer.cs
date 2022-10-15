using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;

namespace CommentManagement.Infrastructure.Configuration.Permissions
{
    public class CommentPermissionExposer:IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {"Comment",new List<PermissionDto>(){
                        new PermissionDto("ListComment",CommentPermissions.ListComment),
                        new PermissionDto("SearchComment",CommentPermissions.SearchComment),
                        new PermissionDto("ConfirmComment",CommentPermissions.ConfirmComment),
                        new PermissionDto("CanceledComment",CommentPermissions.CanceledComment),


                    }
                }
            };
        }
    }
}
