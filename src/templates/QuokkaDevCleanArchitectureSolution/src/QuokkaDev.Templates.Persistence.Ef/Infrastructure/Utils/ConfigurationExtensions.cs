using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;

namespace QuokkaDev.Templates.Persistence.Ef.Infrastructure.Utils
{
    internal static class ConfigurationExtensions
    {
        internal static ModelConfigurationBuilder AutoRegisterConverters(this ModelConfigurationBuilder builder)
        {
            Assembly assembly = typeof(ConfigurationExtensions).Assembly;

            Type vct = typeof(ValueConverter);
            var converters = assembly.GetTypes().Where(t => t.BaseType!.IsGenericType && t.BaseType?.BaseType?.FullName == vct.FullName);

            foreach (var converter in converters)
            {
                builder.Properties(converter.BaseType!.GenericTypeArguments[0])
                    .HaveConversion(converter);
            }

            return builder;
        }
    }
}
