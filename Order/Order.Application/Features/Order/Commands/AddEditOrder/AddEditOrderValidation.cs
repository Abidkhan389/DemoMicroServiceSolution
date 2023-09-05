using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Order.Application.Features.Order.Commands.AddEditOrder
{
    public class AddEditOrderValidation: AbstractValidator<AddEditOrderCommands>
    {
        public AddEditOrderValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEmpty().WithMessage("{ProductName} is Required. ")
                .NotNull();
            RuleFor(c => c.OrderedOn)
                .NotEmpty().WithMessage("{ProductCode} is Required. ")
                .NotNull();
            RuleFor(c => c.OrdersDetails)
                .NotEmpty().WithMessage("{ProductPrice} is required.")
                .NotNull();
        }
    }
}
