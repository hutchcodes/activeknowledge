using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Common.Extensions
{
    public static class StringExtensions
    {
        public static Guid ToGuidOrDefault(this string inValue)
        {
            var outValue = Guid.Empty;
            Guid.TryParse(inValue, out outValue);
            return outValue;
        }
    }
}
