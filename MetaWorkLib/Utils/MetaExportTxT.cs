using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MetaWorkLib.Utils
{
    public static class MetaExportTxT
    {


        private static string GetHead<TEntity>()
        {
            return CustomExportAttributeHandle.FieldDisplayNames<TEntity>() + "\r\n";
        }

        private static string GetDataRow<TEntity>(TEntity entity)
        {
            return entity.FieldDatas();
        }

        public static string GetDataRows<TEntity>(IEnumerable<TEntity> entities)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(GetHead<TEntity>());
            foreach (var item in entities)
            {
                stringBuilder.Append(GetDataRow(item));
            }
            return stringBuilder.ToString();
        }
    }
}
