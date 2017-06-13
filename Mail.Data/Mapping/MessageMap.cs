using Mail.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Mail.Data.Mapping
{
	public class MessageMap : EntityTypeConfiguration<Message>
	{
		public MessageMap()
		{
			this.ToTable("Messages");

			this.HasKey(m => m.Id);

			this.HasRequired(m => m.ReceiverUser)
				.WithMany()
				.HasForeignKey(m => m.ReceiverUserId);

			this.HasRequired(m => m.SenderUser)
				.WithMany()
				.HasForeignKey(m => m.SenderUserId);
		}
	}
}
