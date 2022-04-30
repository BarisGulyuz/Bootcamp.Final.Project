using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Entities.DTOs.ShoppingCart;

namespace Teleperformance.Business.Validation.ShoppingCart
{
    public class ShoppingCartUpdateValidator : AbstractValidator<ShoppingCartUpdateDto>
    {
        public ShoppingCartUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().Length(1,35);
        }
    }
}
