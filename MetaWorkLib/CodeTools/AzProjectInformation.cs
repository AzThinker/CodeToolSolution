using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaWorkLib.CodeTools
{
    public class AzProjectInformation
    {
        public bool DalMapSaveFolder { get; set; }
        public string DalMapDir { get; set; }

        public bool HasDalInterface { get; set; }

        public bool HasDalMapExt { get; set; }

        public bool HasDalLayer { get; set; }

        public bool HasBllExt { get; set; }

        public string DalLayerDir { get; set; }

        public string DalLayerSuffix { get; set; }
        public bool HasWebServe { get; set; }
        public bool HasWebServeExt { get; set; }
        public bool HasBatchDell { get; set; }
        public bool HasUpDataBmp { get; set; }
        public bool HasJosnData { get; set; }
        public bool HasJquerySet { get; set; }
        public bool HasAreaSet { get; set; }
        public bool TreeCollapse { get; set; }
        public string WebUIDir { get; set; }
        public bool BuessInterface { get; set; }
        public string DalInterfaceMapDir { get; set; }
        public bool HasDalMapHaspubfiles { get; set; }

        public string Ver { get; set; }

        public bool IsTempSaveToFile { get; set; }
    }
}
