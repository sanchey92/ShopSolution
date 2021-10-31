using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopSolution.Core.Entities;
using ShopSolution.Core.Specifications;

namespace ShopSolution.Infrastructure.Data
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> specification)
        {
            var query = inputQuery;

            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            query = specification.Includes.Aggregate(query,
                (current, include) => current.Include(include));

            return query;
        }
    }
}