namespace WebApi.Controllers
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;

	using WebApi.Models;

	[Route( "api/[controller]" )]
	[ApiController]
	public class PaymentDetailsController : ControllerBase
	{
		#region Fields

		private readonly PaymentDetailContext _context;

		#endregion

		#region Constructors

		public PaymentDetailsController ( PaymentDetailContext context )
		{
			this._context = context;
		}

		#endregion

		#region API Methods

		// GET: api/PaymentDetails
		[HttpGet]
		public IEnumerable<PaymentDetail> GetPaymentDetails ()
		{
			return this._context.PaymentDetails;
		}

		// GET: api/PaymentDetails/5
		[HttpGet( "{id}" )]
		public async Task<IActionResult> GetPaymentDetail ( [FromRoute] int id )
		{
			if ( !this.ModelState.IsValid )
			{
				return this.BadRequest( this.ModelState );
			}

			var paymentDetail = await this._context.PaymentDetails.FindAsync( id );

			if ( paymentDetail == null )
			{
				return this.NotFound();
			}

			return this.Ok( paymentDetail );
		}

		// PUT: api/PaymentDetails/5
		[HttpPut( "{id}" )]
		public async Task<IActionResult> PutPaymentDetail ( [FromRoute] int id, [FromBody] PaymentDetail paymentDetail )
		{
			if ( !this.ModelState.IsValid )
			{
				return this.BadRequest( this.ModelState );
			}

			if ( id != paymentDetail.PmId )
			{
				return this.BadRequest();
			}

			this._context.Entry( paymentDetail ).State = EntityState.Modified;

			try
			{
				await this._context.SaveChangesAsync();
			}
			catch ( DbUpdateConcurrencyException )
			{
				if ( !this.PaymentDetailExists( id ) )
				{
					return this.NotFound();
				}

				throw;
			}

			return this.NoContent();
		}

		// POST: api/PaymentDetails
		[HttpPost]
		public async Task<IActionResult> PostPaymentDetail ( [FromBody] PaymentDetail paymentDetail )
		{
			if ( !this.ModelState.IsValid )
			{
				return this.BadRequest( this.ModelState );
			}

			this._context.PaymentDetails.Add( paymentDetail );
			await this._context.SaveChangesAsync();

			return this.CreatedAtAction( "GetPaymentDetail", new { id = paymentDetail.PmId }, paymentDetail );
		}

		// DELETE: api/PaymentDetails/5
		[HttpDelete( "{id}" )]
		public async Task<IActionResult> DeletePaymentDetail ( [FromRoute] int id )
		{
			if ( !this.ModelState.IsValid )
			{
				return this.BadRequest( this.ModelState );
			}

			var paymentDetail = await this._context.PaymentDetails.FindAsync( id );
			if ( paymentDetail == null )
			{
				return this.NotFound();
			}

			this._context.PaymentDetails.Remove( paymentDetail );
			await this._context.SaveChangesAsync();

			return this.Ok( paymentDetail );
		}

		#endregion

		#region Private Methods

		private bool PaymentDetailExists ( int id )
		{
			return this._context.PaymentDetails.Any( e => e.PmId == id );
		}

		#endregion
	}
}