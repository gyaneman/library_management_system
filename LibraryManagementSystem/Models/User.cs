using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Security.Cryptography;

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
        private string hashedPassword = null;

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
            string _hashedPassword,
            string _created_at, 
            string _edited_at
            ) : base(_id, _created_at, _edited_at)
        {
            name = _name;
            email = _email;
            hashedPassword = _hashedPassword;
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
                this.hashedPassword = value;
            }
            get
            {
                return this.hashedPassword;
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
                        User user = new User(
                            reader["id"].ToString(),
                            reader["name"].ToString(),
                            reader["email"].ToString(),
                            reader["password"].ToString(),
                            reader["created_at"].ToString(),
                            reader["edited_at"].ToString()
                            );
                        users.Add(user);
                    }
                }
            }
            return users;
        }

        public bool CheckPassword(string pass)
        {
            var hash = GetSha256(pass);
            if (this.hashedPassword == hash)
            {
                return true;
            }
            return false;
        }

        private static string GetSha256(string target)
        {
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] byteValue = Encoding.UTF8.GetBytes(target);
            byte[] hash = mySHA256.ComputeHash(byteValue);

            StringBuilder buf = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                buf.AppendFormat("{0:x2}", hash[i]);
            }
            return buf.ToString();
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
            if (hashedPassword != null)
            {
                Console.WriteLine(hashedPassword);
            }
        }
    }
}
