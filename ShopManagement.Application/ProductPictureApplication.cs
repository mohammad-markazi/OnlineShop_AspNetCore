using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication:IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository = productPictureRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation=new OperationResult();
            var productPicture = new ProductPicture(command.ProductId, command.Picture, command.PictureAlt,
                command.PictureTitle);

            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChanges();
            return operation.Succeeded();

        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(command.Id);
            if (productPicture is null)
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            productPicture.Edit(command.ProductId, command.Picture, command.PictureAlt,
                command.PictureTitle);
            _productPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditProductPicture GetDetail(long id)
        {
            var productPicture = _productPictureRepository.Get(id);
            return new EditProductPicture()
            {
                Id = productPicture.Id,
                Picture = productPicture.Picture,
                PictureAlt = productPicture.PictureAlt,
                PictureTitle = productPicture.PictureTitle,
                ProductId = productPicture.ProductId,
            };
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture is null)
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            productPicture.Removed();
            _productPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture is null)
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            productPicture.Restore();
            _productPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel command)
        {
            var query = _productPictureRepository.GetAll();

            if (command.ProductId != 0)
                query = query.Where(x => x.ProductId == command.ProductId);

            var result = query.Include(x => x.Product).Select(x => new ProductPictureViewModel()
            {
                CreationDate = x.CreationDate.ToString(),
                Id = x.Id,
                Picture = x.Picture,
                ProductName = x.Product.Name,
                IsRemoved = x.IsRemoved
            });
            return result.OrderByDescending(x=>x.Id).ToList();
        }
    }
}
