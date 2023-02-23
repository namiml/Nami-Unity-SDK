namespace NamiSdk.Utils
{
	public static class JsonUtils
	{
		public static string AddJsonParam(this string jsonString, string paramName, string paramValue)
		{
			return jsonString.Insert(jsonString.Length - 1, ",\"" + paramName + "\"" + ":" + paramValue);
		}

		public static string AddJsonParam(this string jsonString, string paramName, int? paramValue)
		{
			return jsonString.AddJsonParam(paramName, paramValue == null ? "null" : paramValue.ToString());
		}
	}
}