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

		public string Syntax {
			get {
				return string.Empty;
			}
		}

		public string Help {
			get {
				return "Starts the match currently in lobby.";
			}
		}

		public void Execute(RocketPlayer caller, string[] args) {
			MatchManager.Instance.StartCountdown ();
			RocketChat.Say (caller, "Successfully started the countdown.");
		}
	}
}

