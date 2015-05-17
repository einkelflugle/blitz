using System.Linq;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using Rocket.Unturned;
using Rocket.Unturned.Plugins;

namespace Blitz
{
	public class CommandUnit : IRocketCommand
	{
		public bool RunFromConsole {
			get {
				return false;
			}
		}

		public string Name {
			get {
				return "unit";
			}
		}

		public string Usage {
			get {
				return "{0} [UnitName]";
			}
		}

		public string Help {
			get {
				return "Changes your unit. Usage: /" + string.Format(Usage, Name);
			}
		}

		public void Execute(RocketPlayer caller, string[] command)
		{
			// If a unit was not specified, output a list of available units.
			if (command.Length == 0) {
				RocketChat.Say (caller, "You did not specify a unit.");
				RocketChat.Say (caller, "Units are: " + Unit.UnitList);
				return;
			}

			// If a unit name was entered, make sure it is valid.
			Unit unit = Unit.FromString (command [0], false);
			if (unit == null) {
				RocketChat.Say (caller, "Class not found.");
				return;
			}

			// Check the user's current unit.
			if (PlayerData.ForPlayer(caller).Unit.Equals(unit.Name)) {
				RocketChat.Say (caller, "You are already " + unit.Name);
				return;
			} else {
				RocketChat.Say (caller, "You have changed unit.");
				caller.Inventory.Clear ();
			}

			// Add them to their new unit.
			Team t = Team.ForPlayer(PlayerData.ForPlayer(caller));
			PlayerData pd = (from PlayerData d in t.Players
			                 where d.SteamID64.Equals (caller.CSteamID.ToString ())
			                 select d).FirstOrDefault<PlayerData> ();

			pd.Unit = unit.Name;
			Blitz.Instance.Configuration.Save ();

			// Give them their items.
			if (!Unit.GiveLoadout(PlayerData.ForPlayer(caller))) {
				RocketChat.Say (caller, "Failed to find items in loadout.");
			}
		}
	}
}

