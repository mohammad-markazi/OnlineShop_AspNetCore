﻿using System;
using System.Collections.Generic;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Application
{
    public class AccountApplication:IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IFileUploader _fileUploader;
        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IFileUploader fileUploader)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateAccount command)
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
    }
}
