using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Application.DataTransfer
{
    public class GetUserDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime DateRegistered { get; set; }

        public RoleDto Role { get; set; }

        public IEnumerable<ShippingAddressDto> ShippingAddresses { get; set; }

        public IEnumerable<GetOrderDto> Orders { get; set; }
    }
}
