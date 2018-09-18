using MetaWorkLib.MetaInit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinCodeView.UI
{
    internal class ObjDataConverter : EnumConverter
    {
        private Type type;

        public ObjDataConverter(Type type)
            : base(type)
        {
            this.type = type;
        }

        public override object ConvertTo(ITypeDescriptorContext cntx, CultureInfo cult, object value, Type destType)
        {
            FieldInfo fieldInfo = type.GetField(Enum.GetName(type, value));
            DescriptionAttribute descAtt =
                (DescriptionAttribute)Attribute.GetCustomAttribute(
                fieldInfo, typeof(DescriptionAttribute));

            if (descAtt != null)
            {
                return descAtt.Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public override object ConvertFrom(ITypeDescriptorContext cntx, CultureInfo cult, object value)
        {
            foreach (FieldInfo fieldInfo in type.GetFields())
            {
                DescriptionAttribute descAtt = (DescriptionAttribute)Attribute.GetCustomAttribute(
                    fieldInfo, typeof(DescriptionAttribute));

                if ((descAtt != null) && ((string)value == descAtt.Description))
                {
                    return Enum.Parse(type, fieldInfo.Name);
                }
            }
            return Enum.Parse(type, (string)value);
        }
    }

    internal class ObjDataPresentationConverter : ExpandableObjectConverter
    {

        private string GetObjDataType(ObjDataTypeEnum value)
        {
            Type type = typeof(ObjDataTypeEnum);
            FieldInfo fieldInfo = type.GetField(Enum.GetName(type, value));
            DescriptionAttribute descAtt =
                (DescriptionAttribute)Attribute.GetCustomAttribute(
                fieldInfo, typeof(DescriptionAttribute));

            if (descAtt != null)
            {
                return descAtt.Description;
            }
            else
            {
                return value.ToString();
            }

        }

        private ObjDataTypeEnum GetObjDataTypeStr(string value)
        {
            Type type = typeof(ObjDataTypeEnum);
            foreach (FieldInfo fieldInfo in type.GetFields())
            {
                DescriptionAttribute descAtt = (DescriptionAttribute)Attribute.GetCustomAttribute(
                    fieldInfo, typeof(DescriptionAttribute));

                if ((descAtt != null) && (value == descAtt.Description))
                {
                    return (ObjDataTypeEnum)Enum.Parse(type, fieldInfo.Name);
                }
            }
            return (ObjDataTypeEnum)Enum.Parse(type, value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context,
                                   System.Type destinationType)
        {
            if (destinationType == typeof(ObjDataPresentation))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, System.Type destinationType)
        {
            if (destinationType == typeof(System.String) &&
                 value is ObjDataPresentation)
            {
                ObjDataPresentation op = (ObjDataPresentation)value;
                return "类型:" + GetObjDataType(op.ObjDataType) +
                       "，查询: " + op.IsSchemaForOther +
                       "，更新: " + op.UpdateTableName +
                       "，存储: " + op.StoreProcedureQuery;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                try
                {
                    string s = (string)value;
                    int colon = s.IndexOf(':');
                    int comma = s.IndexOf(',');
                    if (colon != -1 && comma != -1)
                    {
                        ObjDataTypeEnum objdatetype = GetObjDataTypeStr(s.Substring(colon + 1, (comma - colon - 1)));
                        colon = s.IndexOf(':', comma + 1);
                        comma = s.IndexOf(',', comma + 1);
                        bool isview = Boolean.Parse(s.Substring(colon + 1, (comma - colon - 1)));
                        colon = s.IndexOf(':', comma + 2);
                        comma = s.IndexOf(',', comma + 2);
                        string updatetablefrom = s.Substring(colon + 1, (comma - colon - 1));
                        colon = s.IndexOf(':', comma + 3);
                        string storeProcedureQuery = s.Substring(colon + 1);

                        ObjDataPresentation op = new ObjDataPresentation();
                        op.ObjDataType = objdatetype;
                        op.IsSchemaForOther = isview;
                        op.UpdateTableName = updatetablefrom;
                        op.StoreProcedureQuery = storeProcedureQuery;
                        return op;
                    }
                }
                catch
                {
                    throw new ArgumentException(
                        "无法将“" + (string)value +
                                           "”转换为 SpellingOptions 类型");
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }

    internal class SpecialitySetConverter : ExpandableObjectConverter
    {

        public override bool CanConvertTo(ITypeDescriptorContext context,
                           System.Type destinationType)
        {
            if (destinationType == typeof(ObjDataPresentation))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, System.Type destinationType)
        {
            if (destinationType == typeof(System.String) && value is SpecialitySet)
            {
                SpecialitySet op = (SpecialitySet)value;
                return "缓存:" + op.PageCache +
                       "，大文本: " + op.BigText;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                try
                {
                    string s = (string)value;
                    int colon = s.IndexOf(':');
                    int comma = s.IndexOf(',');
                    if (colon != -1 && comma != -1)
                    {
                        bool ispagecache = Boolean.Parse(s.Substring(colon + 1, (comma - colon - 1)));
                        colon = s.IndexOf(':', comma + 1);
                        bool isbigtext = Boolean.Parse(s.Substring(colon + 1));

                        SpecialitySet op = new SpecialitySet();
                        op.PageCache = ispagecache;
                        op.BigText = isbigtext;
                        return op;
                    }
                }
                catch
                {
                    throw new ArgumentException(
                        "无法将“" + (string)value +
                                           "”转换为 SpellingOptions 类型");
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }

    public class UpdateTableFromConverter : StringConverter
    {
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(StringArray);
        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public static List<string> StringArray = DataHelper.GetGetDBListForProperty();

    }
}

public class CheckBoxInPropertyGridEditor : UITypeEditor
{
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
        return UITypeEditorEditStyle.Modal;
    }

    public override bool GetPaintValueSupported(ITypeDescriptorContext context)

    {

        return true;

    }


    public override void PaintValue(PaintValueEventArgs e)

    {


        ControlPaint.DrawCheckBox(e.Graphics, e.Bounds, bool.Parse(e.Value.ToString()) ? ButtonState.Checked : ButtonState.Normal);

    }

}
 
