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
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {
        public RegisterView()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            string last_name = txtLast.Text.Trim();
            string email = txtMail.Text.Trim();
            string pass = txtPass.Password.Trim().ToLower();
            string pass2 = txtPass2.Password.Trim().ToLower();
            if (name.Length < 2)
            {
                txtName.ToolTip = "Ім'я мусить містити не менше 2 символів!";
                txtName.Background = Brushes.DarkRed;
            }
            else if (last_name.Length < 2)
            {
                txtLast.ToolTip = "Прізвище мусить містити не менше 2 символів!";
                txtLast.Background = Brushes.DarkRed;
            }
            else if (pass.Length < 6)
            {
                txtPass.ToolTip = "Пароль мусить містити не менше 2 символів!";
                txtPass.Background = Brushes.DarkRed;
            }
            else if (pass != pass2)
            {
                txtPass.ToolTip = "Паролі не співпадають!";
                txtPass.Background = Brushes.DarkRed;
            }
            else if (email.Length < 10)
            {
                txtMail.ToolTip = "E-mail мусить містити не менше 2 символів!";
                txtMail.Background = Brushes.DarkRed;
            }
            else if (!email.Contains("@"))
            {
                txtMail.ToolTip = "E-mail мусить містити знак '@'";
                txtMail.Background = Brushes.DarkRed;
            }
            else if (!email.Contains("."))
            {
                txtMail.ToolTip = "E-mail мусить містити крапку";
                txtMail.Background = Brushes.DarkRed;
            }
            else
            {
                txtName.ToolTip = "";
                txtName.Background = Brushes.Transparent;
                txtLast.ToolTip = "";
                txtLast.Background = Brushes.Transparent;
                txtPass.ToolTip = "";
                txtPass.Background = Brushes.Transparent;
                txtPass2.ToolTip = "";
                txtPass2.Background = Brushes.Transparent;
                txtMail.ToolTip = "";
                txtMail.Background = Brushes.Transparent;
                MessageBox.Show("Працює!!!");
            }
        }
    }
}
