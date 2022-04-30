using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Entities.DTOs.Category;

namespace Teleperformance.Business.Validation.Category
{
    public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori Adı Boş Olmamalı")
               .Length(3, 50).WithMessage("Kategori Adı 3-50 Karakter Olmalıdır");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Kategori Açıklaması Boş Olmamalı")
                .Length(10, 100).WithMessage("Kategori Açıklaması Boş Olmamalı");
        }
    }
}
