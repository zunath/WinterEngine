using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace WinterEngine.Library.Utility
{
    public static class EnumerationHelper
    {
        /// <summary>
        /// Returns the description of an enumeration.
        /// This is typically a "user-friendly" name for use in the UI layer.
        /// </summary>
        /// <param name="enumeration"></param>
        /// <returns></returns>
        public static string GetEnumerationDescription(Enum enumeration)
        {
            Type type = enumeration.GetType();

            MemberInfo[] memberInfo = type.GetMember(enumeration.ToString());

            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    return ((DescriptionAttribute)attributes[0]).Description;
                }
            }

            return enumeration.ToString();
        }


    }
}
