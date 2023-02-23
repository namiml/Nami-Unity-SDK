namespace NamiSdk.Utils
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
	}
}