using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sat.Recruitment.Domain.ExtensionMethods
{
    public static class EnumExtension
    {
        public static string GetDescription<T>(this T enumValue)
                   where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description;
        }
    }
}
