using System;
using System.ComponentModel;
using System.Collections.Generic;

#nullable disable

namespace Test2.Model
{
    public partial class Test
    {
        public Test()
        {
            Parameters = new HashSet<Parameter>();
        }

        public int TestId { get; set; }
        public DateTime TestDate { get; set; }
        public string BlockName { get; set; }
        public string Note { get; set; }

        public virtual ICollection<Parameter> Parameters { get; set; }
    }
}
