using System;

namespace Blitz
{
	public class CapturePointObjective : Objective
	{
		public CapturePoint CapturePoint;

		public override int MatchTime {
			get {
				return this.CapturePoint.MinimumTime;
			}
			set {
				this.CapturePoint.MinimumTime = value;
			}
		}

		public CapturePointObjective ()
		{
			
		}
	}
}

