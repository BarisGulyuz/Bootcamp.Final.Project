using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Entities.DTOs.Product;

namespace Teleperformance.Business.Validation.Product
{
    public class ProductUpdateValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(" Alanı Boş Bırakılamaz");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Kategori Alanı Boş Bırakılamaz");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ürün Adı Boş Bırakılamaz");
            RuleFor(x => x.Name).Length(2, 150).WithMessage("Ürün Adı 2 - 150 Karakter Uzunluğunda Olmalıdır");
        }
    }
}
