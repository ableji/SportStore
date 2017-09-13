using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Model.Domain
{
    public class Province 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IList<City> Cities { get; set; }

    }
}
