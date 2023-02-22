using JetBrains.Annotations;

namespace NamiSdk.Utils
{
	public static class JsonUtils
	{
		public static string AddJsonParam(this string jsonString, string paramName, string paramValue)
		{
			return jsonString.Insert(jsonString.Length - 1, ",\"" + paramName + "\"" + ":" + paramValue);
		}
		
		public static string AddJsonParam<TEnum>(this string jsonString, string paramName, TEnum? paramValue) where TEnum : struct
		{
			var enumValue = paramValue == null ? "null" : paramValue.Value.ToString();
			return jsonString.Insert(jsonString.Length - 1, ",\"" + paramName + "\"" + ":" + enumValue);
		}
	}
}