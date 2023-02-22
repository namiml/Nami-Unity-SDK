using NamiSdk.Utils;
using UnityEngine;

namespace NamiSdk
{
    public class NamiSKU
    {
        public NamiSKU(AndroidJavaObject ajo)
        {
            Name = ajo.CallStr("getName");
            SkuId = ajo.CallStr("getSkuId");
            SkuDetails = ajo.CallAJO("getSkuDetails").CallStr("getOriginalJson");
            Type = ajo.CallAJO("getType").JavaToEnum<NamiSKUType>("_");
        }

        public string Name { get; private set; }
        public string SkuId { get; private set; }
        public string SkuDetails { get; private set; }  // TODO check out SkuDetails class
        public NamiSKUType Type { get; private set; }

        // TODO implement product variable for IOS (SkuDetails is working for Android only)
    }
}