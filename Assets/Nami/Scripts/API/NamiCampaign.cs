using NamiSdk.JNI;
using UnityEngine;

namespace NamiSdk
{
    public class NamiCampaign
    {
        public NamiCampaign(AndroidJavaObject ajo)
        {
            Paywall = ajo.CallStr("getPaywall");
            Segment = ajo.CallStr("getSegment");
            Type = ajo.CallAJO("getType").JavaToEnum<NamiCampaignRuleType>();
            Value = ajo.CallStr("getValue");;
        }

        public string Paywall { get; private set; }
        public string Segment { get; private set; }
        public NamiCampaignRuleType Type { get; private set; }
        public string Value { get; private set; }
    }
}