using System.Collections.Generic;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHost.Areas.Administration.Pages.Shared;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.Orders
{
    public class IndexModel :BasePageModel
    {
        private readonly IOrderApplication _orderApplication;
        private readonly IAccountApplication _accountApplication;

        public IndexModel(IOrderApplication orderApplication, IAccountApplication accountApplication)
        {
            _orderApplication = orderApplication;
            _accountApplication = accountApplication;
        }
        public List<OrderViewModel> Orders { get; set; }
        public SelectList Account;

        [BindProperty(SupportsGet = true)]
        public  OrderSearchModel SearchModel { get; set; }
        // [NeedPermission(ShopPermissions.ListProductCategory)]

        public void OnGet()

        {
            Account = new SelectList(_accountApplication.GetAll(), "Id", "FullName");

            Orders = _orderApplication.Search(SearchModel);
        }

        public IActionResult OnGetItems(long id)
        {
            var items = _orderApplication.GetOrderItemsBy(id);

            return Partial("Items", items);
        }

        public IActionResult OnGetConfirm(long id)
        {
            _orderApplication.PaymentSucceeded(id, 0);
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetCanceled(long id)
        {
        _orderApplication.Canceled(id);
        return RedirectToPage("./Index");

        }




    }
}
