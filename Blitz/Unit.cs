using System;
using System.Collections.Generic;
using System.Linq;

namespace Blitz
{
	public class Unit
	{
		public string Name;
		public bool Default;
		public List<UnitItem> Loadout;

		public static string UnitList {
			get {
				string list = "";
				List<Unit> units = Blitz.Instance.Configuration.Units;

				for (int i = 0; i < units.Count; i++) {
					list += units [i].Name;
					if (i < units.Count - 1) {
						list += ", ";
					}
				}

				return list;
			}
		}

		public static Unit DefaultUnit {
			get {
				Unit unit = (from Unit u in Blitz.Instance.Configuration.Units
					where u.Default
					select u).FirstOrDefault<Unit> ();

				return unit;
			}
		}

		public Unit()
		{
		}

		public Unit(string name, bool isDefault, List<UnitItem> loadout)
		{
			this.Name = name;
			this.Default = isDefault;

			List<UnitItem> flattenedLoadout = new List<UnitItem> ();
			UnitItem current = null;
			int count = 0;
			foreach (UnitItem i in loadout) {
				if (current == null || !i.ItemID.Equals(current.ItemID)) {
					if (count > 0) {
						flattenedLoadout.Add (new UnitItem (current.ItemID, Convert.ToByte(count)));
					}
					current = i;
					count = 1;
				} else {
					count++;
				}
			}
			if (count > 0) {
				flattenedLoadout.Add (new UnitItem (current.ItemID, Convert.ToByte(count)));
			}
			this.Loadout = flattenedLoadout;
		}

		public static Unit FromString(string name, bool strict = true)
		{
			// Try to find a unit with the specified name
			Unit unit;
			if (strict) {
				unit = (from Unit u in Blitz.Instance.Configuration.Units
					where u.Name.ToLower ().Equals (name.ToLower ())
					select u).FirstOrDefault<Unit> ();
			} else {
				unit = (from Unit u in Blitz.Instance.Configuration.Units
					where u.Name.ToLower ().Contains(name.ToLower ())
					select u).FirstOrDefault<Unit> ();
			}


			// If there is no unit with the specified name, use the default one.
			if (unit == null) {
				unit = DefaultUnit;
			}

			return unit;
		}

		public static bool GiveLoadout(PlayerData data)
		{
			bool success = true;
			foreach (UnitItem i in Unit.FromString(data.Unit).Loadout) {
				if (!Blitz.tryForceGiveItem(data.GetRocketPlayer(), i.ItemID, i.Amount)) {
					success = false;
				}
			}
			return success;
		}
	}
}

