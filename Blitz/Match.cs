using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Blitz
{
	public class Match
	{
		[XmlAttribute ("name")]
		public string Name;

		public Lobby Lobby;
		public Objective Objective;

		[XmlArrayItem ("Spawn")]
		public List<Spawn> Spawns;

		[XmlArrayItem ("Unit")]
		public List<Unit> Units;

		public int MatchTime;
		public int Lives;

		public Match ()
		{
		}
	}
}

