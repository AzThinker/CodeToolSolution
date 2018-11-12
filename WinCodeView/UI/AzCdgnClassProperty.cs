using MetaWorkLib.Config;
using MetaWorkLib.Domain;
using MetaWorkLib.MetaInit;
using MetaWorkLib.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WinCodeView.UI
{
    public partial class AzCdgnClassProperty : UserControl
    {
        private AzMetaTableEntity azMetaTable;
        private AzClassCreatProperty azClassCreatProperty = new AzClassCreatProperty();
        private static readonly Attribute BrowsableFalse = new BrowsableAttribute(false);
        private static readonly Attribute BrowsableTrue = new BrowsableAttribute(true);
        private static readonly Attribute ReadOnlyTrue = new ReadOnlyAttribute(true);
        private static readonly Attribute ReadOnlyFalse = new ReadOnlyAttribute(false);
        public AzCdgnClassProperty()
        {
            InitializeComponent();
            propertyGrid1.SelectedObjectsChanged += new EventHandler(propertyGrid1_SelectedObjectsChanged);
        }

        private void AzCdgnClassProperty_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = new AzNodeLevel0Property();
            if (DesignMode)
            {
                return;
            }
            if (string.Compare(System.Diagnostics.Process.GetCurrentProcess().ProcessName, "devenv") == 0)
            {
                return;
            }
            UpdateTableFromConverter.StringArray= DataHelper.GetGetDBListForProperty();

        }
        void propertyGrid1_SelectedObjectsChanged(object sender, EventArgs e)
        {
            propertyGrid1.Tag = propertyGrid1.PropertySort;
            propertyGrid1.PropertySort = PropertySort.CategorizedAlphabetical;
            propertyGrid1.Paint += new PaintEventHandler(propertyGrid1_Paint);
        }

        private void SetClassCreatPropertyTrue()
        {
            var props = TypeDescriptor.GetProperties(typeof(AzClassCreatProperty)).AsEnumerable<PropertyDescriptor>(); ;
            foreach (var p in props)
            {
                PropertyHelper.AddAttribute(typeof(AzClassCreatProperty), p.Name, BrowsableTrue);
            }

            var propsobj = TypeDescriptor.GetProperties(typeof(ObjDataPresentation)).AsEnumerable<PropertyDescriptor>(); ;
            foreach (var p in propsobj)
            {
                PropertyHelper.AddAttribute(typeof(ObjDataPresentation), p.Name, BrowsableTrue);
            }
            PropertyHelper.AddAttribute(typeof(ObjDataPresentation), "ObjDataType", ReadOnlyTrue);
            PropertyHelper.AddAttribute(typeof(ObjDataPresentation), "UpdateTableName", ReadOnlyTrue);
            //var propsspset = TypeDescriptor.GetProperties(typeof(SpecialitySet)).AsEnumerable<PropertyDescriptor>(); ;
            //foreach (var p in propsspset)
            //{
            //    PropertyHelper.AddAttribute(typeof(SpecialitySet), p.Name, BrowsableTrue);
            //}
        }

        public void SetSelectedObject(AzMetaTableEntity azMetaTableEntity = null)
        {
            azMetaTable = azMetaTableEntity;
            if (azMetaTableEntity == null)
            {
                AzNodeLevel0Property azNodeLevel0Property = new AzNodeLevel0Property
                {
                    NameSpace = AzNormalSet.GetAzNormalSet().AzBase.AzProjectSpace,
                    ProjectName = AzNormalSet.GetAzNormalSet().AzBase.AzProjectName,
                    Nick = AzNormalSet.GetAzNormalSet().AzBase.AzNick
                };
                toolStripButton1.Enabled = false;
                propertyGrid1.SelectedObject = azNodeLevel0Property;
                azClassCreatProperty = null;
            }
            else
            {
                toolStripButton1.Enabled = true;
                azClassCreatProperty = GetItemClassCreatProperty(azMetaTableEntity);// new AzClassCreatProperty();

                SetClassCreatPropertyTrue();
                if (azClassCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_tables ||
                    azClassCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_customTables)
                {
                    List<string> listp = new List<string> { "HasBussniesJson", "HasBigText", "HasExec", "HasSpClass" };
                    PropertyHelper.AddAttribute(typeof(AzClassCreatProperty), listp, BrowsableFalse);
                    List<string> listfalseObj = new List<string> { "IsSchemaForOther", "UpdateTableName", "StoreProcedureQuery" };
                    PropertyHelper.AddAttribute(typeof(ObjDataPresentation), listfalseObj, BrowsableFalse);
                    //PropertyHelper.AddAttribute(typeof(SpecialitySet), "BigText", BrowsableFalse);
                    PropertyHelper.AddAttribute(typeof(ObjDataPresentation), "ObjDataType", ReadOnlyTrue);




                }
                else

                if (azClassCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_views ||
                    azClassCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_customViews)
                {
                    List<string> listp = new List<string> { "HasBussniesJson", "HasBigText", "HasExec", "HasSpClass" };
                    PropertyHelper.AddAttribute(typeof(AzClassCreatProperty), listp, BrowsableFalse);
                    List<string> listfalseObj = new List<string> { "StoreProcedureQuery" };
                    if (azClassCreatProperty.ObjPresentation.IsSchemaForOther)
                    {
                        listfalseObj = new List<string> { "UpdateTableName", "StoreProcedureQuery" };
                    }

                    PropertyHelper.AddAttribute(typeof(ObjDataPresentation), listfalseObj, BrowsableFalse);
                    PropertyHelper.AddAttribute(typeof(ObjDataPresentation), "UpdateTableName", ReadOnlyFalse);
                    PropertyHelper.AddAttribute(typeof(SpecialitySet), "BigText", BrowsableFalse);
                }
                else

                if (azClassCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_FuncstoredProcedure)
                {
                    List<string> listp = new List<string> { "HasBussniesJson", "HasBigText", "HasViewDetail",
                    "HasViewEdit","HasViewDelete","HasViewAdd","HasControllerAsynPage", "HasSpClass" ,
                    "HasControllerList","HasControllerDetail","HasControllerEdit","HasControllerDelete",
                    "HasControllerAdd","HasDtoConstruction","HasBussniesList","HasBussniesDetail",
                    "HasBussniesEdit","HasBussniesDelete","HasBussniesAdd"};
                    PropertyHelper.AddAttribute(typeof(AzClassCreatProperty), listp, BrowsableFalse);

                    List<string> listfalseObj = new List<string> { "IsSchemaForOther", "UpdateTableName" };
                    PropertyHelper.AddAttribute(typeof(ObjDataPresentation), listfalseObj, BrowsableFalse);

                    PropertyHelper.AddAttribute(typeof(ObjDataPresentation), "UpdateTableName", ReadOnlyFalse);
                    PropertyHelper.AddAttribute(typeof(SpecialitySet), "BigText", BrowsableFalse);
                }
                else if (azClassCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_QuerystoredProcedure)
                {
                    List<string> listp = new List<string> { "HasBussniesJson", "HasBigText", "HasViewDetail",
                    "HasViewEdit","HasViewDelete","HasViewAdd", "HasExec",
                    "HasControllerDetail","HasControllerEdit","HasControllerDelete",
                    "HasControllerAdd","HasDtoConstruction","HasBussniesDetail",
                    "HasBussniesEdit","HasBussniesDelete","HasBussniesAdd","HasControllerAsynPage"};
                    PropertyHelper.AddAttribute(typeof(AzClassCreatProperty), listp, BrowsableFalse);

                    List<string> listfalseObj = new List<string> { "IsSchemaForOther", "UpdateTableName" };
                    PropertyHelper.AddAttribute(typeof(ObjDataPresentation), listfalseObj, BrowsableFalse);

                    PropertyHelper.AddAttribute(typeof(ObjDataPresentation), "UpdateTableName", ReadOnlyFalse);
                    PropertyHelper.AddAttribute(typeof(SpecialitySet), "BigText", BrowsableFalse);
                }


                propertyGrid1.SelectedObject = azClassCreatProperty;
                propertyGrid1.ExpandAllGridItems();
            }

        }

        public static AzClassCreatProperty GetItemClassCreatProperty(AzMetaTableEntity azMetaTableEntity)
        {
            AzClassCreatProperty azClassCreatProperty = new AzClassCreatProperty();

            if (!string.IsNullOrWhiteSpace(azMetaTableEntity.CodeSetVales))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                azClassCreatProperty = js.Deserialize<AzClassCreatProperty>(azMetaTableEntity.CodeSetVales);
            }
            azClassCreatProperty.ClassName = azMetaTableEntity.ClassName.Replace(' ', '_');
            azClassCreatProperty.DisplayName = azMetaTableEntity.ClassDisPlay;
            azClassCreatProperty.CurrentSelect = azMetaTableEntity.SchemaName;
            azClassCreatProperty.NameSpace = AzNormalSet.GetAzNormalSet().AzBase.AzProjectSpace;
            azClassCreatProperty.ProjectName = AzNormalSet.GetAzNormalSet().AzBase.AzProjectName;
            azClassCreatProperty.Nick = AzNormalSet.GetAzNormalSet().AzBase.AzNick;

            if (azMetaTableEntity.ObjModeType == 1)
            {
                switch (azMetaTableEntity.ObjDataType)
                {
                    case 1:
                        azClassCreatProperty.ObjPresentation.ObjDataType = ObjDataTypeEnum.atk_tables;
                        break;
                    case 2:
                        azClassCreatProperty.ObjPresentation.ObjDataType = ObjDataTypeEnum.atk_views;
                        break;
                    case 3:
                        if (string.IsNullOrWhiteSpace(azClassCreatProperty.ObjPresentation.StoreProcedureQuery))
                        {
                            azClassCreatProperty.ObjPresentation.ObjDataType = ObjDataTypeEnum.atk_FuncstoredProcedure;
                        }
                        else
                        {
                            azClassCreatProperty.ObjPresentation.ObjDataType = ObjDataTypeEnum.atk_QuerystoredProcedure;
                        }

                        break;
                }
            }
            else
            {
                switch (azMetaTableEntity.ObjDataType)
                {
                    case 1:
                        azClassCreatProperty.ObjPresentation.ObjDataType = ObjDataTypeEnum.atk_customTables;
                        break;
                    case 2:
                        azClassCreatProperty.ObjPresentation.ObjDataType = ObjDataTypeEnum.atk_customViews;
                        break;
                }
            }
            //确保IsSchemaForOther设置正确
            if (azClassCreatProperty.ObjPresentation.IsSchemaForOther)
            {
                if (!(azClassCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_views ||
                   azClassCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_customViews))
                {
                    azClassCreatProperty.ObjPresentation.IsSchemaForOther = false;
                }
            }

            if (azClassCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_tables ||
                   azClassCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_customTables)
            {
                azClassCreatProperty.HasBussniesJson = false;
                azClassCreatProperty.HasBigText = false;
                azClassCreatProperty.HasExec = false;
                azClassCreatProperty.HasSpClass = false;
                azClassCreatProperty.ObjPresentation.IsSchemaForOther = false;
                azClassCreatProperty.ObjPresentation.UpdateTableName = "";
                azClassCreatProperty.ObjPresentation.StoreProcedureQuery = "";
            }
            else

               if (azClassCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_views ||
                   azClassCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_customViews)
            {
                azClassCreatProperty.HasBussniesJson = false;
                azClassCreatProperty.HasBigText = false;
                azClassCreatProperty.HasExec = false;
                azClassCreatProperty.HasSpClass = false;
                if (azClassCreatProperty.ObjPresentation.IsSchemaForOther)
                {
                    azClassCreatProperty.ObjPresentation.UpdateTableName = "";
                }
            }
            else if (azClassCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_FuncstoredProcedure)
            {
                azClassCreatProperty.HasBussniesJson = false;
                azClassCreatProperty.HasBigText = false;
                azClassCreatProperty.HasViewDetail = false;
                azClassCreatProperty.HasViewEdit = false;
                azClassCreatProperty.HasViewDelete = false;
                azClassCreatProperty.HasViewAdd = false;
                azClassCreatProperty.HasControllerAsynPage = false;
                azClassCreatProperty.HasControllerList = false;
                azClassCreatProperty.HasControllerDetail = false;
                azClassCreatProperty.HasControllerEdit = false;
                azClassCreatProperty.HasControllerDelete = false;
                azClassCreatProperty.HasControllerAdd = false;
                azClassCreatProperty.HasControllerDelete = false;
                azClassCreatProperty.HasDtoConstruction = false;
                azClassCreatProperty.HasBussniesDetail = false;
                azClassCreatProperty.HasBussniesEdit = false;
                azClassCreatProperty.HasBussniesDelete = false;
                azClassCreatProperty.HasBussniesAdd = false;

                azClassCreatProperty.ObjPresentation.IsSchemaForOther = false;
                azClassCreatProperty.ObjPresentation.UpdateTableName = "";
            }
            else if (azClassCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_QuerystoredProcedure)
            {
                azClassCreatProperty.HasBussniesJson = false;
                azClassCreatProperty.HasBigText = false;
                azClassCreatProperty.HasViewDetail = false;
                azClassCreatProperty.HasViewEdit = false;
                azClassCreatProperty.HasViewDelete = false;
                azClassCreatProperty.HasViewAdd = false;
                azClassCreatProperty.HasExec = false;
                azClassCreatProperty.HasControllerDetail = false;
                azClassCreatProperty.HasControllerEdit = false;
                azClassCreatProperty.HasControllerDelete = false;
                azClassCreatProperty.HasControllerAdd = false;
                azClassCreatProperty.HasControllerDelete = false;
                azClassCreatProperty.HasDtoConstruction = false;
                azClassCreatProperty.HasBussniesDetail = false;
                azClassCreatProperty.HasBussniesEdit = false;
                azClassCreatProperty.HasBussniesDelete = false;
                azClassCreatProperty.HasBussniesAdd = false;
                azClassCreatProperty.HasControllerAsynPage = false;
                azClassCreatProperty.ObjPresentation.IsSchemaForOther = false;
                azClassCreatProperty.ObjPresentation.UpdateTableName = "";

            }

            azClassCreatProperty.ObjPresentation.UpdateTableName = azMetaTableEntity.SchemaName;
            return azClassCreatProperty;
        }

        public AzClassCreatProperty GetClassCreatProperty()
        {
            return azClassCreatProperty;
        }

        private void propertyGrid1_Paint(object sender, PaintEventArgs e)
        {

            var categorysinfo = propertyGrid1.SelectedObject.GetType().GetField("categorys", BindingFlags.NonPublic | BindingFlags.Instance);
            if (categorysinfo != null)
            {
                var categorys = categorysinfo.GetValue(propertyGrid1.SelectedObject) as List<String>;
                propertyGrid1.CollapseAllGridItems();
                GridItemCollection currentPropEntries = propertyGrid1.GetType().GetField("currentPropEntries", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(propertyGrid1) as GridItemCollection;
                var newarray = currentPropEntries.Cast<GridItem>().OrderBy((t) => categorys.IndexOf(t.Label)).ToArray();
                currentPropEntries.GetType().GetField("entries", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(currentPropEntries, newarray);


                propertyGrid1.PropertySort = (PropertySort)propertyGrid1.Tag;
                propertyGrid1.ExpandAllGridItems();
            }
            propertyGrid1.Paint -= new PaintEventHandler(propertyGrid1_Paint);
        }

        private void propertyGrid1_Validating(object sender, CancelEventArgs e)
        {

        }

        [Category("Atk_Save")]
        public event Action OnMasterSaveNotification;

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {

        }

        public AzMetaTableEntity GetAzMetaTableEntity()
        {
            return azMetaTable;
        }

        private void SaveMasterInfo()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            if (string.IsNullOrWhiteSpace(azClassCreatProperty.ObjPresentation.UpdateTableName))
            {
                azClassCreatProperty.ObjPresentation.UpdateTableName = azMetaTable.SchemaName;
            }
            azMetaTable.CodeSetVales = js.Serialize(azClassCreatProperty);
            if (azMetaTable.ComeFrom == 1)
            {
                azMetaTable.ObjModeType = 1;
                azMetaTable.AppName = MetadataOperate.GetDefAppNameUpdate(AzNormalSet.GetAzNormalSet().AzBase.AzTablePrefix);
                azMetaTable = AzMetaTableHandle.Handle().Insert(azMetaTable);
                return;
            }
            AzMetaTableHandle.Handle().Updata(azMetaTable);
            SetSelectedObject(azMetaTable);
            OnMasterSaveNotification?.Invoke();
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定保存当前修改吗？") == DialogResult.Cancel)
            {
                return;
            }
            SaveMasterInfo();
            //  propertyGrid1.SelectedObject = azClassCreatProperty;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            propertyGrid1.ExpandAllGridItems();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            propertyGrid1.CollapseAllGridItems();
        }


        public void ClearMasterInfo()
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定清除当前主信息吗？") == DialogResult.Cancel)
            {
                return;
            }
            azClassCreatProperty = new AzClassCreatProperty();
            azClassCreatProperty.ClassName = azMetaTable.ClassName.Replace(' ', '_');
            azClassCreatProperty.DisplayName = azMetaTable.ClassDisPlay;
            azClassCreatProperty.CurrentSelect = azMetaTable.SchemaName;
            azClassCreatProperty.NameSpace = AzNormalSet.GetAzNormalSet().AzBase.AzProjectSpace;
            azClassCreatProperty.ProjectName = AzNormalSet.GetAzNormalSet().AzBase.AzProjectName;
            azClassCreatProperty.Nick = AzNormalSet.GetAzNormalSet().AzBase.AzNick;
            propertyGrid1.SelectedObject = azClassCreatProperty;

            SaveMasterInfo();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
