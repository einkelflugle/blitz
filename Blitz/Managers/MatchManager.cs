using System;
using System.Timers;
using System.Collections.Generic;
using Rocket.Unturned;

namespace Blitz
{
	public class MatchManager
	{
		public static MatchManager Instance { get; private set; }

		public MatchState State;

		public MatchManager ()
		{
			Instance = this;
			this.State = MatchState.LOBBY;
		}

		public enum MatchState {
			LOBBY, IN_PROGRESS, FINISHED
		}

		public void StartCountdown()
		{
			new Countdown (
				60,
				"Match starts in {0} seconds!",
				new List<int> { 60, 30, 20, 10, 5, 4, 3, 2, 1 },
				this.StartMatch
			);
		}

		public void StartMatch()
		{
			this.State = MatchState.IN_PROGRESS;
			int matchTime = Blitz.Instance.Configuration.MatchTime;
			new Countdown (
				matchTime,
				"{0} seconds left.",
				new List<int> { matchTime - 10, matchTime / 2, 5, 4, 3, 2, 1 },
				this.EndMatch
			);
			foreach (Team team in Blitz.Instance.Configuration.Teams) {
				foreach (PlayerData pd in team.Players) {
					pd.GetRocketPlayer ().Teleport (SpawnManager.Instance.GetSpawnpoint (pd), 0);
					pd.GetRocketPlayer ().Inventory.Clear ();
					Unit.GiveLoadout (pd);
				}
			}
		}

		public void EndMatch() {
			this.State = MatchState.FINISHED;
			RocketChat.Say ("Match over.");
			foreach (Team team in Blitz.Instance.Configuration.Teams) {
				foreach (PlayerData pd in team.Players) {
					pd.GetRocketPlayer ().Teleport (Blitz.Instance.Configuration.Lobby.GetLocation(), 0);
					pd.GetRocketPlayer ().Inventory.Clear ();
				}
			}
		}
	}
}

