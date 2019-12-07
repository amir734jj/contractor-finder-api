using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using StructureMap;

namespace Api.Utilities
{
    public static class AutoMapperBuilder
    {
        private static bool ImplementsGenericInterface(this Type type, Type interfaceType)
        {
            return type.IsGenericType(interfaceType) || type.GetTypeInfo().ImplementedInterfaces
                       .Any(@interface => @interface.IsGenericType(interfaceType));
        }

        private static bool IsGenericType(this Type type, Type genericType)
        {
            return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == genericType;
        }

        public static IMapper ResolveMapper(IContext ctx, params Assembly[] assembliesToScan)
        {
            var allTypes = assembliesToScan
                .Where(a => !a.IsDynamic && a.GetName().Name != nameof(AutoMapper))
                .Distinct() // avoid AutoMapper.DuplicateTypeMapConfigurationException
                .SelectMany(a => a.DefinedTypes)
                .ToArray();

            var openTypes = new[]
            {
                typeof(IValueResolver<,,>),
                typeof(IMemberValueResolver<,,,>),
                typeof(ITypeConverter<,>),
                typeof(IValueConverter<,>),
                typeof(IMappingAction<,>)
            };

            return new MapperConfiguration(mapperConfig =>
            {
                foreach (var type in openTypes.SelectMany(openType => allTypes
                    .Where(t => t.IsClass 
                                && !t.IsAbstract
                                && t.AsType().ImplementsGenericInterface(openType))))

                {
                    mapperConfig.AddProfile((Profile) ctx.GetInstance(type.AsType()));
                }
                
            }).CreateMapper();
        } 
    }
}