using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        //public GeneralProfile()
        //{
        //    //CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
        //    //CreateMap<CreateProductCommand, Product>();
        //    //CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
        //}

        public GeneralProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var method = type.GetMethod("Mapping") ??
                             type.GetInterface("IMapFrom`1")
                                .GetMethod("Mapping");

                var instance = Activator.CreateInstance(type);

                method?.Invoke(instance, new object[] { this });
            }
        }
    }
}
