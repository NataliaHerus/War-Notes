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
    }
}