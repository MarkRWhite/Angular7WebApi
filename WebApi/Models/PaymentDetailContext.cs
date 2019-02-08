namespace WebApi.Models
{
	using Microsoft.EntityFrameworkCore;

	public class PaymentDetailContext : DbContext
	{
		#region Constructors

		public PaymentDetailContext (DbContextOptions<PaymentDetailContext> options)
			: base(options)
		{

		}

		#endregion

		#region Models

		public DbSet<PaymentDetail> PaymentDetails { get; set; }

		#endregion
	}
}