#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using AOT;
using NamiSDK.Utils;
using UnityEngine;

namespace NamiSDK
{
    public static class Callbacks
    {
        internal delegate void StringCallbackDelegate(IntPtr actionPtr, string data);

        [MonoPInvokeCallback(typeof(StringCallbackDelegate))]
        public static void StringCallbackWrapper(IntPtr actionPtr, string data)
        {
            if (actionPtr != IntPtr.Zero)
            {
                var action = actionPtr.Cast<Action<string>>();
                NamiHelper.Queue(() => action(data));
            }
        }

        public static IntPtr New(Action<string> callback)
        {
            _init_stringCallback(StringCallbackWrapper);
            return callback.GetPointer();
        }

        [DllImport("__Internal")]
        private static extern void _init_stringCallback(StringCallbackDelegate stringCallback);
    }
}
#endif