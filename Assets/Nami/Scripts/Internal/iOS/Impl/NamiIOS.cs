using System.Runtime.InteropServices;
using NamiSdk.Interfaces;

namespace NamiSdk.Implementation
{
    public class NamiIOS : INami
    {
        public void Init(NamiConfiguration configuration)
        {
            _nm_init(configuration?.JSON);
        }

        [DllImport("__Internal")]
        private static extern void _nm_init(string configurationJson);
    }
}