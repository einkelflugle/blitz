using System;
using Rocket.Logging;
using Rocket.RocketAPI;
using Rocket.RocketAPI.Events;
using UnityEngine;
using System.Threading;

namespace Blitz
{
	public class PlayerReviveListener
	{
		private Timer timer;

		public PlayerReviveListener ()
		{
			RocketPlayerEvents.OnPlayerRevive += new RocketPlayerEvents.PlayerRevive (this.onPlayerRevive);
		}

		private void onPlayerRevive(RocketPlayer player, Vector3 position, byte angle)
		{
			timer = new Timer((TimerCallback) (obj =>
				{
					player.Teleport (SpawnManager.Instance.GetSpawnpoint (PlayerData.ForPlayer (player)), 0);
					Unit.GiveLoadout (PlayerData.ForPlayer (player));
					this.timer.Dispose();
					this.timer = (Timer) null;
				}), (object) null, 50, -1);
		}
	}
}

