using Finance.Entities.Models;
using FluentValidation;

namespace Finance.Services.Validators
{
    public class AmountValidator : AbstractValidator<Amount>
    {
        public AmountValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("Bu alan boş olamaz.")
                .MinimumLength(2).WithMessage("İşlem adı minimum 2 karakter olmalıdır.")
                .MaximumLength(30).WithMessage("İşlem adı maximum 30 karakter olabilir.");

            RuleFor(x => x.amount)
                .NotEmpty().WithMessage("Bu alan boş olamaz.")
                .InclusiveBetween(1, 15).WithMessage("İşlem miktarı 2 ile 15 arasında olmalıdır.");
        }
    }
}
