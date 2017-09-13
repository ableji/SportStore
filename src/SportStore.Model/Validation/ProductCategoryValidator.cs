using FluentValidation;
using SportStore.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Model.Validation
{
    public class ProductCategoryValidator: AbstractValidator<ProductCategory>
    {
        public ProductCategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Invalida Name");
            RuleFor(x => x.Name).MaximumLength(500).WithMessage("Invalida Length");

        }
    }
}
