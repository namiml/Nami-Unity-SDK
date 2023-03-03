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
            if (NamiCustomerManager.IsLoggedIn)
            {
                NamiCustomerManager.Logout();
            }
            else
            {
                NamiCustomerManager.Login(uuid);
            }

            button.interactable = false;
        }

        private void UpdateAccountState(AccountStateAction accountState, bool success, string error)
        {
            UpdateLoginState(accountState == AccountStateAction.Login);
        }

        private void UpdateLoginState(bool isLoggedIn)
        {
            text.text = isLoggedIn ? logoutText : loginText;
            button.interactable = true;
        }
    }
}
