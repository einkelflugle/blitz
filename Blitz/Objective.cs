using System;
using System.Xml;
using System.Xml.Serialization;

namespace Blitz
{
	public class Objective
	{
		[XmlAttribute ("type")]
		public string Type;
		public Region Region;

		public Objective()
		{
			
		}
	}
}

