using Book_Hopper.Services;
using BookHopperApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Book_Hopper.ViewModels
{
    class UserRegisterViewModel : ViewModelBase
    {
        // Fields
        private string _username;
        private string _email;
        private string _firstName;
        private string _lastName;
        private String _password;
        private String _confirmPassword;
        private string _errorMessage;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public String Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public String ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand RegisterCommand { get; }
        public ICommand RedirectToLoginCommand { get; }

        public UserRegisterViewModel()
        {
            RegisterCommand = new ViewModelCommand_RelayCommand_(ExecuteRegisterCommand, CanExecuteRegisterCommand);
            RedirectToLoginCommand = new ViewModelCommand_RelayCommand_(ExecuteRedirectToLoginCommand, CanExecuteRedirectToLoginCommand);
        }

        private async void ExecuteRegisterCommand(object obj)
        {
            try
            {
                // Validation logic
                if (string.IsNullOrWhiteSpace(Username) ||
                    string.IsNullOrWhiteSpace(Email) ||
                    string.IsNullOrWhiteSpace(FirstName) ||
                    string.IsNullOrWhiteSpace(LastName) ||
                    Password == null ||
                    ConfirmPassword == null)
                {
                    ErrorMessage = "All fields are required.";
                    return;
                }

               
                if (Password != ConfirmPassword)
                {
                    ErrorMessage = "Password and Confirm Password must match.";
                    return;
                }

                // Additional validation checks
                if (AuthService.UserExists(Username))
                {
                    ErrorMessage = "Username already exists. Please choose a different username.";
                    return;
                }

                if (AuthService.EmailExists(Email))
                {
                    ErrorMessage = "Email already exists. Please use a different email.";
                    return;
                }

                if (!AuthService.IsValidEmail(Email))
                {
                    ErrorMessage = "Invalid email address.";
                    return;
                }

                // Registration logic
                bool isRegistered = await Task.Run(() => AuthService.RegisterUser(Username, Email, FirstName, LastName, Password));

                if (isRegistered)
                {
                    return;
                }
                else
                {
                    ErrorMessage = "Registration failed. Please try again.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred: {ex.Message}";
            }
        }

        private bool CanExecuteRegisterCommand(object obj)
        {
            return true;
        }
        private void ExecuteRedirectToLoginCommand(object obj)
        {
            // Create an instance of LoginView
            UserLogin loginView = new UserLogin();

            // Show LoginView
            loginView.Show();

            if (obj is Window window)
            {
                window.Close();
            }
        }


        private bool CanExecuteRedirectToLoginCommand(object obj)
        {
            return true;
        }
    }
}
