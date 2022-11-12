using BusinessLogicLayer;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services.Interfaces;
using System.Windows;
using System.Windows.Media;

namespace WarNotes.View
{
    public partial class UpdateView : Window
    {
        UserDetailDTO user;
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;

        public UpdateView(ICategoryService categoryService, IArticleService articleService)
        {
            InitializeComponent();
            _categoryService = categoryService;
            _articleService = articleService;

            user = new UserDetailDTO();
            user.FirstName = "Anna";
            user.LastName = "Koshmal";
            user.Email = "anna@gmail.com";
            Hasher hash1 = new Hasher("111");
            string hashPass = hash1.ComputeHash();
            user.Password = hashPass;

            this.DataContext = user;

        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {

            if (txtPass2.Password == "" || txtPass.Password == "")
            {
                user.FirstName = txtName.Text;
                user.LastName = txtLast.Text;
                user.Email = txtMail.Text;
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
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            UserProfile backToProfile = new UserProfile(_categoryService, _articleService);
            backToProfile.Show();
            Hide();
        }
    }
}
