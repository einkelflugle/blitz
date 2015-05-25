using System.Xml.Serialization;

namespace Blitz
{
	public class UnitItem
	{
		[XmlAttribute ("id")]
		public ushort ItemID;

		[XmlAttribute ("amount")]
		public byte Amount;

		[XmlAttribute ("team")]
		public string TeamName;

		protected UnitItem()
		{
		}

		public UnitItem(ushort itemId, byte amount = (byte)1, string teamName = null)
		{
			this.ItemID = itemId;
			this.Amount = (byte)amount;
			this.TeamName = teamName;
		}
	}
}

