using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductApplication:IProductApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IProductCategoryRepository _categoryRepository;

        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader, IProductCategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
            _categoryRepository = categoryRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation=new OperationResult();
            if (_productRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var categorySlog = _categoryRepository.GetAll().Select(x => new { x.Slug, x.Id })
                .First(x => x.Id == command.CategoryId);


            var fileName = _fileUploader.Upload(command.Picture, "ProductCategoryPictures",categorySlog.Slug, slug);

            var product = new Product(
                command.Code, command.Name,
                command.ShortDescription, command.Description,
                fileName, command.PictureAlt, command.PictureTitle,
                slug, command.Keywords, command.MetaDescription, command.CategoryId);

            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return operation.Succeeded();

        }

        public OperationResult Edit(EditProduct command)
        {

            var operation = new OperationResult();
            if (_productRepository.Exists(x => x.Id!=command.Id && x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var product = _productRepository.GetWithCategory(command.Id);
            if(product is null)
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            var slug=command.Slug.Slugify();

            var categorySlog = _categoryRepository.GetAll().Select(x => new { x.Slug, x.Id })
                .First(x => x.Id == command.CategoryId);

            _fileUploader.RemoveFile(product.Picture);
            var fileName = _fileUploader.Upload(command.Picture, "ProductCategoryPictures", product.Category.Slug, slug);


            product.Edit(command.Code, command.Name, 
                command.ShortDescription, command.Description,
                fileName, command.PictureAlt, command.PictureTitle,
                slug, command.Keywords, command.MetaDescription, command.CategoryId);

            _productRepository.SaveChanges();
            return operation.Succeeded();

        }

        public EditProduct GetDetail(long id)
        {
            return _productRepository.GetDetail(id);
        }

        public List<ProductViewModel> Search(SearchProductModel command)
        {
            return _productRepository.Search(command);
        }

        

        public List<ProductViewModel> GetAll()
        {
            return _productRepository.GetAll().Select(x => new ProductViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

        }
    }
}
