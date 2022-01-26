using BookStoreModels.UserModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Interface
{
    public interface IUserManager
    {
        Task<UserRegistrationModel> Register(UserRegistrationModel user);
        Task<string> Login(UserCredentialsModel user);
        Task<bool> ForgotPassword(string email);
        Task<bool> ResetPassword(UserCredentialsModel user);
    }
}
