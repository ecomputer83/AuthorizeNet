using System;

namespace AuthorizeNet
{
	/// <summary>
	/// The devise which read in the Credit Card
	/// </summary>
	public enum DeviceType
	{
		/// <summary>
		/// </summary>
		Unknown = 1,
		/// <summary>
		/// </summary>
		UnattendedTerminal = 2,
		/// <summary>
		/// </summary>
		SelfServiceTerminal = 3,
		/// <summary>
		/// </summary>
		ElectronicCashRegister = 4,
		/// <summary>
		/// </summary>
		PersonalComputerBasedTerminal = 5,
		/// <summary>
		/// </summary>
		AirPay = 6,
		/// <summary>
		/// </summary>
		WirelessPOS = 7,
		/// <summary>
		/// </summary>
		Website = 8,
		/// <summary>
		/// </summary>
		DialTerminal = 9,
		/// <summary>
		/// </summary>
		VirtualTerminal = 10
	}
}