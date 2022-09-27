using System.Collections.Generic;
using System.Linq;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using InventoryManagement.Application.Contracts.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHost.Areas.Administration.Pages.Shared;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IndexModel :BasePageModel
    {
        private readonly IInventoryApplication _inventoryApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(IInventoryApplication inventoryApplication, IProductApplication productApplication)
        {
            _inventoryApplication = inventoryApplication;
            _productApplication = productApplication;
        }
        public List<InventoryViewModel> Inventory { get; set; }
        public SelectList Products { get; set; }
        [BindProperty(SupportsGet = true)]
        public  InventorySearchModel SearchModel { get; set; }
        public void OnGet()

        {
            Products = new SelectList(_productApplication.GetAll(), "Id", "Name");

            Inventory = _inventoryApplication.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory();
           command.RelatedEntities = _productApplication.GetAll().ToDictionary(x => x.Id, x => x.Name);
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateInventory command)
        {
            var result = _inventoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var inventory = _inventoryApplication.GetDetail(id);
            inventory.RelatedEntities = _productApplication.GetAll().ToDictionary(x => x.Id, x => x.Name);

            return Partial("./Edit", inventory);
        }

        public JsonResult OnPostEdit(EditInventory command)
        {
            var result = _inventoryApplication.Edit(command);

            return new JsonResult(result);
        }

        public IActionResult OnGetIncrease(long id)
        {
            var command = new IncreaseInventory()
            {
                InventoryId = id
            };
            return Partial("./Increase", command);
        }

        public JsonResult OnPostIncrease(IncreaseInventory command)
        {
           var result= _inventoryApplication.Increase(command);

            return new JsonResult(result);

        }
        public IActionResult OnGetReduce(long id)
        {
            var command = new ReduceInventory()
            {
                InventoryId = id
            };
            return Partial("./Reduce", command);
        }

        public JsonResult OnPostReduce(ReduceInventory command)
        {
            var result = _inventoryApplication.Reduce(command);

            return new JsonResult(result);

        }

        public IActionResult OnGetLog(long id)
        {
            var logs = _inventoryApplication.GetOperationLog(id);
            return Partial("./OperationLog", logs);

        }



    }
}
