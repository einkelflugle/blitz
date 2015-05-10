using System;
using Rocket.RocketAPI;
using System.Linq;
using UnityEngine;

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
				RocketChatManager.Say (caller, "You did not specify a team or unit. Usage: /" + string.Format(Usage, Name));
				RocketChatManager.Say (caller, "Teams are: " + Team.TeamList);
				RocketChatManager.Say (caller, "Units are: " + Unit.UnitList);
				return;
			}

			// Make sure a team exists with the specified name.
			// TODO extract into 'Team##FromString' method.
			Team team = (from Team t in Blitz.Instance.Configuration.Teams
			             where t.Name.ToLower ().Contains (command [0].ToLower ())
			             select t).FirstOrDefault<Team> ();

			if (team == null) {
				RocketChatManager.Say (caller, "You did not specify a valid team.");
				RocketChatManager.Say (caller, "Teams are: " + Team.TeamList);
				return;
			}

			// Make sure a unit exists with the specified name.
			Unit unit = Unit.FromString(command[1]);

			if (unit == null) {
				RocketChatManager.Say (caller, "You did not specify a valid unit.");
				RocketChatManager.Say (caller, "Units are: " + Unit.UnitList);
				return;
			}

			Vector3 playerLoc = caller.Position;
			team.Spawns.Add (new Spawn (playerLoc.x, playerLoc.y, playerLoc.z, unit.Name));
			RocketChatManager.Say (caller, string.Format("Successfully added a new spawn at {0} for {1} {2}.", playerLoc, team.Name, unit.Name));
			Blitz.Instance.Configuration.Save ();
		}
	}
}

