using BusinessLogicLayer.Authentication;
using BusinessLogicLayer.Enums;
using BusinessLogicLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WarNotes.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        protected readonly IUserService _userService;
        protected readonly IAuthenticator _authenticator;
        public string TextArticle { get; set; }
        public MainView(IUserService userService, IAuthenticator authenticator)
        {
            InitializeComponent();
            LoadCategories();
            LoadHeaders();
            _userService = userService;
            _authenticator = authenticator;   
        }
        private void LoadCategories()
        {
            for (int i = 0; i < 4; i++)
            {
                RadioButton rb = new RadioButton() { Content = "Radio button " + i, IsChecked = i == 0 };
                rb.Checked += (sender, args) => {
                    Console.WriteLine("Pressed " + (sender as RadioButton).Tag);
                };
                rb.Unchecked += (sender, args) => { /* Do stuff */ };
                rb.Tag = i;

                categories.Children.Add(rb);

                
            }
        }
        private void LoadHeaders()
        {
            for (int i = 0; i < 4; i++)
            {
                Button rb = new Button() { Content = "Radio button " + i};
                rb.Click += LoadArticles; 
                
                rb.Tag = i;

                headers.Children.Add(rb);


            }
        }
        private void LoadArticles(object sender, RoutedEventArgs e)
        {
            TextArticle = "jdhbfjhdbfjhbfkgjbnkfjgnbkjfngkbjfnkgnbfkhbgjhbfjghb jgbjfhg jgbjf gjhr gbhr gtbh rgh jrhg rh bir igh brjh gbjhr gbr hb rhg bjr brh gjh rg brh gbbrh gbj ";
            this.txtArt.Text = TextArticle;
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
                UserProfile userProfile = new UserProfile(_userService, _authenticator);
                userProfile.Show();
                Hide();
            }

            if (_authenticator.CurrentAccount.Role is Role.Admin)
            {
                AdminProfileView adminProfile = new AdminProfileView(_userService, _authenticator);
                adminProfile.Show();
                Hide();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView(_userService, _authenticator);
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
