#if UNITY_IOS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NamiSDK.Interfaces;
using NamiSDK.MiniJSON;

namespace NamiSDK.Implementation
{
    public class NamiEntitlementManagerIOS : INamiEntitlementManager
    {
        public List<NamiEntitlement> Active()
        {
            List<NamiEntitlement> entitlements = null;

            var data = _nm_active();

            var dictionary = Json.DeserializeDictionary(data);
            if (dictionary != null)
            {
                dictionary.TryGetValue("entitlements", out var entitlementsObject);
                entitlements = Json.DeserializeList(entitlementsObject)?.Select(jsonObject => new NamiEntitlement(jsonObject)).ToList();
            }

            return entitlements;
        }

        public bool IsEntitlementActive(string referenceId)
        {
            return _nm_isEntitlementActive(referenceId);
        }

        public void Refresh(Action<List<NamiEntitlement>> refreshCallback)
        {
            _nm_refresh(
                refreshCallback == null ? IntPtr.Zero : Callbacks.New(data =>
                {
                    List<NamiEntitlement> entitlements = null;

                    var dictionary = Json.DeserializeDictionary(data);
                    if (dictionary != null)
                    {
                        dictionary.TryGetValue("entitlements", out var entitlementsObject);
                        entitlements = Json.DeserializeList(entitlementsObject)?.Select(jsonObject => new NamiEntitlement(jsonObject)).ToList();
                    }

                    refreshCallback.Invoke(entitlements);
                }));
        }

        public void RegisterActiveEntitlementsHandler(Action<List<NamiEntitlement>> activeEntitlementsCallback)
        {
            if (activeEntitlementsCallback == null) return;
            _nm_registerActiveEntitlementsHandler(
                Callbacks.New(data =>
                {
                    List<NamiEntitlement> entitlements = null;

                    var dictionary = Json.DeserializeDictionary(data);
                    if (dictionary != null)
                    {
                        dictionary.TryGetValue("entitlements", out var entitlementsObject);
                        entitlements = Json.DeserializeList(entitlementsObject)?.Select(jsonObject => new NamiEntitlement(jsonObject)).ToList();
                    }

                    activeEntitlementsCallback.Invoke(entitlements);
                }));
        }

        [DllImport("__Internal")]
        private static extern string _nm_active();

        [DllImport("__Internal")]
        private static extern bool _nm_isEntitlementActive(string referenceId);

        [DllImport("__Internal")]
        private static extern void _nm_refresh(IntPtr refreshCallbackPtr);

        [DllImport("__Internal")]
        private static extern void _nm_registerActiveEntitlementsHandler(IntPtr activeEntitlementsCallbackPtr);
    }
}
#endif