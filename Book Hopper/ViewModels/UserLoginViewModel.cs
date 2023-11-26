using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Book_Hopper.Services;
using Book_Hopper.CustomControls;
using Book_Hopper.Model;
using Book_Hopper.Helpers;
using BookHopperApp.View;

namespace Book_Hopper.ViewModels
{
    class UserLoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private string _errorMessage;


        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }


        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }


        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }


        public ICommand LoginCommand { get; }
        public UserLoginViewModel()
        {
            LoginCommand = new ViewModelCommand_RelayCommand_(ExecuteLoginCommand, CanExecuteLoginCommand);
            RegisterCommand = new ViewModelCommand_RelayCommand_(ExecuteRegisterCommand, CanExecuteRegisterCommand);
        }

        private bool _isLoginInProgress;

        public bool IsLoginInProgress
        {
            get { return _isLoginInProgress; }
            set
            {
                _isLoginInProgress = value;
                OnPropertyChanged(nameof(IsLoginInProgress));
            }
        }

        private async void ExecuteLoginCommand(object obj)
        {
            if (IsLoginInProgress)
            {
                return; // Login already in progress
            }

            try
            {
                IsLoginInProgress = true;
                ErrorMessage = string.Empty;

                // Convert string password to SecureString
                SecureString securePassword = SecureStringHelper.ConvertToSecureString(Password);

                // Authenticate user
                UserModel authenticatedUser = await Task.Run(() => AuthService.AuthenticateUser(Username, securePassword));

                if (authenticatedUser != null)
                {
                    if (Username == "admin")
                    {
                        AdminDashboard adminDashboard = new AdminDashboard();
                        adminDashboard.Show();
                    }
                    else
                    {
                        UserBooks userBooks = new UserBooks(authenticatedUser);
                        userBooks.Show();
                    }

                    FindWindow<Window>()?.Close();
                }
                else
                {
                    ErrorMessage = "Invalid username or password.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred: {ex.Message}";
            }
            finally
            {
                IsLoginInProgress = false;
            }
        }


        private bool CanExecuteLoginCommand(object obj)
        {
            return true;
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

        public ICommand RegisterCommand { get; }

        private void ExecuteRegisterCommand(object obj)
        {
            // Create an instance of RegisterView
            UserRegister userRegister = new UserRegister();

            // Show RegisterView
            userRegister.Show();
            FindWindow<Window>()?.Close();
        }

        private bool CanExecuteRegisterCommand(object obj)
        {
            return true;
        }
        private static T FindWindow<T>() where T : Window
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is T)
                {
                    return (T)window;
                }
            }
            return null;
        }
    }
}
