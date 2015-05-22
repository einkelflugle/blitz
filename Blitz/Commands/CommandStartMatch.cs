using System;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using Rocket.Unturned;

namespace Blitz
{
	public class CommandStartMatch : IRocketCommand
	{
		public bool RunFromConsole {
			get {
				return true;
			}
		}

		public string Name {
			get {
				return "start";
			}
		}

		public string Usage {
			get {
				return "{0}";
			}
		}

		public string Help {
			get {
				return "Starts the match currently in lobby. Usage: /" + string.Format (Usage, Name);
			}
		}

		public void Execute(RocketPlayer caller, string[] args) {
			MatchManager.Instance.StartCountdown ();
			RocketChat.Say (caller, "Successfully started the countdown.");
		}
	}
}

