using BookStoreModels.UserModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Interface
{
    public interface IUserRepository
    {
       Task<UserRegistrationModel> Register(UserRegistrationModel user);
       Task<string> Login(UserCredentialsModel user);
       Task<bool> ForgotPassword(string email);
       Task<bool> ResetPassword(UserCredentialsModel user);
    }
}
