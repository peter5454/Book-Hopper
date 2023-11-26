using Book_Hopper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookHopperApp.View
{
    /// <summary>
    /// Interaction logic for UserEditProfile.xaml
    /// </summary>
    public partial class UserEditProfile : Window
    {
        public UserEditProfile(UserModel user)
        {
            InitializeComponent();
            var viewModel = new Book_Hopper.ViewModels.UserEditProfileViewModel(user);
            DataContext = viewModel;
        }

    }
}
