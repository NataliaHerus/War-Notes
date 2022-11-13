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
    /// Interaction logic for SavedArticlesView.xaml
    /// </summary>
    public partial class SavedArticlesView : Window
    {
        protected readonly IUserService _userService;
        protected readonly IAuthenticator _authenticator;
        public SavedArticlesView(IUserService userService, IAuthenticator authenticator)
        {
            InitializeComponent();
            List<ArticleDTO> items = new List<ArticleDTO>();
            items.Add(new ArticleDTO() { Title = "Що має бути в аптечці для надзвичайних ситуацій?" });
            items.Add(new ArticleDTO() { Title = "Стаття 234" });
            items.Add(new ArticleDTO() { Title = "Що робити?" });
            items.Add(new ArticleDTO() { Title = "Що має бути  ситуацій?" });
            items.Add(new ArticleDTO() { Title = "Що має бути в аптечці для надзвичайних ситуацій?" });
            items.Add(new ArticleDTO() { Title = "Що має боівлівоілво их ситуацій?" });
            items.Add(new ArticleDTO() { Title = "Що має бути в аптечці для надзвичайних ситуацій?" });
            items.Add(new ArticleDTO() { Title = "Що має бути в аптечці для надзвичайних ситуацій?" });
            items.Add(new ArticleDTO() { Title = "Що має бутив вівілвсілвоілс бво" });
            items.Add(new ArticleDTO() { Title = "ілвіловілволллллллллл" });
            items.Add(new ArticleDTO() { Title = "Що має бути в аптечці для надзвичайних ситуацій?" });
            items.Add(new ArticleDTO() { Title = "Що має бути олявілвоілвоілволв?" });
            items.Add(new ArticleDTO() { Title = "Що має бути в аптечці для надзвичайних ситуацій?" });

            SavedArticlesList.ItemsSource = items;
            _userService = userService;
            _authenticator = authenticator;

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            UserProfile backToProfile = new UserProfile(_userService, _authenticator);
            backToProfile.Show();
            Hide();
        }
    }
}
