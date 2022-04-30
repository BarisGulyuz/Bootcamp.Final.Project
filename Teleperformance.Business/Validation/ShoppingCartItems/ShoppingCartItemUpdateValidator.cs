using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Entities.DTOs.ShoppingCartItems;

namespace Teleperformance.Business.Validation.ShoppingCartItems
{
    public class ShoppingCartItemUpdateValidator : AbstractValidator<ShoppingCartItemUpdateDto>
    {
        public ShoppingCartItemUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
}
