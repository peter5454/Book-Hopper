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
    class UserCreatedViewModel : ViewModelBase
    {
        public ICommand LoginCommand { get; }

        public UserCreatedViewModel()
        {
            LoginCommand = new ViewModelCommand_RelayCommand_(ExecuteRedirectToLoginCommand, CanExecuteRedirectToLoginCommand);
        }
        private void ExecuteRedirectToLoginCommand(object obj)
        {
            // Create an instance of LoginView
            UserLogin loginView = new UserLogin();

            // Show LoginView
            loginView.Show();

            FindWindow<Window>()?.Close();
        }
        private bool CanExecuteRedirectToLoginCommand(object obj)
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
