﻿using System;
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
using Book_Hopper.Model;

namespace BookHopperApp.View
{
    /// <summary>
    /// Interaction logic for UserBooks.xaml
    /// </summary>
    public partial class UserBooks : Window
    {
        public UserBooks(UserModel user)
        {
            InitializeComponent();

            // Set the DataContext of the AccountView
            var viewModel = new Book_Hopper.ViewModels.UserBooksViewModel(user);
            DataContext = viewModel;
        }
    }
}
