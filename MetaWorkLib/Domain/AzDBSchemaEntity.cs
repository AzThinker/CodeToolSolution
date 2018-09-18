using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaWorkLib.Domain
{
    public class AzDBSchemaEntity
    {
        public int Id { get; set; }
        public string ObjDataName { get; set; }

        public int ObjDataType { get; set; }

        public string ObjDataDisplay { get; set; }

    }
}
