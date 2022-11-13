using BusinessLogicLayer.Authentication;
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
    /// Interaction logic for AllUsersView.xaml
    /// </summary>
    public partial class AllUsersView : Window
    {
        protected readonly IUserService _userService;
        protected readonly IAuthenticator _authenticator;
        public AllUsersView(IUserService userService, IAuthenticator authenticator)
        {
            InitializeComponent();
            _userService = userService;
            _authenticator = authenticator;
            AllUsersList.ItemsSource = _userService.GetAllUsersListAsync();
           
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            AdminProfileView exit = new AdminProfileView(_userService, _authenticator);
            exit.Show();
            Hide();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UserDetailDTO user = (UserDetailDTO)AllUsersList.SelectedValue;
            if (user != null)
            {
                user.IsBlocked = true;
                _userService.UpdateUser(user);
            }
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UserDetailDTO user = (UserDetailDTO)AllUsersList.SelectedValue;
            if (user != null)
            {
                user.IsBlocked = false;
                _userService.UpdateUser(user);
            }
        }
    }
}
