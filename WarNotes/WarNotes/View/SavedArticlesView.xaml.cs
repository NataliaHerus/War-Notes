using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services.Interfaces;
using System.Collections.Generic;
using System.Windows;

namespace WarNotes.View
{
    public partial class SavedArticlesView : Window
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;

        public SavedArticlesView(ICategoryService categoryService, IArticleService articleService)
        {
            InitializeComponent();
            _categoryService = categoryService;
            _articleService = articleService;

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

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            UserProfile backToProfile = new UserProfile(_categoryService, _articleService);
            backToProfile.Show();
            Hide();
        }
    }
}
