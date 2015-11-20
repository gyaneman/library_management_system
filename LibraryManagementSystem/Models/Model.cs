using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace LibraryManagementSystem.Models
{
    enum Result
    {
        Success,
        Failed
    }

    class Model
    {
        enum State
        {
            New,        // 新しいオブジェクト
            Recorded,   // DBから取得・DBに保存してから、まだ変更されていない状態
            Edited      // 変更されてからまだ保存されていない状態
        }

        static string dbPath = "c:\\Users\\kataoka\\Application\\dbdata\\library_management_system.sqlite3";
        protected static string dbConStr = "Data Source=" + dbPath + ";Version=3;";

        State state = State.New;
        private string id;
        private string created_at;
        private string edited_at;

        public Model()
        {
            this.state = State.New;
        }

        public Model(string _id, string _created_at, string _edited_at)
        {
            this.id = _id;
            this.state = State.Recorded;
            this.created_at = _created_at;
            this.edited_at = _edited_at;
        }

        public string Id
        {
            set { }
            get
            {
                return id;
            }
        }

        public string CreatedAt
        {
            set { }
            get
            {
                return this.created_at;
            }
        }

        public string EditedAt
        {
            set { }
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
