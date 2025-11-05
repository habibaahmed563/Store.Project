using Store.Project.Domain.Entities.Procuts;
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
        public ProductsWithBrandAndTypeSpecifications(int? brandId, int? typeId,string? sort,string? search,int? pageIndex,int? pageSize) : base
            (
                 P =>
                (!brandId.HasValue || P.BrandId == brandId)
                &&
                (!typeId.HasValue  || P.TypeId == typeId)
                &&
                 (string.IsNullOrEmpty(search) ||P.Name.ToLower().Contains(search.ToLower()))
            )
        {

                ApplyPagination(pageSize.Value, pageIndex.Value);

                ApplySorting(sort);

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
