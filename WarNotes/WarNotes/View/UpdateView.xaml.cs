using BusinessLogicLayer;
using BusinessLogicLayer.Authentication;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services.Interfaces;
using System.Runtime.InteropServices;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace WarNotes.View
{
    public partial class UpdateView : Window
    {
        UserDetailDto user;

        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;
        private readonly IAuthenticator _authenticator;

        public UpdateView(
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

            user = new UserDetailDto();
            user.FirstName = _authenticator.CurrentAccount!.FirstName;
            user.LastName = _authenticator.CurrentAccount.LastName;
            user.Email = _authenticator.CurrentAccount.Email;
            user.Password = _authenticator.CurrentAccount.Password;
            user.Role = _authenticator.CurrentAccount.Role;
            user.Id = _authenticator.CurrentAccount.Id;
            this.DataContext = user;
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {

            if (txtPass2.Password == "" || txtPass.Password == "")
            {
                user.FirstName = txtName.Text;
                user.LastName = txtLast.Text;
                user.Email = txtMail.Text;
                _userService.UpdateUser(user);
                txtPass.Background = Brushes.Transparent;
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
                    _userService.UpdateUser(user);
                    txtPass.Background = Brushes.Transparent;
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
            _authenticator.CurrentAccount!.FirstName = user.FirstName;
            _authenticator.CurrentAccount.LastName = user.LastName;
            _authenticator.CurrentAccount.Email = user.Email;
            _authenticator.CurrentAccount.Password = user.Password;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainView exitView = new MainView(_categoryService, _articleService, _userService, _authenticator);

            exitView.Show();
            Hide();

            /*UserProfile backToProfile = new UserProfile(_categoryService, _articleService, _userService, _authenticator);

            backToProfile.Show();
            Hide();*/
        }
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

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
