﻿using BusinessLogicLayer.Authentication;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services.Interfaces;
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

namespace WarNotes.View
{
    /// <summary>
    /// Interaction logic for AdminProfileView.xaml
    /// </summary>
    public partial class AdminProfileView : Window
    {
        protected readonly IUserService _userService;
        protected readonly IAuthenticator _authenticator;
        public AdminProfileView(IUserService userService, IAuthenticator authenticator)
        {
            InitializeComponent();
            UserDetailDTO user = new UserDetailDTO();
            user.FirstName = "Admin";
            user.Email = "admin@gmail.com";
            this.DataContext = user;
            _userService = userService;
            _authenticator = authenticator;
        }

        private void showUsers_Click(object sender, RoutedEventArgs e)
        {
            AllUsersView openUsers = new AllUsersView(_userService, _authenticator);
            openUsers.Show();
            Hide();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainView exitView = new MainView(_userService, _authenticator);
            exitView.Show();
            Hide();
        }
    }
}
