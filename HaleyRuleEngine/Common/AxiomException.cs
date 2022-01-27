using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haley.Enums
{
    public enum AxiomException
    {
        NoException,
        FormatException,
        NullInputException,
        PropertyNotFoundException,
        SourceValueNotFoundException,
        TargetValueNotFoundException,
        OperatorSymbolNotFound,
        ValueTypeConversionError
    }
}
