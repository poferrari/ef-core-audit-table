using Bogus;
using DomainHistoryEF.Domain.Catalogs.Entities;
using DomainHistoryEF.Domain.Catalogs.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainHistoryEF.Domain.Catalogs.Services
{
    public class ChangeProductService : IChangeProductService
    {
        private const string _locale = "pt_BR";
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public ChangeProductService(ICategoryRepository categoryRepository,
                                    IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task Send()
        {
            var categories = await GetCategories();

            //var product = await GetProduct(categories);
            var productId = Guid.Parse("C1026C1A-71D2-53E6-79A1-9B511A06A8A0");

            await UpdateProduct(productId, categories);
        }

        private async Task<List<Category>> GetCategories()
        {
            var categories = await _categoryRepository.GetAll();

            if (categories is null || !categories.Any())
            {
                categories = new Faker<Category>(_locale)
                                        .RuleFor(c => c.Id, f => f.Random.Guid())
                                        .RuleFor(c => c.Name, f => f.Commerce.Department())
                                        .Generate(15);

                await _categoryRepository.AddRange(categories);
            }

            return categories;
        }

        private async Task<Product> GetProduct(List<Category> categories)
        {
            var product = CreateProduct(categories);

            await _productRepository.Add(product);

            return product;
        }

        private async Task UpdateProduct(Guid productId, List<Category> categories)
        {
            var productDb = await _productRepository.GetById(productId);

            var productChange = CreateProduct(categories);
            productDb.Name = productChange.Name;
            productDb.Value = productChange.Value;
            productDb.Quantity = productChange.Quantity;
            productDb.CategoryId = productChange.CategoryId;
            //productDb.Category = categories.First(t => t.Id == productChange.CategoryId);
            productDb.Dimension.Depth = productChange.Dimension.Depth;
            productDb.Dimension.Height = productChange.Dimension.Height;
            //productDb.Dimension = productChange.Dimension;

            await _productRepository.Update(productDb);
        }

        private Product CreateProduct(List<Category> categories)
        {
            var categoriesId = categories.Select(t => t.Id).ToList();

            var dimension = new Faker<Dimension>()
                                  .RuleFor(c => c.Height, f => f.Random.Double())
                                  .RuleFor(c => c.Width, f => f.Random.Double())
                                  .RuleFor(c => c.Depth, f => f.Random.Double())
                                  .Generate();

            var product = new Faker<Product>(_locale)
                                 .RuleFor(c => c.Id, f => f.Random.Guid())
                                 .RuleFor(c => c.Name, f => f.Commerce.ProductName())
                                 .RuleFor(c => c.Description, f => f.Commerce.ProductDescription())
                                 .RuleFor(c => c.CategoryId, f => f.PickRandom(categoriesId))
                                 .RuleFor(c => c.Value, f => f.Random.Decimal(5.0m))
                                 .RuleFor(c => c.Quantity, f => f.Random.Int(1))
                                 .RuleFor(c => c.Active, f => true)
                                 .RuleFor(c => c.Dimension, f => dimension)
                                 .RuleFor(c => c.CreatedAtDate, f => DateTime.Now)
                                 .RuleFor(c => c.CreatedBy, f => f.Company.CompanyName())
                                 .Generate();

            product.UpdatededAtDate = null;
            product.UpdatedByUser = null;
            return product;
        }
    }
}
