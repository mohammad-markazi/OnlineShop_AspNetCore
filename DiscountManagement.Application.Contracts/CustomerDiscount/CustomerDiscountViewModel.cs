﻿namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public class CustomerDiscountViewModel
    {
        public long Id { get; set; }
        public long EntityId { get; set; }
        public string EntityName { get; set; }
        public int DiscountRate { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CreationDate { get; set; }
        public string Reason { get; set; }
    }
}