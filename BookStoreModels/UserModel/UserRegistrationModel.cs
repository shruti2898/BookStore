using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreModels.UserModel
{
    public class UserRegistrationModel
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserMobile { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
    }
}
