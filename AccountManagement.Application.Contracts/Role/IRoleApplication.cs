using System.Collections.Generic;
using _0_Framework.Application;
using _0_Framework.Domain;

namespace AccountManagement.Application.Contracts.Role
{
    public interface IRoleApplication
    {
        OperationResult Create(CreateRole command);
        OperationResult Edit(EditRole command);
        EditRole GetDetail(long id);
        List<RoleViewModel> GetAll();
        EditRole GetRoleByType(int type);

    }
}