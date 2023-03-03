using NamiSDK;
using UnityEngine;
using UnityEngine.UI;

public class EntitlementCard : MonoBehaviour
{
    [SerializeField] private Text titleText;

    public void UpdateInfo(NamiEntitlement entitlement)
    {
        if (entitlement == null)
        {
            titleText.text = "";
        }
        else
        {
            titleText.text = "Name: " + entitlement.Name + "\nRefId: " + entitlement.ReferenceId;
        }
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}
