using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace LibraryManagementSystem.Models
{
    class LendingHistory: Model
    {
        /// <summary>
        /// テーブル名
        /// </summary>
        private static string TABLE_NAME = "lending_history";

        /// <summary>
        /// 貸したユーザ
        /// </summary>
        private User user = null;

        /// <summary>
        /// 貸した本
        /// </summary>
        private Book book = null;

        /// <summary>
        /// 返却予定日
        /// </summary>
        private string returnDate = null;

        /// <summary>
        /// 返却した日 未返却はnull
        /// </summary>
        private string completionDate = null;

        public LendingHistory() : base()
        { }

        /// <summary>
        /// DBから履歴取得時にインスタンス初期化するため
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_user"></param>
        /// <param name="_book"></param>
        /// <param name="_returnDate"></param>
        /// <param name="_completionDate"></param>
        /// <param name="_created_at"></param>
        /// <param name="_edited_at"></param>
        private LendingHistory(
            string _id,
            User _user,
            Book _book,
            string _returnDate,
            string _completionDate,
            string _created_at,
            string _edited_at
            ) : base(_id, _created_at, _edited_at)
        {
            this.user = _user;
            this.book = _book;
            this.returnDate = _returnDate;
            this.completionDate = _completionDate;
        }

        public static List<LendingHistory> GetUnreturnedBookOfUser(User _user)
        {
            List<LendingHistory> result = new List<LendingHistory>();
            using (SQLiteConnection cn = new SQLiteConnection(dbConStr))
            {
                cn.Open();
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM " + TABLE_NAME
                    //+ " INNER JOIN user ON lending_history.user_id"
                    + " INNER JOIN book ON lending_history.book_id"
                    + " WHERE completion_date IS NULL"
                    + " AND user_id = @USER_ID"
                    + ";";

                cmd.Parameters.Add(new SQLiteParameter("@USER_ID", _user.Id));
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        /*User user = new User(
                            reader["user.id"].ToString(),
                            reader["user.name"].ToString(),
                            reader["user.email"].ToString(),
                            reader["user.password"].ToString(),
                            reader["user.created_at"].ToString(),
                            reader["user.edited_at"].ToString()
                            );*/
                        Book book = new Book(
                            reader["book.id"].ToString(),
                            reader["book.isbn"].ToString(),
                            reader["book.title"].ToString(),
                            reader["book.author"].ToString(),
                            reader["book.publisher"].ToString(),
                            reader["book.series"].ToString(),
                            reader["book.created_at"].ToString(),
                            reader["book.edited_at"].ToString()
                            );
                        LendingHistory history = new LendingHistory(
                            reader["lending_history.id"].ToString(),
                            _user,
                            book,
                            reader["lending_history.return_date"].ToString(),
                            reader["lending_history.completion_date"].ToString(),
                            reader["lending_history.created_at"].ToString(),
                            reader["lending_history.edited_at"].ToString()
                            );
                        result.Add(history);
                    }
                }
            }
            return result;
        }
    }
}
