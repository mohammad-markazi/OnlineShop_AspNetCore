using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication:IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUploader _fileUploader;
        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository, IFileUploader fileUploader)
        {
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation=new OperationResult();
            if (_productCategoryRepository.Exists(x=>x.Name==command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();

            var fileName = _fileUploader.Upload(command.Picture, "ProductCategoryPictures", slug);

            var productCategory = new ProductCategory(command.Name, command.Description, fileName,
                command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug);

            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);
            if (productCategory is null)
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            if (_productCategoryRepository.Exists(x=>x.Name==command.Name && x.Id!=command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            _fileUploader.RemoveFile(productCategory.Picture);
            var fileName = _fileUploader.Upload(command.Picture, "ProductCategoryPictures", slug);

            productCategory.Edit(command.Name, command.Description, fileName,
                command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug);

            _productCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditProductCategory GetDetail(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> Search(SearchProductCategory command)
        {

          return _productCategoryRepository.Search(command);
        }

        public List<ProductCategoryViewModel> GetAll()
        {
         var productCategories=   _productCategoryRepository.GetAll().Select(x => new ProductCategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
         return productCategories;
        }
    }
}
