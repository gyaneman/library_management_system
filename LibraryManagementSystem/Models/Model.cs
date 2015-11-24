using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace LibraryManagementSystem.Models
{
    public enum Result
    {
        Success,
        Failed
    }

    public class Model
    {
        enum State
        {
            New,        // 新しいオブジェクト
            Recorded,   // DBから取得・DBに保存してから、まだ変更されていない状態
            Edited      // 変更されてからまだ保存されていない状態
        }

        static string dbPath = "c:\\Users\\kataoka\\Application\\dbdata\\library_management_system.sqlite3";
        protected static string dbConStr = "Data Source=" + dbPath + ";Version=3;";
        State state;
        private string id;
        private string created_at;
        private string edited_at;

        public Model()
        {
            this.state = State.New;
        }

        protected Model(string _id, string _created_at, string _edited_at)
        {
            this.id = _id;
            this.state = State.Recorded;
            this.created_at = _created_at;
            this.edited_at = _edited_at;
        }

        public static void InitDB()
        {
            using (SQLiteConnection cn = new SQLiteConnection(dbConStr))
            using (SQLiteCommand cmd = cn.CreateCommand())
            {
                string[] tableNames = { "user", "series", "book", "lending_history" };

                string tableCheckStr =
                    @"select * from sqlite_master where type='table' and name='user';";
                string createUserTableStr =
                    @"CREATE TABLE user ( `id`	INTEGER NOT NULL, `name`	TEXT NOT NULL UNIQUE,   `email`	TEXT NOT NULL UNIQUE,   `password`	TEXT NOT NULL UNIQUE,   `created_at`	TEXT,   `edited_at`	TEXT,   PRIMARY KEY(id) ); ";
                string createCategoryTableStr =
                    @"CREATE TABLE `series` (	`id`	INTEGER NOT NULL,	`name`	TEXT NOT NULL UNIQUE,	PRIMARY KEY(id));";
                string createBookTableStr =
                    @"CREATE TABLE book (`id` INTEGER NOT NULL, `isbn` INTEGER UNIQUE, `title` TEXT NOT NULL UNIQUE, `author` TEXT, `publisher`	TEXT, `series` INTEGER,	`created_at` TEXT, `edited_at` TEXT, PRIMARY KEY(id), FOREIGN KEY(`series`) REFERENCES series(id)); ";
                string createLendingHistoryTableStr =
                    @"CREATE TABLE lending_history (	`id`	INTEGER,	`user_id`	INTEGER NOT NULL,	`book_id`	INTEGER NOT NULL,	`return_date`	TEXT,	`completion_date`	TEXT,	`created_at`	TEXT NOT NULL,	`edited_at`	TEXT NOT NULL,	PRIMARY KEY(id),    FOREIGN KEY(`user_id`) REFERENCES user(id),    FOREIGN KEY(`book_id`) REFERENCES book(id)); ";
                cn.Open();
                
                bool flag = false;
                foreach (string tableName in tableNames)
                {
                    Console.WriteLine("Checking table: " + tableName);
                    cmd.CommandText = tableCheckStr;
                    cmd.Parameters.Add(new SQLiteParameter("@TABLE_NAME", tableName));
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("No table " + tableName);
                            flag = true;
                        }
                    }
                }

                if (!flag)
                {
                    Console.WriteLine("CREATE TABLES");
                    cmd.CommandText = createUserTableStr;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = createCategoryTableStr;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = createBookTableStr;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = createLendingHistoryTableStr;
                    cmd.ExecuteNonQuery();
                }
                cn.Close();
            }
        }

        public string Id
        {
            get
            {
                return id;
            }
        }

        public string CreatedAt
        {
            get
            {
                return this.created_at;
            }
        }

        public string EditedAt
        {
            get
            {
                return this.edited_at;
            }
        }

        protected void Edited()
        {
            state = State.Edited;
        }

        protected void Update()
        {
            state = State.Recorded;
        }
    }
}
