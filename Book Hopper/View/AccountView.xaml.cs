using Book_Hopper.Model;
using System.Windows;

namespace Book_Hopper.View
{
    public partial class AccountView : Window
    {
        public AccountView(UserModel user)
        {
            InitializeComponent();

            // Set the DataContext of the AccountView
            var viewModel = new Book_Hopper.ViewModels.AccountViewModel(user);
            DataContext = viewModel;

        }
    }
}
