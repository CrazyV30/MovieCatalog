using backEnd.Contracts;
using FluentValidation;

namespace backEnd.Validators
{
    public class MovieUpdateRequestValidator:AbstractValidator<UpdateMovieRequest>
    {
        public MovieUpdateRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().Length(1, 50).WithMessage("Title is too long");
            RuleFor(x => x.Description).NotEmpty().Length(1, 250).WithMessage("Description is too long");
            RuleFor(x => x.Director).NotEmpty().Length(5, 40).WithMessage("Director is too short or too long");
            RuleFor(x => x.PhotoPath).NotEmpty().Must(uri => Uri.IsWellFormedUriString(uri, UriKind.RelativeOrAbsolute)).WithMessage("Uncorrect poster url");
            RuleFor(x => x.MoviePath).NotEmpty().Must(uri => Uri.IsWellFormedUriString(uri, UriKind.RelativeOrAbsolute)).WithMessage("Uncorrect poster url");
            RuleFor(x => x.Runtime).NotEmpty().Matches(@"^[0]?[01234]:[0-5][0-9]$").WithMessage("Runtime does not match pattern X:YY");
            RuleFor(x => x.ReleaseYear).NotEmpty().LessThanOrEqualTo(DateTime.Now.Year).GreaterThan(1920).WithMessage("Year must be more than 1920 and less than this year");
        }
    }
}
