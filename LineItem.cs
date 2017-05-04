using System;
using System.Runtime.CompilerServices;

namespace AuthorizeNet
{
	/// <summary>
	/// Representational class for a transactional line item
	/// </summary>
	public class LineItem
	{
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the ID.
		/// </summary>
		/// <value>The ID.</value>
		public string ID
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the quantity.
		/// </summary>
		/// <value>The quantity.</value>
		public decimal Quantity
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:AuthorizeNet.LineItem" /> is taxable.
		/// </summary>
		/// <value><c>true</c> if taxable; otherwise, <c>false</c>.</value>
		public bool Taxable
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the unit price.
		/// </summary>
		/// <value>The unit price.</value>
		public decimal UnitPrice
		{
			get;
			set;
		}

		public LineItem()
		{
		}
	}
}