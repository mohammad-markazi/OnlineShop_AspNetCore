using System.Collections.Generic;
using _0_Framework.Infrastructure;

namespace DiscountManagement.Infrastructure.Configuration.Permissions
{
    public class DiscountPermissionExposer:IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {"CustomerDiscount",new List<PermissionDto>(){
                        new PermissionDto("ListCustomerDiscount",DiscountPermissions.ListCustomerDiscount),
                        new PermissionDto("SearchCustomerDiscount",DiscountPermissions.SearchCustomerDiscount),
                        new PermissionDto("CreateCustomerDiscount",DiscountPermissions.CreateCustomerDiscount),
                        new PermissionDto("EditCustomerDiscount",DiscountPermissions.CreateCustomerDiscount),
                    

                    }
                },
                {"ColleagueDiscount",new List<PermissionDto>(){
                        new PermissionDto("ListColleagueDiscount",DiscountPermissions.ListColleagueDiscount),
                        new PermissionDto("SearchColleagueDiscount",DiscountPermissions.SearchColleagueDiscount),
                        new PermissionDto("CreateColleagueDiscount",DiscountPermissions.CreateColleagueDiscount),
                        new PermissionDto("EditColleagueDiscount",DiscountPermissions.EditColleagueDiscount),
                        new PermissionDto("RemoveColleagueDiscount",DiscountPermissions.RemoveColleagueDiscount),
                        new PermissionDto("RestoreColleagueDiscount",DiscountPermissions.RestoreColleagueDiscount),

                    }
                },
            };
        }
    }
}
