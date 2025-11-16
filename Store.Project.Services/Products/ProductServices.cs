using AutoMapper;
using Store.Project.Domain.Contracts;
using Store.Project.Domain.Entities.Procuts;
using Store.Project.Domain.Exceptions.NotFound;
using Store.Project.Services.Abstractions.Products;
using Store.Project.Services.Specifications;
using Store.Project.Services.Specifications.Products;
using Store.Project.Shared;
using Store.Project.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Store.Project.Services.Products
{
    public class ProductServices(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<PaginationResponse<ProductResponse>> GetAllProductsAsync(ProductQueryParameters parameters)
        {
            //var spec = new BaseSpecifications<int, Product>(null);
            //spec.Include.Add(P => P.Brand);
            //spec.Include.Add(P => P.Type);

            var spec = new ProductsWithBrandAndTypeSpecifications( parameters);

            var products = await _unitOfWork.GetRepository<int, Product>().GetAllAsync(spec);

            var result = _mapper.Map<IEnumerable<ProductResponse>>(products);

            var specCount = new ProductsCountSpecifications(parameters);

            var count = await _unitOfWork.GetRepository<int, Product>().CountAsync(specCount);

            return new PaginationResponse<ProductResponse>(parameters.PageIndex,parameters.PageSize,count ,result);
        }

        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {
            var spec = new ProductsWithBrandAndTypeSpecifications(id);

            var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(id);

            if (product is null) throw new ProductNotFoundException(id);

            var result = _mapper.Map<ProductResponse>(product);
            return result;

        }

        public async Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<int, ProductBrand>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(brands);
            return result;
        }
        public async Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<int, ProductType>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(types);
            return result;
        }
    }
}
