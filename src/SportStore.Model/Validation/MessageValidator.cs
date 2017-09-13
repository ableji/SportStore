using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using SportStore.Model.Domain;

namespace SportStore.Model.Validation
{
    public class MessageValidator: AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Enter Name");
            RuleFor(x => x.Name).Length(3,20).WithMessage("Invalid Name Length");

            RuleFor(x => x.Subject).MaximumLength(30).WithMessage("Long Subject");

            RuleFor(x => x.body).MaximumLength(200).WithMessage("Long Message");
        }
    }
}
