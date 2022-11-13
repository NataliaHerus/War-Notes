using BusinessLogicLayer.Authentication;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services.Interfaces;
using System.Windows;

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

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            AdminProfileView exit = new AdminProfileView(_categoryService, _articleService, _userService, _authenticator);

            exit.Show();
            Hide();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UserDetailDTO user = (UserDetailDTO)AllUsersList.SelectedValue;
            if (user != null)
            {
                user.IsBlocked = true;
                _userService.UpdateUser(user);
                MessageBox.Show("Користувача заблоковано. Вхід в систему обмежено");
            }
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UserDetailDTO user = (UserDetailDTO)AllUsersList.SelectedValue;
            if (user != null)
            {
                user.IsBlocked = false;
                _userService.UpdateUser(user);
                MessageBox.Show("Користувача розблоковано. Тепер він може увійти в систему");
            }
        }
    }
}
