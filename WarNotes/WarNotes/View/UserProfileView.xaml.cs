using BusinessLogicLayer.Authentication;
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
    /// <summary>
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Window
    {
        protected readonly IUserService _userService;
        protected readonly IAuthenticator _authenticator;
        public UserProfile(IUserService userService, IAuthenticator authenticator)
        {
            InitializeComponent();
            UserDetailDTO user = new UserDetailDTO();
            this.DataContext = user;
            _userService = userService;
            _authenticator = authenticator;
            user.FirstName = _authenticator.CurrentAccount.FirstName;
            user.LastName = _authenticator.CurrentAccount.LastName;
            user.Email = _authenticator.CurrentAccount.Email;
        }
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainView exitView = new MainView(_userService, _authenticator);
            exitView.Show();
            Hide();
        }

        private void openLiked_Click(object sender, RoutedEventArgs e)
        {
            LikedArticlesView openLiked = new LikedArticlesView(_userService, _authenticator);
            openLiked.Show();
            Hide();

        }
        private void openSaved_Click(object sender, RoutedEventArgs e)
        {
            SavedArticlesView openSaved = new SavedArticlesView(_userService, _authenticator);
            openSaved.Show();
            Hide();
        }
        private void update_Click(object sender, RoutedEventArgs e)
        {
            UpdateView update = new UpdateView(_userService, _authenticator);
            update.Show();
            Hide();
        }
    }
}
