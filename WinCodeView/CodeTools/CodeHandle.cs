using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaWorkLib.Config;
using MetaWorkLib.Domain;
using MetaWorkLib.MetaInit;
using MetaWorkLib.Utils;
using WinCodeView.UI;

namespace WinCodeView.CodeTools
{
    public static class CodeHandle
    {
        private static StringBuilder _msgstringBuilder;

        private static bool hasSummary;

        public static bool HasSummary { get => hasSummary; set => hasSummary = value; }

        static CodeHandle()
        {
            _msgstringBuilder = new StringBuilder();
        }

        public static string GetCodeHandleMsg()
        {
            string result = _msgstringBuilder.ToString();
            _msgstringBuilder.Clear();
            return result;
        }
        public static string GetCurrentTemplatePath()
        {
            var azset = AzNormalSet.GetAzNormalSet().AzBase;
            string filspath = azset.AzTemplateFolder + @"\";
            return filspath;
        }

        public static string GetTemplateConfig()
        {
            string path = GetCurrentTemplatePath() + "TemplateSet.json";
            return FileHelper.ReadTemplateFile(path);
        }

        #region DalDto


        private static string GetPropertyList(this string willrepStr, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in azMetaCloums)
            {
                if (item.IsSelect == true && ((item.IsDataField == true) || (item.IsBinaryTo == true)))
                {
                    if (hasSummary)
                    {
                        stringBuilder.AddLineStatement("/// <summary>");
                        stringBuilder.AddLineStatement($"///{item.FldDisplay}");
                        stringBuilder.AddLineStatement("/// </summary>");
                    }
                   
                    if (item.IsNullable == true)
                    {
                        var entitype = MetaDataTypeHandle.GetMetaDataType(item.FldType);
                        stringBuilder.AddLineStatement($"public {entitype.CodeGeneric} {item.FldNameTo} {{ get;set;}}");
                    }
                    else
                    {
                        stringBuilder.AddLineStatement($"public {item.FldCodeType} {item.FldNameTo} {{ get;set;}}");
                    }

                }

            }

            return willrepStr.ReaplaceTemplateForWord("Ai_Class_Property_List", stringBuilder.ToString());
        }

        private static string GetAtrrFormFld(string addAttr)
        {
            if (string.IsNullOrWhiteSpace(addAttr))
            {
                return string.Empty;
            }

            var lines = addAttr.Replace("  ", "|").Split('|');
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in lines)
            {
                stringBuilder.AddLineStatement($"{item}");
            }

