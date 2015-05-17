using System;
using Rocket.Unturned;
using SDG;
using System.Timers;
using UnityEngine;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;

namespace Blitz
{
	public class CommandBombard : IRocketCommand
	{

		public bool RunFromConsole {
			get {
				return false;
			}
		}

		public string Name {
			get {
				return "bombard";
			}
		}

		public string Help {
			get {
				return "Unleashes a deadly barrage of exploding shells on the enemy. Usage: /" + Name;
			}
		}

		public void Execute(RocketPlayer caller, string[] command)
		{
			System.Random rand = new System.Random ();
			int bombsCount = 0;
			Timer tmr = new Timer (500 * rand.Next(1, 6) + 200);
			Vector3 initialPosition = caller.Position;

			tmr.Elapsed += delegate {
				tmr.Interval = 500 * rand.Next(1, 6) + 200;
				bombsCount++;
				if (bombsCount > 10) {
					tmr.Stop ();
				}
				Vector3 explosionPosition = new Vector3 (
					initialPosition.x + rand.Next (-25, 25),
					initialPosition.y + rand.Next(2, 10),
					initialPosition.z + rand.Next (-25, 25)
				);
				// The below code is taken from the SDG method 'Grenade#Explode'.
				DamageTool.explode (explosionPosition, 10, EDeathCause.GRENADE, 200, 200, 200, 200, 200, 1000);
				EffectManager.sendEffect (34, EffectManager.N, explosionPosition);
				RocketChat.Say ("Bomb");
			};
			tmr.Start ();
			RocketChat.Say (caller, "You are being bombarded by the enemy!");
		}

	}
}

