using System;
using Rocket.Unturned.Plugins;
using SDG;
using Rocket.Unturned.Player;

namespace Blitz
{
	public class Blitz : RocketPlugin<BlitzConfiguration>
	{
		public static Blitz Instance { get; private set; }

		protected override void Load()
		{
			Instance = this;
			this.ReloadConfiguration ();

			new PlayerConnectListener ();
			new PlayerDeathListener ();
			new PlayerReviveListener ();

			new MatchManager ();
			new SpawnManager ();
		}
	}
}

