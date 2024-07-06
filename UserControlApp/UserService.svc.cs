using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace UserControlApp
{
    public class UserService : IUserService
    {
        private readonly string _connectionString;

        public UserService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["UserControlDB"].ConnectionString;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            try
            {


                var users = new List<UserDTO>();
                using (var connection = GetConnection())
                using (var command = new SqlCommand("SELECT * FROM Users", connection))
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var user = new UserDTO
                            {
                                Id = Convert.ToUInt32(reader["Id"]),
                                FullName = reader["FullName"].ToString(),
                                DRFO = reader["DRFO"].ToString(),
                                Email = reader["Email"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                Created = Convert.ToDateTime(reader["Created"]),
                                LastUpdated = Convert.ToDateTime(reader["LastUpdated"])
                            };
                            users.Add(user);
                        }
                    }
                }
                return users;
            }
            catch (SqlException ex)
            {
                throw new Exception("An error occurred while selecting uses from the database.", ex);
            }
        }

        public async Task<int> AddUserAsync(UserDTO user)
        {
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand("INSERT INTO Users (FullName, DRFO, Email, PhoneNumber, Created, LastUpdated) VALUES (@FullName, @DRFO, @Email, @PhoneNumber, @Created, @LastUpdated)", connection))
                {
                    command.Parameters.AddWithValue("@FullName", user.FullName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DRFO", user.DRFO ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", user.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Created", DateTime.UtcNow);
                    command.Parameters.AddWithValue("@LastUpdated", DateTime.UtcNow);

                    await connection.OpenAsync();
                    return await command.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("An error occurred while adding the user to the database.", ex);
            }
        }

        public async Task<int> UpdateUserAsync(uint id, UserDTO user)
        {
            try
            {


                using (var connection = GetConnection())
                using (var command = new SqlCommand("UPDATE Users SET FullName=@FullName, DRFO=@DRFO, Email=@Email, PhoneNumber=@PhoneNumber, LastUpdated=@LastUpdated WHERE Id=@Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", (int)id);
                    command.Parameters.AddWithValue("@FullName", user.FullName);
                    command.Parameters.AddWithValue("@DRFO", user.DRFO);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                    command.Parameters.AddWithValue("@LastUpdated", DateTime.UtcNow);

                    await connection.OpenAsync();
                    return await command.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("An error occurred while updating the user in the database.", ex);
            }
        }

        public async Task<int> DeleteUserAsync(uint id)
        {
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand("DELETE FROM Users WHERE ID=@Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", (int)id);

                    await connection.OpenAsync();
                    return await command.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("An error occurred while deleting the user from the database.", ex);
            }
        }
    }
}
