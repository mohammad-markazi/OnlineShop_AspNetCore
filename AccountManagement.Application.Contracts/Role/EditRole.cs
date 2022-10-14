﻿using _0_Framework.Infrastructure;
using System.Collections.Generic;

namespace AccountManagement.Application.Contracts.Role
{
    public class EditRole : CreateRole
    {
        public long Id { get; set; }

        public List<PermissionDto> MapPermissions { get; set; }
        public List<int> Permissions { get; set; }

    }
}