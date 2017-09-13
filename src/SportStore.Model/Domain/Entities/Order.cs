using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using FluentValidation.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportStore.Model.Domain
{
    //[Validator(typeof(OrderValidator))]
    public class Order : BaseModel
    {

        [BindNever]
        public bool Shiped { get; set; }
        public string UserId { get; set; }

        public virtual IList<CartItem> CartItems { get; set; }
        public virtual AppUser User { get; set; }

    }
}

