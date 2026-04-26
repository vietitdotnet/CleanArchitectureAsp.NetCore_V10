using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Entities.Owns
{
    public class AnonCustomer
    {
      private AnonCustomer() { }
      private AnonCustomer(string customerName, string phoneNumber, string? email = null)
        {
            CustomerName = customerName;
            PhoneNumber = phoneNumber;
            Email = email;
           
        }

        public string CustomerName { get; private set; } = null!;
        public string PhoneNumber { get; private set; } = null!;
        public string? Email { get; private set; }


        public static AnonCustomer Create(
         string customerName,
         string phoneNumber,
         string? email = null)
        {
            if (string.IsNullOrWhiteSpace(customerName))
                throw new ArgumentException("Tên khách hàng là bắt buộc");

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Số điện thoại là bắt buộc");

            // normalize
            customerName = customerName.Trim();
            phoneNumber = phoneNumber.Trim();
            email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();

            return new AnonCustomer(customerName, phoneNumber, email);
        }

    }


}
