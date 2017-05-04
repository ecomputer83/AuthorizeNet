using System;

namespace AuthorizeNet
{
	public interface ICustomerGateway
	{
		string AddCreditCard(string profileID, string cardNumber, int expirationMonth, int expirationYear, string cardCode, Address billToAddress);

		string AddCreditCard(string profileID, string cardNumber, int expirationMonth, int expirationYear, string cardCode);

		string AddShippingAddress(string profileID, Address address);

		string AddShippingAddress(string profileID, string first, string last, string street, string city, string state, string zip, string country, string phone);

		IGatewayResponse Authorize(string profileID, string paymentProfileID, decimal amount, decimal tax, decimal shipping);

		IGatewayResponse Authorize(string profileID, string paymentProfileID, decimal amount);

		IGatewayResponse Authorize(Order order);

		IGatewayResponse AuthorizeAndCapture(string profileID, string paymentProfileID, decimal amount, decimal tax, decimal shipping);

		IGatewayResponse AuthorizeAndCapture(string profileID, string paymentProfileID, decimal amount);

		IGatewayResponse AuthorizeAndCapture(Order order);

		IGatewayResponse Capture(string profileID, string paymentProfileId, string cardCode, decimal amount, string approvalCode);

		Customer CreateCustomer(string email, string description);

		bool DeleteCustomer(string profileID);

		bool DeletePaymentProfile(string profileID, string paymentProfileID);

		bool DeleteShippingAddress(string profileID, string shippingAddressID);

		Customer GetCustomer(string profileID);

		string[] GetCustomerIDs();

		Address GetShippingAddress(string profileID, string shippingAddressID);

		IGatewayResponse Refund(string profileID, string paymentProfileId, string approvalCode, string transactionId, decimal amount);

		bool UpdateCustomer(Customer customer);

		bool UpdatePaymentProfile(string profileID, PaymentProfile profile);

		bool UpdateShippingAddress(string profileID, Address address);

		string ValidateProfile(string profileID, string paymentProfileID, string shippingAddressID, ValidationMode mode);

		string ValidateProfile(string profileID, string paymentProfileID, ValidationMode mode);

		IGatewayResponse Void(string profileID, string paymentProfileId, string approvalCode, string transactionId);
	}
}