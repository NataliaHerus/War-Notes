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
    public partial class AllUsersView : Window
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;

        public AllUsersView(ICategoryService categoryService, IArticleService articleService)
        {
            InitializeComponent();
            _categoryService = categoryService;
            _articleService = articleService;

            List<UserForAdminDTO> users = new List<UserForAdminDTO>();
            users.Add(new UserForAdminDTO() {FirstName = "Анна", LastName = "Берко", Email = "berko@gmail.com" });
            users.Add(new UserForAdminDTO() { FirstName = "Олег", LastName = "Сопко", Email = "osopko22@gmail.com" });
            users.Add(new UserForAdminDTO() { FirstName = "Марина", LastName = "Сертинюк", Email = "sert@gmail.com" });
            users.Add(new UserForAdminDTO() { FirstName = "Павло", LastName = "Рак", Email = "pavlo@gmail.com" });
            users.Add(new UserForAdminDTO() { FirstName = "Марта", LastName = "Коваль", Email = "koval@gmail.com" });
            users.Add(new UserForAdminDTO() { FirstName = "Анна", LastName = "Берко", Email = "berko@gmail.com" });
            users.Add(new UserForAdminDTO() { FirstName = "Олег", LastName = "Сопко", Email = "osopko22@gmail.com" });
            users.Add(new UserForAdminDTO() { FirstName = "Марина", LastName = "Сертинюк", Email = "sert@gmail.com" });
            users.Add(new UserForAdminDTO() { FirstName = "Павло", LastName = "Рак", Email = "pavlo@gmail.com" });
            users.Add(new UserForAdminDTO() { FirstName = "Марта", LastName = "Коваль", Email = "koval@gmail.com" });

            AllUsersList.ItemsSource = users;
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            AdminProfileView exit = new AdminProfileView(_categoryService, _articleService);
            exit.Show();
            Hide();
        }
    }
}
