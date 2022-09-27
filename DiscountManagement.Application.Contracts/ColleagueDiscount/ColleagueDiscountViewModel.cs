﻿namespace DiscountManagement.Application.Contracts.ColleagueDiscount
{
    public class ColleagueDiscountViewModel
    {
        public long Id { get; set; }
        public string EntityName { get; set; }
        public long EntityId { get; set; }
        public int DiscountRate { get; set; }
        public bool IsRemoved { get; set; }
    }
}