using System;
using System.Security;
using Npgsql;
using System.Windows;
using Book_Hopper.View;

namespace Book_Hopper.Services
{
    public class AuthService
    {
        private static readonly string ConnectionString = "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=postgres;Pooling=true;Minimum Pool Size=0;Maximum Pool Size=100; Connection Lifetime=0;";

        public static bool AuthenticateUser(string username, SecureString password)
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
                                return true; // Passwords match
                            }
                        }
                    }
                }
            }

            return false; // No matching user or password incorrect
        }

        public static bool RegisterUser(string username, string email, string fName, string lName, SecureString password)
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
                    cmd.Parameters.AddWithValue("Password", ConvertToUnsecureString(password)); // Convert SecureString to plain string
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

        private static void RedirectLoginPage(Window currentWindow)
        {
            // Create an instance of LoginView
            LoginView loginView = new LoginView();

            // Show LoginView
            loginView.Show();
            currentWindow?.Close();
        }



    }
}
