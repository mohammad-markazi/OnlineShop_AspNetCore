using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class AccountApplication:IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IFileUploader _fileUploader;
        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IFileUploader fileUploader, IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
            _roleRepository = roleRepository;
        }

        public OperationResult Register(RegisterAccount command)
        {
            var operation = new OperationResult();
            if (_accountRepository.Exists(x => x.Username == command.Username || x.Mobile==command.Mobile))
                return operation.Failed(ApplicationMessages.DuplicatedAccount);

            var password = _passwordHasher.Hash(command.Password);
            var filename = _fileUploader.Upload(command.Profile, "Profiles");

            var account = new Account(command.FullName,
                command.Username, password, command.Mobile, filename, command.RoleId);
            _accountRepository.Create(account);
            _accountRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account is null)
                return operation.Failed(ApplicationMessages.NotFoundRecord);

            if (_accountRepository.Exists(x => x.Id!=command.Id && (x.Username == command.Username || x.Mobile == command.Mobile)))
                return operation.Failed(ApplicationMessages.DuplicatedAccount);


            if (command.Profile is not null)
                _fileUploader.RemoveFile(account.Profile);
            
           var filename= _fileUploader.Upload(command.Profile, "Profiles");

            account.Edit(command.FullName,command.Username,command.Mobile,filename,command.RoleId);
            _accountRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation=new OperationResult();

            var account = _accountRepository.Get(command.Id);
            if (account is null)
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            if (command.Password != command.RePassword)
                return operation.Failed(ApplicationMessages.PasswordNotMatch);

            var password = _passwordHasher.Hash(command.Password);
            account.ChangePassword(password);

            _accountRepository.SaveChanges();

            return operation.Succeeded();
        }

        public List<AccountViewModel> Search(AccountSearchModel model)
        {
            return _accountRepository.Search(model);
        }

        public EditAccount GetDetail(long id)
        {
            return _accountRepository.GetDetail(id);
        }

        public OperationResult<AccountViewModel> Login(Login command)
        {
            var operation=new OperationResult<AccountViewModel>();
            var account = _accountRepository.GetByUsername(command.Username);
            if (account is null)
                return operation.Failed(ApplicationMessages.NotFoundUsernameOrPassword);

            var (verified, _) = _passwordHasher.Check(account.Password, command.Password);

            if (!verified)
                return operation.Failed(ApplicationMessages.NotFoundUsernameOrPassword);

          var permissions=  _roleRepository.Get(account.RoleId).Permissions.Select(x=>x.Code).ToList();
            var result= operation.Succeeded();
            result.Data = new AccountViewModel()
            {
                Id = account.Id,
                FullName = account.FullName,
                Username = account.Username,
                RoleId = account.RoleId,
                Permissions = permissions
            };

            return operation;
        }
    }
}
