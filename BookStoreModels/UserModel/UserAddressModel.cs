using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreModels.UserModel
{
	public class UserAddressModel
	{
		[Key]
		public int AddressId { get; set; }
		public int UserId { get; set; }
		public string AddressType { get; set; }
		public string FullAddress { get; set; }
		public string City { get; set; }
		public string State { get; set; }
	}
}
