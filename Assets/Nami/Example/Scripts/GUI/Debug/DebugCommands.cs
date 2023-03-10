using System;
using System.Collections.Generic;
using NamiSDK;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NamiExample
{
    public class DebugCommands : MonoBehaviour
    {
        [SerializeField] private CommandButton commandButtonInstance;

        private Dictionary<string, Action> commands;
        private readonly List<CommandButton> commandButtons = new List<CommandButton>();

        private void Awake()
        {
            InitCommands();
            foreach (var command in commands)
            {
                var commandButton = Instantiate(commandButtonInstance, commandButtonInstance.transform.parent);
                commandButton.Name = command.Key;
                commandButton.Action = command.Value;

                commandButtons.Add(commandButton);
            }

            commandButtonInstance.gameObject.SetActive(false);
        }

        private void InitCommands()
        {
            commands = new Dictionary<string, Action>
            {
                {
                    "NamiCampaignManager.AllCampaigns", () =>
                    {
                        var logMessage = "NamiCampaignManager.AllCampaigns: ";

                        var campaigns = NamiCampaignManager.AllCampaigns();
                        if (campaigns == null)
                        {
                            logMessage += "null";
                            Debug.Log(logMessage);
                            return;
                        }

                        logMessage += "\n> Count: " + campaigns.Count;
                        if (campaigns.Count > 0)
                        {
                            var index = Random.Range(0, campaigns.Count);
                            var campaign = campaigns[index];
                            logMessage += "\n> Campaigns[" + index + "]: " + 
                                          "\n>> (iOS only) Id: " + campaign.Id +
                                          "\n>> (iOS only) Rule: " + campaign.Rule +
                                          "\n>> Paywall: " + campaign.Paywall +
                                          "\n>> Segment: " + campaign.Segment +
                                          "\n>> (Android only) Type: " + campaign.Type +
                                          "\n>> Value: " + campaign.Value;
                        }

                        Debug.Log(logMessage);
                    }
                },
                {
                    "NamiCampaignManager.RegisterAvailableCampaignsHandler", () =>
                    {
                        NamiCampaignManager.RegisterAvailableCampaignsHandler(campaigns =>
                        {
                            var logCallbackMessage = "NamiCampaignManager.RegisterAvailableCampaignsHandler: Callback received:" + "\nCampaigns: ";

                            if (campaigns == null)
                            {
                                logCallbackMessage += "null";
                                Debug.Log(logCallbackMessage);
                                return;
                            }

                            logCallbackMessage += "\n> Count: " + campaigns.Count;
                            if (campaigns.Count > 0)
                            {
                                var index = Random.Range(0, campaigns.Count);
                                var campaign = campaigns[index];
                                logCallbackMessage += "\n> Campaigns[" + index + "]: " +
                                              "\n>> (iOS only) Id: " + campaign.Id +
                                              "\n>> (iOS only) Rule: " + campaign.Rule +
                                              "\n>> Paywall: " + campaign.Paywall +
                                              "\n>> Segment: " + campaign.Segment +
                                              "\n>> (Android only) Type: " + campaign.Type +
                                              "\n>> Value: " + campaign.Value;
                            }

                            Debug.Log(logCallbackMessage);
                        });

                        Debug.Log("NamiCampaignManager.RegisterAvailableCampaignsHandler: Callback Registered");
                    }
                },
                {
                    "NamiCustomerManager.IsLoggedIn", () =>
                    {
                        var logMessage = "NamiCustomerManager.IsLoggedIn: ";
                        logMessage += NamiCustomerManager.IsLoggedIn;

                        Debug.Log(logMessage);
                    }
                },
                {
                    "NamiCustomerManager.LoggedInId", () =>
                    {
                        var logMessage = "NamiCustomerManager.LoggedInId: ";
                        logMessage += NamiCustomerManager.LoggedInId;

                        Debug.Log(logMessage);
                    }
                },
                {
                    "NamiCustomerManager.JourneyState", () =>
                    {
                        var logMessage = "NamiCustomerManager.JourneyState: ";

                        var journeyState = NamiCustomerManager.JourneyState;
                        logMessage += "\nJourneyState: " +
                                      "\n> FormerSubscriber: " + journeyState?.FormerSubscriber +
                                      "\n> InGracePeriod: " + journeyState?.InGracePeriod +
                                      "\n> InTrialPeriod: " + journeyState?.InTrialPeriod +
                                      "\n> InIntroOfferPeriod: " + journeyState?.InIntroOfferPeriod +
                                      "\n> IsCancelled: " + journeyState?.IsCancelled +
                                      "\n> (Android only) InPause: " + journeyState?.InPause +
                                      "\n> InAccountHold: " + journeyState?.InAccountHold;

                        Debug.Log(logMessage);
                    }
                },
                {
                    "NamiEntitlementManager.Active", () =>
                    {
                        var logMessage = "NamiEntitlementManager.Active: ";
                        
                        var entitlements = NamiEntitlementManager.Active();
                        logMessage += "\nNamiEntitlements Count: " + entitlements?.Count;

                        Debug.Log(logMessage);
                    }
                },
                {
                    "NamiPaywallManager.Dismiss", () =>
                    {
                        Debug.Log("NamiPaywallManager.Dismiss Called");
                        NamiPaywallManager.Dismiss();
                    }
                },
                {
                    "NamiPaywallManager.RegisterCloseHandler", () =>
                    {
                        NamiPaywallManager.RegisterCloseHandler(() =>
                        {
                            Debug.Log("NamiPaywallManager.RegisterCloseHandler: Callback received");
                            if (Application.platform != RuntimePlatform.Android)
                            {
                                NamiPaywallManager.Dismiss();
                            }
                        });

                        Debug.Log("NamiPaywallManager.RegisterCloseHandler: Callback Registered");
                    }
                },
                {
                    "NamiPaywallManager.RegisterSignInHandler", () =>
                    {
                        NamiPaywallManager.RegisterSignInHandler(() =>
                        {
                            Debug.Log("NamiPaywallManager.RegisterSignInHandler: Callback received");
                        });

                        Debug.Log("NamiPaywallManager.RegisterSignInHandler: Callback Registered");
                    }
                },
                {
                    "NamiPaywallManager.RegisterBuySkuHandler", () =>
                    {
                        NamiPaywallManager.RegisterBuySkuHandler(skuRefId  =>
                        {
                            Debug.Log("NamiPaywallManager.RegisterBuySkuHandler: Callback received");
                        });

                        Debug.Log("NamiPaywallManager.RegisterBuySkuHandler: Callback Registered");
                    }
                },
                {
                    "NamiPurchaseManager.RegisterPurchasesChangedHandler", () =>
                    {
                        NamiPurchaseManager.RegisterPurchasesChangedHandler((purchases, purchaseState, error) =>
                        {
                            Debug.Log("NamiPaywallManager.RegisterPurchasesChangedHandler: Callback received");
                        });

                        Debug.Log("NamiPaywallManager.RegisterPurchasesChangedHandler: Callback Registered");
                    }
                },
                {
                    "NamiPurchaseManager.RegisterRestorePurchasesHandler (iOS only)", () =>
                    {
                        NamiPurchaseManager.RegisterRestorePurchasesHandler((purchaseState, newPurchases, oldPurchases, error) =>
                        {
                            Debug.Log("NamiPaywallManager.RegisterPurchasesChangedHandler: Callback received");
                        });

                        Debug.Log("NamiPaywallManager.RegisterPurchasesChangedHandler: Callback Registered");
                    }
                },
                {
                    "NamiPurchaseManager.RestorePurchases (iOS only)", () =>
                    {
                        Debug.Log("NamiPurchaseManager.RestorePurchases Called");
                        NamiPurchaseManager.RestorePurchases((purchaseState, newPurchases, oldPurchases, error) =>
                        {
                            Debug.Log("Restore purchases callback received");
                        });
                    }
                },
            };
        }
    }
}
