
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuokkaDev.Templates.Domain.Common.Keys;

namespace QuokkaDev.Templates.Persistence.Ef.Infrastructure.Converters
{

    internal class HelloWorldMessageIdConverter : ValueConverter<HelloWorldMessageId, Guid>
    {
        public HelloWorldMessageIdConverter()
        : base(
             v => (Guid)v,
             v => (HelloWorldMessageId)v)
        {
        }
    }
}