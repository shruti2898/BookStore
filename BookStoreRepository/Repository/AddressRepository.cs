using BookStoreModels.UserModel;
using BookStoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly string connectionString;
        private IConfiguration Configuration;
        public AddressRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.connectionString = configuration.GetConnectionString("DbConnection");
        }
        public async Task<UserAddressModel> AddAddress(UserAddressModel address)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
            try
            {
                SqlCommand command = new SqlCommand("spAddAddress", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", address.UserId);
                command.Parameters.AddWithValue("@AddressType", address.AddressType);
                command.Parameters.AddWithValue("@FullAddress", address.FullAddress);
                command.Parameters.AddWithValue("@City", address.City);
                command.Parameters.AddWithValue("@State", address.State);
                command.Parameters.Add("@AddressId", SqlDbType.Int).Direction = ParameterDirection.Output;
                connection.Open();

                await command.ExecuteNonQueryAsync();

                var result = command.Parameters["@AddressId"].Value;
                if (result != DBNull.Value)
                {
                    address.AddressId = (int)result;
                    return address;
                }
                return null;
            }
            catch (ArgumentNullException exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<UserAddressModel> UpdateAddress(UserAddressModel address)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
            try
            {
                SqlCommand command = new SqlCommand("spUpdateAddress", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AddressId", address.AddressId);
                command.Parameters.AddWithValue("@UserId", address.UserId);
                command.Parameters.AddWithValue("@AddressType", address.AddressType);
                command.Parameters.AddWithValue("@FullAddress", address.FullAddress);
                command.Parameters.AddWithValue("@City", address.City);
                command.Parameters.AddWithValue("@State", address.State);
                connection.Open();
                var result = await command.ExecuteNonQueryAsync();
                return result == 1 ? address : null;
            }
            catch (ArgumentNullException exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
