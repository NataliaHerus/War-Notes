using BusinessLogicLayer;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Services.DTO;
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
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {
        protected readonly IUserService _userService;
        public RegisterView(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string firstName = txtName.Text.Trim();
            string lastName = txtLast.Text.Trim();
            string email = txtMail.Text.Trim();
            string password = txtPass.Password.Trim().ToLower();
            string confirmPassword = txtPass2.Password.Trim().ToLower();
            if (firstName.Length < 2)
            {
                txtName.ToolTip = "Ім'я мусить містити не менше 2 символів!";
                txtName.Background = Brushes.DarkRed;
            }
            else if (lastName.Length < 2)
            {
                txtLast.ToolTip = "Прізвище мусить містити не менше 2 символів!";
                txtLast.Background = Brushes.DarkRed;
            }
            else if (password.Length < 6)
            {
                txtPass.ToolTip = "Пароль мусить містити не менше 2 символів!";
                txtPass.Background = Brushes.DarkRed;
            }
            else if (password != confirmPassword)
            {
                txtPass.ToolTip = "Паролі не співпадають!";
                txtPass.Background = Brushes.DarkRed;
            }
            else if (email.Length < 10)
            {
                txtMail.ToolTip = "E-mail мусить містити не менше 2 символів!";
                txtMail.Background = Brushes.DarkRed;
            }
            else if (!email.Contains("@"))
            {
                txtMail.ToolTip = "E-mail мусить містити знак '@'";
                txtMail.Background = Brushes.DarkRed;
            }
            else if (!email.Contains("."))
            {
                txtMail.ToolTip = "E-mail мусить містити крапку";
                txtMail.Background = Brushes.DarkRed;
            }
            if (_userService.GetUserByEmailAsync(email) is not null)
            {
                MessageBox.Show("Користувач з такою адресою вже існує. Будь ласка, замініть на іншу");
            }
            else
            {
                Hasher hash = new Hasher(password);
                string hashedPassword = hash.ComputeHash();
                UserDTO user = new UserDTO();

                user.FirstName = firstName;
                user.LastName = lastName;
                user.Email = email;
                user.Password = hashedPassword;
                await _userService.CreateUserAsync(user);
                MessageBox.Show("Користувача успішно зареєстровано");
            }

            txtName.ToolTip = "";
            txtName.Background = Brushes.Transparent;
            txtLast.ToolTip = "";
            txtLast.Background = Brushes.Transparent;
            txtPass.ToolTip = "";
            txtPass.Background = Brushes.Transparent;
            txtPass2.ToolTip = "";
            txtPass2.Background = Brushes.Transparent;
            txtMail.ToolTip = "";
            txtMail.Background = Brushes.Transparent;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView(_userService);
            loginView.Show();
            Hide();
        }
    }
}
