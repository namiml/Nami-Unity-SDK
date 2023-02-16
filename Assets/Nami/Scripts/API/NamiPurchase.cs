using System;
using System.Collections.Generic;
using System.Linq;
using NamiSdk.Android;
using NamiSdk.JNI;
using UnityEngine;

namespace NamiSdk
{
    public class NamiPurchase
    {
       private readonly AndroidJavaObject _ajo;

       private NamiPurchase(AndroidJavaObject ajo)
       { 
          this._ajo = ajo;
       }

       public static List<NamiPurchase> ExtractFromJavaList(AndroidJavaObject ajoList)
       { 
          return ajoList.FromJavaList<AndroidJavaObject>().Select(ajo => new NamiPurchase(ajo)).ToList();
       }

       public long GetPurchaseInitiatedTimestamp()
       {
          return _ajo.CallLong("getPurchaseInitiatedTimestamp");
       }

       public DateTime GetExpires()
       {
          return _ajo.CallAJO("getExpires").JavaToDateTime();
       }

       public NamiPurchaseSource GetPurchaseSource()
       {
          return _ajo.CallAJO("getPurchaseSource").JavaToEnum<NamiPurchaseSource>();
       }

       public string GetSkuId()
       {
          return _ajo.CallStr("getSkuId");
       }

       public string GetSkuUUID()
       {
          return _ajo.CallStr("getSkuUUID");
       }
       
       public string GetTransactionIdentifier()
       {
          return _ajo.CallStr("getTransactionIdentifier");
       }
       
       public string GetPurchaseToken()
       {
          return _ajo.CallStr("getPurchaseToken");
       }
       
       public string GetLocalizedDescription()
       {
          return _ajo.CallStr("getLocalizedDescription");
       }
    }
}