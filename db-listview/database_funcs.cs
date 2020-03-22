using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace db_listview
{
    class database_funcs
    {
        private static readonly string sConnStr = new NpgsqlConnectionStringBuilder
        {
            Host = Database.Default.Host,
            Port = Database.Default.Port,
            Database = Database.Default.Name,
            Username = Environment.GetEnvironmentVariable("POSTGRESQL_USERNAME"),
            Password = Environment.GetEnvironmentVariable("POSTGRESQL_PASSWORD"),
            AutoPrepareMinUsages = 2,
            MaxAutoPrepare = 10
        }.ConnectionString;
        public static bool IsLoginExists(string login)
        {
            using (var sConn = new NpgsqlConnection(sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = $@"SELECT COUNT(*) FROM users WHERE lower(@currentLogin) = lower(login);"
                };
                sCommand.Parameters.AddWithValue("@currentLogin", login);
                return (long)sCommand.ExecuteScalar() > 0;
            }
        }
        public static (long, string, string) AddUser(string login, string password, DateTime reg_date)
        {
            byte[] salt = login_and_password.GetSalt();
            string salt_str = Convert.ToBase64String(salt);
            string hash_str = Convert.ToBase64String(login_and_password.GetHash(password, salt));
            using (var sConn = new NpgsqlConnection(sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = $@"INSERT INTO users (login, password_hash, salt, reg_date) VALUES (@login, @password_hash, @salt, @reg_date) RETURNING user_id"
                };
                sCommand.Parameters.AddWithValue("@login", login);
                sCommand.Parameters.AddWithValue("@password_hash", hash_str);
                sCommand.Parameters.AddWithValue("@salt", salt_str);
                sCommand.Parameters.AddWithValue("@reg_date", reg_date);
                return ((long)sCommand.ExecuteScalar(), hash_str, salt_str);
            }
        }
        public static (string, string) UpdateUser(long id, string login, string password, DateTime reg_date)
        {
            byte[] salt = login_and_password.GetSalt();
            string salt_str = Convert.ToBase64String(salt);
            string hash_str = Convert.ToBase64String(login_and_password.GetHash(password, salt));
            using (var sConn = new NpgsqlConnection(sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = $@"UPDATE users
                                    SET login = @login, password_hash = @password_hash, salt = @salt, reg_date = @reg_date
                                    WHERE user_id = @user_id"
                };
                sCommand.Parameters.AddWithValue("@user_id", id);
                sCommand.Parameters.AddWithValue("@login", login);
                sCommand.Parameters.AddWithValue("@password_hash", hash_str);
                sCommand.Parameters.AddWithValue("@salt", salt_str);
                sCommand.Parameters.AddWithValue("@reg_date", reg_date);
                sCommand.ExecuteNonQuery(); 
                return (hash_str, salt_str);
            }
        }
        public static void InitialiseLV(ListView listview_db)
        {
            listview_db.Clear();
            listview_db.Columns.Add("Логин");
            listview_db.Columns.Add("Хеш пароля");
            listview_db.Columns.Add("Соль");
            listview_db.Columns.Add("Дата регистрации");
            using(var sConn = new NpgsqlConnection(sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = @"SELECT user_id, login, password_hash, salt, reg_date FROM users"
                };
                var reader = sCommand.ExecuteReader();
                while(reader.Read())
                {
                    var lvi = new ListViewItem(new[]
                    {
                        (string) reader["login"],
                        (string) reader["password_hash"],
                        (string) reader["salt"],
                        ((DateTime) reader["reg_date"]).ToLongDateString()
                    })
                    {
                        Tag = Tuple.Create((long)reader["user_id"], (DateTime)reader["reg_date"])
                    };
                    listview_db.Items.Add(lvi);
                }
            }
            listview_db.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listview_db.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
    }
}
