﻿using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Application._Utilites;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.ProductAgg.Services;

namespace Shop.Application.Products.Create
{
    public class CreateProductCommandHandler : IBaseCommandHandler<CreateProductCommand>
    {
        private readonly IProductDomainService _productDomainService;
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;
        public CreateProductCommandHandler(IProductDomainService productDomainService, IProductRepository productRepository, IFileService fileService)
        {
            _productDomainService = productDomainService;
            _productRepository = productRepository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.ProductImage);
            var product = new Product(request.Title, imageName, request.Description, request.CategoryId, request.SubCategoryId
                , request.SecondarySubCategoryId, request.Slug, request.SeoData, _productDomainService);
            
            _productRepository.Add(product);

            var specifcations = new List<ProductSpecification>();
            request.Specifications.ToList().ForEach(specification =>
            {
                specifcations.Add(new ProductSpecification(specifcations.Key, specifcations.Value));
            });
            product.SetSpecification(specifcations);
            await _productRepository.Save();
            return OperationResult.Success();
        }
    }
}
