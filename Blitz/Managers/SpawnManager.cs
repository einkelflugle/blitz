using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using SDG;
using Rocket.Unturned;

namespace Blitz
{
	public class SpawnManager
	{
		public static SpawnManager Instance { get; private set; }

		private readonly System.Random rand;

		public SpawnManager ()
		{
			Instance = this;
			rand = new System.Random();
		}

		public Vector3 GetSpawnpoint(PlayerData p)
		{
			Team t = Team.ForPlayer (p);
			if (MatchManager.Instance.State != MatchManager.MatchState.IN_PROGRESS) {
				MatchManager.Instance.GivePlayerLobbyItems (p.GetRocketPlayer ());
				return MatchManager.Instance.CurrentMatch.Lobby.GetLocation ();
			}

			List<Spawn> spawns = MatchManager.Instance.CurrentMatch.Spawns.Where (
				s => s.UnitName.ToLower ().Equals (p.Unit.ToLower ()) &&
					s.TeamName.ToLower().Equals(t.Name.ToLower())
			).ToList();

			// If the spawn's unit is "Default"
			if (spawns.Count == 0) {
				spawns = MatchManager.Instance.CurrentMatch.Spawns.Where (s => (s.UnitName.ToLower ().Equals ("default")) && s.TeamName.ToLower().Equals(Team.ForPlayer(p).Name.ToLower())).ToList ();
			}
			
			Spawn chosenSpawn = spawns[rand.Next (0, spawns.Count)];
			
			return new Vector3(chosenSpawn.x, chosenSpawn.y, chosenSpawn.z);
		}
	}
}

