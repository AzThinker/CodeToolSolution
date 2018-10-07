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
    internal static class AzSql_Statement
    {
        #region ParamCreate


        public static string ParamCreatekey(IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            var cols = azMetaCloums.AsQueryable().Where(m => m.IsKeyField == true).ToList();
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var col in cols)
            {
                MetaDataType metaDataType = MetaDataTypeHandle.GetMetaDataType(col.FldType);
                stringBuilder.AddLineStatement("param = new SqlParameter();", 2);
                stringBuilder.AddLineStatement("param.ParameterName = \"@" + col.FldName + "\";", 2);
                stringBuilder.AddLineStatement($"param.Value=azItem.{col.FldNameTo};", 2);
                if (metaDataType.CodeSign == 2)
                {
                    stringBuilder.AddLineStatement($"param.Size = {col.FldLenCode};", 2);
                }
                stringBuilder.AddLineStatement($"param.SqlDbType = SqlDbType.{metaDataType.DBCodeType};", 2);
                stringBuilder.AddLineStatement("cmd.Parameters.Add(param);", 2);
            }

            return stringBuilder.ToString();
        }

        private static string ParamCreate(AzMetaCloumEntity azMetaCloum, AzClassCreatProperty classCreatProperty)
        {
            StringBuilder stringBuilder = new StringBuilder();
            MetaDataType metaDataType = MetaDataTypeHandle.GetMetaDataType(azMetaCloum.FldType);
            stringBuilder.AddLineStatement("param = new SqlParameter();", 2);
            stringBuilder.AddLineStatement("param.ParameterName = \"@" + azMetaCloum.FldName + "\";", 2);
            if (azMetaCloum.IsOutParam == 0)
            {
                if (azMetaCloum.IsNullable == false)
                {
                    stringBuilder.AddLineStatement($"param.Value={BaseConstants.ParamPer}Item.{azMetaCloum.FldNameTo};", 2);
                }
                else
                {
                    stringBuilder.AddLineStatement($"if ({BaseConstants.ParamPer}Item.{azMetaCloum.FldNameTo}==null)", 2);
                    stringBuilder.AddLineStatement(" {param.Value = System.DBNull.Value;}", 2);
                    stringBuilder.AddLineStatement($"else", 2);
                    stringBuilder.AddLineStatement($"{{ param.Value={BaseConstants.ParamPer}Item.{azMetaCloum.FldNameTo};}};", 2);
                }
            }
            else
            {
                stringBuilder.AddLineStatement("param.Direction = ParameterDirection.Output;}", 2);
            }
            if (metaDataType.CodeSign == 2)
            {
                stringBuilder.AddLineStatement($"param.Size = {azMetaCloum.FldLenCode};", 2);
            }
            stringBuilder.AddLineStatement($"param.SqlDbType = SqlDbType.{metaDataType.DBCodeType};", 2);
            stringBuilder.AddLineStatement("cmd.Parameters.Add(param);", 2);
            stringBuilder.AppendLine();
            return stringBuilder.ToString();
        }

        private static string ParamIdentityCreate(string idFldType)
        {
            StringBuilder stringBuilder = new StringBuilder();
            MetaDataType metaDataType = MetaDataTypeHandle.GetMetaDataType(idFldType);
            stringBuilder.AddLineStatement($"param = new SqlParameter();", 2);
            stringBuilder.AddLineStatement($"param.SqlDbType = SqlDbType.{metaDataType.DBCodeType};", 2);
            stringBuilder.AddLineStatement($"param.ParameterName = \"@getautouid\";", 2);
            stringBuilder.AddLineStatement($"param.Direction = ParameterDirection.Output;", 2);
            stringBuilder.AddLineStatement($"cmd.Parameters.Add(param);", 2);
            stringBuilder.AppendLine();
            return stringBuilder.ToString();
        }

        public static string GetCorrectType(AzMetaCloumEntity azMetaCloum)
        {
            MetaDataType metaDataType = MetaDataTypeHandle.GetMetaDataType(azMetaCloum.FldType);
            string ctype = metaDataType.CodeType;
            if (azMetaCloum.IsNullable == true)
            {
                ctype = metaDataType.CodeGeneric;
            }

            return ctype;
        }
        private static string ParamIdentityPropertyCreate(AzMetaCloumEntity azMetaCloum)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AddLineStatement($"if (c>0)", 3);
            stringBuilder.AddLineStatement($"{{", 4);
            stringBuilder.AddLineStatement($"{BaseConstants.ParamPer}Item.{azMetaCloum.FldNameTo}=({GetCorrectType(azMetaCloum)})cmd.Parameters[\"@getautouid\"].Value;", 4);
            stringBuilder.AddLineStatement($"}}", 4);
            stringBuilder.AppendLine();
            return stringBuilder.ToString();
        }


        public static (string models, string addId) ParamCreates(IEnumerable<AzMetaCloumEntity> azMetaCloums, AzClassCreatProperty classCreatProperty, AzOperateSqlModel azOperate)
        {
            bool opm = false;
            StringBuilder stringBuilder = new StringBuilder();
            string item2 = "";
            foreach (var col in azMetaCloums)
            {
                opm = false;
                switch (azOperate)
                {
                    case AzOperateSqlModel.atkInsert:
                        opm = (col.IsSelect == true) && (col.IsCreate == true) && (col.IsDataField == true);
                        break;
                    case AzOperateSqlModel.atkUpdate:
                        opm = (col.IsSelect == true) && (col.IsUpdata == true) && (col.IsDataField == true);
                        break;
                    case AzOperateSqlModel.atkSelect:
                        opm = (col.IsSelect == true) && (col.IsDataField == true);
                        break;
                }
                bool insertcmd = (azOperate == AzOperateSqlModel.atkInsert);
                if (insertcmd && (col.IsIdentity == true))
                {
                    stringBuilder.AddLineStatement(ParamIdentityCreate(col.FldType));
                    item2 = ParamIdentityPropertyCreate(col);
                }
                if (opm && (col.IsIdentity != true) && insertcmd)// || (opm && (col.IsIdentity != true)))
                {
                    stringBuilder.AddLineStatement(ParamCreate(col, classCreatProperty));
                }

            }

            return (stringBuilder.ToString(), item2);
        }

        public static string ParamInputDefault(IEnumerable<AzMetaCloumEntity> azMetaCloums, AzClassCreatProperty classPro)
        {
            if (classPro.ObjPresentation.ObjDataType < ObjDataTypeEnum.atk_FuncstoredProcedure)
            {
                return string.Empty;
            }

            string result = string.Empty;
            foreach (var col in azMetaCloums)
            {
                string fn = GetCorrectType(col);
                if (string.IsNullOrWhiteSpace(result))
                {
                    result = $"default({fn})";
                }
                else
                {
                    result = $"{result},default({fn})";
                }

            }
            return result;

        }
        #endregion

        #region Sql_Fragment


        public static (string codesInfo, string addInfo) Sql_Fragment_For_Insert(IEnumerable<AzMetaCloumEntity> azMetaCloums, AzClassCreatProperty classPro)
        {

            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            StringBuilder s1 = new StringBuilder();
            StringBuilder s2 = new StringBuilder();
            string Item2 = string.Empty;
            int idcount = 0; int rowcount = 0; string comma = ""; int tabcount = 0;
            stringBuilder1.AddLineStatement($"{BaseConstants.ParamPer}StrBuilder.Append(\" Insert Into {classPro.ObjPresentation.UpdateTableName}(\");", 2);
            stringBuilder2.AddLineStatement($"{BaseConstants.ParamPer}StrBuilder.Append(\" Values( \");", 2);
            stringBuilder.AddLineStatement($"#region  增加SQL语句字串", 2);
            #region cols


            foreach (var col in azMetaCloums)
            {
                if (col.IsIdentity == true)
                {
                    idcount = 1;
                }

                if ((col.IsDataField == true) && (col.IsCreate == true) && (col.IsSelect == true) && (col.IsIdentity != true))
                {

                    if (tabcount > 0)
                    {
                        comma = ",";
                    }

                    if (string.IsNullOrWhiteSpace(s1.ToString()))
                    {
                        s1.Append($"{BaseConstants.ParamPer}StrBuilder.Append(\"{comma}[{col.FldName}]");
                        s2.Append($"{BaseConstants.ParamPer}StrBuilder.Append(\"{comma}@{col.FldName}");
                    }
                    else
                    {
                        s1.Append($",[{col.FldName}]");
                        s2.Append($",@{col.FldName}");
                    }
                    if (rowcount == 5)
                    {
                        stringBuilder1.AddLineStatement(s1.ToString() + "\");", 2);
                        stringBuilder2.AddLineStatement(s2.ToString() + "\");", 2);
                        rowcount = 0;
                        s1.Clear();
                        s2.Clear();
                        tabcount += 1;
                    }
                    rowcount += 1;

                }


            }
            #endregion

            if (string.IsNullOrWhiteSpace(s1.ToString()))
            {
                s1.AddLineStatement($"{BaseConstants.ParamPer}StrBuilder.Append(\")\");", 2);
                stringBuilder1.Append(s1);
            }
            else
            {
                stringBuilder1.Append(s1.ToString() + ")\");");
            }

            if (string.IsNullOrWhiteSpace(s2.ToString()))
            {
                s2.AddLineStatement($"{BaseConstants.ParamPer}StrBuilder.Append(\")\");", 2);
                stringBuilder2.Append(s2);
            }
            else
            {
                stringBuilder2.Append(s2.ToString() + ")\");");
            }

            //   stringBuilder2.Append(s2.ToString() + ")\");");
            stringBuilder.Append(stringBuilder1.ToString());
            stringBuilder.Append(stringBuilder2.ToString());
            if (idcount == 1)
            {
                Item2 = $"{BaseConstants.ParamPer}StrBuilder.Append(\";select @getautouid = SCOPE_IDENTITY()\"); ".AddPerTab(2);
            }
            stringBuilder.AppendLine(Item2);
            stringBuilder.AddLineStatement($"#endregion", 2);
            return (stringBuilder.ToString(), Item2);


        }

        public static string Sql_Fragment_For_Fetch(IEnumerable<AzMetaCloumEntity> azMetaCloums, string addper = "")
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder s1 = new StringBuilder();

            int rowcount = 0; string comma = ""; int tabcount = 0;
            #region cols
            foreach (var col in azMetaCloums)
            {

                if ((col.IsDataField == true) && (col.IsSelect == true))
                {

                    if (tabcount > 0)
                    {
                        comma = ",";
                    }

                    if (string.IsNullOrWhiteSpace(s1.ToString()))
                    {
                        s1.Append($"{BaseConstants.ParamPer}StrBuilder.Append(\"{comma}{addper}[{col.FldName}]");

                    }
                    else
                    {
                        s1.Append($",{addper}[{col.FldName}]");
                    }
                    if (rowcount == 5)
                    {
                        stringBuilder.AddLineStatement(s1.ToString() + "\");", 0);
                        rowcount = 0;
                        s1.Clear();
                        tabcount += 1;
                    }
                    rowcount += 1;
                }
            }
            #endregion
            if (!string.IsNullOrWhiteSpace(s1.ToString()))
            {
                stringBuilder.AddLineStatement(s1.ToString() + "\");", 2);
            }
            return stringBuilder.ToString();
        }

        public static string Sql_Fragment_For_Update(IEnumerable<AzMetaCloumEntity> azMetaCloums, AzClassCreatProperty classPro)
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder s1 = new StringBuilder();
            StringBuilder s2 = new StringBuilder();
            s2.AddLineStatement($"{BaseConstants.ParamPer}StrBuilder.Append(\"Update [a0] Set \"); ", 2);
            stringBuilder.AddLineStatement("#region  更新SQL语句字串", 2);
            int rowcount = 0; string comma = ""; int tabcount = 0;
            #region cols
            foreach (var col in azMetaCloums)
            {

                if ((col.IsDataField == true) && (col.IsSelect == true) && (col.IsUpdata == true) && (col.IsIdentity != true))
                {

                    if (tabcount > 0)
                    {
                        comma = ",";
                    }

                    if (string.IsNullOrWhiteSpace(s1.ToString()))
                    {
                        s1.Append($"{BaseConstants.ParamPer}StrBuilder.Append(\"{comma}[a0].[{col.FldName}]=@{col.FldName}");
                    }
                    else
                    {
                        s1.Append($",[a0].[{col.FldName}]=@{col.FldName}");
                    }
                    if (rowcount == 5)
                    {
                        s2.AddLineStatement(s1.ToString() + "\");", 2);
                        rowcount = 0;
                        s1.Clear();
                        tabcount += 1;
                    }
                    rowcount += 1;
                }
            }
            #endregion

            if (!string.IsNullOrWhiteSpace(s1.ToString()))
            {
                s2.AddLineStatement($"{s1.ToString()} From {classPro.ObjPresentation.UpdateTableName} As [a0]\");", 2);
            }
            stringBuilder.Append(s2.ToString());
            stringBuilder.AddLineStatement("#endregion");
            return stringBuilder.ToString();
        }

        #endregion

        #region MulitKeyStr

        public static string MulitKeyStrSQL(IEnumerable<AzMetaCloumEntity> azMetaCloums, string perstr = "")
        {
            var cols = azMetaCloums.AsQueryable().Where(m => m.IsKeyField == true).ToList();
            string s1 = ""; string s2 = "";
            foreach (var col in cols)
            {
                s2 = $"[a0].[{col.FldNameTo}]=@{col.FldNameTo}";
                if (string.IsNullOrWhiteSpace(s1))
                {
                    s1 = s2;
                }
                else
                {
                    s1 = $"{s1} And {s2}";
                }
            }
            if (!string.IsNullOrWhiteSpace(s1) && !string.IsNullOrWhiteSpace(perstr))
            {
                s1 = $"{perstr} {s1}".AddPerTab(2);
            }
            return s1;
        }
        public static string MulitKeyStrCondition(IEnumerable<AzMetaCloumEntity> azMetaCloums, string left, string right, bool paramId)
        {
            var cols = azMetaCloums.AsQueryable().Where(m => m.IsKeyField == true).ToList();
            string s1 = ""; string s2 = "";
            foreach (var col in cols)
            {
                s2 = $"{left}{col.FldNameTo}=={right}{col.FldNameTo}";
                if (string.IsNullOrWhiteSpace(s1))
                {
                    if (paramId)
                    {
                        s1 = $"{left}{col.FldNameTo}==Id"; ;
                    }
                    else
                    {
                        s1 = s2;
                    }

                }
                else
                {
                    s1 = $"{s1} && {s2}";
                }
            }
            return s1;
        }

        public static AzMetaCloumEntity KeyFieldFirst(IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            var result = azMetaCloums.AsQueryable().Where(m => m.IsKeyField == true).FirstOrDefault();
            if (result == null)
            {
                result = new AzMetaCloumEntity();
                result.FldName = "未设置关键定段";
                result.FldType = "nvarchar";
                result.FldCodeType = "string";
                result.FldNameTo = "未设置关键定段";
            }
            return result;
        }

        public static string MyMulitKeyStrParm(IEnumerable<AzMetaCloumEntity> azMetaCloums, bool useId = true)
        {
            var cols = azMetaCloums.AsQueryable().Where(m => m.IsKeyField == true).ToList();
            string s1 = "";
            foreach (var col in cols)
            {
                var fldt = GetCorrectType(col);
                if (string.IsNullOrWhiteSpace(s1))
                {

                    if (useId)
                    {
                        s1 = $"{fldt} Id";//MVC
                    }
                    else
                    {
                        s1 = $"{fldt} {col.FldNameTo}";
                    }
                }
                else
                {
                    s1 = s1 + $",{fldt} {col.FldNameTo}";
                }
            }
            return s1;
        }

        public static string MulitKeyStrView(IEnumerable<AzMetaCloumEntity> azMetaCloums, string perStr, bool useId = true)
        {
            var cols = azMetaCloums.AsQueryable().Where(m => m.IsKeyField == true).ToList();
            string s1 = "";
            foreach (var col in cols)
            {
                if (string.IsNullOrWhiteSpace(s1))
                {

                    if (useId)
                    {
                        s1 = $"Id={perStr}{col.FldNameTo}";//MVC
                    }
                    else
                    {
                        s1 = $"{col.FldNameTo}={perStr}{col.FldNameTo}";
                    }
                }
                else
                {
                    s1 = s1 + $",{col.FldNameTo}={perStr}{col.FldNameTo}";
                }
            }
            return s1;
        }
        public static string MulitKeyStrParmNo(IEnumerable<AzMetaCloumEntity> azMetaCloums, bool useId = true)
        {
            var cols = azMetaCloums.AsQueryable().Where(m => m.IsKeyField == true).ToList();
            string s1 = "";
            foreach (var col in cols)
            {
                if (string.IsNullOrWhiteSpace(s1))
                {

                    if (useId)
                    {
                        s1 = $"Id";//MVC
                    }
                    else
                    {
                        s1 = $"{col.FldName}";
                    }
                }
                else
                {
                    s1 = s1 + $",{col.FldName}";
                }
            }
            return s1;
        }

        public static string MulitKeySummaryParam(IEnumerable<AzMetaCloumEntity> azMetaCloums, bool useId = true)
        {
            var cols = azMetaCloums.AsQueryable().Where(m => m.IsKeyField == true).ToList();
            StringBuilder stringBuilder = new StringBuilder();
            bool first = false;
            foreach (var col in cols)
            {
                if (first)
                {
                    stringBuilder.AddLineStatement($"///<param name=\"{col.FldNameTo}\"{col.FldDisplay}", 2);
                }
                else
                {
                    stringBuilder.AddLineStatement($"///<param name=\"Id\"{col.FldDisplay}", 2);
                }

            }
            return stringBuilder.ToString();
        }


        #endregion

        #region SQlFiled
        public static string SQlFiledCopyItem(string paramName, IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string codetype = "";
            stringBuilder.AddLineStatement("#region  类赋值");
            foreach (var col in azMetaCloums)
            {
                if (col.IsSelect == true)
                {
                    codetype = GetCorrectType(col);
                    if (col.IsNullable == true)
                    {
                        stringBuilder.AddLineStatement($"{paramName}{col.FldNameTo}={BaseConstants.ParamPer}DataReader[\"{col.FldName}\"] is DBNull ? null : ({codetype}){BaseConstants.ParamPer}DataReader[\"{col.FldName}\"];//{col.FldDisplay}", 3);
                    }
                    else
                    {
                        stringBuilder.AddLineStatement($"{paramName}{col.FldNameTo}=({codetype}){BaseConstants.ParamPer}DataReader[\"{col.FldName}\"];//{col.FldDisplay}", 3);
                    }
                }
            }
            stringBuilder.AddLineStatement("#endregion");

            return stringBuilder.ToString();
        }
        #endregion

        #region SpParam

        private static string GetSpFldName(string fldName, string perStr)
        {
            return $"{perStr}{fldName.Substring(1)}";
        }

        public static string SpParamCreate(AzMetaCloumEntity azMetaCloum)
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool iscannull = azMetaCloum.IsNullable != true;
            string spfld = GetSpFldName(azMetaCloum.FldName, "P_");
            string param_name = GetSpFldName(azMetaCloum.FldName, "param_");
            stringBuilder.AddLineStatement($"SqlParameter {param_name} = new SqlParameter();", 2);
            stringBuilder.AddLineStatement($"{param_name}.ParameterName =\"{azMetaCloum.FldName}\";", 2);
            if (azMetaCloum.IsOutParam == 0)
            {
                if (iscannull)
                {
                    stringBuilder.AddLineStatement($"{param_name}.Value ={BaseConstants.ParamPer}Item.{spfld};", 2);
                }
                else
                {
                    stringBuilder.AddLineStatement($"if ({BaseConstants.ParamPer}Item.{spfld}==null)", 2);
                    stringBuilder.AddLineStatement($"{{{param_name}.Value =System.DBNull.Value;}}", 2);
                    stringBuilder.AddLineStatement($"else");
                    stringBuilder.AddLineStatement($"{{{param_name}.Value ={BaseConstants.ParamPer}Item.{spfld};}};", 2);
                }
                stringBuilder.AddLineStatement($"{param_name}.Direction = ParameterDirection.Input;", 2);
            }
            else
            {
                stringBuilder.AddLineStatement($"{param_name}.Direction = ParameterDirection.Output;", 2);
            }
            MetaDataType metaDataType = MetaDataTypeHandle.GetMetaDataType(azMetaCloum.FldType);
            if (metaDataType.CodeSign == 2)
            {
                if (azMetaCloum.FldLen < 1)
                {
                    stringBuilder.AddLineStatement($"{param_name}.Size = int.MaxValue - 1;", 2);
                }
                else
                {
                    stringBuilder.AddLineStatement($"{param_name}.Size = {azMetaCloum.FldLen};", 2);
                }

            }
            stringBuilder.AddLineStatement($"{param_name}.SqlDbType = SqlDbType.{metaDataType.DBCodeType};", 2);
            stringBuilder.AddLineStatement($"cmd.Parameters.Add({param_name});", 2);
            stringBuilder.AppendLine();
            return stringBuilder.ToString();
        }

        public static string SpParamCreates(AzMetaTableEntity azMetaTable)
        {

            if (string.IsNullOrWhiteSpace(azMetaTable.SchemaName))
            {
                return "";
            }

            IEnumerable<AzMetaCloumEntity> azMetaCloums = AzMetaCloumHandle.Handle()
                                                           .Select()
                                                           .Where(m => m.TableName == azMetaTable.SchemaName)
                                                           .Go();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var col in azMetaCloums)
            {
                stringBuilder.AppendLine(SpParamCreate(col));
            }

            return stringBuilder.ToString();
        }

        public static string SpParamOutCreate(IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AddLineStatement("#region   传出参数", 2);
            bool hvalue = false;
            foreach (var col in azMetaCloums)
            {

                if (col.IsOutParam == 1)
                {
                    string spfld = GetSpFldName(col.FldName, "P_");
                    string param_name = GetSpFldName(col.FldName, "param_");
                    string codetype = GetCorrectType(col);

                    if (col.IsNullable == true)
                    {
                        stringBuilder.AddLineStatement($"{BaseConstants.ParamPer}Item.{spfld}={param_name}.Value == DBNull.Value ? null : ({codetype}){param_name}.Value;", 3);
                    }
                    else
                    {
                        stringBuilder.AddLineStatement($"{BaseConstants.ParamPer }Item.{spfld}=({codetype}){param_name}.Value;", 3);
                    }
                    hvalue = true;
                }
            }
            stringBuilder.AddLineStatement("#endregion", 2);
            if (hvalue)
            {
                return stringBuilder.ToString();

            }
            else
            {
                return string.Empty;
            }

        }

        public static string SpParamMarker_1(IEnumerable<AzMetaCloumEntity> azMetaCloums, bool hasReturn)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var col in azMetaCloums)
            {
                string spFldName = GetSpFldName(col.FldName, BaseConstants.ParamPer);
                stringBuilder.AddLineStatement($"/// <param name=\"{spFldName}\">{col.FldDisplay}</param>", 2);
            }
            if (hasReturn)
            {
                stringBuilder.AddLineStatement($"/// <returns></returns>", 2);
            }
            return stringBuilder.ToString();
        }
        public static string SpParamarg_For_Mothod(IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            string p = "";
            foreach (var col in azMetaCloums)
            {

                MetaDataType metaDataType = MetaDataTypeHandle.GetMetaDataType(col.FldType);
                if (string.IsNullOrWhiteSpace(p))
                {
                    p = $"{ GetCorrectType(col)} {GetSpFldName(col.FldName, BaseConstants.ParamPer)}";
                }
                else
                {
                    p += $",{ GetCorrectType(col)} {GetSpFldName(col.FldName, BaseConstants.ParamPer)}";
                }
            }

            return p;
        }

        public static string SpParamarg_For_Assignment(IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            string p = "";
            foreach (var col in azMetaCloums)
            {
                string spFldName = GetSpFldName(col.FldName, BaseConstants.ParamPer);
                string spfld = GetSpFldName(col.FldName, "P_");
                MetaDataType metaDataType = MetaDataTypeHandle.GetMetaDataType(col.FldType);
                if (string.IsNullOrWhiteSpace(p))
                {
                    p = $"{spfld} = {spFldName}";
                }
                else
                {
                    p += $",{spfld} = {spFldName}";
                }
            }

            return p;
        }

        public static string SpParam_Input(IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            string p = "";
            foreach (var col in azMetaCloums)
            {
                string spFldName = GetSpFldName(col.FldName, BaseConstants.ParamPer);

                MetaDataType metaDataType = MetaDataTypeHandle.GetMetaDataType(col.FldType);
                if (string.IsNullOrWhiteSpace(p))
                {
                    p = $"{spFldName}";
                }
                else
                {
                    p += $",{spFldName}";
                }
            }

            return p;
        }

        public static string SpParam_InputThis(IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            string p = "";
            foreach (var col in azMetaCloums)
            {
                string spFldName = GetSpFldName(col.FldName, "this.P_");

                MetaDataType metaDataType = MetaDataTypeHandle.GetMetaDataType(col.FldType);
                if (string.IsNullOrWhiteSpace(p))
                {
                    p = $"{spFldName}";
                }
                else
                {
                    p += $",{spFldName}";
                }
            }
            return p;
        }
        #endregion

        #region BLL

        public static string DefaultValuesCreate(IEnumerable<AzMetaCloumEntity> azMetaCloums, AzClassCreatProperty classPro)
        {
            if (classPro.HasDtoConstruction != true)
            {
                return "//字段属性无默认初始化".AddPerTab(2);
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var col in azMetaCloums)
            {
                if ((col.IsIdentity != true) && (col.IsSelect == true) && (col.IsDefvalue == true))
                {
                    stringBuilder.AddLineStatement($"{col.FldNameTo} = {col.Defvalue };");
                }

            }
            return stringBuilder.ToString();
        }

        public static string PropertyForSemicolon(IEnumerable<AzMetaCloumEntity> azMetaCloums, string left, string right, params string[] noinclude)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AddLineStatement($"#region  类赋值", 2);
            foreach (var col in azMetaCloums)
            {
                bool nc = true;
                if (noinclude != null)
                { nc = !noinclude.Contains(col.FldNameTo); };
                if ((col.IsSelect == true) && nc)
                {
                    stringBuilder.AddLineStatement($"{left}{col.FldNameTo} = {right}{col.FldNameTo};//{col.FldDisplay}", 2);
                }

            }
            stringBuilder.AddLineStatement($"#endregion", 2);
            return stringBuilder.ToString();
        }


        public static string PropertyForComma(IEnumerable<AzMetaCloumEntity> azMetaCloums, string left, string right, params string[] noinclude)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AddLineStatement($"#region  类赋值", 2);
            foreach (var col in azMetaCloums)
            {
                bool nc = true;
                if (noinclude != null)
                { nc = !noinclude.Contains(col.FldNameTo); };
                if ((col.IsSelect == true) && nc && ((col.IsDataField == true) || (col.IsBinaryTo == true)))
                {
                    stringBuilder.AddLineStatement($"{left}{col.FldNameTo} = {right}{col.FldNameTo},//{col.FldDisplay}", 2);
                }

            }
            stringBuilder.AddLineStatement($"#endregion", 2);
            return stringBuilder.ToString();
        }

        public static string PropertyForCommaNoId(IEnumerable<AzMetaCloumEntity> azMetaCloums, string left, string right, params string[] noinclude)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AddLineStatement($"#region  类赋值", 2);
            foreach (var col in azMetaCloums)
            {
                bool nc = true;
                if (noinclude != null)
                { nc = !noinclude.Contains(col.FldNameTo); };
                if ((col.IsIdentity != true) && (col.IsSelect == true) && nc && ((col.IsDataField == true) || (col.IsBinaryTo == true)))
                {
                    stringBuilder.AddLineStatement($"{left}.{col.FldNameTo} = {right}.{col.FldNameTo},//{col.FldDisplay}", 2);
                }

            }
            stringBuilder.AddLineStatement($"#endregion", 2);
            return stringBuilder.ToString();
        }

        public static string GetpropertySpCopyParamList(IEnumerable<AzMetaCloumEntity> azMetaCloums, string left, string right)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AddLineStatement($"#region  类赋值", 2);
            foreach (var col in azMetaCloums)
            {
                string spfld = GetSpFldName(col.FldNameTo, "P_");
                stringBuilder.AddLineStatement($"{left}.{spfld} = {right}.{spfld};//{col.FldDisplay}", 2);
            }
            stringBuilder.AddLineStatement($"#endregion", 2);
            return stringBuilder.ToString();
        }

        public static string GetpropertySpList(IEnumerable<AzMetaCloumEntity> azMetaCloums)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var col in azMetaCloums)
            {
                string spfld = GetSpFldName(col.FldNameTo, "P_");
                string spfldtype = GetCorrectType(col);

                stringBuilder.AddLineStatement($"/// <summary>", 2);
                stringBuilder.AddLineStatement($"/// {col.FldDisplay}", 2);
                stringBuilder.AddLineStatement($"///</summary>", 2);
                stringBuilder.AddLineStatement($"public {spfldtype} {spfld} {{ get; set;}}", 2);
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
        #endregion
    }
}
