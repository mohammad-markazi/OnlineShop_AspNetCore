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
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication:IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;
        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IProductRepository productRepository, IFileUploader fileUploader)
        {
            _productPictureRepository = productPictureRepository;
            _productRepository = productRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation=new OperationResult();
            var product = _productRepository.GetWithCategory(command.ProductId);
            var fileName = _fileUploader.Upload(command.Picture, "ProductCategoryPictures", product.Category.Slug,
                product.Slug);

            var productPicture = new ProductPicture(command.ProductId, fileName, command.PictureAlt,
                command.PictureTitle);

            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChanges();
            return operation.Succeeded();

        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.GetWithProductAndCategoryBy(command.Id);
            if (productPicture is null)
                return operation.Failed(ApplicationMessages.NotFoundRecord);

            _fileUploader.RemoveFile(productPicture.Picture);

            var fileName = _fileUploader.Upload(command.Picture, "ProductCategoryPictures",
                productPicture.Product.Category.Slug,
                productPicture.Product.Slug);

            productPicture.Edit(command.ProductId, fileName, command.PictureAlt,
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
                // Picture = productPicture.Picture,
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
                CreationDate = x.CreationDate.ToFarsi(),
                Id = x.Id,
                Picture = x.Picture,
                ProductName = x.Product.Name,
                IsRemoved = x.IsRemoved
            });
            return result.OrderByDescending(x=>x.Id).ToList();
        }
    }
}
