using System;
using System.Collections.Generic;
using Rocket.Unturned;
using System.Linq;
using Rocket.Unturned.Plugins;
using Rocket.Unturned.Player;
using UnityEngine;

namespace Blitz
{
	public class Team
	{
		public string Name;
		public Color Color;
		public List<PlayerData> Players;

		public static List<Team> Teams = new List<Team> { new Team ("red", new Color(0.833f, 0.207f, 0.243f)), new Team ("blue", new Color(0.188f, 0.518f, 0.671f)) };

		public static string TeamList {
			get {
				string list = "";

				for (int i = 0; i < Teams.Count; i++) {
					list += Teams[i].Name;
					if (i < Teams.Count - 1) {
						list += ", ";
					}
				}

				return list;
			}
		}

		public Team (string name, Color color)
		{
			Name = name;
			Color = color;
			Players = new List<PlayerData> ();
		}

		public static Team ForPlayer(PlayerData data)
		{
			Team team = (from Team t in Teams
				where t.Players.Contains(data)
				select t).FirstOrDefault<Team> ();

			return team;
		}

		public static Team ForPlayer(RocketPlayer p)
		{
			Team team = (from Team t in Teams
				where t.Players.Contains(PlayerData.ForPlayer(p))
				select t).FirstOrDefault<Team> ();
			
			return team;
		}

		public static List<PlayerData> AllPlayers ()
		{
			List<PlayerData> players = new List<PlayerData> (Teams[0].Players);
			players.AddRange (Teams [1].Players);
			return players;
		}

		public bool AddPlayer(PlayerData p)
		{
			Players.Add (p);
			return true;
		}

		public bool RemovePlayer (PlayerData pd)
		{
			Players.Remove (pd);
			return true;
		}

		public static void TellCurrentTeam(PlayerData pd)
		{
			Team team = Team.ForPlayer (pd);
			RocketChat.Say (pd.GetRocketPlayer(), "You are on the " + team.Name + " team.", team.Color);
		}
	}
}

