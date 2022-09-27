using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication:IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operation=new OperationResult();
            if (_colleagueDiscountRepository.Exists(x =>
                    x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var colleagueDiscount = new ColleagueDiscount(command.ProductId, command.DiscountRate);

            _colleagueDiscountRepository.Create(colleagueDiscount);
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation = new OperationResult();

            var colleagueDiscount = _colleagueDiscountRepository.Get(command.Id);
            if (colleagueDiscount is null)
                return operation.Failed(ApplicationMessages.NotFoundRecord);

            if (_colleagueDiscountRepository.Exists(x =>x.Id!=command.Id &&
                    x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            colleagueDiscount.Edit(command.ProductId,command.DiscountRate);
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditColleagueDiscount GetDetail(long id)
        {
            var colleagueDiscount=_colleagueDiscountRepository.Get(id);

            return new EditColleagueDiscount()
            {
                DiscountRate = colleagueDiscount.DiscountRate,
                Id = colleagueDiscount.Id,
                ProductId = colleagueDiscount.ProductId
            };
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel model)
        {
            var products = _colleagueDiscountRepository.GetProducts().Select(x => new { Id = x.Key, Name = x.Value }).ToList();
            var query = _colleagueDiscountRepository.GetAll();

            if (model.ProductId != 0)
                query = query.Where(x => x.ProductId == model.ProductId);
            var result = query.Select(x => new ColleagueDiscountViewModel()
            {
                Id = x.Id,
                EntityId = x.ProductId,
                DiscountRate = x.DiscountRate,
                IsRemoved = x.IsRemoved
            }).OrderByDescending(x => x.Id).ToList();

            result.ForEach(discount =>
            {
                discount.EntityName = products.First(dic => dic.Id == discount.EntityId).Name;
            });

            return result;
        }

        public void Remove(long id)
        {
            var colleagueDiscount = _colleagueDiscountRepository.Get(id);

            colleagueDiscount.Remove();
            _colleagueDiscountRepository.SaveChanges();
        }

        public void Restore(long id)
        {
            var colleagueDiscount = _colleagueDiscountRepository.Get(id);

            colleagueDiscount.Restore();
            _colleagueDiscountRepository.SaveChanges();
        }
    }
}
