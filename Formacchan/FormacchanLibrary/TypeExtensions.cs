using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extensions
{
    public static class TypeExtensions
    {
        public static bool IsCollectionType(this Type type)
        {
            try
            {                                 
                if (type.IsConstructedGenericType)         
                {                                          
                    var types = type.GetGenericTypeDefinition().GetInterfaces();
                    return types.Contains(typeof(IEnumerable));
                }
                return type.IsArray || type is ICollection || type is IEnumerable;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValueTypeOrString(this Type type)
        {
            return type.Equals(typeof(string)) || type.IsValueType;
        }
    }
}
