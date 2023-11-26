using Book_Hopper.Model;
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
    class UserProfileViewModel : ViewModelBase
    {
        private UserModel _user;

        public UserModel User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public ICommand LogoutCommand { get; }
        public ICommand ChangeInfoCommand { get; }

        // Constructor with UserModel parameter
        public UserProfileViewModel()
        {
        }
        public UserProfileViewModel(UserModel user)
        {
            User = user;

            LogoutCommand = new ViewModelCommand_RelayCommand_(ExecuteLogoutCommand);
            ChangeInfoCommand = new ViewModelCommand_RelayCommand_(ExecuteChangeInfoCommand, CanExecuteChangeInfoCommand);
        }

        private void ExecuteLogoutCommand(object obj)
        {
            // Create an instance of LoginView
            UserLogin loginView = new UserLogin();

            // Show LoginView
            loginView.Show();

            FindWindow<Window>()?.Close();
        }

        private void ExecuteChangeInfoCommand(object obj)
        {
            // Create an instance of ChangeInformationView
            UserEditProfile userEditProfile = new UserEditProfile(User);

            // Create an instance of ChangeInformationViewModel
            UserEditProfileViewModel userEditProfileViewModel = new UserEditProfileViewModel(User);

            // Set the DataContext of ChangeInformationView to the new ChangeInformationViewModel
            userEditProfile.DataContext = userEditProfileViewModel;

            // Show ChangeInformationView and close the current window
            userEditProfile.Show();
            FindWindow<Window>()?.Close();
        }

        private bool CanExecuteChangeInfoCommand(object obj)
        {
            return true;
        }

        // Find the window dynamically
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
