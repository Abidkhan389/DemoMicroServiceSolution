using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Product.Application.Features.Product.Commands.AddEditProfuct
{
    public class AddEditProductValidation: AbstractValidator<AddEditProductCommands>
    {
        public AddEditProductValidation()
        {
            RuleFor(c => c.ProductName)
                .NotEmpty().WithMessage("{ProductName} is Required. ")
                .NotNull()
                .MaximumLength(350).WithMessage("{ProductName}  must not exceed 350 characters. ")
                .MinimumLength(4).WithMessage("{ProductName}  must be grated than  4 characters");
            RuleFor(c => c.ProductCode)
                .NotEmpty().WithMessage("{ProductCode} is Required. ")
                .NotNull()
                .MaximumLength(25).WithMessage("{ProductCode} must be not exceed 25. ")
                .MinimumLength(4).WithMessage("{ProductCode} must be greater than 4 characters. ");
            RuleFor(c => c.ProductPrice)
                .NotEmpty().WithMessage("{ProductPrice} is required.")
                .NotNull()
                .Must(price => Regex.IsMatch(price.ToString(), @"^\d+$")).WithMessage("{ProductPrice} must be a valid integer.");
        }
    }
}
