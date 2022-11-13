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
using System.Media;
using System.Collections.Generic;
using BusinessLogicLayer.DTO;

namespace WarNotes.View
{
    public partial class MainView : Window
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;

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
            headers.Visibility = Visibility.Hidden;
            scrollerBlock.Visibility = Visibility.Visible;

            var title = new Label() { Content = articleHeader, FontSize = 23, HorizontalAlignment = HorizontalAlignment.Center };
            articleBlock.Children.Add(title);

            var article = _articleService.GetArticleByTitle(articleHeader, SelectedCategoryId);
            article.Text = article.Text.Replace(@"\n", "\n");
            article.Text = article.Text.Replace(@"\t", "\t");

            var blocksOfText = article.Text.Split("(ФОТО:");

            foreach(var block in blocksOfText)
            {
                var text = GetPhotos(block, article);
                var soundButtons = GetSounds(ref text);

                DisplayArticle(text);

                foreach (var sound in soundButtons)
                    articleBlock.Children.Add(sound);
            }

            var buttonBack = new Button { Content = "Назад", Style = (Style)FindResource("backButton") };
            buttonBack.Click += BackClicked;


            var img = new Image()
            {
                Width = 23,
                Height = 23,
                Source = new BitmapImage(new Uri($"../Images/like.png", UriKind.RelativeOrAbsolute))
            };

            var img2 = new Image()
            {
                Width = 23,
                Height = 23,
                Source = new BitmapImage(new Uri($"../Images/buttonSave.png", UriKind.RelativeOrAbsolute))
            };

            articleBlock.Children.Add(img);
            articleBlock.Children.Add(img2);

            articleBlock.Children.Add(buttonBack);
            
        }

        private void DisplayArticle(string text)
        {
            var myFlowDoc = new FlowDocument();
            myFlowDoc.Blocks.Add(new Paragraph(new Run(text)));

            var myRichTextBox = new RichTextBox() { IsReadOnly = true, FontSize = 16 };
            myRichTextBox.Background = Brushes.Transparent;
            myRichTextBox.BorderBrush = Brushes.Transparent;

            myRichTextBox.Document = myFlowDoc;

            articleBlock.Children.Add(myRichTextBox);
        }

        private void SoundPlay(object sender, RoutedEventArgs e)
        {
            var btnSender = (Button)sender;
            var soundName = btnSender.Content;

            var path = $"../../../Sounds/{soundName}.wav";
            var player = new SoundPlayer(path);

            player.Play();
        }

        private string GetPhotos(string text, ArticleDTO article)
        {
            if (text.Contains("photo"))
            {
                int pFrom = text.IndexOf("photo");
                int pTo = text.LastIndexOf(".png") + ".png".Length;

                var photoName = text.Substring(pFrom, pTo - pFrom);
                text = text.Replace(photoName + ")", "");

                var img = new Image()
                {
                    Width = 400,
                    Height = 300,
                    Source = new BitmapImage(new Uri($"../Images/Materials/Category{article.CategoryId}/{photoName}", UriKind.RelativeOrAbsolute))
                };

                articleBlock.Children.Add(img);
            }

            return text;
        }

        private List<Button> GetSounds(ref string text)
        {
            var soundButtons = new List<Button>();

            if (text.Contains("(SOUND:"))
            {
                var innerBlocks = text.Split("(SOUND:");
                for (int i = 1; i < innerBlocks.Length; i++)
                {
                    int pFrom = 0;
                    int pTo = innerBlocks[i].LastIndexOf(".wav") + ".wav".Length;

                    var soundName = innerBlocks[i].Substring(pFrom, pTo - pFrom);
                    text = text.Replace("(SOUND:" + soundName + ")", "");

                    var soundBtn = new Button() 
                    {
                        Content = soundName.Replace(".wav", ""),
                        Style = (Style)FindResource("soundButton") 
                    };

                    soundBtn.Click += SoundPlay;
                    soundButtons.Add(soundBtn);
                }
            }

            return soundButtons;
        }

        private async void BackClicked(object sender, RoutedEventArgs e)
        {
            articleBlock.Children.Clear();
            headers.Visibility = Visibility.Visible;
            scrollerBlock.Visibility = Visibility.Hidden;

            var categoryName = await _categoryService.GetCategoryNameById(SelectedCategoryId);
            LoadHeaders(categoryName);
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
        private void LikeButton_Click(object sender, RoutedEventArgs e) 
        {
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
        }

    }
}
