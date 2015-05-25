using System.Linq;
using UnityEngine;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using Rocket.Unturned;
using Rocket.Unturned.Plugins;

namespace Blitz
{
	public class CommandAddSpawn : IRocketCommand
	{
		public bool RunFromConsole {
			get {
				return false;
			}
		}

		public string Name {
			get {
				return "addspawn";
			}
		}

		public string Usage {
			get {
				return "{0} [TeamName] [UnitName]";
			}
		}

		public string Help {
			get {
				return "Adds a spawn. Usage: /" + string.Format(Usage, Name);
			}
		}

		public void Execute(RocketPlayer caller, string[] command)
		{
			// If a team or unit was not specified, output a list of available options.
			if (command.Length < 2) {
				RocketChat.Say (caller, "You did not specify a team or unit. Usage: /" + string.Format(Usage, Name));
				RocketChat.Say (caller, "Teams are: " + Team.TeamList);
				RocketChat.Say (caller, "Units are: " + Unit.UnitList);
				return;
			}

			// Make sure a team exists with the specified name.
			// TODO extract into 'Team##FromString' method.
			Team team = (from Team t in Team.Teams
			             where t.Name.ToLower ().Contains (command [0].ToLower ())
			             select t).FirstOrDefault<Team> ();

			if (team == null) {
				RocketChat.Say (caller, "You did not specify a valid team.");
				RocketChat.Say (caller, "Teams are: " + Team.TeamList);
				return;
			}

			// Make sure a unit exists with the specified name.
			Unit unit = Unit.FromString(command[1]);

			if (unit == null) {
				RocketChat.Say (caller, "You did not specify a valid unit.");
				RocketChat.Say (caller, "Units are: " + Unit.UnitList);
				return;
			}

			Vector3 playerLoc = caller.Position;
			MatchManager.Instance.CurrentMatch.Spawns.Add (new Spawn (playerLoc.x, playerLoc.y, playerLoc.z, unit.Name, command[0]));
			RocketChat.Say (caller, string.Format("Successfully added a new spawn at {0} for {1} {2}.", playerLoc, team.Name, unit.Name));
			Blitz.Instance.Configuration.Save ();
		}
	}
}

