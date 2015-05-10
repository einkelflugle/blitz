using System;
using Rocket.RocketAPI;
using SDG;
using Steamworks;
using Rocket.RocketAPI.Events;
using Rocket.Logging;
using System.Threading;

namespace Blitz
{
	public class PlayerDeathListener
	{
		public PlayerDeathListener ()
		{
			RocketPlayerEvents.OnPlayerDeath += new RocketPlayerEvents.PlayerDeath (this.onPlayerDeath);
		}

		private void onPlayerDeath(RocketPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer)
		{
			player.Inventory.Clear();
		}
	}
}

