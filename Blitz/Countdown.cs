using System;
using System.Collections.Generic;
using System.Threading;
using Rocket.Unturned;
using UnityEngine;

namespace Blitz
{
	public class Countdown
	{
		private Timer Timer;
		private Action OnFinish;
		private int Ticks;
		private string Message;
		private List<int> NumbersToPrint;

		public Countdown (int start, string message, List<int> numbersToPrint, Action onFinish)
		{
			this.OnFinish = onFinish;
			TimerCallback tcb = this.Tick;
			this.Timer = new Timer (tcb, null, 0, 1000);
			this.Ticks = start;
			this.Message = message;
			this.NumbersToPrint = numbersToPrint;
		}

		private void Tick(System.Object stateInfo)
		{
			// If the time has run out, stop the timer
			if (Ticks == 0) {
				Timer.Change (Timeout.Infinite, Timeout.Infinite);
				this.OnFinish.Invoke ();
			}

			if (NumbersToPrint.Contains(Ticks)) {
				RocketChat.Say (string.Format (Message, Ticks), Color.cyan);
			}

			Ticks--;
		}
	}
}

