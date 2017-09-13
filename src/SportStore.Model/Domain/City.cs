using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Model.Domain
{
    public class City 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ProvinceID { get; set; }
        public virtual Province Province { get; set; }

    }
}
