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
				return "Starts the match queue.";
			}
		}

		public void Execute(RocketPlayer caller, string[] args) {
			if (MatchManager.Instance.State != MatchManager.MatchState.DISABLED) {
				RocketChat.Say (caller, "Match already running.");
				return;
			}
			MatchManager.Instance.StartCountdown ();
			RocketChat.Say (caller, "Successfully started the countdown.");
		}
	}
}

