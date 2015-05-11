using System;
using Rocket.RocketAPI;

namespace Blitz
{
	public class CommandRemoveUnit : IRocketCommand
	{
		public bool RunFromConsole {
			get {
				return true;
			}
		}

		public string Name {
			get {
				return "removeunit";
			}
		}

		public string Usage {
			get {
				return "{0} [UnitName]";
			}
		}

		public string Help {
			get {
				return "Removes a unit. Usage: /" + string.Format(Usage, Name);
			}
		}

		public void Execute(RocketPlayer caller, string[] command)
		{
			if (command.Length < 1) {
				RocketChatManager.Say (caller, "You did not specify a unit name. Usage: /" + string.Format (Usage, Name));
				return;
			}

			Unit u = Unit.FromString (command [0]);

			if (u == null) {
				RocketChatManager.Say (caller, "Failed to find unit '" + command [0] + "'.");
				return;
			}

			Blitz.Instance.Configuration.Units.Remove (u);
			Blitz.Instance.Configuration.Save ();
			RocketChatManager.Say (caller, "Successfully removed unit '" + command [0] + "'.");
		}
	}
}

