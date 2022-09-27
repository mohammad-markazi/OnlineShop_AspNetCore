using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication: ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            var operation=new OperationResult();
            if (_customerDiscountRepository.Exists(x =>
                    x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();

            var customerDiscount = new CustomerDiscount(command.ProductId, command.DiscountRate, startDate,
                endDate,command.Reason);

            _customerDiscountRepository.Create(customerDiscount);

            _customerDiscountRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation = new OperationResult();

            var customerDiscount = _customerDiscountRepository.Get(command.Id);
            if (customerDiscount is null)
                return operation.Failed(ApplicationMessages.NotFoundRecord);

            if (_customerDiscountRepository.Exists(x =>x.Id!=command.Id &&
                    x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            customerDiscount.Edit(command.ProductId, command.DiscountRate, startDate,
                endDate, command.Reason);
            _customerDiscountRepository.SaveChanges();

            return operation.Succeeded();
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel model)
        {
            var pr = new object();
           
            var products = _customerDiscountRepository.GetProducts().Select(x=> new
            {
                Id=x.Key,
                Value=x.Value,
            });
            var query = _customerDiscountRepository.GetAll();

            if (model.ProductId != 0)
                query = query.Where(x => x.ProductId == model.ProductId);

            if (!string.IsNullOrWhiteSpace(model.StartDate))
            {
                var startDate = model.StartDate.ToGeorgianDateTime();
                query = query.Where(x => x.StartDate >= startDate);
            }

            if (!string.IsNullOrWhiteSpace(model.EndDate))
            {
                var endDate = model.EndDate.ToGeorgianDateTime();
                query = query.Where(x => x.EndDate <= endDate);
            }

            var result= query.Select(x => new CustomerDiscountViewModel()
            {
                Id = x.Id,
                EntityId = x.ProductId,
                DiscountRate = x.DiscountRate,
                StartDate = x.StartDate.ToFarsi(),
                EndDate = x.EndDate.ToFarsi(),
                CreationDate = x.CreationDate.ToFarsi(),
                Reason = x.Reason
            }).OrderByDescending(x=>x.Id).ToList();

            result.ForEach(discount =>
            {
                discount.EntityName = products.First(dic => dic.Id == discount.EntityId).Value;
            });

            return result;
        }

        public EditCustomerDiscount GetDetail(long id)
        {
            var customerDiscount= _customerDiscountRepository.Get(id);
            return new EditCustomerDiscount()
            {
                Id = customerDiscount.Id,
                DiscountRate = customerDiscount.DiscountRate,
                EndDate = customerDiscount.EndDate.ToString(CultureInfo.InvariantCulture),
                ProductId = customerDiscount.ProductId,
                Reason = customerDiscount.Reason,
                StartDate = customerDiscount.StartDate.ToString(CultureInfo.InvariantCulture)
            };
        }
    }
}
