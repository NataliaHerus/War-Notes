using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Text;

namespace ConsoleAppAdoNet
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddUserSecrets<Program>()
            .Build();
            var connectionString = config.GetConnectionString("WarNotesDB");

            PrintAllData(connectionString);
            FillAllData(config);
            Console.ReadLine();
        }

        public static void FillAllData(IConfigurationRoot config)
        {
            var insertDataQuery = new StringBuilder("INSERT INTO users (firstname, lastname, email, password, role, isblocked) VALUES ");
            for (int i = 0; i < 30; i++)
            {
                insertDataQuery.Append($"('First name {i + 1}', 'Last name {i + 1}', 'Email {i + 1}', 'Password {i + 1}', 1, {i % 2 == 0})");
                insertDataQuery.Append(i + 1 != 30 ? ", " : ";");
            }

            Fill(insertDataQuery.ToString(), config);

            insertDataQuery = new StringBuilder("INSERT INTO categories (categoryname) VALUES ");
            for (int i = 0; i < 30; i++)
            {
                insertDataQuery.Append($"('Category {i + 1}')");
                insertDataQuery.Append(i + 1 != 30 ? ", " : ";");
            }

            Fill(insertDataQuery.ToString(), config);

            insertDataQuery = new StringBuilder("INSERT INTO articles (createdat, editedat, text, categoryid) VALUES ");
            for (int i = 0; i < 30; i++)
            {
                insertDataQuery.Append($"('{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}', '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}', 'Text {i + 1}', 1)");
                insertDataQuery.Append(i + 1 != 30 ? ", " : ";");
            }

            Fill(insertDataQuery.ToString(), config);

            insertDataQuery = new StringBuilder("INSERT INTO liked_articles (userid, articleid) VALUES ");
            for (int i = 0; i < 30; i++)
            {
                insertDataQuery.Append($"({i + 1}, {i + 1})");
                insertDataQuery.Append(i + 1 != 30 ? ", " : ";");
            }

            Fill(insertDataQuery.ToString(), config);

            insertDataQuery = new StringBuilder("INSERT INTO saved_articles (userid, articleid) VALUES ");
            for (int i = 0; i < 30; i++)
            {
                insertDataQuery.Append($"({i + 1}, {i + 1})");
                insertDataQuery.Append(i + 1 != 30 ? ", " : ";");
            }

            Fill(insertDataQuery.ToString(), config);
        }

        public static void Fill(string insertDataQuery, IConfigurationRoot config)
        {
            NpgsqlConnectionStringBuilder sb = new NpgsqlConnectionStringBuilder();
            sb.Host = config["Host"];
            sb.Port = int.Parse(config["Port"]);
            sb.Username = config["Username"];
            sb.Password = config["Password"];
            sb.Database = config["Database"];

            using (NpgsqlConnection conn = new NpgsqlConnection(sb.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(insertDataQuery, conn))
                {
                    cmd.Parameters.AddWithValue("USER", sb.Username);
                    cmd.Parameters.AddWithValue("PW", sb.Password);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void PrintAllData(string connectionString)
        {
            PrintAllUsers(connectionString);
            PrintAllCategories(connectionString);
            PrintAllArticles(connectionString);
            PrintAllLikedArticles(connectionString);
            PrintAllSavedArticles(connectionString);
        }

        public static void PrintAllUsers(string connectionString)
        {
            var users = new List<User>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string querry = "SELECT * FROM users";
                NpgsqlCommand command = new(querry, connection);
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string firstName = reader.GetString(1);
                        string lastName = reader.GetString(2);
                        string email = reader.GetString(3);
                        string password = reader.GetString(4);
                        int role = reader.GetInt32(5);
                        bool isBlocked = reader.GetBoolean(6);

                        var user = new User { Id = id, Role = role, FirstName = firstName, LastName = lastName, Email = email, Password = password, IsBlocked = isBlocked };
                        users.Add(user);
                    }
                }

                foreach (var user in users)
                    Console.WriteLine(user);
            }

            Console.WriteLine("\n=================================================\n");
        }

        public static void PrintAllCategories(string connectionString)
        {
            var categories = new List<Category>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string querry = "SELECT * FROM categories";
                NpgsqlCommand command = new(querry, connection);
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string categoryName = reader.GetString(1);

                        var category = new Category { Id = id, CategoryName = categoryName };
                        categories.Add(category);
                    }
                }

                foreach (var category in categories)
                    Console.WriteLine(category);
            }

            Console.WriteLine("\n=================================================\n");
        }

        public static void PrintAllArticles(string connectionString)
        {
            var articles = new List<Article>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string querry = "SELECT * FROM articles";
                NpgsqlCommand command = new(querry, connection);
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        DateTime createdAt = reader.GetDateTime(1);
                        DateTime editedAt = reader.GetDateTime(2);
                        string text = reader.GetString(3);
                        int categoryId = reader.GetInt32(4);

                        var article = new Article { Id = id, CreatedAt = createdAt, EditedAt = editedAt, Text = text, CategoryId = categoryId };
                        articles.Add(article);
                    }
                }

                foreach (var article in articles)
                    Console.WriteLine(article);
            }

            Console.WriteLine("\n=================================================\n");
        }

        public static void PrintAllLikedArticles(string connectionString)
        {
            var likedArticles = new List<LikedArticles>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string querry = "SELECT * FROM liked_articles";
                NpgsqlCommand command = new(querry, connection);
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        int userId = reader.GetInt32(1);
                        int articleId = reader.GetInt32(2);

                        var likedArticle = new LikedArticles { Id = id, UserId = userId, ArticleId = articleId };
                        likedArticles.Add(likedArticle);
                    }
                }

                foreach (var likedArticle in likedArticles)
                    Console.WriteLine(likedArticle);
            }

            Console.WriteLine("\n=================================================\n");
        }

        public static void PrintAllSavedArticles(string connectionString)
        {
            var savedArticles = new List<SavedArticles>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string querry = "SELECT * FROM saved_articles";
                NpgsqlCommand command = new(querry, connection);
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        int userId = reader.GetInt32(1);
                        int articleId = reader.GetInt32(2);

                        var savedArticle = new SavedArticles { Id = id, UserId = userId, ArticleId = articleId };
                        savedArticles.Add(savedArticle);
                    }
                }

                foreach (var savedArticle in savedArticles)
                    Console.WriteLine(savedArticle);
            }

            Console.WriteLine("\n=================================================\n");
        }
    }
}