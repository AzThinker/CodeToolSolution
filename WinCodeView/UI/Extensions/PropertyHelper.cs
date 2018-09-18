//元ネタ
//http://tenera-it.be/blog/2011/06/add-attriutes-to-a-property-at-runtime/
//https://github.com/TN8001/DynamicAttribute
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace WinCodeView.UI
{
    //注意) さらっと調べただけなので正しい情報は各自調べてください！！

    //Attribute（属性）はコンパイル時に付けられるもので、実行時にどうにかできるものではありません。
    //が、PropertyGridはTypeDescriptor経由で属性を取得するので、そこでどうにかすることが可能です。
    //本来はCustomTypeDescriptorやTypeDescriptionProvider等を使うのが本筋のようです。
    //しかし面倒なので元ネタを参考に直接リフレクションで書き換える力技です。
    //動作確認はできましたが、この先も動く保証はありません。
    //只是查了一下，请大家调查正确的信息! !
    // attribute(属性)是在编译时安装的，在执行时还能不能解决。
    //propertygrid是通过type descriptor的方式来获取属性的，所以可以在那里解决。
    //原本使用的是customtypedescriptor和typedescriptionprovider等。
    //但由于麻烦的缘故，为参考原作而直接在reflead转换的能力技巧。
    //虽然已经确认了动作，但没有任何保证。
    public static class PropertyHelper
    {
        /// <summary>在属性上设定属性(如果存在相同的属性)</summary>
        /// <param name="type">类型</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="attribute">指定特性</param>
        public static void AddAttribute(Type type, string propertyName, Attribute attribute)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (attribute == null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            var prop = TypeDescriptor.GetProperties(type).AsEnumerable<PropertyDescriptor>()
                                                         .Where(x => x.Name == propertyName)
                                                         .FirstOrDefault();
            if (prop == null)
            {
                throw new ArgumentException($"{type} 类型 {propertyName} 不存在。");
            }

            Add(prop, attribute);
        }

        /// <summary>多个属性指定同一特性</summary>
        /// <param name="type">类型</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="attribute">指定特性</param>
        public static void AddAttribute(Type type, IEnumerable<string> propertyNames, Attribute attribute)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (propertyNames == null)
            {
                throw new ArgumentNullException(nameof(propertyNames));
            }

            if (attribute == null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            var props = TypeDescriptor.GetProperties(type).AsEnumerable<PropertyDescriptor>();
            var except = propertyNames.Except(props.Select(x => x.Name));
            if (except.Any())
            {
                throw new ArgumentException($"{type} 类型 {string.Join(", ", except)} 不存在");
            }

            foreach (var prop in props)
            {
                if (propertyNames.Contains(prop.Name))
                {
                    Add(prop, attribute);
                }
            }
        }

        /// <summary>移除特性</summary>
        /// <param name="type">类型</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="attributeType">>移出特性</param>
        public static void RemoveAttribute(Type type, string propertyName, Type attributeType)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (attributeType == null)
            {
                throw new ArgumentNullException(nameof(attributeType));
            }

            var prop = TypeDescriptor.GetProperties(type).AsEnumerable<PropertyDescriptor>()
                                                         .Where(x => x.Name == propertyName)
                                                         .FirstOrDefault();
            if (prop == null)
            {
                throw new ArgumentException($"{type} 类型 {propertyName} 不存在。");
            }

            Remove(prop, attributeType);
        }

        /// <summary>移除多个特性</summary>
        /// <param name="type">类型</param>
        /// <param name="propertyNames">属性名列表</param>
        /// <param name="attributeType">>移出特性</param>
        public static void RemoveAttribute(Type type, IEnumerable<string> propertyNames, Type attributeType)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (propertyNames == null)
            {
                throw new ArgumentNullException(nameof(propertyNames));
            }

            if (attributeType == null)
            {
                throw new ArgumentNullException(nameof(attributeType));
            }

            var props = TypeDescriptor.GetProperties(type).AsEnumerable<PropertyDescriptor>();
            var except = propertyNames.Except(props.Select(x => x.Name));
            if (except.Any())
            {
                throw new ArgumentException($"{type} 类型 {string.Join(", ", except)} 不存在");
            }

            foreach (var prop in props)
            {
                if (propertyNames.Contains(prop.Name))
                {
                    Remove(prop, attributeType);
                }
            }
        }




        private static void Add(PropertyDescriptor prop, Attribute attribute)
        {
            var pi = GetPropertyInfo(prop, "AttributeArray");
            var attributes = pi.GetValue(prop, null) as Attribute[];
            var newAttributes = attributes.Where(x => x.GetType() != attribute.GetType()).ToList();
            newAttributes.Add(attribute);
            pi.SetValue(prop, newAttributes.ToArray(), null);
        }
        private static void Remove(PropertyDescriptor prop, Type attributeType)
        {
            var pi = GetPropertyInfo(prop, "AttributeArray");
            var attributes = pi.GetValue(prop, null) as Attribute[];
            var newAttributes = attributes.Where(x => x.GetType() != attributeType);
            pi.SetValue(prop, newAttributes.ToArray(), null);
        }

        private static PropertyInfo GetPropertyInfo(PropertyDescriptor prop, string name)
            => prop.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.NonPublic);
    }

    internal static class IEnumerableExtensions
    {
        public static IEnumerable<T> AsEnumerable<T>(this IEnumerable collection)
        {
            foreach (var e in collection)
            {
                yield return (T)e;
            }
        }
    }

    //https://stackoverflow.com/questions/43331145/how-can-i-improve-performance-of-an-addrange-method-on-a-custom-bindinglist
    internal static class BindingListExtensions
    {
        public static void AddRange<T>(this BindingList<T> bindingList, IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            var oldRaiseEventsValue = bindingList.RaiseListChangedEvents;
            try
            {
                bindingList.RaiseListChangedEvents = false;

                foreach (var value in collection)
                {
                    bindingList.Add(value);
                }
            }
            finally
            {
                bindingList.RaiseListChangedEvents = oldRaiseEventsValue;

                if (bindingList.RaiseListChangedEvents)
                {
                    bindingList.ResetBindings();
                }
            }
        }
    }
}
