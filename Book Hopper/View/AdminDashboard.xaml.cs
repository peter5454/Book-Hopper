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
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Window
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void Click_AdminBooks(object sender, RoutedEventArgs e)
        {
            AdminBooks ab = new AdminBooks();
            ab.Show();
        }

        private void Click_Home(object sender, RoutedEventArgs e)
        {
           /* AdminDashboard ad = new AdminDashboard();
            ad.Show(); */
        }

        private void Click_Members(object sender, RoutedEventArgs e)
        {

        }
    }
}
