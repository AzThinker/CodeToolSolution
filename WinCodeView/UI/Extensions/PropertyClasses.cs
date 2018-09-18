using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinCodeView.UI
{
    public enum ObjDataTypeEnum
    {

        [Description("未设置")]
        atk_none,
        [Description("数据表")]
        atk_tables,
        [Description("自定义数据表")]
        atk_customTables,
        [Description("数据视图")]
        atk_views,
        [Description("自定义数据视图")]
        atk_customViews,
        [Description("子数据")]
        atk_childViews,
        [Description("查询类存储过程")]
        atk_QuerystoredProcedure,
        [Description("执行类存储过程")]
        atk_FuncstoredProcedure,
    }
}
