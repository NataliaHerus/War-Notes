using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.EntityFramework;
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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        protected readonly IUserService _userService;
        private readonly ICategoryService _categoryService;

        public LoginView(IUserService userService, ICategoryService categoryService)
        {
            InitializeComponent();
            _userService = userService;
            _categoryService = categoryService;
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
            MainView registerView = new MainView(_categoryService);
            registerView.Show();
            Hide();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterView registerView = new RegisterView(_userService, _categoryService);
            registerView.Show();
            Hide();
        }
    }
}
