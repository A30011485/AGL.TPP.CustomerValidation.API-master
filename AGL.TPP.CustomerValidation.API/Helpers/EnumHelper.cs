namespace AGL.TPP.CustomerValidation.API.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;

    /// <summary>
    /// Helper for enum types
    /// </summary>
    public class EnumHelper
    {
        // Returns the enum object, if present, when the 'Value' in 'EnumMember' attribute is supplied
        // Returns default (first item in enum) otherwise
        public static T GetItemFromEnumMemberAttributeValue<T>(string enumMemberValue)
        {
            var result = GetEnumMemberAttributeValues<T>().FirstOrDefault(item => item.Value == enumMemberValue);
            return result.Key;
        }

        /// <summary>
        /// Returns enum member (or default - the element passed in by caller) from the string value
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="enumString">Enum string</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="ignoreCase">Ignore case</param>
        /// <returns></returns>
        public static T GetValidMemberOrDefault<T>(string enumString, T defaultValue, bool ignoreCase = false) where T : struct
        {
            Enum.TryParse(enumString, ignoreCase, out T outputEnum);
            return outputEnum;
        }

        /// <summary>
        /// Gets attribute value of an enum member
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <returns>Dictionary of enum and description</returns>
        public static Dictionary<T, string> GetEnumMemberAttributeValues<T>()
        {
            Dictionary<T, string> serializationValues = new Dictionary<T, string>();
            IEnumerable<T> enumValues = GetValues<T>();
            foreach (var enumMember in enumValues)
            {
                var memInfo = typeof(T).GetMember(enumMember.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(EnumMemberAttribute), false);
                if (attributes.Length > 0)
                {
                    var value = ((EnumMemberAttribute)attributes[0]).Value;
                    serializationValues.Add(enumMember, value);
                }
            }
            return serializationValues;
        }

        /// <summary>
        /// Gets the enum values
        /// </summary>
        /// <typeparam name="T">Generic enum type</typeparam>
        /// <returns>Returns list of enum values</returns>
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// Gets the description of an enum
        /// </summary>
        /// <param name="value">Enum value</param>
        /// <returns>Returns the enum description</returns>
        public static string GetDescription(Enum value)
        {
            return
                value
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description
                ?? value.ToString();
        }

        /// <summary>
        ///     A generic extension method that aids in reflecting
        ///     and retrieving any attribute that is applied to an `Enum`.
        /// </summary>
        public static TAttribute GetAttribute<TAttribute>(Enum enumType)
            where TAttribute : Attribute
        {
            return enumType.GetType()
                .GetMember(enumType.ToString())
                .First()
                .GetCustomAttribute<TAttribute>();
        }
    }
}
