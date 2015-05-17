using System.Xml.Serialization;

namespace Blitz
{
	public class UnitItem
	{
		[XmlAttribute ("id")]
		public ushort ItemID;

		[XmlAttribute ("amount")]
		public byte Amount;

		protected UnitItem()
		{
		}

		public UnitItem(ushort itemId, byte amount = (byte)1)
		{
			this.ItemID = itemId;
			this.Amount = (byte)amount;
		}
	}
}

