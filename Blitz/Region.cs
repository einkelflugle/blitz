using System;
using System.Xml;
using System.Xml.Serialization;

namespace Blitz
{
	[XmlInclude (typeof(CapturePoint))]

	public class Region
	{
		[XmlAttribute ("x")]
		public float x;
		[XmlAttribute ("y")]
		public float y;
		[XmlAttribute ("z")]
		public float z;

		public Region()
		{
		}

		public Region(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
	}
}

