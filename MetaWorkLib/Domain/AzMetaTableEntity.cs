// 元数据表 业务类
using SqlRepoEx.Model;
using SqlRepoEx.SqlServer.CustomAttribute;


namespace MetaWorkLib.Domain
{
    /// <summary>
    /// 元数据表 业务类
    /// </summary>
    public sealed class AzMetaTableEntity
    {

        #region  业务属性定义

        [IdentityFiled]
        [KeyFiled]
        public int Id { get; set; }

        /// <summary>
        ///  字段长:100 个汉字字符
        /// </summary>
        public string ObjModeName { get; set; }
        /// <summary>
        ///  字段长:100 个汉字字符
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        ///  字段长:100 个汉字字符
        /// </summary>
        public string SchemaName { get; set; }
        /// <summary>
        ///  字段长:100 个汉字字符
        /// </summary>
        public string SchemaFrom { get; set; }

        /// <summary>
        ///  字段长:100 个汉字字符
        /// </summary>
        public string ClassDisPlay { get; set; }

        /// <summary>
        ///  大文本字段
        /// </summary>
        public string CodeSetVales { get; set; }



        /// <summary>
        ///  大文本字段
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        ///  大文本字段
        /// </summary>
        public bool IsCustom { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ObjDataType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ObjModeType { get; set; }

        /// <summary>
        ///  字段长:100 个汉字字符
        /// </summary>
        public string AppName { get; set; }


        [NonDatabaseField]
        /// <summary>
        /// 1、来源于结构查询，2、来源于MetaTable
        /// </summary>
        public int ComeFrom { get; set; }


        [NonDatabaseField]
        /// <summary>
        ///标记是表？查询？存储？
        /// </summary>
        public string DbObjType { get; set; }
        #endregion


    }
}
