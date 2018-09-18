using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaWorkLib.CustomAttribute
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    internal class ExportMetaAttribute : Attribute
    {
        public ExportMetaAttribute()
        {

        }
    }
}
