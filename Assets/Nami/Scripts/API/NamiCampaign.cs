using NamiSdk.MiniJSON;
using NamiSdk.Utils;
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

        public NamiCampaign(object json)
        {
            var dictionary = Json.DeserializeDictionary(json);
            if (dictionary != null)
            {
                dictionary.TryGetValue("id", out var idObject);
                dictionary.TryGetValue("rule", out var ruleObject);
                dictionary.TryGetValue("paywall", out var paywallObject);
                dictionary.TryGetValue("segment", out var segmentObject);
                dictionary.TryGetValue("type", out var typeObject);
                dictionary.TryGetValue("value", out var valueObject);

                Id = (string)idObject;
                Rule = (string)ruleObject;
                Paywall = (string)paywallObject;
                Segment = (string)segmentObject;
                if (typeObject != null) Type = (NamiCampaignRuleType)(long)typeObject;
                Value = (string)valueObject;
            }
        }

        /// <summary> Apple platforms only </summary>
        public string Id { get; private set; }

        /// <summary> Apple platforms only </summary>
        public string Rule { get; private set; }

        public string Paywall { get; private set; }

        public string Segment { get; private set; }

        /// <summary> Android platforms only </summary>
        public NamiCampaignRuleType Type { get; private set; }

        public string Value { get; private set; }
    }
}