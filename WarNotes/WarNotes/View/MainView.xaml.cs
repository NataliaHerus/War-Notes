using BusinessLogicLayer.Services.Interfaces;
using System;
using System.Windows.Controls;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WarNotes.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;

        public string TextArticle { get; set; }
        public int SelectedCategoryId { get; set; }

        public MainView(ICategoryService categoryService, IArticleService articleService)
        {
            _categoryService = categoryService;
            _articleService = articleService;

            InitializeComponent();
            LoadCategories();
        }

        private async Task LoadCategories()
        {
            var allCategories = await _categoryService.GetAllCategoriesAsync();

            for (int i = 0; i < allCategories.Count(); i++)
            {
                var rb = new RadioButton() { Content = allCategories.ElementAt(i).CategoryName };

                rb.Tag = i + 1;
                rb.Click += LoadHeaders;

                rb.Style = (Style)FindResource("categoryButton");

                categories.Children.Add(rb);
            }
        }

        private void LoadHeaders(object sender, RoutedEventArgs e)
        {
            scrollerBlock.Visibility = Visibility.Hidden;

            var radioButton = (RadioButton)sender;
            var categoryName = (string)radioButton.Content;
            SelectedCategoryId = (int)radioButton.Tag;

            LoadHeaders(categoryName);
        }

        private void LoadHeaders(string categoryName)
        {
            var allHeaders = _articleService.GetArticleHeadersByCategoryName(categoryName);
            headers.Children.Clear();
            headers.Visibility = Visibility.Visible;

            for (int i = 0; i < allHeaders.Count(); i++)
            {
                var rb = new RadioButton() { Content = allHeaders.ElementAt(i) };

                rb.Tag = i + 1;
                rb.Click += LoadArticle;

                rb.Style = (Style)FindResource("headerButton");

                headers.Children.Add(rb);
            }
        }

        private void LoadArticle(object sender, RoutedEventArgs e)
        {
            var articleHeader = (string)((RadioButton)sender).Content;
            LoadArticle(articleHeader);
        }

        private void LoadArticle(string articleHeader)
        {
            articleBlock.Children.Clear();
            scrollerBlock.Visibility = Visibility.Visible;

            headers.Children.Clear();
            headers.Visibility = Visibility.Hidden;

            var article = _articleService.GetArticleByTitle(articleHeader, SelectedCategoryId);

            FlowDocument myFlowDoc = new FlowDocument();

            myFlowDoc.Blocks.Add(new Paragraph(new Run(article.Text)));
            
            RichTextBox myRichTextBox = new RichTextBox() { IsReadOnly = true, FontSize = 16 };
            myRichTextBox.Background = Brushes.Transparent;
            myRichTextBox.BorderBrush = Brushes.Transparent;

            myRichTextBox.Document = myFlowDoc;

            var test = new Image()
            {
                Width = 400,
                Height = 300,
                Source = new BitmapImage(new Uri(@"../Images/Materials/test3.png", UriKind.RelativeOrAbsolute))
            };

            articleBlock.Children.Add(myRichTextBox);
            articleBlock.Children.Add(test);
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }
        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            //if role == user
            UserProfile userProfile = new UserProfile(_categoryService, _articleService);
            userProfile.Show();
            Hide();


            //if role == admin
            /*AdminProfileView adminProfile = new AdminProfileView();
            adminProfile.Show();
            Hide();*/
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }
    }
}
