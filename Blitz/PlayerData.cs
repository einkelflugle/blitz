using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Steamworks;
using System.Linq;
using Rocket.Unturned.Player;

namespace Blitz
{
	public class PlayerData
	{
		[XmlAttribute ("steamid64")]
		public string SteamID64;

		[XmlAttribute ("rank")]
		public ERank Rank;

		[XmlAttribute ("unit")]
		public string Unit;

		protected PlayerData()
		{
		}

		/// <summary>
		/// This constructor is used in serialization.
		/// The player's unit will be set when they join.
		/// </summary>
		public PlayerData(string steamid64, string rank)
		{
			this.SteamID64 = steamid64;
			this.Rank = (ERank)Enum.Parse (typeof(ERank), rank);
			this.Unit = null;
		}

		/// <summary>
		/// This constructor is used in serialization.
		/// </summary>
		public PlayerData(string steamid64, string rank, string unit)
		{
			this.SteamID64 = steamid64;
			this.Rank = (ERank)Enum.Parse (typeof(ERank), rank);
			this.Unit = unit;
		}

		public PlayerData(CSteamID id, ERank rank, Unit unit)
		{
			this.SteamID64 = id.ToString();
			this.Rank = rank;
			this.Unit = unit.Name;
		}

		/// <summary>
		/// Finds the saved player data associated with the specified RocketPlayer.
		/// </summary>
		public static PlayerData ForPlayer (RocketPlayer player)
		{
			List<PlayerData> t1 = new List<PlayerData>(Blitz.Instance.Configuration.Teams [0].Players);
			List<PlayerData> t2 = new List<PlayerData>(Blitz.Instance.Configuration.Teams [1].Players);
			t1.AddRange (t2);

			PlayerData data = (from PlayerData d in t1
				where d.SteamID64.Equals (player.CSteamID.ToString ())
				select d).FirstOrDefault<PlayerData> ();

			return data;
		}

		public RocketPlayer GetRocketPlayer()
		{
			return RocketPlayer.FromCSteamID (new CSteamID(Convert.ToUInt64(this.SteamID64)));
		}
	}
}

