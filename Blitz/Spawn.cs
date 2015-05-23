using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Blitz
{
	public class Spawn
	{
		[XmlAttribute ("x")]
		public float x;

		[XmlAttribute ("y")]
		public float y;

		[XmlAttribute ("z")]
		public float z;

		[XmlAttribute ("unit")]
		public string UnitName;

		[XmlAttribute ("team")]
		public string TeamName;

		protected Spawn()
		{
		}

		public Spawn(float x, float y, float z, string unitName, string teamName)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.UnitName = unitName;
			this.TeamName = teamName;
		}
	}
}

