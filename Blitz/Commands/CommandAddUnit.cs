﻿using System;
using System.Collections.Generic;
using System.Linq;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using Rocket.Unturned;
using Rocket.Unturned.Plugins;

namespace Blitz
{
	public class CommandAddUnit : IRocketCommand
	{
		public bool RunFromConsole {
			get {
				return true;
			}
		}

		public string Name {
			get {
				return "addunit";
			}
		}

		public string Usage {
			get {
				return "{0} [UnitName] [ItemID] [ItemID] ...";
			}
		}

		public string Help {
			get {
				return "Adds a unit. Usage: /" + string.Format(Usage, Name);
			}
		}

		public void Execute(RocketPlayer caller, string[] command)
		{
			if (command.Length < 2) {
				RocketChat.Say (caller, "You did not specify a name or item IDs. Usage: /" + string.Format(Usage, Name));
				return;
			}

			Unit u = (from Unit unit in Blitz.Instance.Configuration.Units
			          where unit.Name.ToLower ().Equals (command [0])
			          select unit).FirstOrDefault<Unit> ();
			if (u != null) {
				RocketChat.Say (caller, "A unit already exists with that name.");
				return;
			}

			List<UnitItem> loadout = new List<UnitItem> ();

			for (int i = 1; i < command.Length; i++) {
				loadout.Add (new UnitItem (UInt16.Parse (command [i])));
			}

			Blitz.Instance.Configuration.Units.Add (new Unit (command [0], false, loadout));
			Blitz.Instance.Configuration.Save ();
			RocketChat.Say (caller, "Successfully added new unit '" + command [0] + "'.");
		}
	}
}

