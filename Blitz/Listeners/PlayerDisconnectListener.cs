using System;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;

namespace Blitz
{
	public class PlayerDisconnectListener
	{
		public PlayerDisconnectListener ()
		{
			RocketServerEvents.OnPlayerDisconnected += new RocketServerEvents.PlayerDisconnected(this.onPlayerDisconnect);
		}

		private void onPlayerDisconnect (RocketPlayer player)
		{
			Team t = Team.ForPlayer (player);
			t.RemovePlayer (PlayerData.ForPlayer (player));
		}
	}
}

