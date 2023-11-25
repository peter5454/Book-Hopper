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
using Book_Hopper.View;
using Book_Hopper.CustomControls;
using Book_Hopper.Model;
using Book_Hopper.Helpers;

namespace Book_Hopper.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        //Fields
        private string _username;
        private string _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

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

        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        public ICommand LoginCommand { get; }
        public LoginViewModel()
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
                    // Redirect to AccountView with the authenticated user
                    AccountView accountView = new AccountView(authenticatedUser);
                    accountView.Show();

                    // Close the current window
                    if (obj is Window window)
                    {
                        window.Close();
                    }
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
            return !IsLoginInProgress && !string.IsNullOrWhiteSpace(Username) && Username.Length >= 3 && Password != null && Password.Length >= 3;
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
            RegisterView registerView = new RegisterView();

            // Show RegisterView
            registerView.Show();
            if (obj is Window window)
            {
                window.Close();
            }
        }

        private bool CanExecuteRegisterCommand(object obj)
        {
            return true;
        }

    }
}
