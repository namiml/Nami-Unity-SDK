using System;
using System.Collections.Generic;

namespace NamiSDK.Interfaces
{
	public interface INamiCampaignManager
	{
		public void Launch(string label, LaunchHandler launchHandler = null, PaywallActionHandler paywallActionHandler = null);
		public List<NamiCampaign> AllCampaigns();
		public void RegisterAvailableCampaignsHandler(Action<List<NamiCampaign>> availableCampaignsCallback);
	}
}