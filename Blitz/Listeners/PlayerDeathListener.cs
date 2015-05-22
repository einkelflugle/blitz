using SDG;
using Steamworks;
using Rocket.Unturned.Player;
using Rocket.Unturned.Events;
using Rocket.Unturned;
using System.Collections.Generic;

namespace Blitz
{
	public class PlayerDeathListener
	{
		public PlayerDeathListener ()
		{
			RocketPlayerEvents.OnPlayerDeath += new RocketPlayerEvents.PlayerDeath (this.onPlayerDeath);
		}

		private void onPlayerDeath(RocketPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer)
		{
			player.Inventory.Clear();
//			List<Item> itemsToRemove = new List<Item> (player.Inventory.Items);
//			foreach (UnitItem i in Unit.FromString(PlayerData.ForPlayer(player).Unit).Loadout) {
//				Item item = new Item (i.ItemID, true);
//				if (itemsToRemove.Contains(item)) {
//					itemsToRemove.Remove(item);
//				}
//			}
//			foreach (Item item in itemsToRemove) {
//				player.Inventory.removeItem ((byte)item.ItemID, item.Amount);
//			}
		}
	}
}

