using BusinessLogicLayer.Authentication;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services.Interfaces;
using System.Runtime.InteropServices;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace WarNotes.View
{
    public partial class AllUsersView : Window
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;
        private readonly IAuthenticator _authenticator;

        public AllUsersView(
            ICategoryService categoryService,
            IArticleService articleService,
            IUserService userService,
            IAuthenticator authenticator)
        {
            InitializeComponent();
            _categoryService = categoryService;
            _articleService = articleService;
            _userService = userService;
            _authenticator = authenticator;

            AllUsersList.ItemsSource = _userService.GetAllUsersList();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            AdminProfileView exit = new AdminProfileView(_categoryService, _articleService, _userService, _authenticator);

            exit.Show();
            Hide();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UserDetailDto user = (UserDetailDto)AllUsersList.SelectedValue;
            if (user != null)
            {
                user.IsBlocked = true;
                _userService.UpdateUser(user);
                MessageBox.Show("Користувача заблоковано. Вхід в систему обмежено");
            }
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UserDetailDto user = (UserDetailDto)AllUsersList.SelectedValue;
            if (user != null)
            {
                user.IsBlocked = false;
                _userService.UpdateUser(user);
                MessageBox.Show("Користувача розблоковано. Тепер він може увійти в систему");
            }
        }
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

    }
}
