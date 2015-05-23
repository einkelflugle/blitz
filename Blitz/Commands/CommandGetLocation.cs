using System;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using Rocket.Unturned;

namespace Blitz
{
	public class CommandGetLocation : IRocketCommand
	{
		public bool RunFromConsole {
			get {
				return false;
			}
		}

		public string Name {
			get {
				return "loc";
			}
		}

		public string Usage {
			get {
				return "{0}";
			}
		}

		public string Help {
			get {
				return "Returns your current location on the map. Usage: /" + string.Format (Usage, Name);
			}
		}

		public void Execute(RocketPlayer caller, string[] args) {
			RocketChat.Say (caller, string.Format("x = {0}, y = {1}, z = {2}", caller.Position.x, caller.Position.y, caller.Position.z));
		}
	}
}

