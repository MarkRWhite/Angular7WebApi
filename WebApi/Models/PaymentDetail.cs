using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class PaymentDetail
	{
		[Key]
		public int PmId { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(100)")]
		public string CardOwnerName { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(16)")]
		public string CardNumber { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(5)")]
		public string ExpirationDate { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(3)")]
		public string Cvv { get; set; }
	}
}
