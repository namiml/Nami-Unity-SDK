namespace NamiSdk
{
    public static class Callbacks
    {
        public delegate void StringCallback(string data);
        public static StringCallback New(StringCallback callback) => callback;
    }
}