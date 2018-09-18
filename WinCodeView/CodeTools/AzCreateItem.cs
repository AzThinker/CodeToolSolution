using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinCodeView.UI;

namespace WinCodeView.CodeTools
{
    public class AzCreateItem
    {
        public bool AzthinkerDal_Interface { get; set; }

        public bool AzthinkerDal_SQL { get; set; }

        public bool AzthinkerBll_Class { get; set; }

        public bool AzthinkerBll_ListClass { get; set; }


        public bool AzthinkerClass_WebUIDto { get; set; }


        public bool AzthinkerClass_WebListUIDto { get; set; }


        public bool AzthinkerClass_WebHandle { get; set; }


        public bool AzthinkerControllers { get; set; }


        public bool AzthinkerView_Create { get; set; }

        public bool AzthinkerView_Delete { get; set; }

        public bool AzthinkerView_Details { get; set; }

        public bool AzthinkerView_Edit { get; set; }


        public bool AzthinkerView_Index { get; set; }

        public bool AzthinkerView_IndexPage { get; set; }

        public bool AzthinkerView_IndexPageDetails { get; set; }

        public AzCreateItem()
        {
            AzthinkerDal_Interface = false;
            AzthinkerDal_SQL = false;
            AzthinkerBll_Class = false;
            AzthinkerBll_ListClass = false;
            AzthinkerClass_WebUIDto = false;
            AzthinkerClass_WebListUIDto = false;
            AzthinkerClass_WebHandle = false;
            AzthinkerControllers = false;
            AzthinkerView_Create = false;
            AzthinkerView_Delete = false;
            AzthinkerView_Details = false;
            AzthinkerView_Edit = false;
            AzthinkerView_Index = false;
            AzthinkerView_IndexPage = false;
            AzthinkerView_IndexPageDetails = false;
        }

        public static bool GetAzCreateItemEnable(string opCreate, AzClassCreatProperty creatProperty)
        {
            AzCreateItem azCreateItem = GetAzCreateItem(creatProperty);
            var props = TypeDescriptor.GetProperties(typeof(AzCreateItem)).Find(opCreate, true);
            if (props == null)
            {
                return false;
            }

            return (bool)props.GetValue(azCreateItem);
        }
        public static AzCreateItem GetAzCreateItem(AzClassCreatProperty creatProperty)
        {
            AzCreateItem createItem = new AzCreateItem();
            if (creatProperty == null)
            {
                return createItem;
            }
            if (creatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_tables ||
                   creatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_customTables)
            {
                createItem.AzthinkerDal_Interface = true;
                createItem.AzthinkerDal_SQL = true;
                createItem.AzthinkerBll_Class = true;
                createItem.AzthinkerBll_ListClass = creatProperty.HasBussniesList;
                createItem.AzthinkerClass_WebUIDto = true;
                createItem.AzthinkerClass_WebListUIDto = true;
                createItem.AzthinkerClass_WebHandle = true;
                createItem.AzthinkerControllers = true;
                createItem.AzthinkerView_Create = creatProperty.HasControllerAdd;
                createItem.AzthinkerView_Delete = creatProperty.HasControllerDelete;
                createItem.AzthinkerView_Details = creatProperty.HasControllerDetail;
                createItem.AzthinkerView_Edit = creatProperty.HasControllerEdit;

                createItem.AzthinkerView_Index = creatProperty.HasControllerList && !creatProperty.HasControllerAsynPage;
                createItem.AzthinkerView_IndexPage = creatProperty.HasControllerList && creatProperty.HasControllerAsynPage;
                createItem.AzthinkerView_IndexPageDetails = creatProperty.HasControllerList && creatProperty.HasControllerAsynPage;
            }
            else if ((creatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_views ||
                 creatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_customViews) && (!creatProperty.ObjPresentation.IsSchemaForOther))
            {

                createItem.AzthinkerDal_Interface = true;
                createItem.AzthinkerDal_SQL = true;
                createItem.AzthinkerBll_Class = true;
                createItem.AzthinkerBll_ListClass = creatProperty.HasBussniesList;
                createItem.AzthinkerClass_WebUIDto = true;
                createItem.AzthinkerClass_WebListUIDto = true;
                createItem.AzthinkerClass_WebHandle = true;
                createItem.AzthinkerControllers = true;
                createItem.AzthinkerView_Create = creatProperty.HasControllerAdd && !(string.IsNullOrWhiteSpace(creatProperty.ObjPresentation.UpdateTableName));
                createItem.AzthinkerView_Delete = creatProperty.HasControllerDelete && !(string.IsNullOrWhiteSpace(creatProperty.ObjPresentation.UpdateTableName));
                createItem.AzthinkerView_Details = creatProperty.HasControllerDetail;
                createItem.AzthinkerView_Edit = creatProperty.HasControllerEdit && !(string.IsNullOrWhiteSpace(creatProperty.ObjPresentation.UpdateTableName));

                createItem.AzthinkerView_Index = creatProperty.HasControllerList && !creatProperty.HasControllerAsynPage;
                createItem.AzthinkerView_IndexPage = creatProperty.HasControllerList && creatProperty.HasControllerAsynPage;
                createItem.AzthinkerView_IndexPageDetails = creatProperty.HasControllerList && creatProperty.HasControllerAsynPage;
            }
            else if (creatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_FuncstoredProcedure)
            {
                createItem.AzthinkerDal_Interface = true;
                createItem.AzthinkerDal_SQL = true;
                createItem.AzthinkerBll_Class = true;
                createItem.AzthinkerClass_WebUIDto = true;
                createItem.AzthinkerClass_WebHandle = true;

            }
            else if (creatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_QuerystoredProcedure)
            {
                createItem.AzthinkerDal_Interface = true;
                createItem.AzthinkerDal_SQL = true;
                createItem.AzthinkerBll_Class = true;
                createItem.AzthinkerBll_ListClass = true;
                createItem.AzthinkerClass_WebUIDto = true;
                createItem.AzthinkerClass_WebHandle = true;
                createItem.AzthinkerClass_WebListUIDto = true;
                createItem.AzthinkerControllers = true;
                createItem.AzthinkerView_Index = creatProperty.HasControllerList && !creatProperty.HasControllerAsynPage;
                createItem.AzthinkerView_IndexPage = creatProperty.HasControllerList && creatProperty.HasControllerAsynPage;
                createItem.AzthinkerView_IndexPageDetails = creatProperty.HasControllerList && creatProperty.HasControllerAsynPage;

            }
            return createItem;

        }
    }
}
