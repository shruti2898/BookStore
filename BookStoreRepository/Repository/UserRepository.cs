using BookStoreModels.UserModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using Experimental.System.Messaging;

namespace BookStoreRepository.Repository
{
    public class UserRepository : IUserRepository
    { 
        private readonly string connectionString;
        private readonly string Secret;
        private IConfiguration Configuration;
        public UserRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.connectionString = configuration.GetConnectionString("DbConnection");
            this.Secret = configuration.GetValue<string>("SecretJWT");
        }

        public async Task<UserRegistrationModel> Register(UserRegistrationModel userDetails)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
            try
            {
                userDetails.UserPassword = PasswordEncryption(userDetails.UserPassword);
                SqlCommand command = new SqlCommand("spRegisterUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserName", userDetails.UserName);
                command.Parameters.AddWithValue("@UserMobile", userDetails.UserMobile);
                command.Parameters.AddWithValue("@UserEmail", userDetails.UserEmail);
                command.Parameters.AddWithValue("@UserPassword", userDetails.UserPassword);
                command.Parameters.Add("@UserId", SqlDbType.Int).Direction = ParameterDirection.Output;
                connection.Open();

                await command.ExecuteNonQueryAsync();

                var result = command.Parameters["@UserId"].Value;
                if (result != DBNull.Value)
                {
                    userDetails.UserId = (int)result;
                    return userDetails;
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

        public async Task<string> Login(UserCredentialsModel user)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
            try
            {
                user.UserPassword = PasswordEncryption(user.UserPassword);
                SqlCommand command = new SqlCommand("spLoginUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserEmail", user.UserEmail);
                command.Parameters.AddWithValue("@UserPassword", user.UserPassword);
                connection.Open();
              
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    int id = Convert.ToInt32(reader["UserId"]);
                    return GenerateJwtToken(user.UserEmail,id);
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

        public string PasswordEncryption(string password)
        {
            try
            {
                byte[] encryptData = new byte[password.Length];
                encryptData = Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encryptData);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode " + ex.Message);
            }
        }

        public string GenerateJwtToken(string email, int userId)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.Secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Email, email),
                new Claim("UserId", userId.ToString())
            }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        public async Task<bool> ForgotPassword(string email)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
            try
            { 
                SqlCommand command = new SqlCommand("spForgotPassword", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserEmail", email);
                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    string fullname = reader["UserName"].ToString();
                    string smtpEmail = this.Configuration.GetValue<string>("Smtp:SmtpUsername");
                    string smtpPassword = this.Configuration.GetValue<string>("Smtp:SmtpPassword");
                    MailMessage sendEmail = new MailMessage();

                    SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                    sendEmail.From = new MailAddress(smtpEmail);
                    sendEmail.To.Add(email);
                    sendEmail.Subject = "Reset your BookStore account password";
                    this.SendMSMQ(fullname);
                    sendEmail.Body = this.ReceiveMSMQ();
                    smtpServer.Port = 587;
                    smtpServer.Credentials = new System.Net.NetworkCredential(smtpEmail, smtpPassword);
                    smtpServer.EnableSsl = true;

                    await smtpServer.SendMailAsync(sendEmail);
                    return true;
                }
                return false;
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
        public void SendMSMQ(string fullname)
        {
            MessageQueue messageQueue;
            if (MessageQueue.Exists(@".\Private$\BookStore"))
            {
                messageQueue = new MessageQueue(@".\Private$\BookStore");
            }
            else
            {
                messageQueue = MessageQueue.Create(@".\Private$\BookStore");
            }

            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            string body = $"Hello {fullname},\n " +
                          $"A password reset for your BookStore account was requested.Please click the link below to change your password.\n" +
                          $"http://localhost:4200/resetPassword";
            messageQueue.Label = "Mail Body";
            messageQueue.Send(body);
        }

        public string ReceiveMSMQ()
        {
            MessageQueue messageQueue = new MessageQueue(@".\Private$\BookStore");
            var receiveMessage = messageQueue.Receive();
            receiveMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            return receiveMessage.Body.ToString();
        }

        public async Task<bool> ResetPassword(UserCredentialsModel user)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
            try
            {
                user.UserPassword = PasswordEncryption(user.UserPassword);
                SqlCommand command = new SqlCommand("spResetPassword", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserEmail", user.UserEmail);
                command.Parameters.AddWithValue("@UserPassword", user.UserPassword);
                connection.Open();
                var result = await command.ExecuteNonQueryAsync();
                if(result == 1)
                {
                    return true;
                }
                return false;
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
