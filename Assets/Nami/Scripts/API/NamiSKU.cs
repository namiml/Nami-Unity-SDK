using System.Collections.Generic;
using NamiSDK.MiniJSON;
using NamiSDK.Utils;
using UnityEngine;

namespace NamiSDK
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

        public NamiSKU(object json)
        {
            var dictionary = Json.DeserializeDictionary(json);
            if (dictionary != null)
            {
                dictionary.TryGetValue("name", out var nameObject);
                dictionary.TryGetValue("skuId", out var skuIdObject);
                dictionary.TryGetValue("product", out var productObject);
                dictionary.TryGetValue("type", out var typeObject);

                Name = (string)nameObject;
                SkuId = (string)skuIdObject;
                Product = (string)productObject;
                if (typeObject != null) Type = (NamiSKUType)(long)typeObject;
            }
        }

        public string Name { get; private set; }

        public string SkuId { get; private set; }

        /// <summary> GooglePlay only </summary>
        public string SkuDetails { get; private set; }

        /// <summary> Apple only </summary>
        public string Product { get; private set; }

        public NamiSKUType Type { get; private set; }
    }
}