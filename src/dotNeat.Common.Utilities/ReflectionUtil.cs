namespace dotNeat.Common.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    public static class ReflectionUtil
    {
        #region attributes discovery

        public static TAttribute? GetAttribute<TAttribute>(object attributedObj)
            where TAttribute : Attribute
        {
            switch (attributedObj)
            {
                case Enum enumValue:
                    {
                        FieldInfo? fi = enumValue.GetType().GetField(enumValue.ToString());
                        TAttribute[]? attributes = (TAttribute[]?)fi?.GetCustomAttributes(typeof(TAttribute), false);
                        return attributes != null && attributes.Length > 0 ? attributes[0] : null;
                    }
                default:
                    {
                        Type attributedType = attributedObj.GetType();
                        TAttribute[] attributes = (TAttribute[])attributedType.GetCustomAttributes(typeof(TAttribute), false);
                        return attributes.Length > 0 ? attributes[0] : null;
                    }
            }
        }

        #endregion attributes discovery

        #region data fields and properties discovery

        public static FieldInfo[] GetAllDataFields(Type type)
        {
            if (type == null)
            {
                return Array.Empty<FieldInfo>();
            }

            List<Type> relevantTypes = new();
            Type? relevantType = type;
            while (relevantType != null)
            {
                relevantTypes.Add(relevantType);
                relevantType = relevantType.BaseType;
            }

            List<FieldInfo> fieldInfos = new();
            relevantTypes.Reverse(); // eventually, we want following order: most base type's data fields first...
            foreach (Type t in relevantTypes)
            {
                fieldInfos.AddRange(t.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic));
            }

            return fieldInfos.ToArray();
        }

        public static FieldInfo[] GetAllStaticDataFields(Type type)
        {
            FieldInfo[] memberInfos =
                type.GetFields(BindingFlags.Static | BindingFlags.NonPublic);

            return memberInfos;
        }

        public static FieldInfo[] GetAllPublicStaticFields(Type type)
        {
            FieldInfo[] memberInfos =
                type.GetFields(BindingFlags.Static | BindingFlags.Public);

            return memberInfos;
        }

        public static PropertyInfo[] GetAllPublicInstanceProperties(Type type)
        {
            PropertyInfo[] memberInfos =
                type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            return memberInfos;
        }

        public static PropertyInfo[] GetAllPublicStaticProperties(Type type)
        {
            PropertyInfo[] memberInfos =
                type.GetProperties(BindingFlags.Static | BindingFlags.Public);

            return memberInfos;
        }

        public static TFieldDataType? GetStaticFieldValue<TFieldDataType>(FieldInfo staticField)
        {
            Debug.Assert(staticField.IsStatic, nameof(staticField.IsStatic));

            object? valueObject = staticField.GetValue(null);
            if (valueObject != null)
            {
                TFieldDataType result = (TFieldDataType)valueObject;

                return result;
            }

            return default;
        }

        public static TField?[] GetAllPublicStaticFieldValues<TField>(Type type)
        {
            FieldInfo[] memberInfos =
                type.GetFields(BindingFlags.GetField | BindingFlags.Static | BindingFlags.Public);

            List<TField?> values = new List<TField?>(memberInfos.Length);
            foreach (FieldInfo memberInfo in memberInfos)
            {
                object? valueObject = memberInfo.GetValue(null);
                TField? value = valueObject != null ? (TField)valueObject : default;
                values.Add(value);
            }
            return values.ToArray();
        }

        #endregion data fields and properties discovery

        public static Type[] GetNestedTypes(Type hostType, BindingFlags nestedTypesBindingFlags = BindingFlags.Public)
        {
            return hostType.GetNestedTypes(nestedTypesBindingFlags);
        }

        public static Type? GetNestedTypeByName(Type hostType, string nestedTypeName, BindingFlags nestedTypeBindingFlags = BindingFlags.Public)
        {
            return hostType.GetNestedType(nestedTypeName, nestedTypeBindingFlags);
        }

        public static Type[] GetSubClassesOf(Type baseType)
        {
            Assembly assembly = baseType.Assembly;
            return GetSubClassesOf(baseType, assembly);
        }

        public static Type[] GetSubClassesOf(Type baseType, Assembly searchAssembly)
        {
            List<Type> types = searchAssembly.GetTypes()
                .Where(t => t.IsSubclassOf(baseType))
                .ToList();
            if (!types.Any())
            {
                types.Add(baseType);
            }
            return types.ToArray();
        }

        public static bool DoesTypeImplementInterface(Type type, Type interfaceType)
        {
            Debug.Assert(type is not null, nameof(type));
            Debug.Assert(interfaceType is not null, nameof(interfaceType));
            Debug.Assert(interfaceType.IsInterface, nameof(interfaceType));

            return type.GetInterfaces().Any(i => i.FullName == interfaceType.FullName);
        }

        public static Type[] GetTypesHierarchy(Type? type)
        {
            if (type == null)
            {
                return Array.Empty<Type>();
            }

            Type[] baseTypes = GetBaseTypesHierarchy(type);
            List<Type> types = new(baseTypes.Length + 1);
            types.Add(type);
            types.AddRange(baseTypes);
            return types.ToArray();
        }

        public static Type[] GetBaseTypesHierarchy(Type type)
        {
            Type? baseType = type?.BaseType;
            if (type == null || baseType == null)
            {
                return Array.Empty<Type>();
            }

            List<Type> baseTypesList = new();
            baseTypesList.Add(baseType);
            baseTypesList.AddRange(GetBaseTypesHierarchy(baseType));
            return baseTypesList.ToArray();
        }

        public static Type[] GetCommonHierarchyTypes(Type lType, Type rType)
        {
            Type? bestCommonType = GetTopCommonSuperType(lType, rType);
            return GetTypesHierarchy(bestCommonType);
        }

        public static Type? GetTopCommonSuperType(Type lType, Type rType)
        {
            Type[] lBases = GetTypesHierarchy(lType);
            Type[] rBases = GetTypesHierarchy(rType);

            foreach (Type lBase in lBases)
            {
                foreach (Type rBase in rBases)
                {
                    if (lBase == rBase)
                    {
                        return lBase;
                    }
                }
            }

            return null;
        }

        public static Type[] GetImplementedInterfaceTypes(Type type)
        {
            if (type == null)
            {
                return Array.Empty<Type>();
            }

            return type.GetInterfaces();
        }

        public static Type[] GetCommonImplementedInterfaces(Type lType, Type rType)
        {
            return GetCommonImplementedInterfaces(new Type[] { lType, rType });
        }

        public static Type[] GetCommonImplementedInterfaces(Type[] types)
        {
            if (types == null || types.Length == 0)
            {
                return Array.Empty<Type>();
            }

            switch (types.Length)
            {
                case 1:
                    return GetImplementedInterfaceTypes(types.First());
                default:
                    Type[] commonInterfaceTypes =
                        GetImplementedInterfaceTypes(types.First());
                    int i = 0;
                    while (commonInterfaceTypes.Length > 0 && ++i < types.Length)
                    {
                        commonInterfaceTypes =
                            commonInterfaceTypes
                            .Intersect(GetImplementedInterfaceTypes(types[i]))
                            .ToArray();
                    }
                    return commonInterfaceTypes.ToArray();
            }
        }
    }
}

