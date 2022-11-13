using BusinessLogicLayer.Authentication;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services.Interfaces;
using System.Windows;

namespace WarNotes.View
{
    public partial class AdminProfileView : Window
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;
        private readonly IAuthenticator _authenticator;

        public AdminProfileView(
            ICategoryService categoryService,
            IArticleService articleService,
            IUserService userService,
            IAuthenticator authenticator)
        {
            InitializeComponent();
            _categoryService = categoryService;
            _articleService = articleService;

            UserDetailDTO user = new UserDetailDTO();
            this.DataContext = user;
            _userService = userService;
            _authenticator = authenticator;
            user.FirstName = _authenticator.CurrentAccount.FirstName;
            user.LastName = _authenticator.CurrentAccount.LastName;
            user.Email = _authenticator.CurrentAccount.Email;
        }

        private void showUsers_Click(object sender, RoutedEventArgs e)
        {
            AllUsersView openUsers = new AllUsersView(_categoryService, _articleService, _userService, _authenticator);

            openUsers.Show();
            Hide();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainView exitView = new MainView(_categoryService, _articleService, _userService, _authenticator);

            exitView.Show();
            Hide();
        }
    }
}
