
using SqlRepoEx.Model;
using SqlRepoEx.SqlServer.CustomAttribute;
using System;

// AzMetaCloum 业务类
namespace MetaWorkLib.Domain
{
    /// <summary>
    /// AzMetaCloum 业务类
    /// </summary>
    public sealed class AzMetaCustomCloumEntity
    {

        #region  业务属性定义

        [IdentityFiled]
        [KeyFiled]
        public int Id { get; set; }
        /// <summary>
        /// 字段名称 字段长:256 个汉字字符
        /// </summary>
        public string FldName { get; set; }

        /// <summary>
        /// 代码数据类型 字段长:256 个汉字字符
        /// </summary>
        public string FldCodeType { get; set; }

        /// <summary>
        /// 字段类型 字段长:256 个汉字字符
        /// </summary>
        public string FldType { get; set; }

        /// <summary>
        /// 表名 字段长:256 个汉字字符
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 字段长 字段长:256 个汉字字符
        /// </summary>
        public int? FldLen { get; set; }

        /// <summary>
        /// 代码类型长
        /// </summary>
        public int? FldLenCode { get; set; }


        /// <summary>
        /// 中文名 字段长:256 个汉字字符
        /// </summary>
        public string FldDisplay { get; set; }

        /// <summary>
        /// 生成
        /// </summary>
        public bool? IsSelect { get; set; }

        /// <summary>
        /// 可空
        /// </summary>
        public bool? IsNullable { get; set; }

        /// <summary>
        /// 必填
        /// </summary>
        public bool? IsRequired { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? ShowOrder { get; set; }

        /// <summary>
        /// 自增字段
        /// </summary>
        public bool? IsIdentity { get; set; }

        /// <summary>
        /// 关键字段
        /// </summary>
        public bool? IsKeyField { get; set; }


        public string SchemaFrom { get; set; }

        #endregion



    }
}
