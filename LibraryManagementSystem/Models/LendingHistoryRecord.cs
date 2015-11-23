using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace LibraryManagementSystem.Models
{
    class LendingHistoryRecord: Model
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

        public LendingHistoryRecord() : base()
        { }

        /// <summary>
        /// userのgettersetter
        /// </summary>
        public User LendingUser
        {
            get
            {
                return user;
            }
        }

        /// <summary>
        /// bookのgetter setter
        /// </summary>
        public Book LentBook
        {
            get
            {
                return book;
            }
        }

        /// <summary>
        /// 返却予定日のgetter settter
        /// </summary>
        public string DueDate
        {
            get
            {
                return returnDate;
            }
            set
            {
                returnDate = value;
            }
        }

        /// <summary>
        /// 返却完了日の日
        /// </summary>
        public string CompletionDate
        {
            get
            {
                return completionDate;
            }
            set
            {
                completionDate = value;
            }
        }

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
        private LendingHistoryRecord(
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

        public static List<LendingHistoryRecord> GetUnreturnedBookOfUser(User _user)
        {
            List<LendingHistoryRecord> result = new List<LendingHistoryRecord>();
            using (SQLiteConnection cn = new SQLiteConnection(dbConStr))
            {
                cn.Open();
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM " + TABLE_NAME
                    //+ " INNER JOIN user ON lending_history.user_id"
                    + " INNER JOIN book ON lending_history.book_id = book.id"
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
                        Book _book = new Book(
                        reader[7].ToString(),   // id
                        reader[8].ToString(),   // isbn
                        reader[9].ToString(),   // title
                        reader[10].ToString(),  // author
                        reader[11].ToString(),  // publisher
                        reader[12].ToString(),  // series
                        reader[13].ToString(),  // created_at
                        reader[14].ToString()   // edited_at
                        );
                        LendingHistoryRecord history = new LendingHistoryRecord(
                            reader[0].ToString(),   // id
                            _user,
                            _book,
                            reader[3].ToString(),   // return_date
                            reader[4].ToString(),   // completion_date
                            reader[5].ToString(),   // created_at
                            reader[6].ToString()    // edited_at
                            );
                        history.Show();
                        result.Add(history);
                    }
                }
            }
            return result;
        }

        public void Show()
        {
            Console.WriteLine("============================");
            Console.WriteLine("Lending history");
            user.Show();
            book.Show();
            Console.WriteLine(returnDate);
            if (completionDate == null)
            {
                Console.WriteLine("NULL");
            }
            else
            {
                Console.WriteLine(completionDate);
            }
        }
    }
}
