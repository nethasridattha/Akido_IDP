using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTPortal.Core.ExtensionMethods
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            if (enumValue == null)
                return null;

            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .SingleOrDefault()
                            ?.GetCustomAttribute<DisplayAttribute>()
                            ?.GetName();
        }

        public static string GetValue(this Enum enumValue)
        {
            if (enumValue == null)
                return null;

            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .SingleOrDefault()
                            ?.GetCustomAttribute<EnumMemberAttribute>()
                            ?.Value;
        }

        public static T ToEnum<T>(this string enumString)
        {
            return (T)Enum.Parse(typeof(T), enumString);
        }
    }
}
