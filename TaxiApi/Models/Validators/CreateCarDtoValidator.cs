using FluentValidation;
using TaxiApi.Entities;

namespace TaxiApi.Models.Validators
{
    public class CreateCarDtoValidator: AbstractValidator<CreateCarDto>
    {

        public CreateCarDtoValidator(TaxiDbContext dbContext)
        {
            RuleFor(x => x.Name).MaximumLength(40).NotEmpty();

            RuleFor(x => x.Plate)
                .Length(5, 7)
                .Custom( (value,context) =>
                {
                    var PlateInUse = dbContext.Cars.Any(c => c.Plate == value);

                    if(PlateInUse)
                        context.AddFailure("Plate is already registered");
                });

            RuleFor(x => x.Category).MaximumLength(30);

            RuleFor(x => x.Damage).NotNull();

        }
    }
}
