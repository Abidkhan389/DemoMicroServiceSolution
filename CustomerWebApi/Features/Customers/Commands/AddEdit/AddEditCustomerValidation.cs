using FluentValidation;

namespace CustomerWebApi.Features.Customers.Commands.AddEdit
{
    public class AddEditCustomerValidation: AbstractValidator<AddEditCommand>
    {
        public AddEditCustomerValidation() 
        {
            RuleFor(c => c.CustomerName)
                .NotEmpty().WithMessage("{CustomerName} is required")
                .NotNull()
                .MaximumLength(350).WithMessage("{CustomerName} must not exceed 350 characters. ")
                .MinimumLength(4).WithMessage("{CustomerName} must  be greater than 4 character. ");
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("{Email} is Required")
                .EmailAddress().WithMessage("Invalid email address");
            RuleFor(c => c.MobileNumber)
                .NotEmpty().WithMessage("{MobileNumber} is reuqired. ")
            // Add more specific mobile number validation rules if needed
            .Matches(@"^[0-9]{10}$").WithMessage("Invalid mobile number format."); // Example: 10-digit numeric format
        }
    }
}
