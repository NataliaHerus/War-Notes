using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services.Interfaces;
using System.Collections.Generic;
using System.Windows;

namespace WarNotes.View
{
    public partial class LikedArticlesView : Window
    {
        private readonly ICategoryService _categoryService;

        public LikedArticlesView(ICategoryService categoryService)
        {
            InitializeComponent();
            _categoryService = categoryService;

            List<ArticleDTO> items = new List<ArticleDTO>();
            items.Add(new ArticleDTO() { Title = "aaaaaaaaaaaaaaaaaaaaaaaaaaaa?" });
            items.Add(new ArticleDTO() { Title = "Стаття 234" });
            items.Add(new ArticleDTO() { Title = "Що робити?" });
            items.Add(new ArticleDTO() { Title = "Що має бути  ситуацій?" });
            items.Add(new ArticleDTO() { Title = "vvvvvvvvvvvvvvvvvvvvvvvvvvvv" });
            items.Add(new ArticleDTO() { Title = "Що має боівлівоілво их ситуацій?" });
            items.Add(new ArticleDTO() { Title = "Що має бути в аптечці для надзвичайних ситуацій?" });
            items.Add(new ArticleDTO() { Title = "fddddddddddddddddddddddd" });
            items.Add(new ArticleDTO() { Title = "Що має бутив вівілвсілвоілс бво" });
            items.Add(new ArticleDTO() { Title = "ілвіловілволллллллллл" });
            items.Add(new ArticleDTO() { Title = "Що має бути в аптечці для надзвичайних ситуацій?" });
            items.Add(new ArticleDTO() { Title = "Що має бути олявілвоілвоілволв?" });
            items.Add(new ArticleDTO() { Title = "Що має бути в аптечці для надзвичайних ситуацій?" });

            LikedArticlesList.ItemsSource = items;

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            UserProfile backToProfile = new UserProfile(_categoryService);
            backToProfile.Show();
            Hide();
        }




    }
}
