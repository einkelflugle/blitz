using System;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using UnityEngine;
using Rocket.Unturned;
using Rocket.Unturned.Plugins;

namespace Blitz
{
	public class CommandSetLobby : IRocketCommand
	{
		public bool RunFromConsole {
			get {
				return false;
			}
		}

		public string Name {
			get {
				return "setlobby";
			}
		}

		public string Usage {
			get {
				return "{0}";
			}
		}

		public string Help {
			get {
				return "Sets the lobby. Usage: /" + string.Format (Usage, Name);
			}
		}

		public void Execute (RocketPlayer caller, string[] command)
		{
			Vector3 playerLoc = caller.Position;
			MatchManager.Instance.CurrentMatch.Lobby.x = playerLoc.x;
			MatchManager.Instance.CurrentMatch.Lobby.y = playerLoc.y;
			MatchManager.Instance.CurrentMatch.Lobby.z = playerLoc.z;

			Blitz.Instance.Configuration.Save ();

			RocketChat.Say (caller, string.Format("Successfully set the lobby at {0}.", playerLoc));
		}
	}
}

