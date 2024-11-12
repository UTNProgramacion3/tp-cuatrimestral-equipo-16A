using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum SedeEnum
    {
        [Description("Central")]
        Central = 1,

        [Description("Norte")]
        Norte = 2,

        [Description("Sur")]
        Sur = 3,
    }
}
