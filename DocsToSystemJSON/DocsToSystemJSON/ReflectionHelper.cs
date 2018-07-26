using System;
using System.Reflection;
using BattleTech;

namespace DocsToSystemJSON {
    public static class ReflectionHelper {
        
        public static object InvokePrivateMethode(object instance, string methodname, object[] parameters) {
            Type type = instance.GetType();
            MethodInfo methodInfo = type.GetMethod(methodname, BindingFlags.NonPublic | BindingFlags.Instance);
            return methodInfo.Invoke(instance, parameters);
        }

        public static object InvokePrivateMethode(object instance, string methodname, object[] parameters, Type[] types) {
            Type type = instance.GetType();
            MethodInfo methodInfo = type.GetMethod(methodname, BindingFlags.NonPublic | BindingFlags.Instance, null, types, null);
            return methodInfo.Invoke(instance, parameters);
        }

        public static void SetPrivateProperty(object instance, string propertyname, object value) {
            Type type = instance.GetType();
            PropertyInfo property = type.GetProperty(propertyname, BindingFlags.NonPublic | BindingFlags.Instance);
            property.SetValue(instance, value, null);
        }

        public static void SetPrivateField(object instance, string fieldname, object value) {
            Type type = instance.GetType();
            FieldInfo field = type.GetField(fieldname, BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(instance, value);
        }

        public static object GetPrivateField(object instance, string fieldname) {
            Type type = instance.GetType();
            FieldInfo field = type.GetField(fieldname, BindingFlags.NonPublic | BindingFlags.Instance);
            return field.GetValue(instance);
        }
    }
}