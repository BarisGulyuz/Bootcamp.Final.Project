using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Entities.DTOs.ShoppingCartItems;

namespace Teleperformance.Business.Validation.ShoppingCartItems
{
   public class ShoppingCartItemAddValidator : AbstractValidator<ShoppingCartItemAddDto>
    {
        public ShoppingCartItemAddValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
}
