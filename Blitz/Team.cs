using System;
using System.Collections.Generic;
using Rocket.Unturned;
using System.Linq;
using Rocket.Unturned.Plugins;

namespace Blitz
{
	public class Team
	{
		public string Name;
		public List<PlayerData> Players;
		public List<Spawn> Spawns;

		public static string TeamList {
			get {
				string list = "";
				List<Team> teams = Blitz.Instance.Configuration.Teams;

				for (int i = 0; i < teams.Count; i++) {
					list += teams [i].Name;
					if (i < teams.Count - 1) {
						list += ", ";
					}
				}

				return list;
			}
		}

		public Team ()
		{
		}

		/// <summary>
		/// This constructor is for use in serialization.
		/// </summary>
		public Team(string name, List<PlayerData> players, List<Spawn> spawns)
		{
			this.Name = name;
			this.Players = players;
			this.Spawns = spawns;
		}

		public static Team ForPlayer(PlayerData data)
		{
			Team team = (from Team t in Blitz.Instance.Configuration.Teams
			             where t.Players.Contains (data)
				select t).FirstOrDefault<Team> ();

			return team;
		}

		public bool AddPlayer(PlayerData p)
		{
			Players.Add (p);
			Blitz.Instance.Configuration.Save ();
			p.GetRocketPlayer ().Teleport (SpawnManager.Instance.GetSpawnpoint(p), 0);
			return true;
		}
	}
}

