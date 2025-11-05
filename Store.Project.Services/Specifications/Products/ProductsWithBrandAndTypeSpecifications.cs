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
        public ProductsWithBrandAndTypeSpecifications(int? brandId, int? typeId) : base
            (
                 P =>
                (!brandId.HasValue || P.BrandId == brandId)
                &&
                (!typeId.HasValue  || P.TypeId == typeId)

            )
        {
            ApplyIncludes();
        }

        private void ApplyIncludes()
        {
            Include.Add(P => P.Brand);
            Include.Add(P => P.Type);
        }
    }
}
