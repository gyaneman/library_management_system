using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace LibraryManagementSystem.Models
{
    public class User: Model
    {
        /// <summary>
        /// DBのテーブル名
        /// </summary>
        private static string TABLE_NAME = "user";

        /// <summary>
        /// パラメータ（これはDBにも保存される）
        /// </summary>
        private string name = null;
        private string email = null;
        private string password = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public User() { }
        
        /// <summary>
        /// コンストラクタ
        /// DBからのインスタンスを作った時に使う
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_name"></param>
        /// <param name="_email"></param>
        /// <param name="_created_at"></param>
        /// <param name="_edited_at"></param>
        private User(
            string _id, 
            string _name, 
            string _email, 
            string _created_at, 
            string _edited_at
            ) : base(_id, _created_at, _edited_at)
        {
            name = _name;
            email = _email;
        }

        public string Name
        {
            set
            {
                this.name = value;
                Edited();
            }
            get
            {
                return this.name;
            }
        }

        public string Email
        {
            set
            {
                this.email = value;
            }
            get
            {
                return this.email;
            }
        }

        public string Password
        {
            set
            {
                this.password = value;
            }
            get
            {
                return this.password;
            }
        }


        /// <summary>
        /// DBに存在するユーザデータを全て取得してユーザリストにして返す
        /// </summary>
        /// <returns>ユーザのリスト</returns>
        public static List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            using (SQLiteConnection cn = new SQLiteConnection(dbConStr))
            {
                cn.Open();
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM " + TABLE_NAME;
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User (
                            reader["id"].ToString(),
                            reader["name"].ToString(),
                            reader["email"].ToString(),
                            reader["created_at"].ToString(),
                            reader["edited_at"].ToString()
                            );
                        users.Add(user);
                    }
                }
            }
            return users;
        }

        /// <summary>
        /// デバッグ用のパラメータ出力
        /// </summary>
        public void Show()
        {
            if (name != null)
            {
                Console.WriteLine(name);
            }
            if (email != null)
            {
                Console.WriteLine(email);
            }
        }
    }
}
