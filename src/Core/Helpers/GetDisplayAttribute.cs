using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Helpers
{
    public static class GetDisplayAttribute
    {
        public static string DisplayNameFor<T>(this T item, PropertyInfo propertyInfo) where T : class
        {
            return DisplayName<T>(propertyInfo);
        }

        public static string DisplayNameFor<T, TKey>(this T item, Expression<Func<T, TKey>> property) where T : class
        {
            PropertyInfo propertyInfo = (property.Body as MemberExpression).Member as PropertyInfo;
            return DisplayName<T>(propertyInfo);
        }

        private static string DisplayName<T>(PropertyInfo propertyInfo) where T : class
        {
            var displayName = "";

            if (propertyInfo != null)
            {
                var propertyName = propertyInfo.Name;

                var memberInfo = typeof(T).GetProperty(propertyName);
                var displayAttribute = memberInfo.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
                
                if (displayAttribute != null)
                {
                    displayName = displayAttribute.Name;
                }
                else
                {
                    var displayNameAttribute = memberInfo.GetCustomAttribute(typeof(DisplayNameAttribute)) as DisplayNameAttribute;
                    if (displayNameAttribute != null)
                    {
                        displayName = displayNameAttribute.DisplayName;
                    }
                    else
                    {
                        displayName = propertyName;
                    }
                }
            }

            return displayName;
        }
    }
}