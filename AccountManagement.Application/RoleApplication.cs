using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
using _0_Framework.Infrastructure;
namespace AccountManagement.Application
{
    public class RoleApplication:IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;

        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public OperationResult Create(CreateRole command)
        {
            var operation=new OperationResult();

            if (_roleRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var role = new Role(command.Name,command.Type,new List<Permission>());
            _roleRepository.Create(role);
            _roleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditRole command)
        {
            var operation = new OperationResult();
            var role = _roleRepository.Get(command.Id);

            if (role == null)
                return operation.Failed(ApplicationMessages.NotFoundRecord);

            if (_roleRepository.Exists(x =>x.Id!=role.Id && x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var permission=new List<Permission>();
            command.Permissions.ForEach(Code => permission.Add(new Permission(Code)));
            role.Edit(command.Name,command.Type, permission);
            _roleRepository.SaveChanges();

            return operation.Succeeded();
        }

        public EditRole GetDetail(long id)
        {
            var role = _roleRepository.Get(id);

            return new EditRole()
            {
                Id = role.Id,
                Name = role.Name,
              MapPermissions=role.Permissions.Select(x=>new PermissionDto(x.Name,x.Code)).ToList(),
              Permissions = role.Permissions.Select(x=>x.Code).ToList(),
              Type = role.Type
            };


        }

        public List<RoleViewModel> GetAll()
        {
            return _roleRepository.GetAll().AsNoTracking().Select(x => new RoleViewModel()
            {
                Id = x.Id,
                CreationDate = x.CreationDate.ToFarsi(),
                Name = x.Name
            }).ToList();
        }

        public EditRole GetRoleByType(int type)
        {
            var role= _roleRepository.GetByType(type);
            if (role == null)
                return null;
            return new EditRole()
            {
                Id = role.Id,
                Type = role.Type,
                Name = role.Name
            };
        }
    }
}
