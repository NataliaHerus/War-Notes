using BusinessLogicLayer;
using BusinessLogicLayer.Authentication;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for UpdateView.xaml
    /// </summary>
    public partial class UpdateView : Window
    {
        UserDetailDTO user;
        protected readonly IUserService _userService;
        protected readonly IAuthenticator _authenticator;
        public UpdateView(IUserService userService, IAuthenticator authenticator)
        {
            InitializeComponent();
            user = new UserDetailDTO();
            user.FirstName = "Anna";
            user.LastName = "Koshmal";
            user.Email = "anna@gmail.com";
            Hasher hash1 = new Hasher("111");
            string hashPass = hash1.ComputeHash();
            user.Password = hashPass;

            this.DataContext = user;

        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {

            if (txtPass2.Password == "" || txtPass.Password == "")
            {
                user.FirstName = txtName.Text;
                user.LastName = txtLast.Text;
                user.Email = txtMail.Text;
                ShowResult.Text = "Дані збережено";
                ShowResult.Foreground = Brushes.LightGreen;
                ShowResult.Visibility = Visibility.Visible;
            }
            else
            {
                user.FirstName = txtName.Text;
                user.LastName = txtLast.Text;
                user.Email = txtMail.Text;

                Hasher hash1 = new Hasher(txtPass.Password);
                string hashPass = hash1.ComputeHash();
                if (user.Password == hashPass)
                {
                    Hasher hash2 = new Hasher(txtPass2.Password);
                    user.Password = hash2.ComputeHash();
                    ShowResult.Text = "Дані збережено";
                    ShowResult.Foreground = Brushes.LightGreen;
                    ShowResult.Visibility = Visibility.Visible;

                }
                else
                {
                    txtPass.ToolTip = "Неправильний пароль";
                    txtPass.Background = Brushes.DarkRed;
                    ShowResult.Text = "Помилка";
                    ShowResult.Foreground = Brushes.DarkRed;
                    ShowResult.Visibility = Visibility.Visible;
                }
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            UserProfile backToProfile = new UserProfile(_userService, _authenticator);
            backToProfile.Show();
            Hide();
        }
    }
}