            return stringBuilder.ToString();
        }

        private static string GetPropertyList_WithAttribute(IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var col in azMetaCloums)
            {
                if (AzDalInterface.WebtUiIgnoreProperty(col.FldName))
                {
                    continue;
                }

                if (col.IsSelect == true && ((col.IsDataField == true) || (col.IsBinaryTo == true)))
                {
                    if (hasSummary)
                    {
                        stringBuilder.AddLineStatement("/// <summary>");
                        stringBuilder.AddLineStatement($"///{col.FldDisplay}");
                        stringBuilder.AddLineStatement("/// </summary>");
                    }
                    //  var entitype = MetaDataTypeHandle.GetMetaDataType(item.FldType);
                    if (col.IsRequired == true)
                    {
                        stringBuilder.AddLineStatement($"[Required(ErrorMessage =\"{col.FldDisplay}为必填项！\")]");
                    }
                    stringBuilder.AddLineStatement($"[Display(Name =\"{col.FldDisplay}\")]");
                    stringBuilder.Append(GetAtrrFormFld(col.AddAttr));
                    if (col.IsNullable == true)
                    {
                        var entitype = MetaDataTypeHandle.GetMetaDataType(col.FldType);
                        stringBuilder.AddLineStatement($"public {entitype.CodeGeneric} {col.FldNameTo} {{ get;set;}}");
                    }
                    else
                    {
                        stringBuilder.AddLineStatement($"public {col.FldCodeType} {col.FldNameTo} {{ get;set;}}");
                    }
                }

            }

            return stringBuilder.ToString();
        }

        private static string ReplacContext(this string willrepStr, AzMetaTableEntity azMetaTable)
        {


            var aznormalset = AzNormalSet.GetAzNormalSet();
            var azbase = aznormalset.AzBase;
            return willrepStr.ReaplaceTemplateForWord("Ai_Project_NameSpace", azbase.AzProjectSpace)
                             .ReaplaceTemplateForWord("Ai_Project_UI_FullNameSpace", azbase.AzProjectSpace + @".WebUI")
                             .ReaplaceTemplateForWord("Ai_Bll_ClassName", azMetaTable.ClassName.Replace(' ', '_'))
                             .ReaplaceTemplateForWord("Ai_Bll_Edit_ClassName", azMetaTable.ClassName.Replace(' ', '_'))
                             .ReaplaceTemplateForWord("Ai_Object_ChineseName", string.IsNullOrWhiteSpace(azMetaTable.ClassDisPlay) ? azMetaTable.ClassName.Replace(' ', '_') : azMetaTable.ClassDisPlay)
                             .ReaplaceTemplateForWord("Ai_Bll_List_ClassName", azMetaTable.ClassName.Replace(' ', '_') + "List")
                             .ReaplaceTemplateForWord("Ai_ProjectName", azbase.AzProjectName)
                             .ReaplaceTemplateForWord("Ai_Folder", azMetaTable.ClassName.Replace(' ', '_'))
                             .ReaplaceTemplateForWord("Ai_Cnn_Name", azbase.AzDbSqlConnectionName);
        }

        public static string AzDalDto(AzMetaTableEntity azMetaTable, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            string path = GetCurrentTemplatePath() + "AzthinkerDal_Dto.txt";
            string models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }
            return models.ReplacContext(azMetaTable).GetPropertyList(azMetaCloums);
        }

        #endregion


        #region InterfaceDal



        public static string AzInterfaceDal(AzMetaTableEntity azMetaTable, AzClassCreatProperty classCreatProperty)
        {
            string path = string.Empty;
            string models = string.Empty;
            switch (classCreatProperty.ObjPresentation.ObjDataType)
            {
                case ObjDataTypeEnum.atk_tables:
                case ObjDataTypeEnum.atk_views:
                case ObjDataTypeEnum.atk_customTables:
                case ObjDataTypeEnum.atk_customViews:
                    path = GetCurrentTemplatePath() + "AzthinkerDal_Interface.txt";
                    models = FileHelper.ReadTemplateFile(path);
                    StringBuilder stringBuilder = new StringBuilder();
                    if (classCreatProperty.HasBussniesAdd)
                    {
                        stringBuilder.AppendLine(AzDalInterface.GetDalInterface("DB_Insert"));
                    }
                    if (classCreatProperty.HasBussniesEdit)
                    {
                        stringBuilder.AppendLine(AzDalInterface.GetDalInterface("DB_Update"));
                    }
                    if (classCreatProperty.HasBussniesDelete)
                    {
                        stringBuilder.AppendLine(AzDalInterface.GetDalInterface("DB_Delete"));
                    }
                    if (classCreatProperty.HasBussniesDetail)
                    {
                        stringBuilder.AppendLine(AzDalInterface.GetDalInterface("DB_Fetch"));
                    }
                    if (classCreatProperty.HasBussniesList)
                    {
                        stringBuilder.AppendLine(AzDalInterface.GetDalInterface("DB_FetchList"));
                    }
                    models = models.ReaplaceTemplateForWord("AI_Dal_InterfaceMethod", stringBuilder.ToString());

                    models = models.ReplacContext(azMetaTable);
                    break;

                case ObjDataTypeEnum.atk_FuncstoredProcedure:
                    path = GetCurrentTemplatePath() + "AzthinkerDal_Sp_Cmd_Interface.txt";
                    models = FileHelper.ReadTemplateFile(path);
                    models = models.ReplacContext(azMetaTable);
                    break;

                case ObjDataTypeEnum.atk_QuerystoredProcedure:
                    path = GetCurrentTemplatePath() + "AzthinkerDal_Sp_Query_Interface.txt";
                    models = FileHelper.ReadTemplateFile(path);
                    models = models.ReplacContext(azMetaTable);
                    break;


            }

            return models;
        }


        #endregion



        #region MyDalConcrete

        private static string AzDalConcrete_Insert(IEnumerable<AzMetaCloumEntity> azMetaCloums, AzClassCreatProperty classCreatProperty)
        {
            string path = GetCurrentTemplatePath() + "AzthinkerDal_SQL_Insert.txt";
            string models = string.Empty;
            models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }
            _msgstringBuilder.AppendLine(path);
            models = ReplacContextForDB(models, classCreatProperty.HasBussniesAdd, classCreatProperty);
            if (classCreatProperty.HasBussniesAdd)
            {
                if (models.Contains("Ai_Sql_Insert_Statement"))
                {
                    var framgment1 = AzSql_Statement.Sql_Fragment_For_Insert(azMetaCloums, classCreatProperty);
                    models = models
                        .ReaplaceTemplateForWord("Ai_Sql_Insert_Statement", framgment1.codesInfo)
                        .ReaplaceTemplateForWord("Ai_Sql_Insert_AutoId", framgment1.addInfo);
                }
                if (models.Contains("Ai_Sql_PutIn_Parameters"))
                {
                    var framgment1 = AzSql_Statement.ParamCreates(azMetaCloums, classCreatProperty, AzOperateSqlModel.atkInsert);
                    models = models
                        .ReaplaceTemplateForWord("Ai_Sql_PutIn_Parameters", framgment1.models)
                        .ReaplaceTemplateForWord("Ai_Sql_IDENTITY_Return", framgment1.addId);
                }
            }
            return models;
        }

        private static string AzDalConcrete_Fetch(IEnumerable<AzMetaCloumEntity> azMetaCloums, AzClassCreatProperty classCreatProperty)
        {
            string path = GetCurrentTemplatePath() + "AzthinkerDal_SQL_Fetch.txt";
            string models = string.Empty;
            models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }
            _msgstringBuilder.AppendLine(path);
            models = ReplacContextForDB(models, classCreatProperty.HasBussniesDetail, classCreatProperty);
            if (models.Contains("Ai_Where_KeysField_SQL"))
            {
                models = AzSql_Statement.MulitKeyStrSQL(azMetaCloums, "Where");
            }
            if (models.Contains("Ai_Sql_Commbin_A"))
            {
                var str1 = AzSql_Statement.Sql_Fragment_For_Fetch(azMetaCloums, "[a0].");
                models = models
                    .ReaplaceTemplateForWord("Ai_Sql_Commbin_A", str1)
                    .ReaplaceTemplateForWord("Ai_Query_Table", classCreatProperty.ObjPresentation.UpdateTableName);
            }
            if (classCreatProperty.HasBussniesDetail)
            {
                if (models.Contains("Ai_Sql_Fetch_Statement"))
                {
                    var str1 = AzSql_Statement.Sql_Fragment_For_Fetch(azMetaCloums);
                    models = models
                        .ReaplaceTemplateForWord("Ai_Sql_Fetch_Statement", str1);
                }
                if (models.Contains("Ai_Sql_Field_CopyTo_Class"))
                {
                    var str2 = AzSql_Statement.SQlFiledCopyItem("azItem.", azMetaCloums);
                    models = models
                        .ReaplaceTemplateForWord("Ai_Sql_Field_CopyTo_Class", str2);
                }
                if (models.Contains("Ai_Sql_PutIn_Keys_Parameters"))
                {
                    var str3 = AzSql_Statement.ParamCreatekey(azMetaCloums);
                    models = models
                        .ReaplaceTemplateForWord("Ai_Sql_PutIn_Keys_Parameters", str3);
                }
            }
            return models;
        }
        private static string AzDalConcrete_Update(IEnumerable<AzMetaCloumEntity> azMetaCloums, AzClassCreatProperty classCreatProperty)
        {
            string path = GetCurrentTemplatePath() + "AzthinkerDal_SQL_Update.txt";
            string models = string.Empty;
            models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }
            _msgstringBuilder.AppendLine(path);
            models = ReplacContextForDB(models, classCreatProperty.HasBussniesEdit, classCreatProperty);
            if (models.Contains("Ai_Where_KeysField_SQL"))
            {
                var str0 = AzSql_Statement.MulitKeyStrSQL(azMetaCloums, "Where");
                models = models
                        .ReaplaceTemplateForWord("Ai_Where_KeysField_SQL", str0);
            }

            if (classCreatProperty.HasBussniesEdit)
            {
                if (models.Contains("Ai_Sql_Update_Statement"))
                {
                    var str1 = AzSql_Statement.Sql_Fragment_For_Update(azMetaCloums, classCreatProperty);
                    models = models
                        .ReaplaceTemplateForWord("Ai_Sql_Update_Statement", str1);
                }
                if (models.Contains("Ai_Sql_PutIn_Keys_Parameters"))
                {
                    var str2 = AzSql_Statement.ParamCreatekey(azMetaCloums);
                    models = models
                        .ReaplaceTemplateForWord("Ai_Sql_PutIn_Keys_Parameters", str2);
                }
                if (models.Contains("Ai_Sql_PutIn_Parameters"))
                {
                    var framgment1 = AzSql_Statement.ParamCreates(azMetaCloums, classCreatProperty, AzOperateSqlModel.atkInsert);
                    models = models
                        .ReaplaceTemplateForWord("Ai_Sql_PutIn_Parameters", framgment1.models);
                }
            }
            return models;
        }

        private static string AzDalConcrete_Delete(AzMetaTableEntity azMetaTable, IEnumerable<AzMetaCloumEntity> azMetaCloums, AzClassCreatProperty classCreatProperty)
        {
            string path = GetCurrentTemplatePath() + "AzthinkerDal_SQL_Delete.txt";
            string models = string.Empty;
            models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }
            _msgstringBuilder.AppendLine(path);
            models = models.ReplacContext(azMetaTable);
            models = ReplacContextForDB(models, classCreatProperty.HasBussniesDelete, classCreatProperty);
            if (models.Contains("Ai_Where_KeysField_SQL"))
            {
                var str0 = AzSql_Statement.MulitKeyStrSQL(azMetaCloums, "Where");
                models = models
                        .ReaplaceTemplateForWord("Ai_Where_KeysField_SQL", str0);
            }

            if (models.Contains("Ai_Sql_PutIn_Parameters"))
            {
                var framgment1 = AzSql_Statement.ParamCreates(azMetaCloums, classCreatProperty, AzOperateSqlModel.atkInsert);
                models = models
                    .ReaplaceTemplateForWord("Ai_Sql_PutIn_Parameters", framgment1.models);
            }

            if (models.Contains("Ai_Sql_PutIn_Keys_Parameters"))
            {
                var framgment1 = AzSql_Statement.ParamCreatekey(azMetaCloums);
                models = models
                    .ReaplaceTemplateForWord("Ai_Sql_PutIn_Keys_Parameters", framgment1);
            }
            return models;
        }

        private static string AzDalConcrete_FetchList(AzMetaTableEntity azMetaTable, IEnumerable<AzMetaCloumEntity> azMetaCloums, AzClassCreatProperty classCreatProperty)
        {
            string path = GetCurrentTemplatePath() + "AzthinkerDal_SQL_FetchList.txt";
            string models = string.Empty;
            models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }
            _msgstringBuilder.AppendLine(path);
            models = models.ReplacContext(azMetaTable);
            models = ReplacContextForDB(models, classCreatProperty.HasBussniesDetail, classCreatProperty);
            if (models.Contains("Ai_Sql_Commbin_A"))
            {
                var str1 = AzSql_Statement.Sql_Fragment_For_Fetch(azMetaCloums, "[a0].");
                models = models
                    .ReaplaceTemplateForWord("Ai_Sql_Commbin_A", str1)
                    .ReaplaceTemplateForWord("Ai_Query_Table", classCreatProperty.ObjPresentation.UpdateTableName);
            }
            if (classCreatProperty.HasBussniesDetail || classCreatProperty.HasBussniesList)
            {
                if (models.Contains("Ai_Sql_Fetch_Statement"))
                {
                    var str1 = AzSql_Statement.Sql_Fragment_For_Fetch(azMetaCloums);
                    models = models
                        .ReaplaceTemplateForWord("Ai_Sql_Fetch_Statement", str1);
                }
                if (models.Contains("Ai_Sql_Field_CopyTo_Class"))
                {
                    string str2 = "";

                    if (classCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_QuerystoredProcedure)
                    {
                        IEnumerable<AzMetaCloumEntity> azMetaCloumssp = AzMetaCloumHandle.Handle()
                                                            .Select()
                                                            .Where(m => m.TableName == classCreatProperty.ObjPresentation.StoreProcedureQuery)
                                                            .Go();
                        str2 = AzSql_Statement.SQlFiledCopyItem("vItem.", azMetaCloumssp);
                    }
                    else
                    {
                        str2 = AzSql_Statement.SQlFiledCopyItem("vItem.", azMetaCloums);
                    }




                    models = models
                        .ReaplaceTemplateForWord("Ai_Sql_Field_CopyTo_Class", str2);
                }

                if (models.Contains("Ai_Sql_PutIn_Keys_Parameters"))
                {
                    var str3 = AzSql_Statement.ParamCreatekey(azMetaCloums);
                    models = models
                        .ReaplaceTemplateForWord("Ai_Sql_PutIn_Keys_Parameters", str3);
                }
            }
            return models;
        }

        private static string ReplacContextForDB(string models, bool azAsk, AzClassCreatProperty classCreatProperty)
        {
            if (!azAsk)
            {
                _msgstringBuilder.AppendLine("throw new NotImplementedException();");
                return "throw new NotImplementedException();".AddPerTab(2);

            }
            var azset = AzNormalSet.GetAzNormalSet();
            models = models
                .ReaplaceTemplateForWord("Ai_Do_Table", classCreatProperty.ObjPresentation.UpdateTableName.Trim())
                .ReaplaceTemplateForWord("Ai_Query_Table", classCreatProperty.ObjPresentation.UpdateTableName.Trim())
                .ReaplaceTemplateForWord("Ai_SqlDB_ConnectionString", azset.AzConnectionString.Trim());
            return models;
        }
        public static string AzDalConcrete(AzMetaTableEntity azMetaTable, AzClassCreatProperty classCreatProperty, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            string path = string.Empty;
            string models = string.Empty;
            switch (classCreatProperty.ObjPresentation.ObjDataType)
            {
                case ObjDataTypeEnum.atk_tables:
                case ObjDataTypeEnum.atk_views:
                case ObjDataTypeEnum.atk_customTables:
                case ObjDataTypeEnum.atk_customViews:
                    path = GetCurrentTemplatePath() + "AzthinkerDal_SQL.txt";
                    break;
                case ObjDataTypeEnum.atk_FuncstoredProcedure:
                    path = GetCurrentTemplatePath() + "AzthinkerDal_SQL_SP.txt";
                    break;
                case ObjDataTypeEnum.atk_QuerystoredProcedure:
                    path = GetCurrentTemplatePath() + "AzthinkerDal_SQL_SPQuery.txt";
                    break;

            }
            _msgstringBuilder.AppendLine(path);

            models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            if (classCreatProperty.HasBussniesAdd)
            {
                builder.AddLineStatement($"<${"Ai_Dal_Insert_Single"}>");
            }
            if (classCreatProperty.HasBussniesEdit)
            {
                builder.AddLineStatement($"<${"Ai_Dal_Update_Single"}>");
            }
            if (classCreatProperty.HasBussniesDelete)
            {
                builder.AddLineStatement($"<${"Ai_Dal_Delete_Body_Single"}>");
            }
            if (classCreatProperty.HasBussniesDetail)
            {
                builder.AddLineStatement($"<${"Ai_Dal_Fetch_Body_Single"}>");
            }
            if (classCreatProperty.HasBussniesList)
            {
                builder.AddLineStatement($"<${"Ai_Dal_FetchList_Body"}>");
            }
            models = models.ReaplaceTemplateForWord("Ai_Dal_Interface_imp", builder.ToString());



            models = models.ReplacContext(azMetaTable);
            switch (classCreatProperty.ObjPresentation.ObjDataType)
            {
                case ObjDataTypeEnum.atk_tables:
                case ObjDataTypeEnum.atk_views:
                case ObjDataTypeEnum.atk_customTables:
                case ObjDataTypeEnum.atk_customViews:

                    if (classCreatProperty.HasBussniesAdd)
                    {
                        if (models.Contains("Ai_Dal_Insert_Single"))
                        {
                            var str1 = AzDalConcrete_Insert(azMetaCloums, classCreatProperty);
                            models = models
                                .ReaplaceTemplateForWord("Ai_Dal_Insert_Single", str1);
                        }
                    }
                    if (classCreatProperty.HasBussniesEdit)
                    {
                        if (models.Contains("Ai_Dal_Update_Single"))
                        {
                            var str2 = AzDalConcrete_Update(azMetaCloums, classCreatProperty);
                            models = models
                                .ReaplaceTemplateForWord("Ai_Dal_Update_Single", str2);
                        }
                    }
                    if (classCreatProperty.HasBussniesDelete)
                    {
                        if (models.Contains("Ai_Dal_Delete_Body_Single"))
                        {
                            var str3 = AzDalConcrete_Delete(azMetaTable, azMetaCloums, classCreatProperty);
                            models = models
                                .ReaplaceTemplateForWord("Ai_Dal_Delete_Body_Single", str3);
                        }
                    }
                    if (classCreatProperty.HasBussniesDetail)
                    {
                        if (models.Contains("Ai_Dal_Fetch_Body_Single"))
                        {
                            var str4 = AzDalConcrete_Fetch(azMetaCloums, classCreatProperty);
                            models = models
                                .ReaplaceTemplateForWord("Ai_Dal_Fetch_Body_Single", str4);
                        }
                    }
                    if (classCreatProperty.HasBussniesList)
                    {
                        if (models.Contains("Ai_Dal_FetchList_Body"))
                        {
                            var str5 = AzDalConcrete_FetchList(azMetaTable, azMetaCloums, classCreatProperty);
                            models = models
                                .ReaplaceTemplateForWord("Ai_Dal_FetchList_Body", str5);
                        }
                    }
                    break;
                case ObjDataTypeEnum.atk_FuncstoredProcedure:
                    if (models.Contains("Ai_Sql_Exec_Params"))
                    {
                        var str9 = AzSql_Statement.SpParamCreates(azMetaTable);
                        models = models
                            .ReaplaceTemplateForWord("Ai_Sql_Exec_Params", str9);
                    }
                    if (models.Contains("Ai_Sql_Query_Parameters_Out"))
                    {
                        var str10 = AzSql_Statement.SpParamOutCreate(azMetaCloums);
                        models = models
                            .ReaplaceTemplateForWord("Ai_Sql_Query_Parameters_Out", str10);
                    }
                    break;
                case ObjDataTypeEnum.atk_QuerystoredProcedure:
                    if (models.Contains("Ai_Sql_Field_CopyTo_Class"))
                    {
                        string str2 = "";

                        if (classCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_QuerystoredProcedure)
                        {
                            IEnumerable<AzMetaCloumEntity> azMetaCloumssp = AzMetaCloumHandle.Handle()
                                                                .Select()
                                                                .Where(m => m.TableName == classCreatProperty.ObjPresentation.StoreProcedureQuery)
                                                                .Go();
                            str2 = AzSql_Statement.SQlFiledCopyItem("vItem.", azMetaCloumssp);
                        }
                        else
                        {
                            str2 = AzSql_Statement.SQlFiledCopyItem("vItem.", azMetaCloums);
                        }




                        models = models
                            .ReaplaceTemplateForWord("Ai_Sql_Field_CopyTo_Class", str2);
                        //var str6 = AzSql_Statement.SQlFiledCopyItem("vItem.", azMetaCloums);
                        //models = models
                        //    .ReaplaceTemplateForWord("Ai_Sql_Field_CopyTo_Class", str6);
                    }
                    if (models.Contains("Ai_Sql_Query_Parameters_Out"))
                    {
                        var str7 = AzSql_Statement.SpParamOutCreate(azMetaCloums);
                        models = models
                            .ReaplaceTemplateForWord("Ai_Sql_Query_Parameters_Out", str7);
                    }
                    if (models.Contains("Ai_Sql_Query_Parameters"))
                    {
                        var str8 = AzSql_Statement.SpParamCreates(azMetaTable);
                        models = models
                            .ReaplaceTemplateForWord("Ai_Sql_Query_Parameters", str8);
                    }
                    break;

            }
            switch (classCreatProperty.ObjPresentation.ObjDataType)
            {
                case ObjDataTypeEnum.atk_FuncstoredProcedure:
                case ObjDataTypeEnum.atk_QuerystoredProcedure:
                    if (models.Contains("Ai_Arg_Parameters_Notes"))
                    {
                        var str12 = AzSql_Statement.SpParamMarker_1(azMetaCloums, true);
                        models = models
                            .ReaplaceTemplateForWord("Ai_Arg_Parameters_Notes", str12);
                    }
                    if (models.Contains("Ai_Arg_Parameters"))
                    {
                        var str13 = AzSql_Statement.SpParamarg_For_Mothod(azMetaCloums);
                        models = models
                            .ReaplaceTemplateForWord("Ai_Arg_Parameters", str13);
                    }
                    if (models.Contains("Ai_Arg_Parameters_Call"))
                    {
                        var str14 = AzSql_Statement.SpParam_Input(azMetaCloums);
                        models = models
                            .ReaplaceTemplateForWord("Ai_Arg_Parameters_Call", str14);
                    }
                    break;
            }
            bool ask = classCreatProperty.HasBussniesAdd || classCreatProperty.HasBussniesEdit ||
                    classCreatProperty.HasBussniesDetail || classCreatProperty.HasBussniesList;
            models = ReplacContextForDB(models, ask, classCreatProperty)
                     .ReaplaceTemplateForWord("Ai_KeyField_Name", AzSql_Statement.KeyFieldFirst(azMetaCloums).FldName);
            return models.ReplacContext(azMetaTable);
        }


        #endregion

        #region BllClass

        private static string GetBllInterfaces(AzClassCreatProperty classCreatProperty)
        {
            List<string> result = new List<string>();
            if (classCreatProperty.HasBussniesAdd)
            {
                result.Add(AzDalInterface.GetBLLInterface("DB_Insert"));
            }
            if (classCreatProperty.HasBussniesEdit)
            {
                result.Add(AzDalInterface.GetBLLInterface("DB_Update"));
            }
            if (classCreatProperty.HasBussniesDelete)
            {
                result.Add(AzDalInterface.GetBLLInterface("DB_Delete"));
            }
            if (classCreatProperty.HasBussniesDetail)
            {
                result.Add(AzDalInterface.GetBLLInterface("DB_Fetch"));
            }
            //if (classCreatProperty.HasBussniesList)
            //{
            //    result.Add(AzDalInterface.GetBLLInterface("DB_FetchList"));
            //}
            string retVal = string.Empty;
            foreach (string item in result)
            {
                retVal += String.Format(", {0}", item);

            }

            return retVal;
        }

        public static string AzBll_Class(AzMetaTableEntity azMetaTable, AzClassCreatProperty classCreatProperty, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            string path = string.Empty;
            string models = string.Empty;
            switch (classCreatProperty.ObjPresentation.ObjDataType)
            {
                case ObjDataTypeEnum.atk_tables:
                case ObjDataTypeEnum.atk_views:
                case ObjDataTypeEnum.atk_customTables:
                case ObjDataTypeEnum.atk_customViews:
                    path = GetCurrentTemplatePath() + "AzthinkerBll_Class.txt";
                    break;
                case ObjDataTypeEnum.atk_FuncstoredProcedure:
                    path = GetCurrentTemplatePath() + "AzthinkerBll_Class_Sp_Cmd.txt";
                    break;
                case ObjDataTypeEnum.atk_QuerystoredProcedure:
                    path = GetCurrentTemplatePath() + "AzthinkerBll_Class_Sp_Query.txt";
                    break;

            }
            _msgstringBuilder.AppendLine(path);
            models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }
            models = models.ReplacContext(azMetaTable);
            switch (classCreatProperty.ObjPresentation.ObjDataType)
            {
                case ObjDataTypeEnum.atk_tables:
                case ObjDataTypeEnum.atk_views:
                case ObjDataTypeEnum.atk_customTables:
                case ObjDataTypeEnum.atk_customViews:
                    if (models.Contains("AI_Bll_Interface"))
                    {
                        var str1 = GetBllInterfaces(classCreatProperty);
                        models = models
                            .ReaplaceTemplateForWord("AI_Bll_Interface", str1);
                    }
                    StringBuilder builder = new StringBuilder();
                    if (classCreatProperty.HasBussniesAdd)
                    {
                        builder.AddLineStatement($"<${"Ai_Bll_Method_Insert"}>");
                    }
                    if (classCreatProperty.HasBussniesEdit)
                    {
                        builder.AddLineStatement($"<${"Ai_Bll_Method_Update"}>");
                    }
                    if (classCreatProperty.HasBussniesDelete)
                    {
                        builder.AddLineStatement($"<${"Ai_Bll_Method_Delete"}>");
                    }
                    if (classCreatProperty.HasBussniesDetail)
                    {
                        builder.AddLineStatement($"<${"Ai_Bll_Method_Fetch"}>");
                    }

                    models = models.ReaplaceTemplateForWord("Ai_Bll_Interface_imp", builder.ToString());
                    //

                    if (classCreatProperty.HasBussniesAdd)
                    {
                        if (models.Contains("Ai_Bll_Method_Insert"))
                        {
                            path = GetCurrentTemplatePath() + "AzthinkerBll_Class_Insert.txt";
                            _msgstringBuilder.AppendLine(path);
                            var str2 = FileHelper.ReadTemplateFile(path);
                            str2 = str2.ReplacContext(azMetaTable);
                            models = models.ReaplaceTemplateForWord("Ai_Bll_Method_Insert", str2);
                        }
                    }
                    if (classCreatProperty.HasBussniesEdit)
                    {
                        if (models.Contains("Ai_Bll_Method_Update"))
                        {
                            path = GetCurrentTemplatePath() + "AzthinkerBll_Class_Update.txt";
                            var str2 = FileHelper.ReadTemplateFile(path);
                            _msgstringBuilder.AppendLine(path);
                            str2 = str2.ReplacContext(azMetaTable);
                            models = models.ReaplaceTemplateForWord("Ai_Bll_Method_Update", str2);
                        }
                    }
                    if (classCreatProperty.HasBussniesDelete)
                    {
                        if (models.Contains("Ai_Bll_Method_Delete"))
                        {
                            path = GetCurrentTemplatePath() + "AzthinkerBll_Class_Delete.txt";
                            _msgstringBuilder.AppendLine(path);
                            var str2 = FileHelper.ReadTemplateFile(path);
                            str2 = str2.ReplacContext(azMetaTable);
                            models = models.ReaplaceTemplateForWord("Ai_Bll_Method_Delete", str2);
                        }
                    }
                    if (classCreatProperty.HasBussniesDetail)
                    {
                        if (models.Contains("Ai_Bll_Method_Fetch"))
                        {
                            path = GetCurrentTemplatePath() + "AzthinkerBll_Class_Fetch.txt";
                            _msgstringBuilder.AppendLine(path);
                            var str2 = FileHelper.ReadTemplateFile(path);
                            str2 = str2.ReplacContext(azMetaTable);
                            models = models.ReaplaceTemplateForWord("Ai_Bll_Method_Fetch", str2);
                        }
                    }
                    models = models.GetPropertyList(azMetaCloums);
                    break;
                case ObjDataTypeEnum.atk_FuncstoredProcedure:
                    break;
                case ObjDataTypeEnum.atk_QuerystoredProcedure:
                    IEnumerable<AzMetaCloumEntity> azMetaCloumssp = AzMetaCloumHandle.Handle()
                                                           .Select()
                                                           .Where(m => m.TableName == classCreatProperty.ObjPresentation.StoreProcedureQuery)
                                                           .Go();
                    models = models.GetPropertyList(azMetaCloumssp);
                    break;

            }
            if (models.Contains("Ai_Cmd_PrivateProperty"))
            {
                string str2 = "";

                if (classCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_QuerystoredProcedure)
                {
                    IEnumerable<AzMetaCloumEntity> azMetaCloumssp = AzMetaCloumHandle.Handle()
                                                        .Select()
                                                        .Where(m => m.TableName == azMetaTable.SchemaName)
                                                        .Go();
                    str2 = AzSql_Statement.GetpropertySpList(azMetaCloumssp);
                }
                else
                {
                    str2 = AzSql_Statement.GetpropertySpList(azMetaCloums);
                }
                models = models.ReaplaceTemplateForWord("Ai_Cmd_PrivateProperty", str2);
            }
            models = ReplacContextForDB(models, true, classCreatProperty);
            return models.ReplacContext(azMetaTable);
        }
        public static string AzBll_ListClass(AzMetaTableEntity azMetaTable, AzClassCreatProperty classCreatProperty, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            string path = string.Empty;
            string models = string.Empty;
            switch (classCreatProperty.ObjPresentation.ObjDataType)
            {
                case ObjDataTypeEnum.atk_tables:
                case ObjDataTypeEnum.atk_views:
                case ObjDataTypeEnum.atk_customTables:
                case ObjDataTypeEnum.atk_customViews:
                    path = GetCurrentTemplatePath() + "AzthinkerBll_ListClass.txt";
                    break;
                case ObjDataTypeEnum.atk_FuncstoredProcedure:
                    return models;
                case ObjDataTypeEnum.atk_QuerystoredProcedure:
                    path = GetCurrentTemplatePath() + "AzthinkerBll_ListClass_Sp_Query.txt";
                    break;

            }
            _msgstringBuilder.AppendLine(path);
            models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }

            models = models.ReplacContext(azMetaTable);
            if (classCreatProperty.HasBussniesList)
            {
                if (models.Contains("Ai_Bll_List_Method_Fetch"))
                {
                    path = GetCurrentTemplatePath() + "AzthinkerBll_ListClass_Fetch.txt";
                    var str2 = FileHelper.ReadTemplateFile(path);
                    str2 = str2.ReplacContext(azMetaTable);
                    models = models.ReaplaceTemplateForWord("Ai_Bll_List_Method_Fetch", str2);
                }
            }
            if (models.Contains("Ai_Cmd_PrivateProperty"))
            {
                string str2 = "";

                if (classCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_QuerystoredProcedure)
                {
                    IEnumerable<AzMetaCloumEntity> azMetaCloumssp = AzMetaCloumHandle.Handle()
                                                        .Select()
                                                        .Where(m => m.TableName == azMetaTable.SchemaName)
                                                        .Go();
                    str2 = AzSql_Statement.GetpropertySpList(azMetaCloumssp);
                }
                else
                {
                    str2 = AzSql_Statement.GetpropertySpList(azMetaCloums);
                }
                models = models.ReaplaceTemplateForWord("Ai_Cmd_PrivateProperty", str2);
            }
            return models.ReplacContext(azMetaTable);
        }
        #endregion

        #region WebUI
        public static string AzWebUI_Dto(AzMetaTableEntity azMetaTable, AzClassCreatProperty classCreatProperty, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            string path = string.Empty;
            string models = string.Empty;
            switch (classCreatProperty.ObjPresentation.ObjDataType)
            {
                case ObjDataTypeEnum.atk_tables:
                case ObjDataTypeEnum.atk_views:
                case ObjDataTypeEnum.atk_customTables:
                case ObjDataTypeEnum.atk_customViews:
                    path = GetCurrentTemplatePath() + "AzthinkerClass_WebUIDto.txt";
                    break;
                case ObjDataTypeEnum.atk_FuncstoredProcedure:
                    path = GetCurrentTemplatePath() + "AzthinkerClass_WebUIExcDto.txt";
                    break;
                case ObjDataTypeEnum.atk_QuerystoredProcedure:
                    path = GetCurrentTemplatePath() + "AzthinkerClassSpRes_WebUIDto.txt";
                    break;

            }
            models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }
            models = models.ReplacContext(azMetaTable);
            if (models.Contains("Ai_Class_Property_List_WithAttribute"))
            {
                string str2 = "";

                if (classCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_QuerystoredProcedure)
                {
                    IEnumerable<AzMetaCloumEntity> azMetaCloumssp = AzMetaCloumHandle.Handle()
                                                        .Select()
                                                        .Where(m => m.TableName == classCreatProperty.ObjPresentation.StoreProcedureQuery)
                                                        .Go();
                    str2 = GetPropertyList_WithAttribute(azMetaCloumssp);
                }
                else
                {
                    str2 = GetPropertyList_WithAttribute(azMetaCloums);
                }

                models = models.ReaplaceTemplateForWord("Ai_Class_Property_List_WithAttribute", str2);
            }
            if (models.Contains("Ai_Default_Value_Create"))
            {
                var str2 = AzSql_Statement.DefaultValuesCreate(azMetaCloums, classCreatProperty);
                models = models.ReaplaceTemplateForWord("Ai_Default_Value_Create", str2);
            }
            if (models.Contains("Ai_BLLClass_To_UIDto"))
            {
                string str2 = "";

                if (classCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_QuerystoredProcedure)
                {
                    IEnumerable<AzMetaCloumEntity> azMetaCloumssp = AzMetaCloumHandle.Handle()
                                                        .Select()
                                                        .Where(m => m.TableName == classCreatProperty.ObjPresentation.StoreProcedureQuery)
                                                        .Go();
                    str2 = AzSql_Statement.PropertyForSemicolon(azMetaCloumssp, "this.", "item.");
                }
                else
                {
                    str2 = AzSql_Statement.PropertyForSemicolon(azMetaCloums, "this.", "item.");
                }

                //var str2 = AzSql_Statement.PropertyForSemicolon(azMetaCloums, "this.", "item.");
                models = models.ReaplaceTemplateForWord("Ai_BLLClass_To_UIDto", str2);
            }
            if (models.Contains("Ai_UiDto_To_BLLClass"))
            {
                string str2 = "";

                if (classCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_QuerystoredProcedure)
                {
                    IEnumerable<AzMetaCloumEntity> azMetaCloumssp = AzMetaCloumHandle.Handle()
                                                        .Select()
                                                        .Where(m => m.TableName == classCreatProperty.ObjPresentation.StoreProcedureQuery)
                                                        .Go();
                    str2 = AzSql_Statement.PropertyForSemicolon(azMetaCloumssp, "data.", "this.");
                }
                else
                {
                    str2 = AzSql_Statement.PropertyForSemicolon(azMetaCloums, "data.", "this.");
                }
                models = models.ReaplaceTemplateForWord("Ai_UiDto_To_BLLClass", str2);
            }
            if (models.Contains("Ai_KeyField_CompareTo_if"))
            {
                var str2 = AzSql_Statement.MulitKeyStrCondition(azMetaCloums, "this.", "other.", false);
                if (string.IsNullOrWhiteSpace(str2))
                {
                    str2 = "1==1";
                }

                models = models.ReaplaceTemplateForWord("Ai_KeyField_CompareTo_if", str2);
            }
            if (models.Contains("Ai_UiDto_To_JosnClass"))
            {
                string str2 = "";

                if (classCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_QuerystoredProcedure)
                {
                    IEnumerable<AzMetaCloumEntity> azMetaCloumssp = AzMetaCloumHandle.Handle()
                                                        .Select()
                                                        .Where(m => m.TableName == classCreatProperty.ObjPresentation.StoreProcedureQuery)
                                                        .Go();
                    str2 = AzSql_Statement.PropertyForComma(azMetaCloumssp, "", "this.");
                }
                else
                {
                    str2 = AzSql_Statement.PropertyForComma(azMetaCloums, "", "this.");
                }
                //  var str2 = AzSql_Statement.PropertyForComma(azMetaCloums, "", "this.");
                models = models.ReaplaceTemplateForWord("Ai_UiDto_To_JosnClass", str2);
            }
            if (models.Contains("Ai_Cmd_PrivateProperty"))
            {
                string str2 = "";

                if (classCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_QuerystoredProcedure)
                {
                    IEnumerable<AzMetaCloumEntity> azMetaCloumssp = AzMetaCloumHandle.Handle()
                                                        .Select()
                                                        .Where(m => m.TableName == azMetaTable.SchemaName)
                                                        .Go();
                    str2 = AzSql_Statement.GetpropertySpList(azMetaCloumssp);
                }
                else
                {
                    str2 = AzSql_Statement.GetpropertySpList(azMetaCloums);
                }
                models = models.ReaplaceTemplateForWord("Ai_Cmd_PrivateProperty", str2);
            }
            if (models.Contains("Ai_UiDto_To_BLLClass_Param"))
            {
                string str2 = "";

                if (classCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_QuerystoredProcedure)
                {
                    IEnumerable<AzMetaCloumEntity> azMetaCloumssp = AzMetaCloumHandle.Handle()
                                                        .Select()
                                                        .Where(m => m.TableName == azMetaTable.SchemaName)
                                                        .Go();
                    str2 = AzSql_Statement.GetpropertySpCopyParamList(azMetaCloumssp, "data", "this");
                }
                else
                {
                    str2 = AzSql_Statement.GetpropertySpCopyParamList(azMetaCloums, "data", "this");
                }
                models = models.ReaplaceTemplateForWord("Ai_UiDto_To_BLLClass_Param", str2);
            }
            return models.ReplacContext(azMetaTable);
        }

        public static string AzWebUiList_Dto(AzMetaTableEntity azMetaTable, AzClassCreatProperty classCreatProperty, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            string path = string.Empty;
            switch (classCreatProperty.ObjPresentation.ObjDataType)
            {
                case ObjDataTypeEnum.atk_tables:
                case ObjDataTypeEnum.atk_views:
                case ObjDataTypeEnum.atk_customTables:
                case ObjDataTypeEnum.atk_customViews:
                    path = GetCurrentTemplatePath() + "AzthinkerClass_WebListUIDto.txt";
                    break;
                case ObjDataTypeEnum.atk_FuncstoredProcedure:
                    return string.Empty;

                case ObjDataTypeEnum.atk_QuerystoredProcedure:
                    path = GetCurrentTemplatePath() + "AzthinkerClassSpRes_WebListUIDto.txt";
                    break;

            }
            _msgstringBuilder.AppendLine(path);
            string models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }
            if (models.Contains("Ai_Cmd_PrivateProperty"))
            {
                string str2 = "";

                if (classCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_QuerystoredProcedure)
                {
                    IEnumerable<AzMetaCloumEntity> azMetaCloumssp = AzMetaCloumHandle.Handle()
                                                        .Select()
                                                        .Where(m => m.TableName == azMetaTable.SchemaName)
                                                        .Go();
                    str2 = AzSql_Statement.GetpropertySpList(azMetaCloumssp);
                }
                else
                {
                    str2 = AzSql_Statement.GetpropertySpList(azMetaCloums);
                }
                models = models.ReaplaceTemplateForWord("Ai_Cmd_PrivateProperty", str2);
            }
            if (models.Contains("Ai_UiDto_To_BLLClass_Param"))
            {
                string str2 = "";

                if (classCreatProperty.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_QuerystoredProcedure)
                {
                    IEnumerable<AzMetaCloumEntity> azMetaCloumssp = AzMetaCloumHandle.Handle()
                                                        .Select()
                                                        .Where(m => m.TableName == azMetaTable.SchemaName)
                                                        .Go();
                    str2 = AzSql_Statement.GetpropertySpCopyParamList(azMetaCloumssp, "data", "this");
                }
                else
                {
                    str2 = AzSql_Statement.GetpropertySpCopyParamList(azMetaCloums, "data", "this");
                }
                models = models.ReaplaceTemplateForWord("Ai_UiDto_To_BLLClass_Param", str2);
            }
            return models.ReplacContext(azMetaTable);

        }

        public static string AzWebUiHandle(AzMetaTableEntity azMetaTable, AzClassCreatProperty classCreatProperty, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            string path = string.Empty;
            string models = string.Empty;
            switch (classCreatProperty.ObjPresentation.ObjDataType)
            {
                case ObjDataTypeEnum.atk_tables:
                case ObjDataTypeEnum.atk_views:
                case ObjDataTypeEnum.atk_customTables:
                case ObjDataTypeEnum.atk_customViews:
                    path = GetCurrentTemplatePath() + "AzthinkerClass_WebHandle.txt";
                    break;
                case ObjDataTypeEnum.atk_FuncstoredProcedure:
                    path = GetCurrentTemplatePath() + "AzthinkerClass_WebHandleExec.txt";
                    break;
                case ObjDataTypeEnum.atk_QuerystoredProcedure:
                    path = GetCurrentTemplatePath() + "AzthinkerClassSpRes_WebHandle.txt";
                    break;

            }
            _msgstringBuilder.AppendLine(path);
            models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }

            return models.ReplacContext(azMetaTable);

        }

        #endregion

        #region Controller

        private static string GetSelectStatementLambda(IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int i = 0;
            foreach (var col in azMetaCloums)
            {
                if ((col.IsSelect == true) && (col.VIsShow == true))
                {
                    if (i == 0)
                    {
                        stringBuilder.AppendLine($"s=>s.{col.FldNameTo}");
                    }
                    else
                    {
                        stringBuilder.AddLineStatement($",s=>s.{col.FldNameTo}", 6);
                    }

                    i += 1;
                }
            }
            return stringBuilder.ToString();
        }
        private static string GetUpateStatementLambda(IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int i = 0;
            foreach (var col in azMetaCloums)
            {
                if ((col.IsSelect == true) && (col.VpIsCanEdit == true) && (col.IsIdentity != true))
                {
                    if (i > 0)
                    {
                        stringBuilder.AddLineStatement($".Set(s=>s.{col.FldNameTo},model.{col.FldNameTo})", 6);
                    }
                    else
                    {
                        i = 1;
                        stringBuilder.AppendLine($".Set(s=>s.{col.FldNameTo},model.{col.FldNameTo})");
                    }
                }
            }
            return stringBuilder.ToString();
        }

        private static string GetInsertStatementLambda(IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int i = 0;
            foreach (var col in azMetaCloums)
            {
                if ((col.IsSelect == true) && (col.VpIsCanEdit == true) && (col.IsIdentity != true))
                {
                    if (i > 0)
                    {
                        stringBuilder.AddLineStatement($".With(s=>s.{col.FldNameTo},model.{col.FldNameTo})", 6);
                    }
                    else
                    {
                        i = 1;
                        stringBuilder.AppendLine($".With(s=>s.{col.FldNameTo},model.{col.FldNameTo})");
                    }
                }
            }
            return stringBuilder.ToString();
        }

        private static string GetOrderByStatementLambda(IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            var ordstr = azMetaCloums.Where(c => c.IsKeyField == true).FirstOrDefault();
            if (ordstr == null)
            {
                return string.Empty;
            }

            return $"o=>o.{ordstr.FldNameTo}";

        }

        private static string GetDeleteStatementLambda(IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            string result = string.Empty;
            var ordstr = azMetaCloums.Where(c => c.IsKeyField == true);
            if (ordstr.Count() == 0)
            {
                return result;
            }
            int i = 0;
            
            foreach (var item in ordstr)
            {
                if (i == 0)
                {
                    result= $"c=>c.{item.FldNameTo}== model.{item.FldNameTo}";
                }
                else
                {
                    result += $" && c.{item.FldNameTo}== model.{item.FldNameTo}";
                }

            }


            return result;

        }
        public static string AzMVC_Controllers(AzMetaTableEntity azMetaTable, AzClassCreatProperty classProp, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            string path = string.Empty;
            string models = string.Empty;
            switch (classProp.ObjPresentation.ObjDataType)
            {
                case ObjDataTypeEnum.atk_tables:
                case ObjDataTypeEnum.atk_views:
                case ObjDataTypeEnum.atk_customTables:
                case ObjDataTypeEnum.atk_customViews:
                    path = GetCurrentTemplatePath() + "AzthinkerControllers.txt";
                    break;
                case ObjDataTypeEnum.atk_FuncstoredProcedure:
                    return models;
                case ObjDataTypeEnum.atk_QuerystoredProcedure:
                    path = GetCurrentTemplatePath() + "AzthinkerControllers_SpQuery.txt";
                    break;

            }
            _msgstringBuilder.AppendLine(path);
            models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }

            if (models.Contains("Ai_Controllers_IndexPage"))
            {
                if (classProp.HasControllerList)
                {
                    if (classProp.HasControllerAsynPage)
                    {
                        var str1 = Mvc_Controllers_GetPage(azMetaTable, classProp, azMetaCloums);
                        models = models.ReaplaceTemplateForWord("Ai_Controllers_IndexPage", str1);
                    }
                    else
                    {
                        var str1 = Mvc_Controllers_GetList(azMetaTable, classProp, azMetaCloums);
                        models = models.ReaplaceTemplateForWord("Ai_Controllers_IndexPage", str1);
                    }
                }
            }
            if (models.Contains("Ai_Controllers_Create"))
            {
                var str1 = Mvc_Controllers_Create(azMetaTable, classProp, azMetaCloums);
                models = models.ReaplaceTemplateForWord("Ai_Controllers_Create", str1);
            }
            if (models.Contains("Ai_Controllers_Edit"))
            {
                var str1 = Mvc_Controllers_Edit(azMetaTable, classProp, azMetaCloums);
                models = models.ReaplaceTemplateForWord("Ai_Controllers_Edit", str1);
            }
            if (models.Contains("Ai_Controllers_detail"))
            {
                var str1 = Mvc_Controllers_Get(azMetaTable, classProp, azMetaCloums);
                models = models.ReaplaceTemplateForWord("Ai_Controllers_detail", str1);
            }
            if (models.Contains("Ai_Controllers_Delete"))
            {
                var str1 = Mvc_Controllers_Delete(azMetaTable, classProp, azMetaCloums);
                models = models.ReaplaceTemplateForWord("Ai_Controllers_Delete", str1);
            }
            return models.ReplacContext(azMetaTable);

        }

        public static string Mvc_Controllers_GetPage(AzMetaTableEntity azMetaTable, AzClassCreatProperty classProp, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            string path = string.Empty;
            string models = string.Empty;
            switch (classProp.ObjPresentation.ObjDataType)
            {
                case ObjDataTypeEnum.atk_tables:
                case ObjDataTypeEnum.atk_views:
                case ObjDataTypeEnum.atk_customTables:
                case ObjDataTypeEnum.atk_customViews:
                    path = GetCurrentTemplatePath() + "AzthinkerControllers_IndexPage.txt";
                    break;
                case ObjDataTypeEnum.atk_FuncstoredProcedure:
                    return models;
                case ObjDataTypeEnum.atk_QuerystoredProcedure:
                    path = GetCurrentTemplatePath() + "AzthinkerControllers_IndexPage_SpQuery.txt";
                    break;

            }
            _msgstringBuilder.AppendLine(path);
            models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }
            if (models.Contains("Ai_Paramlist_Default"))
            {
                var str1 = AzSql_Statement.ParamInputDefault(azMetaCloums, classProp);
                models = models.ReaplaceTemplateForWord("Ai_Paramlist_Default", str1);
            }
            if (models.Contains("Ai_Select_Statement_Lambda"))
            {
                var str1 = GetSelectStatementLambda(azMetaCloums);
                models = models.ReaplaceTemplateForWord("Ai_Select_Statement_Lambda", str1);
            }
            return models.ReplacContext(azMetaTable)
                         .MVC_ControllerRepParam(azMetaCloums)
                         .MVC_ReplacContextForCtr(azMetaCloums, classProp);
        }

        public static string Mvc_Controllers_GetList(AzMetaTableEntity azMetaTable, AzClassCreatProperty classProp, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            string path = string.Empty;
            string models = string.Empty;
            switch (classProp.ObjPresentation.ObjDataType)
            {
                case ObjDataTypeEnum.atk_tables:
                case ObjDataTypeEnum.atk_views:
                case ObjDataTypeEnum.atk_customTables:
                case ObjDataTypeEnum.atk_customViews:
                    path = GetCurrentTemplatePath() + "AzthinkerControllers_Index.txt";
                    break;
                case ObjDataTypeEnum.atk_FuncstoredProcedure:
                    return models;
                case ObjDataTypeEnum.atk_QuerystoredProcedure:
                    path = GetCurrentTemplatePath() + "AzthinkerControllers_Index_SpQuery.txt";
                    break;

            }
            _msgstringBuilder.AppendLine(path);
            models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }
            if (models.Contains("Ai_Paramlist_Default"))
            {
                var str1 = AzSql_Statement.ParamInputDefault(azMetaCloums, classProp);
                models = models.ReaplaceTemplateForWord("Ai_Paramlist_Default", str1);
            }
            if (models.Contains("Ai_Select_Statement_Lambda"))
            {
                var str1 = GetSelectStatementLambda(azMetaCloums);
                models = models.ReaplaceTemplateForWord("Ai_Select_Statement_Lambda", str1);
            }
            return models.ReplacContext(azMetaTable)
                         .MVC_ControllerRepParam(azMetaCloums)
                         .MVC_ReplacContextForCtr(azMetaCloums, classProp);
        }

        private static string MVC_Droplist(IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {

            var cols = azMetaCloums.Where(m => m.CustomList == true).ToList();
            if (cols.Count() == 0)
            {
                return string.Empty;
            }

            string path = GetCurrentTemplatePath() + "AzthinkerControllers_DropList.txt";
            _msgstringBuilder.AppendLine(path);
            string models = FileHelper.ReadTemplateFile(path);
            StringBuilder stringBuilder = new StringBuilder();
            string droplistclass = ""; string dataValueField = ""; string dataTextField = ""; string selectedValue = "";
            int i = 1;
            foreach (var col in cols)
            {
                string customlist = col.ListParam;
                string[] clist = customlist.Split('|');
                string tempmodels = models;
                if (clist.Count() < 3)
                {
                    continue;
                }

                droplistclass = clist[0]; dataValueField = clist[1]; dataTextField = clist[2];
                tempmodels = tempmodels
                    .ReaplaceTemplateForWord("Number", $"00{i}")
                    .ReaplaceTemplateForWord("Ai_DropList_ClassName", droplistclass)
                    .ReaplaceTemplateForWord("View_ListField", col.FldName)
                    .ReaplaceTemplateForWord("dataValueField", dataValueField)
                    .ReaplaceTemplateForWord("dataTextField", dataTextField)
                    .ReaplaceTemplateForWord("selectedValue", selectedValue);
                stringBuilder.Append(tempmodels);
                i += 1;

            }
            return stringBuilder.ToString();
        }

        private static string MVC_ControllerRepParam(this string models, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            if (models.ContainsForWord("Ai_FirstKeyType"))
            {
                var str1 = AzSql_Statement.KeyFieldFirst(azMetaCloums).FldType;
                models = models.ReaplaceTemplateForWord("Ai_FirstKeyType", str1);
            }

            if (models.ContainsForWord("Ai_Keys_Arg_Param_Id"))
            {
                var str1 = AzSql_Statement.MyMulitKeyStrParm(azMetaCloums);
                models = models.ReaplaceTemplateForWord("Ai_Keys_Arg_Param_Id", str1);
            }
            if (models.ContainsForWord("Ai_Keys_Field_Param_Id"))
            {
                var str1 = AzSql_Statement.MulitKeySummaryParam(azMetaCloums);
                models = models.ReaplaceTemplateForWord("Ai_Keys_Field_Param_Id", str1);
            }
            if (models.ContainsForWord("Ai_Keys_Param_Id_Lambda_NonePer"))
            {
                var str1 = AzSql_Statement.MulitKeyStrCondition(azMetaCloums, "s.", "", true);
                models = models.ReaplaceTemplateForWord("Ai_Keys_Param_Id_Lambda_NonePer", str1);
            }
            if (models.ContainsForWord("Ai_OrderBy_Statement_Lambda"))
            {
                var str1 = GetOrderByStatementLambda(azMetaCloums);
                models = models.ReaplaceTemplateForWord("Ai_OrderBy_Statement_Lambda", str1);
            }
            if (models.ContainsForWord("Ai_Delete_Statement_Lambda"))
            {
                var str1 = GetDeleteStatementLambda(azMetaCloums);
                models = models.ReaplaceTemplateForWord("Ai_Delete_Statement_Lambda", str1);
            }
            if (models.ContainsForWord("Ai_Dropdown_List"))
            {
                var str1 = MVC_Droplist(azMetaCloums);
                models = models.ReaplaceTemplateForWord("Ai_Dropdown_List", str1);
            }
            if (models.ContainsForWord("Ai_Controllers_Cache"))
            {

                models = models.ReaplaceTemplateForWord("Ai_Controllers_Cache", "[OutputCache(Duration=60)]");
            }
            return models;
        }

        private static string MVC_ReplacContextForCtr(this string models, IEnumerable<AzMetaCloumEntity> azMetaCloums, AzClassCreatProperty classProp)
        {
            if (classProp.UsePower == true)
            {
                models = models.ReaplaceTemplateForWord("Ai_Power_CanCreate", "[Authorize]")
                               .ReaplaceTemplateForWord("Ai_Power_CanEdit", "[Authorize]")
                               .ReaplaceTemplateForWord("Ai_Power_CanDelete", "[Authorize]")
                               .ReaplaceTemplateForWord("Ai_Power_CanGet", "[Authorize]");
            }
            else
            {
                models = models.ReaplaceTemplateForWord("Ai_Power_CanCreate", "")
                               .ReaplaceTemplateForWord("Ai_Power_CanEdit", "")
                               .ReaplaceTemplateForWord("Ai_Power_CanDelete", "")
                               .ReaplaceTemplateForWord("Ai_Power_CanGet", "");
            }

            if (models.Contains("Ai_HttpPost"))
            {
                //if (classProp.HasBigText)
                //{
                //    models = models.ReaplaceTemplateForWord("Ai_HttpPost", "[HttpPost, ValidateInput(false)]");
                //}
                //else
                //{
                // NetCore中没有 ValidateInput(false)
                models = models.ReaplaceTemplateForWord("Ai_HttpPost", "[HttpPost,ValidateAntiForgeryToken]");
                // }


            }
            if (models.Contains("Ai_FirstKeyType"))
            {
                var entity = AzSql_Statement.KeyFieldFirst(azMetaCloums);
                models = models.ReaplaceTemplateForWord("Ai_FirstKeyType", AzSql_Statement.GetCorrectType(entity));
                models = models.ReaplaceTemplateForWord("Ai_FirstKeyName", entity.FldName);
            }

            return models;
        }

        public static string Mvc_Controllers_Create(AzMetaTableEntity azMetaTable, AzClassCreatProperty classProp, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            string path = GetCurrentTemplatePath() + "AzthinkerControllers_Create.txt";
            _msgstringBuilder.AppendLine(path);
            string models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }
            if (models.Contains("Ai_Insert_Statement_Lambda"))
            {
                var str1 = GetInsertStatementLambda(azMetaCloums);
                models = models.ReaplaceTemplateForWord("Ai_Insert_Statement_Lambda", str1);
            }
            return models.ReplacContext(azMetaTable)
                         .MVC_ControllerRepParam(azMetaCloums)
                         .MVC_ReplacContextForCtr(azMetaCloums, classProp);
        }

        public static string Mvc_Controllers_Delete(AzMetaTableEntity azMetaTable, AzClassCreatProperty classProp, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            string path = GetCurrentTemplatePath() + "AzthinkerControllers_Delete.txt";
            _msgstringBuilder.AppendLine(path);
            string models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }
            if (models.Contains("Ai_Select_Statement_Lambda"))
            {
                var str1 = GetSelectStatementLambda(azMetaCloums);
                models = models.ReaplaceTemplateForWord("Ai_Select_Statement_Lambda", str1);
            }
            return models.ReplacContext(azMetaTable)
                         .MVC_ControllerRepParam(azMetaCloums)
                         .MVC_ReplacContextForCtr(azMetaCloums, classProp);
        }

        public static string Mvc_Controllers_Get(AzMetaTableEntity azMetaTable, AzClassCreatProperty classProp, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            string path = GetCurrentTemplatePath() + "AzthinkerControllers_Detail.txt";
            _msgstringBuilder.AppendLine(path);
            string models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }
            if (models.Contains("Ai_Select_Statement_Lambda"))
            {
                var str1 = GetSelectStatementLambda(azMetaCloums);
                models = models.ReaplaceTemplateForWord("Ai_Select_Statement_Lambda", str1);
            }
            return models.ReplacContext(azMetaTable)
                         .MVC_ControllerRepParam(azMetaCloums)
                         .MVC_ReplacContextForCtr(azMetaCloums, classProp);
        }


        public static string Mvc_Controllers_Edit(AzMetaTableEntity azMetaTable, AzClassCreatProperty classProp, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            string path = GetCurrentTemplatePath() + "AzthinkerControllers_Edit.txt";
            _msgstringBuilder.AppendLine(path);
            string models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }
            if (models.Contains("Ai_Select_Statement_Lambda"))
            {
                var str1 = GetSelectStatementLambda(azMetaCloums);
                models = models.ReaplaceTemplateForWord("Ai_Select_Statement_Lambda", str1);
            }
            if (models.Contains("Ai_Upate_Statement_Lambda"))
            {
                var str1 = GetUpateStatementLambda(azMetaCloums);
                models = models.ReaplaceTemplateForWord("Ai_Upate_Statement_Lambda", str1);
            }
            return models.ReplacContext(azMetaTable)
                         .MVC_ControllerRepParam(azMetaCloums)
                         .MVC_ReplacContextForCtr(azMetaCloums, classProp);
        }

        #endregion


        #region View
        private static string ViewEditFiled_Do(AzClassCreatProperty classProp, AzMetaCloumEntity azMetaCloum)
        {
            string path = string.Empty;
            string models = string.Empty;
            string fldname = azMetaCloum.FldType.ToLower();
            if (fldname == "bit")
            {
                path = GetCurrentTemplatePath() + @"ViewHtml\EditorBoolfield.txt";

            }
            else if (fldname.Contains("datetime"))
            {
                path = GetCurrentTemplatePath() + @"ViewHtml\EditorDatefield.txt";
            }
            else if (fldname.Contains("text") && (classProp.HasBigText == true))
            {

                path = GetCurrentTemplatePath() + @"ViewHtml\EditorTxtfield.txt";
            }
            else if (azMetaCloum.CustomList == true)
            {
                path = GetCurrentTemplatePath() + @"ViewHtml\EditorSelectField.txt";
            }
            _msgstringBuilder.AppendLine(path);
            if (string.IsNullOrWhiteSpace(path))
            {
                models = FileHelper.ReadTemplateFile(path);
            }
            if (string.IsNullOrWhiteSpace(models))
            {
                path = GetCurrentTemplatePath() + @"ViewHtml\EditorField.txt";
                _msgstringBuilder.AppendLine(path);
                models = FileHelper.ReadTemplateFile(path);
            }
            if (models.ContainsForWord("Ai_View_Field_Lable"))
            {
                models = models.ReaplaceTemplateForWord("Ai_View_Field_Lable", azMetaCloum.FldDisplay);
            }
            if (models.ContainsForWord("Ai_View_Field_Name"))
            {
                models = models.ReaplaceTemplateForWord("Ai_View_Field_Name", azMetaCloum.FldNameTo);
            }
            if (models.ContainsForWord("Ai_View_Field_SelectDropList"))
            {
                models = models.ReaplaceTemplateForWord("Ai_View_Field_SelectDropList", SelectitemViewHtml(azMetaCloum));
            }
            return models;

        }

        private static string ViewDisplyFiled_Do(AzClassCreatProperty classProp, AzMetaCloumEntity azMetaCloum)
        {
            string path = string.Empty;
            string models = string.Empty;
            string fldname = azMetaCloum.FldType.ToLower();
            if (fldname == "bit")
            {
                path = GetCurrentTemplatePath() + @"ViewHtml\DisplyBoolfield.txt";

            }
            else if (fldname.Contains("datetime"))
            {
                path = GetCurrentTemplatePath() + @"ViewHtml\DisplyDatefield.txt";
            }
            else if (fldname.Contains("text") && (classProp.HasBigText == true))
            {

                path = GetCurrentTemplatePath() + @"ViewHtml\DisplayTxtfield.txt";
            }
            _msgstringBuilder.AppendLine(path);
            if (string.IsNullOrWhiteSpace(path))
            {
                models = FileHelper.ReadTemplateFile(path);
            }
            if (string.IsNullOrWhiteSpace(models))
            {
                path = GetCurrentTemplatePath() + @"ViewHtml\DisplayField.txt";
                _msgstringBuilder.AppendLine(path);
                models = FileHelper.ReadTemplateFile(path);
            }
            if (models.ContainsForWord("Ai_View_Field_Lable"))
            {
                models = models.ReaplaceTemplateForWord("Ai_View_Field_Lable", azMetaCloum.FldDisplay);
            }
            if (models.ContainsForWord("Ai_View_Field_Name"))
            {
                models = models.ReaplaceTemplateForWord("Ai_View_Field_Name", azMetaCloum.FldNameTo);
            }
            return models;

        }

        private static string SelectitemViewHtml(AzMetaCloumEntity azMetaCloum)
        {
            string customlist = azMetaCloum.ListParam;
            string[] clist = customlist.Split('|');

            if (clist.Count() < 3)
            {
                return string.Empty;
            }

            return $"@Html.DropDownList(\"{clist[1]}\", string.Empty)";
        }

        private static string MVC_ReplacContextView(this string models, IEnumerable<AzMetaCloumEntity> azMetaCloums, AzClassCreatProperty classProp)
        {
            string path = GetCurrentTemplatePath() + @"ViewHtml\View_List_IndexLink.txt";
            var linkmodels = FileHelper.ReadListFile(path).ToArray();
            _msgstringBuilder.AppendLine(path);
            if (linkmodels.Count() < 3)
            {
                return string.Empty;
            }

            if (models.ContainsForWord("Ai_Goto_Index_Link"))
            {
                string link = "";
                if (classProp.HasBussniesList)
                {
                    link = linkmodels[0];
                }
                models = models.ReaplaceTemplateForWord("Ai_Goto_Index_Link", link);
            }
            if (models.ContainsForWord("Ai_Goto_Create_Link"))
            {
                string link = "";
                if (classProp.HasBussniesAdd)
                {
                    link = linkmodels[1];
                }
                models = models.ReaplaceTemplateForWord("Ai_Goto_Create_Link", link);
            }
            var fistentity = AzSql_Statement.KeyFieldFirst(azMetaCloums);
            models = models.ReaplaceTemplateForWord("Ai_FirstKeyType", fistentity.FldType)
                           .ReaplaceTemplateForWord("Ai_FirstKeyName", fistentity.FldName);
            if (models.ContainsForWord("Ai_Goto_Index"))
            {
                models = models.ReaplaceTemplateForWord("Ai_Goto_Index", linkmodels[2]);
            }
            return models;
        }
        public static string MulitKeyVHide(IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            var cols = azMetaCloums.AsQueryable().Where(m => m.IsKeyField == true).ToList();
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var col in cols)
            {
                stringBuilder.AddLineStatement($"@Html.HiddenFor(model => model.{col.FldName})");
            }
            return stringBuilder.ToString();
        }

        public static string AzMvc_View_Create(AzMetaTableEntity azMetaTable, AzClassCreatProperty classProp, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            string path = GetCurrentTemplatePath() + "AzthinkerView_Create.txt";
            string models = FileHelper.ReadTemplateFile(path);
            _msgstringBuilder.AppendLine(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }
            var colhiddens = azMetaCloums.Where(m => m.VIsHideShow == true);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var col in colhiddens)
            {
                if (col.IsSelect == true)
                {
                    stringBuilder.AddLineStatement($"@Html.HiddenFor(model => model.{col.FldNameTo})");
                }

            }
            var colshows = azMetaCloums.Where(m => m.IsSelect == true);
            foreach (var col in colshows)
            {
                if ((col.IsCreate == true) && (col.VpIsCanEdit == true))
                {
                    var str1 = ViewEditFiled_Do(classProp, col);
                    stringBuilder.AddLineStatement(str1);
                }
                else if (col.VIsShow == true)
                {
                    var str1 = ViewDisplyFiled_Do(classProp, col);
                    stringBuilder.AddLineStatement(str1);
                }

            }
            models = models.ReaplaceTemplateForWord("Ai_View_Create_FieldSet", stringBuilder.ToString());


            if (models.Contains("Ai_View_Jquery_Validate"))
            {
                path = GetCurrentTemplatePath() + @"ViewHtml\View_Jquery_Validate.txt";
                _msgstringBuilder.AppendLine(path);
                var str2 = FileHelper.ReadTemplateFile(path);
                models = models.ReaplaceTemplateForWord("Ai_View_Jquery_Validate", str2);
            }

            if (models.Contains("Ai_View_Edit_Txt_JQ"))
            {
                path = GetCurrentTemplatePath() + @"ViewHtml\View_Edit_Txt_JQ.txt";
                _msgstringBuilder.AppendLine(path);
                var str2 = FileHelper.ReadTemplateFile(path);
                models = models.ReaplaceTemplateForWord("Ai_View_Edit_Txt_JQ", str2);
            }

            if (models.Contains("Ai_View_Edit_Txt_JQUrl"))
            {
                path = GetCurrentTemplatePath() + @"ViewHtml\View_Edit_Txt_JQUrl.txt";
                _msgstringBuilder.AppendLine(path);
                var str2 = FileHelper.ReadTemplateFile(path);
                models = models.ReaplaceTemplateForWord("Ai_View_Edit_Txt_JQUrl", str2);
            }
            if (models.Contains("Ai_Field_Text"))
            {
                if (classProp.HasBigText == true)
                {
                    var bigentity = azMetaCloums.Where(m => m.FldType.Contains("text")).FirstOrDefault();
                    if (bigentity != null)
                    {
                        models = models.ReaplaceTemplateForWord("Ai_Field_Text", bigentity.FldNameTo);
                    }
                }
            }
            if (models.Contains("Ai_View_Editor_Js"))
            {
                path = GetCurrentTemplatePath() + @"ViewHtml\EditorJquery.txt";
                _msgstringBuilder.AppendLine(path);
                var str2 = FileHelper.ReadTemplateFile(path);
                models = models.ReaplaceTemplateForWord("Ai_View_Editor_Js", str2);
            }
            return models.ReplacContext(azMetaTable)
                         .MVC_ReplacContextView(azMetaCloums, classProp);
        }


        public static string AzMvc_View_Delete(AzMetaTableEntity azMetaTable, AzClassCreatProperty classProp, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            if (classProp.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_FuncstoredProcedure)
            {
                return string.Empty;
            }
            string path = GetCurrentTemplatePath() + "AzthinkerView_Delete.txt";
            _msgstringBuilder.AppendLine(path);
            string models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }
            var colhiddens = azMetaCloums.Where(m => m.VIsHideShow == true);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var col in colhiddens)
            {
                if (col.IsSelect == true)
                {
                    stringBuilder.AddLineStatement($"@Html.HiddenFor(model => model.{col.FldNameTo})");
                }

            }
            var colkeyfilehide= azMetaCloums.Where(m => m.IsKeyField == true);

            foreach (var col in colkeyfilehide)
            {
                stringBuilder.AddLineStatement($"@Html.HiddenFor(model => model.{col.FldNameTo})");
            }
            var colshows = azMetaCloums.Where(m => m.IsSelect == true);
            foreach (var col in colshows)
            {
                var str1 = ViewDisplyFiled_Do(classProp, col);
                stringBuilder.AddLineStatement(str1);
            }
            models = models.ReaplaceTemplateForWord("Ai_View_Detail_FieldSet", stringBuilder.ToString());

            if (models.Contains("Ai_View_Edit_Txt_JQUrl"))
            {
                path = GetCurrentTemplatePath() + @"ViewHtml\View_Edit_Txt_JQUrl.txt";
                _msgstringBuilder.AppendLine(path);
                var str2 = FileHelper.ReadTemplateFile(path);
                models = models.ReaplaceTemplateForWord("Ai_View_Edit_Txt_JQUrl", str2);
            }
            if (models.Contains("Ai_Field_Text"))
            {
                if (classProp.HasBigText == true)
                {
                    var bigentity = azMetaCloums.Where(m => m.FldType.Contains("text")).FirstOrDefault();
                    if (bigentity != null)
                    {
                        models = models.ReaplaceTemplateForWord("Ai_Field_Text", bigentity.FldNameTo);
                    }
                }
            }

            return models.ReplacContext(azMetaTable)
                         .MVC_ReplacContextView(azMetaCloums, classProp);
        }

        private static string Ai_View_Html_Linkto(AzClassCreatProperty classProp, string opStr, bool addul = true)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (addul)
            {
                stringBuilder.Append("<ul>");
            }

            if ((classProp.HasBussniesAdd == true) && (opStr != "Create"))
            {
                stringBuilder.AddLineStatement("@Html.ActionLink(\"增加\", \"Create\", new { <$Ai_View_Keys_Parameters> })");
            }
            if ((classProp.HasBussniesEdit == true) && (opStr != "Edit"))
            {
                stringBuilder.AddLineStatement("@Html.ActionLink(\"编辑\", \"Edit\", new { <$Ai_View_Keys_Parameters> })");
            }
            if ((classProp.HasBussniesDelete == true) && (opStr != "Delete"))
            {
                stringBuilder.AddLineStatement("@Html.ActionLink(\"删除\", \"Delete\", new { <$Ai_View_Keys_Parameters> })");
            }
            if ((classProp.HasBussniesDetail == true) && (opStr != "Details"))
            {
                stringBuilder.AddLineStatement("@Html.ActionLink(\"明细\", \"Details\", new { <$Ai_View_Keys_Parameters> })");
            }
            if (addul)
            {
                stringBuilder.Append("</ul>");
            }

            return stringBuilder.ToString();
        }

        public static string AzMvc_View_Details(AzMetaTableEntity azMetaTable, AzClassCreatProperty classProp, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            if (classProp.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_FuncstoredProcedure)
            {
                return string.Empty;
            }
            string path = GetCurrentTemplatePath() + "AzthinkerView_Detail.txt";
            _msgstringBuilder.AppendLine(path);
            string models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }
            var colhiddens = azMetaCloums.Where(m => m.VIsHideShow == true);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var col in colhiddens)
            {
                if (col.IsSelect == true)
                {
                    stringBuilder.AddLineStatement($"@Html.HiddenFor(model => model.{col.FldNameTo})");
                }

            }
            var colshows = azMetaCloums.Where(m => m.IsSelect == true);
            foreach (var col in colshows)
            {
                var str1 = ViewDisplyFiled_Do(classProp, col);
                stringBuilder.AddLineStatement(str1);
            }
            models = models.ReaplaceTemplateForWord("Ai_View_Detail_FieldSet", stringBuilder.ToString());

            if (models.Contains("Ai_View_Edit_Txt_JQUrl"))
            {
                path = GetCurrentTemplatePath() + @"ViewHtml\View_Edit_Txt_JQUrl.txt";
                _msgstringBuilder.AppendLine(path);
                var str2 = FileHelper.ReadTemplateFile(path);
                models = models.ReaplaceTemplateForWord("Ai_View_Edit_Txt_JQUrl", str2);
            }
            if (models.Contains("Ai_Field_Text"))
            {
                if (classProp.HasBigText == true)
                {
                    var bigentity = azMetaCloums.Where(m => m.FldType.Contains("text")).FirstOrDefault();
                    if (bigentity != null)
                    {
                        models = models.ReaplaceTemplateForWord("Ai_Field_Text", bigentity.FldNameTo);
                    }
                }
            }
            if (models.Contains("Ai_View_Html_Linkto"))
            {
                models = models.ReaplaceTemplateForWord("Ai_View_Html_Linkto", Ai_View_Html_Linkto(classProp, "Details"));
            }
            if (models.Contains("Ai_View_Keys_Parameters"))
            {
                var str2 = AzSql_Statement.MulitKeyStrView(azMetaCloums, "Model.");
                models = models.ReaplaceTemplateForWord("Ai_View_Keys_Parameters", str2);
            }
            return models.ReplacContext(azMetaTable)
                     .MVC_ReplacContextView(azMetaCloums, classProp);
        }

        public static string AzMvc_View_Edit(AzMetaTableEntity azMetaTable, AzClassCreatProperty classProp, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            string path = GetCurrentTemplatePath() + "AzthinkerView_Edit.txt";
            _msgstringBuilder.AppendLine(path);
            string models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }
            var colhiddens = azMetaCloums.Where(m => m.VIsHideShow == true);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var col in colhiddens)
            {
                if (col.IsSelect == true || col.IsKeyField == true)
                {
                    stringBuilder.AddLineStatement($"@Html.HiddenFor(model => model.{col.FldNameTo})");
                }

            }
            var colshows = azMetaCloums.Where(m => m.IsSelect == true);
            foreach (var col in colshows)
            {

                if ((col.IsCreate == true) && (col.VpIsCanEdit == true))
                {
                    var str1 = ViewEditFiled_Do(classProp, col);
                    stringBuilder.AddLineStatement(str1);
                }
                else if (col.VIsShow == true)
                {
                    if (col.IsKeyField == true)
                    {
                        stringBuilder.AddLineStatement($"@Html.HiddenFor(model => model.{col.FldNameTo})");
                    }
                    var str1 = ViewDisplyFiled_Do(classProp, col);
                    stringBuilder.AddLineStatement(str1);
                }

            }
            models = models.ReaplaceTemplateForWord("Ai_View_Edit_FieldSet", stringBuilder.ToString());


            if (models.Contains("Ai_View_Jquery_Validate"))
            {
                path = GetCurrentTemplatePath() + @"ViewHtml\View_Jquery_Validate.txt";
                _msgstringBuilder.AppendLine(path);
                var str2 = FileHelper.ReadTemplateFile(path);
                models = models.ReaplaceTemplateForWord("Ai_View_Jquery_Validate", str2);
            }

            if (models.Contains("Ai_View_Edit_Txt_JQ"))
            {
                path = GetCurrentTemplatePath() + @"ViewHtml\View_Edit_Txt_JQ.txt";
                _msgstringBuilder.AppendLine(path);
                var str2 = FileHelper.ReadTemplateFile(path);
                models = models.ReaplaceTemplateForWord("Ai_View_Edit_Txt_JQ", str2);
            }

            if (models.Contains("Ai_View_Edit_Txt_JQUrl"))
            {
                path = GetCurrentTemplatePath() + @"ViewHtml\View_Edit_Txt_JQUrl.txt";
                _msgstringBuilder.AppendLine(path);
                var str2 = FileHelper.ReadTemplateFile(path);
                models = models.ReaplaceTemplateForWord("Ai_View_Edit_Txt_JQUrl", str2);
            }
            if (models.Contains("Ai_Field_Text"))
            {
                if (classProp.HasBigText == true)
                {
                    var bigentity = azMetaCloums.Where(m => m.FldType.Contains("text")).FirstOrDefault();
                    if (bigentity != null)
                    {
                        models = models.ReaplaceTemplateForWord("Ai_Field_Text", bigentity.FldNameTo);
                    }
                }
            }
            if (models.Contains("Ai_View_Editor_Js"))
            {
                path = GetCurrentTemplatePath() + @"ViewHtml\EditorJquery.txt";
                _msgstringBuilder.AppendLine(path);
                var str2 = FileHelper.ReadTemplateFile(path);
                models = models.ReaplaceTemplateForWord("Ai_View_Editor_Js", str2);
            }
            return models.ReplacContext(azMetaTable)
                         .MVC_ReplacContextView(azMetaCloums, classProp);
        }



        private static string View_Table_Head(IEnumerable<AzMetaCloumEntity> azMetaCloums, AzClassCreatProperty classProp)
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();

            stringBuilder2.AddLineStatement($"{classProp.DisplayName}", 2);
            stringBuilder2.AddLineStatement("</th>");
            stringBuilder2.AddLineStatement("</tr>");
            stringBuilder2.AddLineStatement("<tr>");
            int i = 0;
            foreach (var col in azMetaCloums)
            {
                if ((col.IsSelect == true) && (col.VIsShow == true))
                {
                    stringBuilder2.AddLineStatement($"<th>{col.FldDisplay}</th>");
                    i += 1;
                }
            }
            if (classProp.HasViewAdd || classProp.HasViewDelete || classProp.HasViewEdit || classProp.HasViewDetail)
            {
                stringBuilder2.AddLineStatement($"<th>操作</th>");
                i += 1;
            }

            //stringBuilder2.AddLineStatement("</tr>");
            // stringBuilder.AddLineStatement("<tr>");
            stringBuilder.AddLineStatement($"<th colspan=\" {i} \" class=\"table - title\">", 2);
            stringBuilder.Append(stringBuilder2.ToString());
            return stringBuilder.ToString();
        }
        private static string View_Table_Details(IEnumerable<AzMetaCloumEntity> azMetaCloums, AzClassCreatProperty classProp)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int i = 0;
            foreach (var col in azMetaCloums)
            {
                if ((col.IsSelect == true) && (col.VIsShow == true))
                {
                    stringBuilder.AddLineStatement($"<td> @Html.DisplayFor(modelItem=>item.{col.FldNameTo})</td>");
                    i += 1;
                }
            }
            StringBuilder linkstr = new StringBuilder();
            if (classProp.HasViewAdd || classProp.HasViewDelete || classProp.HasViewEdit || classProp.HasViewDetail)
            {
                linkstr.AddLineStatement("<td>");
            }
            if (classProp.HasViewAdd)
            {
                linkstr.AddLineStatement("@Html.ActionLink(\"增加\", \"Create\", new { <$Ai_View_Keys_Parameters> })");
            }
            if (classProp.HasViewDelete)
            {
                linkstr.AddLineStatement("@Html.ActionLink(\"删除\", \"Delete\", new { <$Ai_View_Keys_Parameters> })");
            }
            if (classProp.HasViewEdit)
            {
                linkstr.AddLineStatement("@Html.ActionLink(\"编辑\", \"Edit\", new { <$Ai_View_Keys_Parameters> })");
            }
            if (classProp.HasViewDetail)
            {
                linkstr.AddLineStatement("@Html.ActionLink(\"明细\", \"Details\", new { <$Ai_View_Keys_Parameters> })");
            }
            if (classProp.HasViewAdd || classProp.HasViewDelete || classProp.HasViewEdit || classProp.HasViewDetail)
            {
                linkstr.AddLineStatement("</td>");
            }
            return stringBuilder.AppendLine(linkstr.ToString()).ToString();
        }
        public static string AzMvc_View_GetList(AzMetaTableEntity azMetaTable, AzClassCreatProperty classProp, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            if (classProp.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_FuncstoredProcedure)
            {
                return string.Empty;
            }
            string path = GetCurrentTemplatePath() + "AzthinkerView_List.txt";
            _msgstringBuilder.AppendLine(path);
            string models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }

            if (classProp.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_QuerystoredProcedure)
            {
                IEnumerable<AzMetaCloumEntity> azMetaCloumssp = AzMetaCloumHandle.Handle()
                                                    .Select()
                                                    .Where(m => m.TableName == classProp.ObjPresentation.StoreProcedureQuery)
                                                    .Go();
                models = models.ReaplaceTemplateForWord("Ai_View_Table_Head", View_Table_Head(azMetaCloumssp, classProp));
                models = models.ReaplaceTemplateForWord("Ai_View_Table_Body", View_Table_Details(azMetaCloumssp, classProp));
            }
            else
            {
                models = models.ReaplaceTemplateForWord("Ai_View_Table_Head", View_Table_Head(azMetaCloums, classProp));
                models = models.ReaplaceTemplateForWord("Ai_View_Table_Body", View_Table_Details(azMetaCloums, classProp));
            }

            return models.ReplacContext(azMetaTable)
                     .MVC_ReplacContextView(azMetaCloums, classProp);
        }


        public static string AzMvc_View_GetListPage(AzMetaTableEntity azMetaTable, AzClassCreatProperty classProp, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            if (classProp.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_FuncstoredProcedure)
            {
                return string.Empty;
            }
            string path = GetCurrentTemplatePath() + "AzthinkerView_ListPage.txt";
            _msgstringBuilder.AppendLine(path);
            string models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }
            return models.ReplacContext(azMetaTable)
                     .MVC_ReplacContextView(azMetaCloums, classProp);
        }


        public static string AzMvc_View_GetListPageDetails(AzMetaTableEntity azMetaTable, AzClassCreatProperty classProp, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            if (classProp.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_FuncstoredProcedure)
            {
                return string.Empty;
            }
            string path = GetCurrentTemplatePath() + "AzthinkerView_ListPageDetails.txt";
            _msgstringBuilder.AppendLine(path);
            string models = FileHelper.ReadTemplateFile(path);
            if (string.IsNullOrWhiteSpace(models))
            {
                return string.Empty;
            }

            //if (classProp.HasControllerAsynPage)
            //{
            //    string str1 = "@Html.AjaxPager(@Model.TotalCount,(int)ViewBag.__<$Ai_Bll_ClassName>Rows,\"<$Ai_View_Ajax_UpdateTargetId>\")";
            //    models = models.ReaplaceTemplateForWord("Ai_View_Page_Partial", str1);
            //}
            //else
            //{
            //    string str1 = "@Html.Pager(@Model.TotalCount,(int)ViewBag.__<$Ai_Bll_ClassName>Rows) ";
            //    models = models.ReaplaceTemplateForWord("Ai_View_Page_Partial", str1);

            //}

            models = models.ReaplaceTemplateForWord("Ai_View_Ajax_UpdateTargetId", $"{azMetaTable.ClassName.Replace(' ', '_').ToLower()}TargetId");
            models = models.ReaplaceTemplateForWord("Ai_View_Table_Head", View_Table_Head(azMetaCloums, classProp));
            models = models.ReaplaceTemplateForWord("Ai_View_Table_Body", View_Table_Details(azMetaCloums, classProp));
            //if (models.Contains("Ai_View_Html_Linkto"))
            //{
            //    models = models.ReaplaceTemplateForWord("Ai_View_Html_Linkto", Ai_View_Html_Linkto(classProp, "", false));
            //}
            if (models.Contains("Ai_View_Keys_Parameters"))
            {
                var str2 = AzSql_Statement.MulitKeyStrView(azMetaCloums, "item.");
                models = models.ReaplaceTemplateForWord("Ai_View_Keys_Parameters", str2);
            }
            return models.ReplacContext(azMetaTable)
                     .MVC_ReplacContextView(azMetaCloums, classProp);
        }
        #endregion

    }
}
