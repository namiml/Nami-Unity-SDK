using System;
using System.Linq;
using UnityEngine;

namespace NamiSDK.Utils
{
    public static class ConvertationUtils
    {
        /// <param name="ajo">AndroidJavaObject of java enum.</param>
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

        /// <param name="ajo">AndroidJavaObject of Java.Util.Date instance.</param>
        public static DateTime JavaToDateTime(this AndroidJavaObject ajo)
        {
            long unixTimeMilliseconds = ajo.Call<long>("getTime");
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddMilliseconds(unixTimeMilliseconds);
        }
    }
}