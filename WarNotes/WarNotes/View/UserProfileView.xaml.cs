using BusinessLogicLayer.DTO;
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
    /// <summary>
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Window
    {
        public UserProfile()
        {
            InitializeComponent();
            UserDetailDTO user = new UserDetailDTO();
            user.FirstName = "Anna";
            user.LastName = "Koshmal";
            user.Email = "anna@gmail.com";
            this.DataContext = user;
        }
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainView exitView = new MainView();
            exitView.Show();
            Hide();
        }

        private void openLiked_Click(object sender, RoutedEventArgs e)
        {
            LikedArticlesView openLiked = new LikedArticlesView();
            openLiked.Show();
            Hide();

        }
        private void openSaved_Click(object sender, RoutedEventArgs e)
        {
            SavedArticlesView openSaved = new SavedArticlesView();
            openSaved.Show();
            Hide();
        }
        private void update_Click(object sender, RoutedEventArgs e)
        {
            UpdateView update = new UpdateView();
            update.Show();
            Hide();
        }
    }
}
