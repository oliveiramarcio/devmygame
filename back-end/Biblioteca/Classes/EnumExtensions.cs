using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Biblioteca.Classes
{
    public static class EnumExtensions<T>
    {
        public static string GetEnumDescription(string value)
        {
            Type type = typeof(T);

            var name = Enum.GetNames(type)
                .Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase))
                .Select(d => d)
                .FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }

            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute) customAttribute[0]).Description : name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> ToList<U>()
        {
            Type enumType = typeof(U);

            if (enumType.BaseType != typeof(Enum))
            {
                throw new ArgumentException("T must be of type System.Enum");
            }

            Array enumValArray = Enum.GetValues(enumType);

            List<T> enumValList = new List<T>(enumValArray.Length);

            foreach (int val in enumValArray)
            {
                enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
            }

            return enumValList;
        }
    }
}