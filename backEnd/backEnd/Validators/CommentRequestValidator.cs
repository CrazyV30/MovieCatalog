using backEnd.Contracts;
using FluentValidation;

namespace backEnd.Validators
{
    public class CommentRequestValidator :AbstractValidator<CommentRequest>
    {
        public CommentRequestValidator()
        {
            RuleFor(x => x.MovieId).NotEmpty().WithMessage("MovieId cannot be empty");
            RuleFor(x => x.AuthorName).NotEmpty().Length(5, 40).WithMessage("AuthorName is too short or too long");
            RuleFor(x => x.Text).NotEmpty().Length(1, 250).WithMessage("Text is too long");
        }
    }
}
