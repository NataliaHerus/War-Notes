using BusinessLogicLayer.Authentication;
using BusinessLogicLayer.Services.Interfaces;
using System.Windows;

namespace WarNotes.View
{
    public partial class SavedArticlesView : Window
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;
        private readonly IAuthenticator _authenticator;

        public SavedArticlesView(
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
            SavedArticlesList.ItemsSource = _articleService.GetSavedArticlesByUserId(_authenticator.CurrentAccount.Id);
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            UserProfile backToProfile = new UserProfile(_categoryService, _articleService, _userService, _authenticator);

            backToProfile.Show();
            Hide();
        }
    }
}
