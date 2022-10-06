﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EfCore.Mapping
{
    public class AccountMapping:IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FullName).HasMaxLength(100);
            builder.Property(x => x.Username).HasMaxLength(100);
            builder.Property(x => x.Password).HasMaxLength(1000);
            builder.Property(x => x.Profile).HasMaxLength(500);

            builder.Property(x => x.Mobile).HasMaxLength(11);

        }
    }
}
