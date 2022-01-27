using BookStoreModels.UserModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Interface
{
    public interface IAddressManager
    {
        Task<UserAddressModel> AddAddress(UserAddressModel address);
        Task<UserAddressModel> UpdateAddress(UserAddressModel address);
    }
}
