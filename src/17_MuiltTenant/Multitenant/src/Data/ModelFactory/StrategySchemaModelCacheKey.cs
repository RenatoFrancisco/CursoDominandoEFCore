using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Multitenant.Data;

namespace EFCore.Multitenant.ModelFactory
{
    public class StrategySchemaModelCacheKey : IModelCacheKeyFactory
    { 
        public object Create(DbContext context)
        {
            var model = new 
            {
                Type = context.GetType(),
                Schema =  (context as ApplicationContext)?.TenantData.TenantId
            };

            return model; 
        }
    } 
}