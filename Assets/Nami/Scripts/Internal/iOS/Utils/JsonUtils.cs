using System;

namespace NamiSDK.Utils
{
	public static class JsonUtils
	{
		private static string AddScopes(this string str)
		{
			return "\"" + str + "\"";
		}

		public static string AddJsonParam(this string jsonString, string paramName, string paramValue, bool useScopes = true)
		{
			return jsonString.Insert(jsonString.Length - 1, "," + paramName.AddScopes() + ":" + (useScopes ? paramValue.AddScopes() : paramValue));
		}

		public static string AddJsonParam(this string jsonString, string paramName, int? paramValue, bool useScopes = false)
		{
			return jsonString.AddJsonParam(paramName, paramValue == null ? "null" : paramValue.ToString(), useScopes);
		}

		public static DateTime ToDateTime(this object obj)
		{
			var unixTimeSeconds = (long)obj;
			var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return epoch.AddSeconds(unixTimeSeconds);
		}
	}
}