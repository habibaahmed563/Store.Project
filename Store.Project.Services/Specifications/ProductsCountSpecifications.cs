using Store.Project.Domain.Entities.Procuts;
using Store.Project.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Project.Services.Specifications
{
    public class ProductsCountSpecifications : BaseSpecifications<int, Product>
    {
        public ProductsCountSpecifications(ProductQueryParameters parameters) : base(
               P =>
                (!parameters.BrandId.HasValue || P.BrandId == parameters.BrandId)
                &&
                (!parameters.TypeId.HasValue || P.TypeId == parameters.TypeId)
                &&
                 (string.IsNullOrEmpty(parameters.Search) || P.Name.ToLower().Contains(parameters.Search.ToLower()))

            )
        {
            
        }
    }
}
