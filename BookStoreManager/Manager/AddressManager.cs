using BookStoreManager.Interface;
using BookStoreModels.UserModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Manager
{
    public class AddressManager : IAddressManager
    {
        private IAddressRepository repository;
        public AddressManager(IAddressRepository repository)
        {
            this.repository = repository;
        }
        public async Task<UserAddressModel> AddAddress(UserAddressModel address)
        {
            try
            {
                return await this.repository.AddAddress(address);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteAddress(int addressId)
        {
            try
            {
                return await this.repository.DeleteAddress(addressId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserAddressModel> UpdateAddress(UserAddressModel address)
        {
            try
            {
                return await this.repository.UpdateAddress(address);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
