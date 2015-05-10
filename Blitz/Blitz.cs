using System;
using Rocket.RocketAPI;
using SDG;

namespace Blitz
{
	public class Blitz : RocketPlugin<BlitzConfiguration>
	{
		public static Blitz Instance { get; private set; }

		protected override void Load()
		{
			Instance = this;
			this.ReloadConfiguration ();

			new PlayerConnectListener ();
			new PlayerDeathListener ();
			new PlayerReviveListener ();

			new SpawnManager ();
		}


		/// <summary>
		/// Overrides the default SDG method 'ItemTool##tryForceGiveItem' with support for full durability.
		/// </summary>
		public static bool tryForceGiveItem (RocketPlayer player, ushort id, byte amount, bool fullDurability = true)
		{
			ItemAsset itemAsset = (ItemAsset)Assets.find (EAssetType.Item, id);
			if (itemAsset != null && !itemAsset.g) {
				for (int i = 0; i < (int)amount; i++) {
					Item l = new Item (id, fullDurability);
					player.Inventory.forceAddItem (l, true);
				}
				return true;
			}
			return false;
		}
	}
}

