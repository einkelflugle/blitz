﻿using UnityEngine;
using System.Linq;
using System.Collections.Generic;

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
			if (MatchManager.Instance.State != MatchManager.MatchState.IN_PROGRESS) {
				return Blitz.Instance.Configuration.Lobby.GetLocation ();
			}

			Team team = (from Team t in Blitz.Instance.Configuration.Teams
			             where t.Players.Contains (p)
			             select t).FirstOrDefault<Team> ();

			List<Spawn> spawns = team.Spawns.Where (s => s.unitName.ToLower ().Equals (p.Unit.ToLower ())).ToList();

			if (spawns.Count == 0) {
				spawns = team.Spawns.Where (s => s.unitName.ToLower ().Equals ("default")).ToList ();
			}
			
			Spawn chosenSpawn = spawns[rand.Next (0, spawns.Count)];
			
			return new Vector3(chosenSpawn.x, chosenSpawn.y, chosenSpawn.z);
		}
	}
}

