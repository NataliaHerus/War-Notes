using System;
using System.Windows.Controls;
﻿using BusinessLogicLayer.Authentication;
using BusinessLogicLayer.Enums;
using BusinessLogicLayer.Services.Interfaces;
using System.Collections.Generic;
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
using BusinessLogicLayer.DTO;

namespace WarNotes.View
{
    public partial class MainView : Window
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;
        private readonly IAuthenticator _authenticator;

        public int SelectedCategoryId { get; set; }
        public int SelectedArticleId { get; set; }

        public MainView(
            ICategoryService categoryService,
            IArticleService articleService,
            IUserService userService,
            IAuthenticator authenticator)
        {
            _categoryService = categoryService;
            _articleService = articleService;
            _userService = userService;
            _authenticator = authenticator;

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

            SelectedArticleId = article.Id;

            var blocksOfText = article.Text.Split("(ФОТО:");

            foreach(var block in blocksOfText)
            {
                var text = GetPhotos(block, article);
                var soundButtons = GetSounds(ref text);

                DisplayArticle(text);

                foreach (var sound in soundButtons)
                    articleBlock.Children.Add(sound);
            }

            DisplayButtons();
            DisplayStatistic();
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

        private void DisplayButtons()
        {
            var buttonBack = new Button { Content = "Назад", Style = (Style)FindResource("backButton") };
            buttonBack.Click += BackClicked;

            var userId = _authenticator.CurrentAccount.Id;
            string buttonPath;

            if (_articleService.ArticleIsLikedByUserId(userId, SelectedArticleId))
                buttonPath = $"../Images/likePressed.png";
            else
                buttonPath = $"../Images/like.png";

            var likeButton = new Image()
            {
                Width = 31,
                Height = 31,
                Source = new BitmapImage(new Uri(buttonPath, UriKind.RelativeOrAbsolute))
            };

            if (_articleService.ArticleIsSavedByUserId(userId, SelectedArticleId))
                buttonPath = $"../Images/savePressed.png";
            else
                buttonPath = $"../Images/save.png";

            var saveButton = new Image()
            {
                Width = 31,
                Height = 31,
                Source = new BitmapImage(new Uri(buttonPath, UriKind.RelativeOrAbsolute))
            };


            likeButton.MouseLeftButtonDown += LikeClicked;
            saveButton.MouseLeftButtonDown += SaveClicked;

            articleBlock.Children.Add(likeButton);
            articleBlock.Children.Add(saveButton);

            articleBlock.Children.Add(buttonBack);
        }

        private void DisplayStatistic()
        {
            int likes = _articleService.GetCountOfLikes(SelectedArticleId);
            int saves = _articleService.GetCountOfSaves(SelectedArticleId);

            var likesLabel = new Label { Content = $"Кількість вподобань: {likes}", FontSize = 17 };
            var savesLabel = new Label { Content = $"Кількість збережень: {saves}", FontSize = 17 };

            articleBlock.Children.Add(likesLabel);
            articleBlock.Children.Add(savesLabel);
        }

        private void LikeClicked(object sender, RoutedEventArgs e)
        {
            var userId = _authenticator.CurrentAccount.Id;

            if (_articleService.ArticleIsLikedByUserId(userId, SelectedArticleId))
                _articleService.DeleteLikedArticle(userId, SelectedArticleId);
            else
                _articleService.AddLikedArticle(userId, SelectedArticleId);

            var articleHeader = _articleService.GetArticleTitleById(SelectedArticleId);
            LoadArticle(articleHeader);
        }

        private void SaveClicked(object sender, RoutedEventArgs e)
        {
            var userId = _authenticator.CurrentAccount.Id;

            if (_articleService.ArticleIsSavedByUserId(userId, SelectedArticleId))
                _articleService.DeleteSavedArticle(userId, SelectedArticleId);
            else
                _articleService.AddSavedArticle(userId, SelectedArticleId);

            var articleHeader = _articleService.GetArticleTitleById(SelectedArticleId);
            LoadArticle(articleHeader);
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
            if (_authenticator.CurrentAccount.Role is Role.User)
            {
                UserProfile userProfile = new UserProfile(_categoryService, _articleService, _userService, _authenticator);
                userProfile.Show();
                Hide();
            }

            if (_authenticator.CurrentAccount.Role is Role.Admin)
            {
                AdminProfileView adminProfile = new AdminProfileView(_categoryService, _articleService, _userService, _authenticator);
                adminProfile.Show();
                Hide();
            }
        }
        
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView(_userService, _categoryService, _articleService, _authenticator);
            _authenticator.Logout();
            loginView.Show();
            Hide();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }
    }
}
