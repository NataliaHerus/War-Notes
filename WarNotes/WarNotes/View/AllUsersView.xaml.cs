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
            List<UserForAdminDTO> users = new List<UserForAdminDTO>();
            users.Add(new UserForAdminDTO() {FirstName = "Анна", LastName = "Берко", Email = "berko@gmail.com" });
            users.Add(new UserForAdminDTO() { FirstName = "Олег", LastName = "Сопко", Email = "osopko22@gmail.com" });
            users.Add(new UserForAdminDTO() { FirstName = "Марина", LastName = "Сертинюк", Email = "sert@gmail.com" });
            users.Add(new UserForAdminDTO() { FirstName = "Павло", LastName = "Рак", Email = "pavlo@gmail.com" });
            users.Add(new UserForAdminDTO() { FirstName = "Марта", LastName = "Коваль", Email = "koval@gmail.com" });
            users.Add(new UserForAdminDTO() { FirstName = "Анна", LastName = "Берко", Email = "berko@gmail.com" });
            users.Add(new UserForAdminDTO() { FirstName = "Олег", LastName = "Сопко", Email = "osopko22@gmail.com" });
            users.Add(new UserForAdminDTO() { FirstName = "Марина", LastName = "Сертинюк", Email = "sert@gmail.com" });
            users.Add(new UserForAdminDTO() { FirstName = "Павло", LastName = "Рак", Email = "pavlo@gmail.com" });
            users.Add(new UserForAdminDTO() { FirstName = "Марта", LastName = "Коваль", Email = "koval@gmail.com" });

            AllUsersList.ItemsSource = users;
            _userService = userService;
            _authenticator = authenticator;
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            AdminProfileView exit = new AdminProfileView(_userService, _authenticator);
            exit.Show();
            Hide();
        }
    }
}
