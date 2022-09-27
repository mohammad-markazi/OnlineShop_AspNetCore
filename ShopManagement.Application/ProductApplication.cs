using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Application
{
    public class ProductApplication:IProductApplication
    {
        private readonly IProductRepository _productRepository;

        public ProductApplication(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation=new OperationResult();
            if (_productRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();

            var product = new Product(
                command.Code, command.Name,
                command.ShortDescription, command.Description,
                command.Picture, command.PictureAlt, command.PictureTitle,
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

            var product = _productRepository.Get(command.Id);
            if(product is null)
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            var slug=command.Slug.Slugify();
            product.Edit(command.Code, command.Name, 
                command.ShortDescription, command.Description,
                command.Picture, command.PictureAlt, command.PictureTitle,
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
