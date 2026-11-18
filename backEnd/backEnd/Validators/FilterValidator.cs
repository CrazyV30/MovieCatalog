using backEnd.Contracts;
using FluentValidation;

namespace backEnd.Validators
{
    public class FilterValidator : AbstractValidator<SearchFilterContract>
    {
        public FilterValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0).WithMessage("Id can`t be less than 0");
            RuleFor(x=>x.Title).MinimumLength(1);
            RuleFor(x=>x.Genre).MinimumLength(1);
            RuleFor(x=>x.Year).GreaterThanOrEqualTo(1920).LessThanOrEqualTo(DateTime.Now.Year);
            RuleFor(x=>x.Country).MinimumLength(1).MaximumLength(25);
            RuleFor(x=>x.KeyWord).MinimumLength(1);
        }
    }
}
