using Book_Hopper.Model;
using Book_Hopper.Services;
using BookHopperApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Book_Hopper.ViewModels
{
    class UserEditProfileViewModel : ViewModelBase
    {
        private UserModel _user;
        private string _originalUsername;
        private SecureString _originalPassword;

        public UserModel User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        private string _errorMessage;
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
                OnPropertyChanged(nameof(CanExecuteConfirmChangesButton));
            }
        }
        private string _errorMessageTwo;
        public string ErrorMessageTwo
        {
            get
            {
                return _errorMessageTwo;
            }
            set
            {
                _errorMessageTwo = value;
                OnPropertyChanged(nameof(ErrorMessageTwo));
                OnPropertyChanged(nameof(CanExecuteConfirmChangesButton));
            }
        }
        public ICommand ConfirmChangesCommand { get; }
        public ICommand CancelCommand { get; }

        private string _newPassword;
        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }
        private string _currentPassword;

        public string CurrentPassword
        {
            get { return _currentPassword; }
            set
            {
                _currentPassword = value;
                OnPropertyChanged(nameof(CurrentPassword));
            }
        }

        private string _confirmNewPassword;
        public string ConfirmNewPassword
        {
            get { return _confirmNewPassword; }
            set
            {
                _confirmNewPassword = value;
                OnPropertyChanged(nameof(ConfirmNewPassword));
            }
        }


        public UserEditProfileViewModel(UserModel user)
        {
            User = user;
            _originalUsername = user.Username;
            _originalPassword = user.GetSecurePassword();

            ConfirmChangesCommand = new ViewModelCommand_RelayCommand_(ExecuteConfirmChangesCommand, CanExecuteConfirmChangesCommand);
            CancelCommand = new ViewModelCommand_RelayCommand_(ExecuteCancelCommand);
        }

        private bool CanExecuteConfirmChangesCommand(object obj)
        {
            // Check if the current password is entered
            if (!IsPasswordValid(User.Password, CurrentPassword))
            {
                ErrorMessageTwo = "Please enter the correct current password.";
                return false;
            }
            ErrorMessageTwo = " ";
            return true;
        }

        private async void ExecuteConfirmChangesCommand(object obj)
        {
            try
            {
                // Check for uniqueness of the username and email
                bool isUsernameUnique = AuthService.IsUsernameUnique(User.Username, _originalUsername);
                bool isEmailUnique = AuthService.IsEmailUnique(User.Email, _originalUsername);

                // Validate email format
                bool isEmailValid = AuthService.IsValidEmail(User.Email);

                if (!IsPasswordEmpty(NewPassword))
                {
                    bool isPasswordTheSame = IsPasswordValid(NewPassword, ConfirmNewPassword);
                    bool isPasswordGoodSize = IsPasswordAppropriateSize(NewPassword);
                    if (!isPasswordTheSame || !isPasswordGoodSize)
                    {
                        if (!isPasswordTheSame)
                            ErrorMessage = "New Password and Confirm New password doesn't match";

                        if (!isPasswordGoodSize)
                            ErrorMessage = "New Password is too small (3 characters)";
                        return;
                    }
                }
                if (!isUsernameUnique || !isEmailUnique || !isEmailValid)
                {
                    if (!isUsernameUnique)
                        ErrorMessage = "Username is not unique.";

                    if (!isEmailUnique)
                        ErrorMessage = "Email is not unique.";

                    if (!isEmailValid)
                        ErrorMessage = "Invalid email format.";
                    return;
                }

                // Update user information in the database


                bool updateSuccess = await Task.Run(() => AuthService.UpdateUserInformation(User, NewPassword, User.Id));

                if (updateSuccess)
                {

                    UserProfile userProfile = new UserProfile(User);


                    userProfile.Show();
                    FindWindow<Window>()?.Close();
                }
                else
                {
                    ErrorMessage = "Failed to update account information. Please try again.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred: {ex.Message}";
            }
        }





        private void ExecuteCancelCommand(object obj)
        {
            // Create an instance of LoginView
            UserProfile userProfile = new UserProfile(User);

            // Show LoginView
            userProfile.Show();
            FindWindow<Window>()?.Close();

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
        public bool CanExecuteConfirmChangesButton
        {
            get { return string.IsNullOrEmpty(ErrorMessage); }
        }
        private bool IsPasswordValid(string password, string confirmPassword)
        {

            // Check if the new password and confirm password match
            if (password != confirmPassword)
            {
                return false;
            }
            return true;
        }
        private bool IsPasswordAppropriateSize(string password)
        {
            if (password.Length < 3)
            {
                return false;
            }
            return true;
        }
        private bool IsPasswordEmpty(string password)
        {
            if (!string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            return true;
        }
    }
}
