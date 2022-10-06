﻿namespace AccountManagement.Application.Contracts.Account
{
    public class AccountViewModel
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public string Username { get; set; }

        public string Mobile { get; set; }
        public string Profile { get; set; }
        public string Role { get; set; }
    }
}