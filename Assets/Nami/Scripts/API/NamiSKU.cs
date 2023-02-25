using System.Collections.Generic;
using NamiSdk.MiniJSON;
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

        public NamiSKU(string json)
        {
            if (Json.Deserialize(json) is Dictionary<string, object> jsonDictionary)
            {
                jsonDictionary.TryGetValue("name", out var nameObject);
                jsonDictionary.TryGetValue("skuId", out var skuIdObject);
                jsonDictionary.TryGetValue("product", out var productObject);
                jsonDictionary.TryGetValue("type", out var typeObject);

                Name = (string)nameObject;
                SkuId = (string)skuIdObject;
                Product = (string)productObject;
                if (typeObject != null) Type = (NamiSKUType)typeObject;
            }
        }

        public string Name { get; private set; }

        public string SkuId { get; private set; }

        /// <summary> Android platforms only </summary>
        public string SkuDetails { get; private set; }  // TODO check out SkuDetails class

        /// <summary> Apple platforms only </summary>
        public string Product { get; private set; }  // TODO check out Product class

        public NamiSKUType Type { get; private set; }
    }
}