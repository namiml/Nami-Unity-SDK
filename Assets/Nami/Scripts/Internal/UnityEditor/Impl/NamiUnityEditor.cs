using NamiSdk.Interfaces;
using UnityEngine;

namespace NamiSdk.Implementation
{
    public class NamiUnityEditor : INami
    {
        public void Init(NamiConfiguration configuration)
        {
            Debug.Log("Nami.Init will be called on iOS and Android platforms.");
        }
    }
}