using Book_Hopper.Model;
using Book_Hopper.Services;
using Book_Hopper.View;
using System.Windows;
using System.Windows.Input;

namespace Book_Hopper.ViewModels
{
    public class AccountViewModel : ViewModelBase
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
        public AccountViewModel()
        { 
        }
            public AccountViewModel(UserModel user)
        {
            User = user;

            LogoutCommand = new ViewModelCommand_RelayCommand_(ExecuteLogoutCommand);
            ChangeInfoCommand = new ViewModelCommand_RelayCommand_(ExecuteChangeInfoCommand, CanExecuteChangeInfoCommand);
        }

        private void ExecuteLogoutCommand(object obj)
        {
            // Redirect to the login view and close the current window
            AuthService.RedirectLoginPage(FindWindow<Window>());
        }

        private void ExecuteChangeInfoCommand(object obj)
        {
            // Create an instance of ChangeInformationView
            ChangeInformationView changeInformationView = new ChangeInformationView();

            // Create an instance of ChangeInformationViewModel
            ChangeInformationViewModel changeInformationViewModel = new ChangeInformationViewModel(User);

            // Set the DataContext of ChangeInformationView to the new ChangeInformationViewModel
            changeInformationView.DataContext = changeInformationViewModel;

            // Show ChangeInformationView and close the current window
            changeInformationView.Show();
            FindWindow<Window>()?.Close();
        }

        private bool CanExecuteChangeInfoCommand(object obj)
        {
            // Implement logic to determine if the "Change Information" command can be executed
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
