using System;
using NamiSDK;
using UnityEngine;
using UnityEngine.UI;

namespace NamiExample
{
    [RequireComponent(typeof(Button))]
    public class LoginButton : MonoBehaviour
    {
        private Button button;
        [SerializeField] private Text text;

        private const string loginText = "Login";
        private const string logoutText = "Logout";

        private readonly string uuid = Guid.NewGuid().ToString();

        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
            
            NamiCustomerManager.RegisterAccountStateHandler(UpdateAccountState);
            UpdateLoginState(NamiCustomerManager.IsLoggedIn);
        }

        private void OnClick()
        {
            Debug.Log(text.text + "button clicked");
            if (NamiCustomerManager.IsLoggedIn)
            {
                Debug.Log("Logout is processing");
                NamiCustomerManager.Logout();
            }
            else
            {
                Debug.Log("Login is processing" + 
                          "\nwithId: " + uuid);
                NamiCustomerManager.Login(uuid);
            }

            button.interactable = false;
        }

        private void UpdateAccountState(AccountStateAction accountState, bool success, string error)
        {
            Debug.Log("Account state callback received" +
                      "\nAccountState: " + accountState +
                      "\nSuccess: " + success +
                      "\nError: " + error);
            UpdateLoginState(accountState == AccountStateAction.Login);
        }

        private void UpdateLoginState(bool isLoggedIn)
        {
            text.text = isLoggedIn ? logoutText : loginText;
            button.interactable = true;

            Debug.Log("Login state changed" +
                      "\nisLoggedIn: " + isLoggedIn);
        }
    }
}
