using Book_Hopper.Model;
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
    public class UserBooksViewModel : ViewModelBase
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
        public UserBooksViewModel(UserModel user)
        {
            User = user;
            RedirectToAccountPage = new ViewModelCommand_RelayCommand_(ExecuteRedirectToAccountPageCommand, CanExecuteRedirectToAccountPageCommand);
        }
       public ICommand RedirectToAccountPage { get; }

        private void ExecuteRedirectToAccountPageCommand(object obj)
        {
            // Create an instance of LoginView
            UserProfile userProfile = new UserProfile(User);

            // Show LoginView
            userProfile.Show();

            FindWindow<Window>()?.Close();
        }
        private bool CanExecuteRedirectToAccountPageCommand(object obj)
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
