using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services.Interfaces;
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
    public partial class UserProfile : Window
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;

        public UserProfile(ICategoryService categoryService, IArticleService articleService)
        {
            InitializeComponent();
            _categoryService = categoryService;
            _articleService = articleService;

            UserDetailDTO user = new UserDetailDTO();
            user.FirstName = "Anna";
            user.LastName = "Koshmal";
            user.Email = "anna@gmail.com";
            this.DataContext = user;
        }
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainView exitView = new MainView(_categoryService, _articleService);
            exitView.Show();
            Hide();
        }

        private void openLiked_Click(object sender, RoutedEventArgs e)
        {
            LikedArticlesView openLiked = new LikedArticlesView(_categoryService, _articleService);
            openLiked.Show();
            Hide();

        }
        private void openSaved_Click(object sender, RoutedEventArgs e)
        {
            SavedArticlesView openSaved = new SavedArticlesView(_categoryService, _articleService);
            openSaved.Show();
            Hide();
        }
        private void update_Click(object sender, RoutedEventArgs e)
        {
            UpdateView update = new UpdateView(_categoryService, _articleService);
            update.Show();
            Hide();
        }
    }
}
