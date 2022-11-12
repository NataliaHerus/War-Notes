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
        public string TextArticle { get; set; }
        public MainView()
        {
            InitializeComponent();
            LoadCategories();
            LoadHeaders();
            
        }
        private void LoadCategories()
        {
            for (int i = 0; i < 4; i++)
            {
                RadioButton rb = new RadioButton() { Content = "Category " + i };

                rb.Tag = i;

                rb.Style = (Style)FindResource("categoryButton");

                categories.Children.Add(rb);
            }
        }
        private void LoadHeaders()
        {
            for (int i = 0; i < 9; i++)
            {
                RadioButton rb = new RadioButton() { Content = "Header " + i };

                rb.Tag = i;

                rb.Style = (Style)FindResource("headerButton");

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
            //if role == user
            UserProfile userProfile = new UserProfile();
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
