using Finance.Entities.Models;
using FluentValidation;

namespace Finance.Services.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            this.RuleFor(x => x.nick_name).NotEmpty().WithMessage("Bu alan boş olamaz")
                .MinimumLength(3).WithMessage("Takma İsim en az 3 karakter olmalıdır.")
                .MaximumLength(20).WithMessage("Takma isim en fazla 20 karakter olmalıdır");

            RuleFor(x => x.email).NotEmpty().WithMessage("Bu alan boş olamaz")
                .MaximumLength(100).WithMessage("E-mail 100 karakterden uzun olamaz.")
                .EmailAddress().WithMessage("E-mail doğru formatta değildir.");

            RuleFor(x => x.password).NotEmpty().WithMessage("Bu alan boş olamaz")
                .MaximumLength(16).WithMessage("Şifre 16 karakterden uzun olamaz.")
                .MinimumLength(6).WithMessage("Şifre 6 karakterden az olamaz");
        }
    }
}
