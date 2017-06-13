using System;

namespace Mail.Data.Entities
{
	public class Message
	{
		public int Id { get; set; }

		public string Text { get; set; }

		public int SenderUserId { get; set; }

		public int ReceiverUserId { get; set; }

		public DateTime CreationTimeUTC { get; set; }

		public virtual User SenderUser { get; set; }

		public virtual User ReceiverUser { get; set; }
	}
}
