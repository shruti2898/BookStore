using BookStoreModels.UserModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Interface
{
    public interface IAddressRepository
    {
        Task<UserAddressModel> AddAddress(UserAddressModel address);
        Task<UserAddressModel> UpdateAddress(UserAddressModel address);
    }
}
