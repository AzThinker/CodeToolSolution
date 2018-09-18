using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaWorkLib.MetaInit
{
    public static class MetaDataTypeHandle
    {
        private static List<MetaDataType> lists = new List<MetaDataType>();
        static MetaDataTypeHandle()
        {
            lists.Add(new MetaDataType { DBType = @"int", CodeType = @"int", DBCodeType = @"Int", DBLen = 4, CodeGeneric = @"int?", CodeSign = 1 });
            lists.Add(new MetaDataType { DBType = @"text", CodeType = @"string", DBCodeType = @"Text", DBLen = -1, CodeGeneric = @"string", CodeSign = 2 });
            lists.Add(new MetaDataType { DBType = @"bigint", CodeType = @"Int64", DBCodeType = @"BigInt", DBLen = 8, CodeGeneric = @"Int64?", CodeSign = 1 });
            lists.Add(new MetaDataType { DBType = @"binary", CodeType = @"byte[]", DBCodeType = @"Binary", DBLen = -1, CodeGeneric = @"byte[]", CodeSign = 0 });
            lists.Add(new MetaDataType { DBType = @"bit", CodeType = @"bool", DBCodeType = @"Bit", DBLen = 1, CodeGeneric = @"bool?", CodeSign = 3 });
            lists.Add(new MetaDataType { DBType = @"char", CodeType = @"string", DBCodeType = @"Char", DBLen = 10, CodeGeneric = @"string", CodeSign = 2 });
            lists.Add(new MetaDataType { DBType = @"datetime", CodeType = @"DateTime", DBCodeType = @"DateTime", DBLen = 8, CodeGeneric = @"DateTime?", CodeSign = 4 });
            lists.Add(new MetaDataType { DBType = @"decimal", CodeType = @"decimal", DBCodeType = @"Decimal", DBLen = 9, CodeGeneric = @"decimal?", CodeSign = 5 });
            lists.Add(new MetaDataType { DBType = @"float", CodeType = @"double", DBCodeType = @"Float", DBLen = 8, CodeGeneric = @"double?", CodeSign = 5 });
            lists.Add(new MetaDataType { DBType = @"image", CodeType = @"byte[]", DBCodeType = @"Image", DBLen = -1, CodeGeneric = @"byte[]", CodeSign = 0 });
            lists.Add(new MetaDataType { DBType = @"money", CodeType = @"decimal", DBCodeType = @"Money", DBLen = 8, CodeGeneric = @"decimal?", CodeSign = 5 });
            lists.Add(new MetaDataType { DBType = @"nchar", CodeType = @"string", DBCodeType = @"NChar", DBLen = 10, CodeGeneric = @"string", CodeSign = 2 });
            lists.Add(new MetaDataType { DBType = @"ntext", CodeType = @"string", DBCodeType = @"NText", DBLen = -1, CodeGeneric = @"string", CodeSign = 6 });
            lists.Add(new MetaDataType { DBType = @"numeric", CodeType = @"decimal", DBCodeType = @"Decimal", DBLen = 9, CodeGeneric = @"decimal?", CodeSign = 5 });
            lists.Add(new MetaDataType { DBType = @"nvarchar", CodeType = @"string", DBCodeType = @"NVarChar", DBLen = 50, CodeGeneric = @"string", CodeSign = 2 });
            lists.Add(new MetaDataType { DBType = @"real", CodeType = @"float", DBCodeType = @"Real", DBLen = 4, CodeGeneric = @"float?", CodeSign = 5 });
            lists.Add(new MetaDataType { DBType = @"smalldatetime", CodeType = @"DateTime", DBCodeType = @"SmallDateTime", DBLen = 4, CodeGeneric = @"DateTime?", CodeSign = 4 });
            lists.Add(new MetaDataType { DBType = @"smallint", CodeType = @"short", DBCodeType = @"SmallInt", DBLen = 2, CodeGeneric = @"short?", CodeSign = 1 });
            lists.Add(new MetaDataType { DBType = @"smallmoney", CodeType = @"decimal", DBCodeType = @"SmallMoney", DBLen = 4, CodeGeneric = @"decimal?", CodeSign = 5 });
            lists.Add(new MetaDataType { DBType = @"timestamp", CodeType = @"byte[]", DBCodeType = @"Timestamp", DBLen = 8, CodeGeneric = @"byte[]", CodeSign = 4 });
            lists.Add(new MetaDataType { DBType = @"tinyint", CodeType = @"byte", DBCodeType = @"TinyInt", DBLen = 1, CodeGeneric = @"byte?", CodeSign = 1 });
            lists.Add(new MetaDataType { DBType = @"varbinary", CodeType = @"byte[]", DBCodeType = @"VarBinary", DBLen = -1, CodeGeneric = @"byte[]", CodeSign = 0 });
            lists.Add(new MetaDataType { DBType = @"varchar", CodeType = @"string", DBCodeType = @"VarChar", DBLen = 50, CodeGeneric = @"string", CodeSign = 2 });
            lists.Add(new MetaDataType { DBType = @"Variant", CodeType = @"Object", DBCodeType = @"Variant", DBLen = -1, CodeGeneric = @"Object", CodeSign = 0 });
            lists.Add(new MetaDataType { DBType = @"uniqueidentifier", CodeType = @"Guid", DBCodeType = @"UniqueIdentifier", DBLen = 16, CodeGeneric = @"Guid?", CodeSign = 0 });
            lists.Add(new MetaDataType { DBType = @"date", CodeType = @"DateTime", DBCodeType = @"Date", DBLen = 8, CodeGeneric = @"DateTime?", CodeSign = 5 });
            lists.Add(new MetaDataType { DBType = @"xml", CodeType = @"string", DBCodeType = @"Xml", DBLen = -1, CodeGeneric = @"string", CodeSign = 2 });

        }

        public static List<MetaDataType> GetMetaDataTypes()
        {
            return lists;
        }
        public static MetaDataType GetMetaDataType(string dbType)
        {

            var result = lists.Where(m => m.DBType == dbType).FirstOrDefault();
            if (result != null)
            {
                return result;
            }

            return new MetaDataType { DBType = dbType, CodeType = dbType, DBCodeType = dbType, CodeGeneric = dbType, DBLen = 0, CodeSign = -1 };
        }
    }

    public class MetaDataType
    {
        public string DBType { get; set; }

        public string CodeType { get; set; }


        public int DBLen { get; set; }


        public string CodeGeneric { get; set; }

        public int CodeSign { get; set; }

        public string DBCodeType { get; set; }
    }
}
