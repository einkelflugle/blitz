using System;
using System.Timers;
using System.Collections.Generic;
using Rocket.Unturned;
using UnityEngine;
using SDG;
using Steamworks;

namespace Blitz
{
	public class MatchManager
	{
		public static MatchManager Instance { get; private set; }

		public Match CurrentMatch { get; set; }
		public MatchState State;
		private System.Random Rand;

		public MatchManager ()
		{
			Instance = this;
			Rand = new System.Random ();
			this.State = MatchState.DISABLED;
			this.CurrentMatch = ChooseNewMatch ();
		}

		public enum MatchState {
			DISABLED, LOBBY, IN_PROGRESS, FINISHED
		}

		public void StartCountdown()
		{
			this.State = MatchState.LOBBY;
			RocketChat.Say ("Next match: " + MatchManager.Instance.CurrentMatch.Name, Color.cyan);

			// Assign each online player to a team.
			foreach (PlayerData pd in Blitz.Instance.Configuration.Players) {
				if (PlayerTool.getSteamPlayer(new CSteamID(UInt64.Parse(pd.SteamID64))) != null) {
					// If they are not already in a team, assign them to a new one.
					if (Team.ForPlayer(pd) == null) {
						Team team1 = Team.Teams[0];
						Team team2 = Team.Teams[1];
						if (team1.Players.Count <= team2.Players.Count) {
							team1.AddPlayer (pd);
						} else {
							team2.AddPlayer (pd);
						}
					}
					pd.GetRocketPlayer ().Inventory.Clear ();
					pd.GetRocketPlayer ().Heal (100, true, true);
					pd.GetRocketPlayer ().Hunger = 0;
					pd.GetRocketPlayer ().Thirst = 0;
				}
			}

			// Start the match countdown timer.
			new Countdown (
				30,
				"Match starts in {0} seconds!",
				new List<int> { 30, 20, 10, 5, 4, 3, 2, 1 },
				this.StartMatch
			);
		}

		public void StartMatch()
		{
			RocketChat.Say ("Now playing: " + MatchManager.Instance.CurrentMatch.Name, Color.cyan);
			this.State = MatchState.IN_PROGRESS;
			int matchTime = CurrentMatch.MatchTime;
//			LightingManager.W = (uint)(LightingManager.A * CurrentMatch.TimeOfDay);
			foreach (PlayerData pd in Team.AllPlayers()) {
				pd.GetRocketPlayer ().Features.GodMode = false;
				pd.GetRocketPlayer ().Teleport (SpawnManager.Instance.GetSpawnpoint (pd), 0);
				pd.GetRocketPlayer ().Inventory.Clear ();
				pd.GetRocketPlayer ().Heal (100, true, true);
				pd.GetRocketPlayer ().Hunger = 0;
				pd.GetRocketPlayer ().Thirst = 0;
				Unit.GiveLoadout (pd);
			}
			new Countdown (
				matchTime,
				"{0} seconds left.",
				new List<int> { matchTime - 10, matchTime / 2, 5, 4, 3, 2, 1 },
				this.EndMatch
			);
		}

		public void EndMatch() {
			this.State = MatchState.FINISHED;
			RocketChat.Say ("Match over.");
			foreach (PlayerData pd in Team.AllPlayers()) {
				pd.GetRocketPlayer ().Inventory.Clear ();
				pd.GetRocketPlayer ().Teleport (SpawnManager.Instance.GetSpawnpoint(pd), 0);
				pd.GetRocketPlayer ().Heal (100, true, true);
				pd.GetRocketPlayer ().Hunger = 0;
				pd.GetRocketPlayer ().Thirst = 0;
				pd.GetRocketPlayer ().Features.GodMode = true;
			}
			this.CurrentMatch = ChooseNewMatch ();
			new Countdown (
				30,
				"Next match in {0} seconds.",
				new List<int> { 30, 10 },
				this.StartCountdown
			);
		}

		private Match ChooseNewMatch()
		{
			return Blitz.Instance.Configuration.Matches [Rand.Next (0, Blitz.Instance.Configuration.Matches.Count)];
		}
	}
}

