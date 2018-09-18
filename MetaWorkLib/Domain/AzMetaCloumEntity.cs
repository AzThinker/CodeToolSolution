using MetaWorkLib.CustomAttribute;
using SqlRepoEx.SqlServer.CustomAttribute;
using System;
using System.ComponentModel.DataAnnotations;

// AzMetaCloum 业务类
namespace MetaWorkLib.Domain
{
    /// <summary>
    /// AzMetaCloum 业务类
    /// </summary>
    public sealed class AzMetaCloumEntity
    {

        #region  业务属性定义
        [ExportMeta]
        [Display(Name ="Id")]
        [IdentityFiled]
        [KeyFiled]
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }


        [ExportMeta]
        [Display(Name = "字段名")]
        /// <summary>
        ///  字段长:256 个汉字字符
        /// </summary>
        public string FldName { get; set; }

        /// <summary>
        ///  字段长:256 个汉字字符
        /// </summary>
        public string FldNameTo { get; set; }

        [ExportMeta]
        [Display(Name = "代码数据类型")]
        /// <summary>
        ///  字段长:256 个汉字字符
        /// </summary>
        public string FldCodeType { get; set; }

        [ExportMeta]
        [Display(Name = "数据库数据类型")]
        /// <summary>
        ///  字段长:256 个汉字字符
        /// </summary>
        public string FldType { get; set; }

        /// <summary>
        ///  字段长:256 个汉字字符
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? FldLen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? FldLenCode { get; set; }

        [ExportMeta]
        [Display(Name = "显示名")]
        /// <summary>
        ///  字段长:256 个汉字字符
        /// </summary>
        public string FldDisplay { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? TpyeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsSelect { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsNullable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsRequired { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsQuery { get; set; }

        /// <summary>
        ///  字段长:10 个汉字字符
        /// </summary>
        public string QueryStr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? MinValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? MaxValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsGrestZero { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsLimit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsUpdata { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsDataField { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? VIsHideShow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? VpIsCanEdit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? VShowWith { get; set; }


        [ExportMeta]
        [Display(Name = "显示顺序")]
        /// <summary>
        /// 
        /// </summary>
        public int? ShowOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? VIsShow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsGridEdit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CustomDates { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? CustomList { get; set; }

        /// <summary>
        ///  字段长:600 个汉字字符
        /// </summary>
        public string ListParam { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsCreate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? VpIsCanEditShow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsIdentity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsKeyField { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsExtProperty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte? Relation { get; set; }

        /// <summary>
        ///  字段长:100 个汉字字符
        /// </summary>
        public string ClassMasterName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsLazy { get; set; }

        /// <summary>
        ///  字段长:100 个汉字字符
        /// </summary>
        public string InKey { get; set; }

        /// <summary>
        ///  字段长:100 个汉字字符
        /// </summary>
        public string OutKey { get; set; }

        /// <summary>
        ///  字段长:100 个汉字字符
        /// </summary>
        public string KeyType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsMethods { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsBinaryTo { get; set; }

        /// <summary>
        ///  字段长:100 个汉字字符
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        ///  字段长:300 个汉字字符
        /// </summary>
        public string Defvalue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsDefvalue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short? DefType { get; set; }

        /// <summary>
        ///  大文本字段
        /// </summary>
        public string AddAttr { get; set; }

        /// <summary>
        ///  字段长:2 个汉字字符
        /// </summary>
        public string XType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IsOutParam { get; set; }

        /// <summary>
        ///  字段长:100 个汉字字符
        /// </summary>
        public string SchemaFrom { get; set; }

        #endregion



    }
}
