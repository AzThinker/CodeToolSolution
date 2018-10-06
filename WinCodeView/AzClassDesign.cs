using MetaWorkLib.Config;
using MetaWorkLib.Domain;
using MetaWorkLib.MetaInit;
using MetaWorkLib.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using WinCodeView.CodeTools;
using WinCodeView.UI;

namespace WinCodeView
{
    public partial class AzClassDesign : Form
    {
        private AzMetaTableEntity metaTableEntity = new AzMetaTableEntity();
        private AzProjectInformation azProjectInformation = new AzProjectInformation();
        private AzNormalSet NormalSet = AzNormalSet.GetAzNormalSet();
        private F_Progress f_Progress;
        public AzClassDesign()
        {
            InitializeComponent();
            f_Progress = new F_Progress(backgroundWorker1);
        }

        public static void ShowAzClassDesign(Form owner)
        {
            if (!MetadataOperate.ConfigWhetherInit())
            {
                GeneralHelpler.SomethingWarning("当前配置未正确设置，请重新设置！！");
                return;
            }

            if (!MetadataOperate.MetaWhetherInit())
            {
                GeneralHelpler.SomethingWarning("当前配置元数据没有初始化，请先生成！！");
                return;
            }
            foreach (var f in owner.MdiChildren)
            {
                if (f.Name == typeof(AzClassDesign).Name)
                {
                    f.BringToFront();
                    return;
                }
            }
            AzClassDesign azClassDesign = new AzClassDesign
            {
                MdiParent = owner
            };
            azClassDesign.Show();
        }

        private void azCdgnDBSchema1_TreeNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            metaTableEntity = new AzMetaTableEntity();
            if (e.Node.Level == 1)
            {
                metaTableEntity = azCdgnDBSchema1.GetAzMetaTableEntity();
                azCdgnDetail1.SetCurrentObject(metaTableEntity, 1);
                azCdgnClassProperty1.SetSelectedObject(metaTableEntity);
            }
            else
            {
                azCdgnDetail1.SetCurrentObject(metaTableEntity, 0);
                azCdgnClassProperty1.SetSelectedObject();
            }
            azCdgnMasterDisplay1.SetMetaTableEntity(metaTableEntity);
        }

        private void azCdgnClassProperty1_Load(object sender, EventArgs e)
        {

        }

        private void AzClassDesign_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            azCdgnClassProperty1.SetSelectedObject();

 

            azProjectInformation = AzCreateItem.GetProjectInformation();
            if (azProjectInformation != null)
            {
                toolStripMenuItem0102.Visible = azProjectInformation.HasDalInterface;
                toolStripMenuItem0201.Visible = azProjectInformation.HasDalLayer;
                toolStripMenuItem02.Visible = azProjectInformation.HasDalInterface || azProjectInformation.HasDalLayer;

                toolStripMenuItem0301.Visible = azProjectInformation.HasBll;
                toolStripMenuItem0303.Visible = azProjectInformation.HasBllList;
                toolStripMenuItem03.Visible = azProjectInformation.HasBll || azProjectInformation.HasBllList;


                toolStripMenuItem0401.Visible = azProjectInformation.HasWebUIDto;
                toolStripMenuItem0402.Visible = azProjectInformation.HasWebListUIDto;
                toolStripMenuItem0403.Visible = azProjectInformation.HasWebListUIHandle;
                toolStripMenuItem04.Visible = azProjectInformation.HasWebUIDto || azProjectInformation.HasWebListUIDto || azProjectInformation.HasWebListUIHandle;

            }




