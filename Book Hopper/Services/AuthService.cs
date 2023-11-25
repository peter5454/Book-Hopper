using System;
using System.Security;
using Npgsql;
using System.Windows;
using Book_Hopper.View;
using Book_Hopper.Model;
using NpgsqlTypes;

namespace Book_Hopper.Services
{
    public class AuthService
    {
        private static readonly string ConnectionString = "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=postgres;Pooling=true;Minimum Pool Size=0;Maximum Pool Size=100; Connection Lifetime=0;";

        public static UserModel AuthenticateUser(string username, SecureString password)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand("SELECT * FROM users WHERE UserName = @Username", connection))
                {
                    cmd.Parameters.AddWithValue("Username", username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Get the hashed password from the database
                            string storedPassword = reader["Password"].ToString();

                            // Convert SecureString to plain string
                            string enteredPassword = ConvertToUnsecureString(password);

                            // Verify the password
                            if (enteredPassword == storedPassword)
                            {
                                // Authentication successful, return user model
                                return new UserModel
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Username = username,
                                    Password = storedPassword,
                                    Email = reader["Email"].ToString(),
                                    Fname = reader["FName"].ToString(),
                                    LName = reader["LName"].ToString()
                                };
                            }
                        }
                    }
                }
            }

            return null; // No matching user or password incorrect
        }

        public static bool RegisterUser(string username, string email, string fName, string lName, string password)
        {
            // Check if input parameters are valid
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(fName) || string.IsNullOrWhiteSpace(lName) ||
                password == null || password.Length < 3)
            {
                return false; // Invalid input
            }

            if (username.Length < 3 || !IsValidEmail(email))
            {
                return false; // Invalid username or email
            }

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                // Check if the user already exists
                if (UserExists(username))
                {
                    return false; // User already exists
                }

                using (var cmd = new NpgsqlCommand("INSERT INTO users (Email, Password, UserName, FName, LName) VALUES (@Email, @Password, @Username, @FName, @LName)", connection))
                {
                    cmd.Parameters.AddWithValue("Email", email);
                    cmd.Parameters.AddWithValue("Password", password); // Convert SecureString to plain string
                    cmd.Parameters.AddWithValue("Username", username);
                    cmd.Parameters.AddWithValue("FName", fName);
                    cmd.Parameters.AddWithValue("LName", lName);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            MessageBox.Show("Registration successful! Redirecting to success page...", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            RedirectLoginPage(Application.Current.MainWindow);
                        });

                    }

                    return rowsAffected > 0;
                }
            }
        }

        public static bool UserExists(string username)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM users WHERE Username = @Username", connection))
                {
                    cmd.Parameters.AddWithValue("Username", username);

                    int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                    return userCount > 0;
                }
            }
        }
        public static bool EmailExists(string email)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM users WHERE Email = @Email", connection))
                {
                    cmd.Parameters.AddWithValue("Email", email);

                    int emailCount = Convert.ToInt32(cmd.ExecuteScalar());

                    return emailCount > 0;
                }
            }
        }

        private static string ConvertToUnsecureString(SecureString securePassword)
        {
            IntPtr unmanagedString = IntPtr.Zero;

            try
            {
                unmanagedString = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return System.Runtime.InteropServices.Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static void RedirectLoginPage(Window currentWindow)
        {
            // Create an instance of LoginView
            LoginView loginView = new LoginView();

            // Show LoginView
            loginView.Show();
            currentWindow?.Close();
        }
        public static bool IsUsernameUnique(string username, string currentUsername)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM users WHERE Username = @Username AND Username != @CurrentUsername", connection))
                {
                    cmd.Parameters.AddWithValue("Username", username);
                    cmd.Parameters.AddWithValue("CurrentUsername", currentUsername);

                    int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                    return userCount == 0;
                }
            }
        }

        public static bool IsEmailUnique(string email, string currentUsername)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM users WHERE Email = @Email AND Username != @CurrentUsername", connection))
                {
                    cmd.Parameters.AddWithValue("Email", email);
                    cmd.Parameters.AddWithValue("CurrentUsername", currentUsername);

                    int emailCount = Convert.ToInt32(cmd.ExecuteScalar());

                    return emailCount == 0;
                }
            }
        }



        public static bool UpdateUserInformation(UserModel user, string newPassword, int id)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE users SET Email = @Email, FName = @FName, LName = @LName, UserName = @UserName";

                // Add password update only if a new password is provided
                if (!string.IsNullOrEmpty(newPassword))
                {
                    updateQuery += ", Password = @Password";
                }

                updateQuery += " WHERE id = @Id";

                using (var cmd = new NpgsqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("Email", user.Email);
                    cmd.Parameters.AddWithValue("FName", user.Fname);
                    cmd.Parameters.AddWithValue("LName", user.LName);
                    cmd.Parameters.AddWithValue("UserName", user.Username);

                    // Add password parameter only if a new password is provided
                    if (!string.IsNullOrEmpty(newPassword))
                    {
                        cmd.Parameters.AddWithValue("Password", newPassword);
                    }

                    cmd.Parameters.AddWithValue("Id", id);

                    // Execute the query
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Return true if at least one row was affected
                    return rowsAffected > 0;
                }
            }
        }













        public static void RedirectToAccountPage(Window currentWindow, UserModel user)
        {
            // Create an instance of account page
            AccountView AccountView = new AccountView(user);

            // Show LoginView
            AccountView.Show();
            currentWindow?.Close();
        }




    }
}
