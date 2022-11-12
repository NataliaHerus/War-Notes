﻿using BusinessLogicLayer;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Services.DTO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using BusinessLogicLayer.Validators;

namespace WarNotes.View
{
    public partial class RegisterView : Window
    {
        protected readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;

        public RegisterView(IUserService userService, ICategoryService categoryService, IArticleService articleService)
        {
            InitializeComponent();
            _userService = userService;
            _categoryService = categoryService;
            _articleService = articleService;
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
            string password = txtPass.Password.Trim();
            string confirmPassword = txtPass2.Password.Trim();
            bool valid = true;
            if (!UserRegistrationValidator.IsValidName(firstName))
            {
                txtName.ToolTip = "Некоректно введено ім'я";
                txtName.Background = Brushes.DarkRed;
                valid = false;
            }
            else
            {
                txtName.Background = Brushes.Transparent;
            }
            if (!UserRegistrationValidator.IsValidName(lastName))
            {
                txtLast.ToolTip = "Некоректно введено прізвище";
                txtLast.Background = Brushes.DarkRed;
                valid = false;
            }
            else
            {
                txtLast.Background = Brushes.Transparent;
            }
            if (!UserRegistrationValidator.IsValidPassword(password))
            {
                txtPass.ToolTip = "Недостатньо безпечний пароль";
                txtPass.Background = Brushes.DarkRed;
                valid = false;
            }
            else
            {
                txtPass.Background = Brushes.Transparent;
            }
            if (password != confirmPassword)
            {
                txtPass2.ToolTip = "Паролі не співпадають!";
                txtPass2.Background = Brushes.DarkRed;
                valid = false;
            }
            else
            {
                txtPass2.Background = Brushes.Transparent;
            }
            if (!UserRegistrationValidator.IsValidEmail(email))
            {
                txtMail.ToolTip = "Некоректно введено email";
                txtMail.Background = Brushes.DarkRed;
                valid = false;
            }
            else
            {
                txtMail.Background = Brushes.Transparent;
            }
            if (_userService.GetUserByEmailAsync(email) is not null)
            {
                MessageBox.Show("Користувач з такою адресою вже існує. Будь ласка, замініть на іншу");
            }
            else if (valid)
            {
                Hasher hash = new Hasher(password);
                string hashedPassword = hash.ComputeHash();
                UserRegistrationDTO user = new UserRegistrationDTO();

                user.FirstName = firstName;
                user.LastName = lastName;
                user.Email = email;
                user.Password = hashedPassword;
                await _userService.CreateUserAsync(user);
                MessageBox.Show("Користувача успішно зареєстровано");
                txtName.ToolTip = "";
            }
            else
            {
                MessageBox.Show("Неможливо зареєструвати користувача, повторіть спробу, зважаючи на підказки");
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView(_userService, _categoryService, _articleService);
            loginView.Show();
            Hide();
        }
    }
}