            azCdgnDBSchema1.GetContextMenu().Items.Insert(0, mspCreateCode);


        }

        private AzProjectInformation GetProjectInformation()
        {
            azProjectInformation.IsTempSaveToFile = azCdgnMasterDisplay1.Atk_WillCreatFiles;

            return azProjectInformation;
        }

        private void azCdgnMasterDisplay1_Atk_LookCurrentDbData(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(metaTableEntity.SchemaName))
            {
                AzLookDbData.ShowAzLookDbData(metaTableEntity.SchemaName);
            }

        }


        private void azCdgnDBSchema1_Atk_ReCreateDbData(object sender, EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定重新导入？如果重新导入原设置将清除！") == DialogResult.Cancel)
            {
                return;
            }

            AzMetaCloumHandle.Handle().InitColumnSchema(metaTableEntity.SchemaName);
            var listcol = AzMetaCustomCloumHandle.Handle().Select().Where(t => t.TableName == metaTableEntity.SchemaName).Go();
            foreach (var item in listcol)
            {
                var entitype = MetaDataTypeHandle.GetMetaDataType(item.FldType);
                if (entitype != null)
                {
                    item.FldCodeType = entitype.CodeType;
                    AzMetaCustomCloumHandle.Handle().Update(item);
                }

            }
            azCdgnDetail1.SetCurrentObject(metaTableEntity, 1);
        }


        private string ExportMeta(IEnumerable<AzMetaCloumEntity> azMetaCloumEntities)
        {
            var metat = azCdgnDBSchema1.GetAzMetaTableEntity();
            string metatstr = "类名：" + metat.ClassName + " 表名：" + metat.SchemaName + " 显示名：" + metat.ClassDisPlay + "\r\n";
            var str = MetaExportTxT.GetDataRows<AzMetaCloumEntity>(azMetaCloumEntities);
            return metatstr + str;
        }

        private void azCdgnDBSchema1_Atk_MetaDataExportXLS(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (FileStream file = new System.IO.FileStream(saveFileDialog1.FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    using (System.IO.TextWriter text = new System.IO.StreamWriter(file, System.Text.Encoding.Default))
                    {
                        text.Write(ExportMeta(azCdgnDetail1.GetCurrentData()));
                    }
                }
            }
        }

        private void azCdgnDBSchema1_Atk_ClearClassInf(object sender, EventArgs e)
        {
            azCdgnClassProperty1.ClearMasterInfo();
        }

        private void azCdgnDBSchema1_OnClearAllAfter()
        {
            metaTableEntity = new AzMetaTableEntity();
            azCdgnDetail1.SetCurrentObject(metaTableEntity, 0);
            azCdgnClassProperty1.SetSelectedObject();
            azCdgnMasterDisplay1.SetMetaTableEntity(metaTableEntity);
        }

        private void toolStripMenuItem07ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void azCdgnDBSchema1_Atk_MenuStrip_Opening(object sender, CancelEventArgs e)
        {
            mspCreateCode.Enabled = metaTableEntity == null ? false : metaTableEntity.Id > 0;
            if (!mspCreateCode.Enabled)
            {
                return;
            }

            MenuItemSet();

        }

        private void MenuItemSet()
        {
            var cp = azCdgnClassProperty1.GetClassCreatProperty();
            mspCreateCode.Enabled = cp != null;
            if (cp == null)
            {
                return;
            }
            AzCreateItem azCreateItem = AzCreateItem.GetAzCreateItem(cp);
            toolStripMenuItem0102.Enabled = azCreateItem.AzthinkerDal_Interface;
            toolStripMenuItem0201.Enabled = azCreateItem.AzthinkerDal_SQL;
            toolStripMenuItem0301.Enabled = azCreateItem.AzthinkerBll_Class;
            toolStripMenuItem0303.Enabled = azCreateItem.AzthinkerBll_ListClass;
            toolStripMenuItem0401.Enabled = azCreateItem.AzthinkerClass_WebUIDto;
            toolStripMenuItem0402.Enabled = azCreateItem.AzthinkerClass_WebListUIDto;
            toolStripMenuItem0403.Enabled = azCreateItem.AzthinkerClass_WebHandle;
            toolStripMenuItem0501.Enabled = azCreateItem.AzthinkerControllers;
            toolStripMenuItem0601.Enabled = azCreateItem.AzthinkerView_Create;
            toolStripMenuItem0602.Enabled = azCreateItem.AzthinkerView_Delete;
            toolStripMenuItem0604.Enabled = azCreateItem.AzthinkerView_Details;
            toolStripMenuItem0603.Enabled = azCreateItem.AzthinkerView_Edit;
            toolStripMenuItem0605.Enabled = azCreateItem.AzthinkerView_Index;
            toolStripMenuItem0606.Enabled = azCreateItem.AzthinkerView_IndexPage;
            toolStripMenuItem0607.Enabled = azCreateItem.AzthinkerView_IndexPageDetails;



        }



        private void azCdgnClassProperty1_OnMasterSaveNotification()
        {
            azCdgnMasterDisplay1.Atk_MasterInit = false;
            metaTableEntity = azCdgnClassProperty1.GetAzMetaTableEntity();
            azCdgnDetail1.SetCurrentObject(metaTableEntity, 1);

        }
        #region MenuItem

        private void toolStripMenuItem0102_Click(object sender, EventArgs e)
        {
            azCdgnDetail1.SetCodeCreate(CreateCodeCurrent("AzthinkerDal_Interface"));
        }

        private void toolStripMenuItem0201_Click(object sender, EventArgs e)
        {
            azCdgnDetail1.SetCodeCreate(CreateCodeCurrent("AzthinkerDal_SQL"));
        }

        private void toolStripMenuItem0301_Click(object sender, EventArgs e)
        {
            azCdgnDetail1.SetCodeCreate(CreateCodeCurrent("AzthinkerBll_Class"));
        }

        private void toolStripMenuItem0303_Click(object sender, EventArgs e)
        {
            azCdgnDetail1.SetCodeCreate(CreateCodeCurrent("AzthinkerBll_ListClass"));

        }

        private void toolStripMenuItem0401_Click(object sender, EventArgs e)
        {
            azCdgnDetail1.SetCodeCreate(CreateCodeCurrent("AzthinkerClass_WebUIDto"));
        }

        private void toolStripMenuItem0402_Click(object sender, EventArgs e)
        {
            azCdgnDetail1.SetCodeCreate(CreateCodeCurrent("AzthinkerClass_WebListUIDto"));
        }

        private void toolStripMenuItem0403_Click(object sender, EventArgs e)
        {
            azCdgnDetail1.SetCodeCreate(CreateCodeCurrent("AzthinkerClass_WebHandle"));
        }

        private void toolStripMenuItem0501_Click(object sender, EventArgs e)
        {
            azCdgnDetail1.SetCodeCreate(CreateCodeCurrent("AzthinkerControllers"));
        }

        private void toolStripMenuItem0601_Click(object sender, EventArgs e)
        {
            azCdgnDetail1.SetCodeCreate(CreateCodeCurrent("AzthinkerView_Create"));
        }

        private void toolStripMenuItem0602_Click(object sender, EventArgs e)
        {
            azCdgnDetail1.SetCodeCreate(CreateCodeCurrent("AzthinkerView_Delete"));
        }

        private void toolStripMenuItem0604_Click(object sender, EventArgs e)
        {
            azCdgnDetail1.SetCodeCreate(CreateCodeCurrent("AzthinkerView_Details"));
        }

        private void toolStripMenuItem0603_Click(object sender, EventArgs e)
        {
            azCdgnDetail1.SetCodeCreate(CreateCodeCurrent("AzthinkerView_Edit"));
        }

        private void toolStripMenuItem0605_Click(object sender, EventArgs e)
        {
            azCdgnDetail1.SetCodeCreate(CreateCodeCurrent("AzthinkerView_Index"));
        }

        private void toolStripMenuItem0606_Click(object sender, EventArgs e)
        {
            azCdgnDetail1.SetCodeCreate(CreateCodeCurrent("AzthinkerView_IndexPage"));
        }

        private void toolStripMenuItem0607_Click(object sender, EventArgs e)
        {
            azCdgnDetail1.SetCodeCreate(CreateCodeCurrent("AzthinkerView_IndexPageDetails"));
        }
        #endregion

        private void azCdgnDBSchema1_Load(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }
            if (string.Compare(System.Diagnostics.Process.GetCurrentProcess().ProcessName, "devenv") == 0)
            {
                return;
            }

            azCdgnMasterDisplay1.SetAzSaveCodeFileFloder(AzNormalSet.GetAzNormalSet().AzBase.AzSaveCodeFileFloder);
        }


        #region 代码生成

        private string CheckMasterSetIsValid(AzMetaTableEntity checkitem, bool showDialogue)
        {
            AzClassCreatProperty creatProperty = AzCdgnClassProperty.GetItemClassCreatProperty(checkitem);
            if (creatProperty.ObjPresentation.IsSchemaForOther)
            {
                if (showDialogue)
                {
                    GeneralHelpler.SomethingError($"当前项目{checkitem.ClassName}({checkitem.ClassDisPlay})为“结构查询”,不能生成代码！");
                }

                return $"{checkitem.ClassName}({checkitem.ClassDisPlay})";
            }

            return string.Empty;

        }

        private string CreateCodeCurrent(string opCreate)
        {
            string result= CreateCodeHandle(opCreate, azCdgnDetail1.GetCurrentObject(), azCdgnClassProperty1.GetClassCreatProperty(), azCdgnDetail1.GetCurrentData());
            azCdgnDetail1.SetCodeCreateMsg(CodeHandle.GetCodeHandleMsg());
            return result;
        }

        private void CreateCodeBath(List<string> oplist, string current)
        {
            var item = AzMetaTableHandle.Handle().Select().Where(m => m.ObjModeName == current).Go().FirstOrDefault();
            if (item == null)
            {
                return;
            }
            var itemDetails = AzMetaCloumHandle.Handle().Select().Where(t => t.TableName == item.SchemaName).OrderBy(t => t.ShowOrder).Go();
            var itemproperty = AzCdgnClassProperty.GetItemClassCreatProperty(item);
            var list = GetCreateList(oplist, itemproperty);

            foreach (string c in list)
            {
                CreateCodeHandle(c, item, itemproperty, itemDetails);
            }
            azCdgnDetail1.SetCodeCreateMsg(CodeHandle.GetCodeHandleMsg());
        }

        private string CreateCodeHandle(string opCreate, AzMetaTableEntity item, AzClassCreatProperty itemproperty, IEnumerable<AzMetaCloumEntity> itemDetails)
        {
            if (!AzCreateItem.GetAzCreateItemEnable(opCreate, itemproperty))
            {
                return string.Empty;
            }

            string path = string.Empty;
            string floder = string.Empty;
            string filename = string.Empty;
            string codestr = string.Empty;
            string ext = ".cs";


            switch (opCreate)
            {
                case "AzthinkerDal_Interface":
                    path = $"{NormalSet.AzBase.AzProjectSpace}.Dal.{NormalSet.AzBase.AzProjectName}";
                    floder = item.ClassName;
                    filename = $"I{item.ClassName}Dal";
                    codestr = CodeHandle.AzInterfaceDal(item, itemproperty);
                    break;
                case "AzthinkerDal_SQL":
                    path = $"{NormalSet.AzBase.AzProjectSpace}.Dal.{NormalSet.AzBase.AzProjectName}";
                    floder = item.ClassName;
                    filename = $"{item.ClassName}Dal";
                    codestr = CodeHandle.AzDalConcrete(item, itemproperty, itemDetails);
                    break;
                case "AzthinkerBll_Class":
                    path = $"{NormalSet.AzBase.AzProjectSpace}.BLL.{NormalSet.AzBase.AzProjectName}";
                    floder = item.ClassName;
                    filename = $"{item.ClassName}Entity";
                    codestr = CodeHandle.AzBll_Class(item, itemproperty, itemDetails);
                    break;
                case "AzthinkerBll_ListClass":
                    path = $"{NormalSet.AzBase.AzProjectSpace}.BLL.{NormalSet.AzBase.AzProjectName}";
                    floder = item.ClassName;
                    filename = $"{item.ClassName}ListEntity";
                    codestr = CodeHandle.AzBll_ListClass(item, itemproperty, itemDetails);
                    break;
                case "AzthinkerClass_WebUIDto":
                    path = $"{NormalSet.AzBase.AzProjectSpace}.UIServer.{NormalSet.AzBase.AzProjectName}";
                    floder = item.ClassName;
                    filename = $"{item.ClassName}WebDto";
                    codestr = CodeHandle.AzWebUI_Dto(item, itemproperty, itemDetails);
                    break;
                case "AzthinkerClass_WebListUIDto":
                    path = $"{NormalSet.AzBase.AzProjectSpace}.UIServer.{NormalSet.AzBase.AzProjectName}";
                    floder = item.ClassName;
                    filename = $"{item.ClassName}ListWebDto";
                    codestr = CodeHandle.AzWebUiList_Dto(item, itemproperty, itemDetails);
                    break;
                case "AzthinkerClass_WebHandle":
                    path = $"{NormalSet.AzBase.AzProjectSpace}.UIServer.{NormalSet.AzBase.AzProjectName}";
                    floder = item.ClassName;
                    filename = $"{item.ClassName}WebHandle";
                    codestr = CodeHandle.AzWebUiHandle(item, itemproperty, itemDetails);
                    break;
                case "AzthinkerControllers":
                    path = $"{NormalSet.AzBase.AzProjectSpace}.WebUI";
                    floder = "Controllers";
                    filename = $"{item.ClassName}Controller";
                    codestr = CodeHandle.AzMVC_Controllers(item, itemproperty, itemDetails);
                    break;
                case "AzthinkerView_Create":
                    path = $"{NormalSet.AzBase.AzProjectSpace}.WebUI\\View";
                    floder = item.ClassName;
                    filename = "Create";
                    ext = ".cshtml";
                    codestr = CodeHandle.AzMvc_View_Create(item, itemproperty, itemDetails);
                    break;
                case "AzthinkerView_Delete":
                    path = $"{NormalSet.AzBase.AzProjectSpace}.WebUI\\View";
                    floder = item.ClassName;
                    filename = "Delete";
                    ext = ".cshtml";
                    codestr = CodeHandle.AzMvc_View_Delete(item, itemproperty, itemDetails);
                    break;
                case "AzthinkerView_Details":
                    path = $"{NormalSet.AzBase.AzProjectSpace}.WebUI\\View";
                    floder = item.ClassName;
                    filename = "Details";
                    ext = ".cshtml";
                    codestr = CodeHandle.AzMvc_View_Details(item, itemproperty, itemDetails);
                    break;
                case "AzthinkerView_Edit":
                    path = $"{NormalSet.AzBase.AzProjectSpace}.WebUI\\View";
                    floder = item.ClassName;
                    filename = "Edit";
                    ext = ".cshtml";
                    codestr = CodeHandle.AzMvc_View_Edit(item, itemproperty, itemDetails);
                    break;
                case "AzthinkerView_Index":
                    path = $"{NormalSet.AzBase.AzProjectSpace}.WebUI\\View";
                    floder = item.ClassName;
                    filename = "Index";
                    ext = ".cshtml";
                    codestr = CodeHandle.AzMvc_View_GetList(item, itemproperty, itemDetails);
                    break;
                case "AzthinkerView_IndexPage":
                    path = $"{NormalSet.AzBase.AzProjectSpace}.WebUI\\View";
                    floder = item.ClassName;
                    filename = "Index";
                    ext = ".cshtml";
                    codestr = CodeHandle.AzMvc_View_GetListPage(item, itemproperty, itemDetails);
                    break;
                case "AzthinkerView_IndexPageDetails":
                    path = $"{NormalSet.AzBase.AzProjectSpace}.WebUI\\View";
                    floder = item.ClassName;
                    filename = "DetailsPage";
                    ext = ".cshtml";
                    codestr = CodeHandle.AzMvc_View_GetListPageDetails(item, itemproperty, itemDetails);
                    break;
            }
            if (string.IsNullOrWhiteSpace(path))
            {
                return "";
            }
            if (string.IsNullOrWhiteSpace(codestr))
            {
                return $"{item.ClassName}({item.ClassDisPlay})，无可生成代码";
            }
            FileHelper.SaveCodeToFile(path, floder, filename, codestr, ext);

            return codestr;
        }

        private List<string> GetCreateList(List<string> list, AzClassCreatProperty itemproperty)
        {
            List<string> templist = new List<string>();
            var s = list.Where(m => m == "AzthinkerView").FirstOrDefault();
            if (string.IsNullOrEmpty(s))
            {
                return list;
            }
            templist.AddRange(list);
            templist.Remove("AzthinkerView");
            templist.Add("AzthinkerView_Create");
            templist.Add("AzthinkerView_Delete");
            templist.Add("AzthinkerView_Details");
            templist.Add("AzthinkerView_Edit");
            if (itemproperty.HasControllerAsynPage)
            {
                templist.Add("AzthinkerView_IndexPage");
                templist.Add("AzthinkerView_IndexPageDetails");
            }
            else
            {
                templist.Add("AzthinkerView_Index");
            }


            return templist;
        }

        private void CreateCodeBathOne(object sender, DoWorkEventArgs e)
        {
            List<string> list = AzCreateCodeSelect.ShowAzCreateCodeSelect();
            if (list.Count() == 0)
            {
                return;
            }

            CreateCodeBath(list, metaTableEntity.ObjModeName);
        }
        private void azCdgnDBSchema1_Atk_CurrentCreateCode(object sender, EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定生成当前项代码！")
                      == DialogResult.OK)
            {
                try
                {
                    backgroundWorker1.DoWork += new DoWorkEventHandler(CreateCodeBathOne);
                    f_Progress.MsgText("生成当前项代码");
                    backgroundWorker1.RunWorkerAsync();
                    f_Progress.ShowDialog(this);
                }
                finally
                {
                    backgroundWorker1.DoWork -= new DoWorkEventHandler(CreateCodeBathOne);
                }
            }
        }

        private void CreateCodeBathSelect(object sender, DoWorkEventArgs e)
        {
            List<string> listselect = azCdgnDBSchema1.GetSelectNodesName();
            if (listselect.Count() == 0)
            {
                GeneralHelpler.SomethingWarning("没有选择的项！");
                return;
            }
            List<string> list = AzCreateCodeSelect.ShowAzCreateCodeSelect();
            if (list.Count() == 0)
            {
                return;
            }

            foreach (string s in listselect)
            {
                CreateCodeBath(list, s);
            }

        }
        private void azCdgnDBSchema1_Atk_BatchCreateCode(object sender, EventArgs e)
        {

            if (GeneralHelpler.ConfirmQuestionOperate("确定生成所有选择项代码！")
                      == DialogResult.OK)
            {
                try
                {
                    backgroundWorker1.DoWork += new DoWorkEventHandler(CreateCodeBathSelect);
                    f_Progress.MsgText("生成所有选择项代码");
                    backgroundWorker1.RunWorkerAsync();
                    f_Progress.ShowDialog(this);
                }
                finally
                {
                    backgroundWorker1.DoWork -= new DoWorkEventHandler(CreateCodeBathSelect);
                }
            }

        }
        #endregion

        private void AzClassDesign_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
