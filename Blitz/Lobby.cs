using System;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

namespace Blitz
{
	public class Lobby
	{
		[XmlAttribute ("x")]
		public float x;

		[XmlAttribute ("y")]
		public float y;

		[XmlAttribute ("z")]
		public float z;

		public Lobby()
		{
		}

		public Lobby(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Vector3 GetLocation()
		{
			return new Vector3 (x, y, z);
		}
	}
}

