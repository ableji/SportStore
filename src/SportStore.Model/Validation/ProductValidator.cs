using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using SportStore.Model.Domain;

namespace SportStore.Model.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {

            RuleFor(p => p.ProductName).NotEmpty().WithMessage("Enter a Name");
            RuleFor(p => p.ProductName).Length(5, 50).WithMessage("Invalid Name Length");
            RuleFor(p => p.ProductPrice).NotEmpty().WithMessage("Enter Product Price");
            RuleFor(p => p.ProductPrice).LessThan(9999999999).WithMessage("Invalid Price");
            RuleFor(p => p.ProductPrice).GreaterThan(0).WithMessage("Invalid Price");
            RuleFor(p => p.ShortDescription).MaximumLength(40).WithMessage("Large description");

        }

    }
}

