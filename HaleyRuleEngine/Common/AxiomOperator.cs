using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haley.Enums
{
    public enum AxiomOperator
    {
        [Description("none")]
        Empty,
        [Description("equals")]
        Equals,
        [Description("doesn't equals")]
        NotEquals,
        [Description("is greater than")]
        GreaterThan,
        [Description("is lesser than")]
        LessThan,
        [Description("contains")]
        Contains,
        [Description("doesn not contains")]
        NotContains,
        [Description("starts with")]
        StartsWith,
        [Description("ends with")]
        EndsWith,
        //Between,
    }
}
