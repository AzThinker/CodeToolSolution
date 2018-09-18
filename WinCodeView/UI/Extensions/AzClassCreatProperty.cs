using MetaWorkLib.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WinCodeView.UI
{
    [TypeConverter(typeof(PropertySorter))]
    public class AzNodeLevel0Property
    {
        #region 1-数据持续化层

        [DisplayName("空间名称"), ReadOnly(true)]
        [Category("1-数据持续化层"), PropertyOrder(2)]
        public string NameSpace { get; set; }

        [DisplayName("工程名称"), ReadOnly(true)]
        [Category("1-数据持续化层"), PropertyOrder(3)]
        public string ProjectName { get; set; }

        [DisplayName("昵称"), ReadOnly(true)]
        [Category("1-数据持续化层"), PropertyOrder(5)]
        public string Nick { get; set; }
        #endregion


    }



    [TypeConverter(typeof(PropertySorter))]
    public class AzClassCreatProperty
    {
        public AzClassCreatProperty()
        {
            ObjPresentation = new ObjDataPresentation();
            //CtrSpecialitySet = new SpecialitySet();
            if (string.Compare(System.Diagnostics.Process.GetCurrentProcess().ProcessName, "devenv") == 0)
            {
                return;
            }
            var valuedef = typeof(AzClassCreatProperty).GetProperties()
                    .Where(p => p.IsDefaultValueField());
            foreach (var inf in valuedef)
            {
                var atr = inf.GetCustomAttribute(typeof(DefaultValueAttribute));
                if (atr != null)
                {
                    inf.SetValue(this, ((DefaultValueAttribute)atr).Value);
                }
            }
        }


        public AzClassCreatProperty(ObjDataPresentation objDataPresentation, SpecialitySet specialitySet)
        {
            ObjPresentation = objDataPresentation;
            //CtrSpecialitySet = specialitySet;
        }

        #region 1-数据持续化层


        [DisplayName("当前选择"), ReadOnly(true)]
        [Category("1-数据持续化层"), PropertyOrder(1)]
        public string CurrentSelect { get; set; }

        [DisplayName("空间名称"), ReadOnly(true)]
        [Category("1-数据持续化层"), PropertyOrder(2)]
        public string NameSpace { get; set; }

        [DisplayName("工程名称"), ReadOnly(true)]
        [Category("1-数据持续化层"), PropertyOrder(3)]
        public string ProjectName { get; set; }


        [DisplayName("昵称"), ReadOnly(true)]
        [Category("1-数据持续化层"), PropertyOrder(5)]
        public string Nick { get; set; }


        [DisplayName("类名称"), ReadOnly(true)]
        [Category("1-数据持续化层"), PropertyOrder(4)]
        public string ClassName { get; set; }

        [DisplayName("显示名称"), ReadOnly(true)]
        [Category("1-数据持续化层"), PropertyOrder(5)]
        public string DisplayName { get; set; }




        [DisplayName("持续化设置")]
        [Category("1-数据持续化层"), PropertyOrder(6)]
        [Browsable(true), ReadOnly(true)]
        public ObjDataPresentation ObjPresentation { get; set; }


        #endregion

        #region 2-业务层方法


        [DisplayName("增加")]
        [Category("2-业务层方法"), PropertyOrder(20)]
        [DefaultValue(true)]
        public bool HasBussniesAdd { get; set; }

        [DisplayName("删除")]
        [Category("2-业务层方法"), PropertyOrder(21)]
        [DefaultValue(true)]
        public bool HasBussniesDelete { get; set; }

        [DisplayName("编辑")]
        [Category("2-业务层方法"), PropertyOrder(22)]
        [DefaultValue(true)]
        public bool HasBussniesEdit { get; set; }

        [DisplayName("明细")]
        [Category("2-业务层方法"), PropertyOrder(23)]
        [DefaultValue(true)]
        public bool HasBussniesDetail { get; set; }

        [DisplayName("列表")]
        [Category("2-业务层方法"), PropertyOrder(23)]
        [DefaultValue(true)]
        public bool HasBussniesList { get; set; }

        [DisplayName("Json化字段")]
        [Category("2-业务层方法"), PropertyOrder(25)]
        [DefaultValue(true)]
        public bool HasBussniesJson { get; set; }

        [DisplayName("属性辅助类")]
        [Category("2-业务层方法"), PropertyOrder(25)]
        [DefaultValue(true)]
        public bool HasSpClass { get; set; }

        [DisplayName("执行辅助类")]
        [Category("2-业务层方法"), PropertyOrder(25)]
        [DefaultValue(true)]
        public bool HasExec { get; set; }
        #endregion

        #region 3-Models/WebUI
        [DisplayName("Dto默认值构造")]
        [Category(@"3-Models/WebUI"), PropertyOrder(25)]
        [DefaultValue(true)]
        public bool HasDtoConstruction { get; set; }

        #endregion

        #region 4-控制器


        [DisplayName("增加")]
        [Category("4-控制器"), PropertyOrder(40)]
        [DefaultValue(true)]
        public bool HasControllerAdd { get; set; }

        [DisplayName("删除")]
        [Category("4-控制器"), PropertyOrder(41)]
        [DefaultValue(true)]
        public bool HasControllerDelete { get; set; }

        [DisplayName("编辑")]
        [Category("4-控制器"), PropertyOrder(42)]
        [DefaultValue(true)]
        public bool HasControllerEdit { get; set; }

        [DisplayName("明细")]
        [Category("4-控制器"), PropertyOrder(43)]
        [DefaultValue(true)]
        public bool HasControllerDetail { get; set; }

        /// <summary>
        /// Net core不再设置
        /// </summary>
        [DisplayName("大文本")]
        [Category("4-控制器"), PropertyOrder(44)]
        [DefaultValue(true)]
        public bool HasBigText { get; set; }

        [DisplayName("列表")]
        [Category("4-控制器"), PropertyOrder(50)]
        [DefaultValue(true)]
        public bool HasControllerList { get; set; }

        //[DisplayName("分页")]
        //[Category("4-控制器"), PropertyOrder(51)]
        //[DefaultValue(true)]
        //public bool HasControllerPage { get; set; }

        [DisplayName("异步分页")]
        [Category("4-控制器"), PropertyOrder(52)]
        [DefaultValue(true)]
        public bool HasControllerAsynPage { get; set; }

        //[DisplayName("特性设置")]
        //[Category("4-控制器"), PropertyOrder(52)]
        //[Browsable(true), ReadOnly(true)]
        //public SpecialitySet CtrSpecialitySet { get; set; }
        #endregion

        #region 5-视图链接
        [DisplayName("链接增加")]
        [Description("在数据列表列中增加一个增加链接；")]
        [Category("5-列表视图链接"), PropertyOrder(60)]
        [DefaultValue(true)]
        public bool HasViewAdd { get; set; }

        [DisplayName("链接删除")]
        [Description("在数据列表列中增加一个删除链接；")]
        [Category("5-列表视图链接"), PropertyOrder(61)]
        [DefaultValue(true)]
        public bool HasViewDelete { get; set; }

        [DisplayName("链接编辑")]
        [Description("在数据列表列中增加一个编辑链接；")]
        [Category("5-列表视图链接"), PropertyOrder(62)]
        [DefaultValue(true)]
        public bool HasViewEdit { get; set; }

        [DisplayName("链接明细")]
        [Description("在数据列表列中增加一个明细链接；")]
        [Category("5-列表视图链接"), PropertyOrder(63)]
        [DefaultValue(true)]
        public bool HasViewDetail { get; set; }
        #endregion


        [DisplayName("权限控制")]
        [Description("在控制器增加[Authorize]标签；")]
        [Category("6-权限"), PropertyOrder(7)]
        [DefaultValue(false)]
        public bool UsePower { get; set; }


        [DisplayName("备注")]
        [Category("7-其他"), PropertyOrder(80)]
        [DefaultValue("")]
        public string Remark { get; set; }
    }

    [TypeConverter(typeof(ObjDataPresentationConverter))]
    public class ObjDataPresentation
    {
        [DisplayName("持续化类型")]
        [Category("1-数据持续化层"), PropertyOrder(7)]
        [TypeConverter(typeof(ObjDataConverter))]
        public ObjDataTypeEnum ObjDataType { get; set; }


        [DisplayName("结构查询")]
        [Description("仅用于查询类存储，与更新结构，设置为真时，生成时不会生成代码；")]
        [Category("1-数据持续化层"), PropertyOrder(8)]
        [DefaultValue(false)]
        public bool IsSchemaForOther { get; set; }

        [DisplayName("更新表名")]
        [Category("1-数据持续化层"), PropertyOrder(9)]
        [DefaultValue("")]
        [TypeConverter(typeof(UpdateTableFromConverter))]
        public string UpdateTableName { get; set; }


        [DisplayName("存储数据结构")]
        [Category("1-数据持续化层"), PropertyOrder(8)]
        [DefaultValue("")]
        [TypeConverter(typeof(UpdateTableFromConverter))]
        public string StoreProcedureQuery { get; set; }
    }

    [TypeConverter(typeof(SpecialitySetConverter))]
    public class SpecialitySet
    {
        [DisplayName("页面缓存")]
        [Category("4-控制器"), PropertyOrder(50)]
        public bool PageCache { get; set; }


        [DisplayName("大文本保存")]
        [Category("4-控制器"), PropertyOrder(51)]
        [DefaultValue(false)]
        public bool BigText { get; set; }
    }

}
