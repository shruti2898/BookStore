using BookStoreManager.Interface;
using BookStoreModels.UserModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository repository;
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UserRegistrationModel> Register(UserRegistrationModel user)
        {
            try
            {
                return await this.repository.Register(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> Login(UserCredentialsModel user)
        {
            try
            {
                return await this.repository.Login(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ForgotPassword(string email)
        {
            try
            {
                return await this.repository.ForgotPassword(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ResetPassword(UserCredentialsModel user)
        {
            try
            {
                return await this.repository.ResetPassword(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
