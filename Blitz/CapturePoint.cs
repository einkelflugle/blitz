using System;
using System.Xml;
using System.Xml.Serialization;

namespace Blitz
{
	public class CapturePoint : Region
	{
		[XmlAttribute ("radius")]
		public float Radius;

		[XmlAttribute ("minimumPlayers")]
		public int MinimumPlayers;

		[XmlAttribute ("minimumTime")]
		public float MimimumTime;

		public CapturePoint () : base(0, 0, 0)
		{
		}
	}
}

