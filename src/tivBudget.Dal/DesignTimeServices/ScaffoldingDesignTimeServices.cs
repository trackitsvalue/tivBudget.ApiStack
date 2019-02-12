using EntityFrameworkCore.Scaffolding.Handlebars;
using freebyTech.Common.Transformations;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace tivBudget.Dal.DesignTimeServices
{
    /// <summary>
    /// This design time service is using the handlebars approach proposed by tonysneed in:
    /// https://github.com/aspnet/EntityFrameworkCore/issues/4038
    /// and implemented in github project:
    /// https://github.com/TrackableEntities/EntityFrameworkCore.Scaffolding.Handlebars
    ///
    /// Another approach for modifying build service can also be found here:
    /// https://romiller.com/2017/02/10/ef-core-1-1-pluralization-in-reverse-engineer/
    /// 
    /// </summary>
    public class ScaffoldingDesignTimeServices : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection services)
        {
            var options = ReverseEngineerOptions.DbContextAndEntities;
            services.AddHandlebarsScaffolding(options);

            services.AddHandlebarsTransformers(
                entityNameTransformer: n => n.Singularize(),
                entityFileNameTransformer: n => n.Singularize(),
                constructorTransformer: e => new EntityPropertyInfo(e.PropertyType.Singularize(), e.PropertyName),
                propertyTransformer: e => new EntityPropertyInfo(e.PropertyType.Singularize(), e.PropertyName),
                navPropertyTransformer: e => new EntityPropertyInfo(e.PropertyType.Singularize(), e.PropertyName));
        }
    }
}
