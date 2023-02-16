using System;
using System.Linq;
using NamiSdk.JNI;
using UnityEngine;

namespace NamiSdk.Android
{
    public static class JavaEnumUtils
    {
        /// <param name="args">String arguments that will be ignoring in the java enum name.</param>
        public static TEnum JavaToEnum<TEnum>(this AndroidJavaObject ajo, params string[] args) where TEnum : struct
        {
            var javaEnumName = args.Aggregate(ajo.JavaToString(), (current, arg) => current.Replace(arg, ""));
            Enum.TryParse(javaEnumName, true, out TEnum enumValue);
            return enumValue;
        }

        public static AndroidJavaObject EnumToJava<TEnum>(this TEnum enumValue, string javaEnumName) where TEnum : struct
        {
            return new AndroidJavaClass(javaEnumName).GetStatic<AndroidJavaObject>(enumValue.ToString().ToUpper());
        }
    }
}