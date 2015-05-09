using System;
using Rocket.RocketAPI;

namespace Blitz
{
	public class Blitz : RocketPlugin
	{
		public static Blitz Instance;

		protected override void Load()
		{
			Instance = this;
		}
	}
}

