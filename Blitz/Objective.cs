using System;
using System.Xml;
using System.Xml.Serialization;

namespace Blitz
{
	[XmlInclude (typeof(CapturePointObjective))]
	[XmlInclude (typeof(DeathmatchObjective))]

	public abstract class Objective
	{
		public abstract int MatchTime { get; set; }
	}
}

