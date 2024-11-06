using FluentValidation;
using Music.Market.Api.DTO;

namespace Music.Market.Api.Validations
{
    public class SaveMusicResourceValidator : AbstractValidator<SaveMusicDTO>
    {
        public SaveMusicResourceValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);

            RuleFor(x => x.ArtistId).NotEmpty().WithMessage("Artist Id must not be 0");
        }
    }
}
