using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg
{
    public class Order:EntityBase
    {
        public long AccountId { get;private set; }

        public double TotalAmount { get; private set; }
        public double DiscountAmount { get; private set; }
        public double PayAmount { get; private set; }
        public PaymentStatus Status { get; private set; }
        public string IssueTrackingNo { get; private set; }
        public long RefId { get; private set; }
        public List<OrderItem> Items { get; private set; }

        public Order(long accountId, double totalAmount, double discountAmount, double payAmount)
        {
            AccountId = accountId;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            PayAmount = payAmount;
            Items = new List<OrderItem>();
        }


        public void PaymentSucceeded(long refId)
        {
            Status = PaymentStatus.Success;
            if (refId != 0) RefId = refId;
        }
        public void Canceled()=>Status=PaymentStatus.Canceled;

        public void SetIssueTrackingNo(string number) => IssueTrackingNo = number;

        public void AddItem(OrderItem item)
        {
            Items.Add(item);

        }
    }


    public enum PaymentStatus
    {
        Unknown,
        Success,
        Canceled
    }
}
