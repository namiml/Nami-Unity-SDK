using NamiSDK.Interfaces;
using UnityEngine;

namespace NamiSDK.Implementation
{
    public class NamiUnityEditor : INami
    {
        public void Init(NamiConfiguration configuration)
        {
            Debug.Log("Nami.Init will be called on iOS and Android platforms.");
        }
    }
}