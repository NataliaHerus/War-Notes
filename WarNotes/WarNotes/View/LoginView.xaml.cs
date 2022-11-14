using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Authentication;
using System.Windows;
using System.Windows.Input;
using System;
using System.ComponentModel;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer;

namespace WarNotes.View
{
    public partial class LoginView : Window
    {
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        private readonly IAuthenticator _authenticator;
        private readonly UserDetailDto user;

        public LoginView(
            IUserService userService,
            ICategoryService categoryService,
            IArticleService articleService,
            IAuthenticator authenticator)
        {
            InitializeComponent();
            _userService = userService;
            _categoryService = categoryService;
            _articleService = articleService;
            _authenticator = authenticator;

            user = new UserDetailDto();
            this.DataContext = user;

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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string password = txtPass.Password.Trim();
            Hasher hash = new Hasher(password);
            string hashedPassword = hash.ComputeHash();

            try
            {
                _authenticator.Login(user.Email!, hashedPassword);
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
            if (_authenticator.IsLoggedIn)
            {
                MainView registerView = new MainView(_categoryService, _articleService, _userService, _authenticator);
                registerView.Show();
                Hide();
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterView registerView = new RegisterView(_userService, _categoryService, _articleService, _authenticator);

            registerView.Show();
            Hide();
        }
    }
}
