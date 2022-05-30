using FluentValidation;
using QuokkaDev.Templates.Application.UseCases.Ping.Queries;

namespace QuokkaDev.Templates.Application.UseCases.Ping.Validators
{
    public class PingQueryValidator : AbstractValidator<PingQuery>
    {
        public PingQueryValidator()
        {
            RuleFor(x => x.EchoRequest).NotEmpty();
        }
    }
}
