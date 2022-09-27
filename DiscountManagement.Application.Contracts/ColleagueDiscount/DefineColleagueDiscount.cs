using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Application.Contracts.ColleagueDiscount
{
    public class DefineColleagueDiscount
    {
        public long ProductId { get;  set; }

        public int DiscountRate { get;  set; }
        public Dictionary<long, string> Products { get; set; }

        public DefineColleagueDiscount()
        {
            Products = new Dictionary<long, string>();
        }
    }
}
