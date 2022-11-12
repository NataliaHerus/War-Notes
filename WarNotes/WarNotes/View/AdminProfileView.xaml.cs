using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services.Interfaces;
using System.Windows;

namespace WarNotes.View
{
    public partial class AdminProfileView : Window
    {
        private readonly ICategoryService _categoryService;

        public AdminProfileView(ICategoryService categoryService)
        {
            InitializeComponent();
            _categoryService = categoryService;

            UserDetailDTO user = new UserDetailDTO();
            user.FirstName = "Admin";
            user.Email = "admin@gmail.com";
            this.DataContext = user;
        }

        private void showUsers_Click(object sender, RoutedEventArgs e)
        {
            AllUsersView openUsers = new AllUsersView(_categoryService);
            openUsers.Show();
            Hide();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainView exitView = new MainView(_categoryService);
            exitView.Show();
            Hide();
        }
    }
}
