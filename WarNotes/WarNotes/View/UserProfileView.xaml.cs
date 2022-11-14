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
    public partial class UserProfile : Window
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;
        private readonly IAuthenticator _authenticator;

        public UserProfile(
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

            UserDetailDTO user = new UserDetailDTO();
            this.DataContext = user;
            user.FirstName = _authenticator.CurrentAccount.FirstName;
            user.LastName = _authenticator.CurrentAccount.LastName;
            user.Email = _authenticator.CurrentAccount.Email;
        }
        private void exit_Click(object sender, RoutedEventArgs e)
        {

            MainView exitView = new MainView(_categoryService, _articleService, _userService, _authenticator);

            exitView.Show();
            Hide();
        }

        private void openLiked_Click(object sender, RoutedEventArgs e)
        {
            LikedArticlesView openLiked = new LikedArticlesView(_categoryService, _articleService, _userService, _authenticator);

            openLiked.Show();
            Hide();
        }
        private void openSaved_Click(object sender, RoutedEventArgs e)
        {
            SavedArticlesView openSaved = new SavedArticlesView(_categoryService, _articleService, _userService, _authenticator);

            openSaved.Show();
            Hide();
        }
        private void update_Click(object sender, RoutedEventArgs e)
        {
            UpdateView update = new UpdateView(_categoryService, _articleService, _userService, _authenticator);

            update.Show();
            Hide();
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
