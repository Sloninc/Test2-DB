using System;
using System.Collections.Generic;

#nullable disable

namespace Test2.Model
{
    public partial class Parameter
    {
        public int ParameterId { get; set; }
        public int TestId { get; set; }
        public string ParameterName { get; set; }
        public decimal? RequiredValue { get; set; }
        public decimal? MeasuredValue { get; set; }

        public virtual Test Test { get; set; }
    }
}
