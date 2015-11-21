using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    class User: Model
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
    }
}
