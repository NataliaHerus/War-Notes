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

        public UserProfile(ICategoryService categoryService)
        {
            InitializeComponent();
            _categoryService = categoryService;

            UserDetailDTO user = new UserDetailDTO();
            user.FirstName = "Anna";
            user.LastName = "Koshmal";
            user.Email = "anna@gmail.com";
            this.DataContext = user;
        }
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainView exitView = new MainView(_categoryService);
            exitView.Show();
            Hide();
        }

        private void openLiked_Click(object sender, RoutedEventArgs e)
        {
            LikedArticlesView openLiked = new LikedArticlesView(_categoryService);
            openLiked.Show();
            Hide();

        }
        private void openSaved_Click(object sender, RoutedEventArgs e)
        {
            SavedArticlesView openSaved = new SavedArticlesView(_categoryService);
            openSaved.Show();
            Hide();
        }
        private void update_Click(object sender, RoutedEventArgs e)
        {
            UpdateView update = new UpdateView(_categoryService);
            update.Show();
            Hide();
        }
    }
}
