using Store.Project.Domain.Entities.Procuts;
using Store.Project.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Project.Services.Specifications.Products
{
    public class ProductsWithBrandAndTypeSpecifications : BaseSpecifications<int, Product>
    {
        public ProductsWithBrandAndTypeSpecifications(int id) : base(P=>P.Id == id)
        {
            ApplyIncludes();
        }
        public ProductsWithBrandAndTypeSpecifications(ProductQueryParameters parameters) : base
            (
                 P =>
                (!parameters.BrandId.HasValue || P.BrandId == parameters.BrandId)
                &&
                (!parameters.TypeId.HasValue  || P.TypeId == parameters.TypeId)
                &&
                 (string.IsNullOrEmpty(parameters.Search) ||P.Name.ToLower().Contains(parameters.Search.ToLower()))
            )
        {

                ApplyPagination(parameters.PageSize, parameters.PageIndex);

                ApplySorting(parameters.Sort);

                ApplyIncludes();
        }

        private void ApplySorting(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "Priceasc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "Pricedesc":
                        AddOrderBy(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;

                }
            }
            else
            {
                //OrderBy = P => P.Name;
                AddOrderBy(P => P.Name);
            }
        }

        private void ApplyIncludes()
        {
            Include.Add(P => P.Brand);
            Include.Add(P => P.Type);
        }
    }
}
