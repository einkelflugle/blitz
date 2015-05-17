using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using Rocket.Unturned;
using Rocket.Unturned.Plugins;


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
				RocketChat.Say (caller, "You did not specify a unit name. Usage: /" + string.Format (Usage, Name));
				return;
			}

			Unit u = Unit.FromString (command [0]);

			if (u == null) {
				RocketChat.Say (caller, "Failed to find unit '" + command [0] + "'.");
				return;
			}

			Blitz.Instance.Configuration.Units.Remove (u);
			Blitz.Instance.Configuration.Save ();
			RocketChat.Say (caller, "Successfully removed unit '" + command [0] + "'.");
		}
	}
}

