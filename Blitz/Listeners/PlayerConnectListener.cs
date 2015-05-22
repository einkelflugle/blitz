using Rocket.Unturned.Player;
using Rocket.Unturned.Events;
using Rocket.Unturned.Logging;
using Rocket.Unturned;


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
				Logger.Log ("\t> Unit: " + Unit.DefaultUnit.Name + ".");

				// Assign the player to the team with the least players.
				Team team1 = Blitz.Instance.Configuration.Teams[0];
				Team team2 = Blitz.Instance.Configuration.Teams[1];
				if (team1.Players.Count <= team2.Players.Count) {
					team1.AddPlayer (data);
				} else {
					team2.AddPlayer (data);
				}
			} else {
				// The player already has saved data.
				Logger.Log ("Using existing saved data for player '" + p.SteamName + "'.");
			}

			Unit.GiveLoadout (PlayerData.ForPlayer (p));
				
			RocketChat.Say (string.Format ("{0} {1} joined.", data.Rank, p.SteamName));
		}
	}
}

