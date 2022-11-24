using FluentValidation;
using $rootnamespace$.$fileinputname$.Commands;
using $rootnamespace$.$fileinputname$.Queries;

namespace $rootnamespace$.$fileinputname$.Validators
{
    /// <summary>
    /// A Fluent Validation Validator for command $fileinputname$Command
    /// </summary>
    public class $fileinputname$CommandValidator : AbstractValidator<$fileinputname$Command>
    {
        public $fileinputname$CommandValidator()
        {
            //RuleFor(x => x.MyProperty).NotEmpty();
        }
    }

    /// <summary>
    /// A Fluent Validation Validator for query $fileinputname$Query
    /// </summary>
    public class $fileinputname$QueryValidator : AbstractValidator<$fileinputname$Query >
    {
        public $fileinputname$QueryValidator()
        {
            //RuleFor(x => x.MyProperty).NotEmpty();
        }
    }
}
