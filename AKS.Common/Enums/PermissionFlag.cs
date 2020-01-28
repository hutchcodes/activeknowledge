using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Common.Enums
{
    [Flags]
    public enum PermissionFlag
    {
        Read,
        Update,
        Delete
    }
}
