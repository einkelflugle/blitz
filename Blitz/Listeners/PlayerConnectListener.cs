using Rocket.Unturned.Player;
using Rocket.Unturned.Events;
using Rocket.Unturned.Logging;
using Rocket.Unturned;
using UnityEngine;
using Rocket.Unturned.Plugins;


namespace Blitz
{
	public class PlayerConnectListener
	{
		public PlayerConnectListener ()
		{
			RocketServerEvents.OnPlayerConnected += new RocketServerEvents.PlayerConnected(this.onPlayerConnect);
		}

		private void onPlayerConnect(RocketPlayer p)
		{
			PlayerData data = PlayerData.ForPlayer (p);

			if (data == null) {
				// The player does not have saved data.
				data = new PlayerData (p.CSteamID, ERank.PRIVATE, Unit.DefaultUnit);
				Logger.Log ("Saving new data for player '" + p.SteamName + "'.");
				Logger.Log ("\t> Rank: " + ERank.PRIVATE + ".");
				Blitz.Instance.Configuration.Players.Add (data);
				Blitz.Instance.Configuration.Save ();
			} else {
				// The player already has saved data.
				Logger.Log ("Using existing saved data for player '" + p.SteamName + "'.");
			}
				
			RocketChat.Say (string.Format ("{0} {1} joined.", data.Rank, p.SteamName));
		}
	}
}

