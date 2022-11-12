using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services.Interfaces;
using System.Windows;

namespace WarNotes.View
{
    public partial class AdminProfileView : Window
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;

        public AdminProfileView(ICategoryService categoryService, IArticleService articleService)
        {
            InitializeComponent();
            _categoryService = categoryService;
            _articleService = articleService;

            UserDetailDTO user = new UserDetailDTO();
            user.FirstName = "Admin";
            user.Email = "admin@gmail.com";
            this.DataContext = user;
        }

        private void showUsers_Click(object sender, RoutedEventArgs e)
        {
            AllUsersView openUsers = new AllUsersView(_categoryService, _articleService);
            openUsers.Show();
            Hide();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainView exitView = new MainView(_categoryService, _articleService);
            exitView.Show();
            Hide();
        }
    }
}
