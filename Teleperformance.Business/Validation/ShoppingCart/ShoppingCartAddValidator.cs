using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Entities.DTOs.ShoppingCart;

namespace Teleperformance.Business.Validation.ShoppingCart
{
    public class ShoppingCartAddValidator : AbstractValidator<ShoppingCartAddDto>
    {
        public ShoppingCartAddValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().Length(1, 35);
        }
    }
}
