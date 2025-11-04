using Store.Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Project.Domain.Contracts
{
    public interface ISpecifications<TKey,TEntity> where TEntity : BaseEntity<TKey>
    {
         List<Expression<Func<TEntity, object>>> Include { get; set; }
         Expression<Func<TEntity, bool>>? Criteria { get; set; }
    }
}
