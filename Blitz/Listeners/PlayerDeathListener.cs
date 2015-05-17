using SDG;
using Steamworks;
using Rocket.Unturned.Player;
using Rocket.Unturned.Events;
using Rocket.Unturned;

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

