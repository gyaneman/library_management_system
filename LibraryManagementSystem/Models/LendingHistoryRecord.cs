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
        private string userId;

        /// <summary>
        /// 貸した本
        /// </summary>
        private Book book = null;
        private string bookId;

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
            set
            {
                user = value;
                userId = value.Id;
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
            set
            {
                book = value;
                bookId = value.Id;
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

        /// <summary>
        /// ユーザインスタンスがないがIDは取得できるとき
        /// `メモリの無駄を省くため
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_userId"></param>
        /// <param name="_bookId"></param>
        /// <param name="_returnDate"></param>
        /// <param name="_completionDate"></param>
        /// <param name="_created_at"></param>
        /// <param name="_edited_at"></param>
        private LendingHistoryRecord(
            string _id,
            string _userId,
            Book _book,
            string _returnDate,
            string _completionDate,
            string _created_at,
            string _edited_at
            ) : base(_id, _created_at, _edited_at)
        {
            this.userId = _userId;
            this.book = _book;
            this.returnDate = _returnDate;
            this.completionDate = _completionDate;
        }

        /// <summary>
        /// 貸し出し情報を追加する
        /// </summary>
        /// <param name="_book">貸し出す本</param>
        /// <param name="_user">貸し出す相手のアカウント</param>
        /// <returns>成功か否か</returns>
        public static Result Create(Book _book, User _user)
        {
            return Create(_book, _user.Id);
        }

        /// <summary>
        /// 貸し出し情報を追加する
        /// </summary>
        /// <param name="_book">貸し出す本</param>
        /// <param name="_userId">貸し出す相手のユーザID</param>
        /// <returns>成功か否か</returns>
        public static Result Create(Book _book, string _userId)
        {
            int c = GetDueDateOfBook(_book).Count;
            if (c != 0)
            {
                Console.WriteLine(c);
                return Result.Failed;
            }
            
            using (SQLiteConnection cn = new SQLiteConnection(dbConStr))
            {
                cn.Open();
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText =
                    "INSERT INTO "
                    + TABLE_NAME + " (user_id, book_id, return_date, created_at, edited_at) "
                    + "VALUES (@USER_ID, @BOOK_ID, DATETIME('now', '+7 days'), DATETIME('now'), DATETIME('now'))";

                cmd.Parameters.Add(new SQLiteParameter("@USER_ID", _userId));
                cmd.Parameters.Add(new SQLiteParameter("@BOOK_ID", _book.Id));
                cmd.ExecuteNonQuery();
                cn.Close();
            }

            return Result.Success;
        }

        /// <summary>
        /// 本を返却する
        /// </summary>
        /// <returns>成功か否か</returns>
        public Result Return()
        {
            if (Id == null)
            {
                return Result.Failed;
            }

            using (SQLiteConnection cn = new SQLiteConnection(dbConStr))
            {
                cn.Open();
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText =
                    @"UPDATE "
                    + TABLE_NAME
                    + @" SET completion_date = DATETIME('now')"
                    + @" WHERE id = @ID";
                cmd.Parameters.Add(new SQLiteParameter("@ID", Id));
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            
            return Result.Success;
        }
        
        /// <summary>
        /// 本の返却状況を返す
        /// </summary>
        /// <param name="_book"></param>
        /// <returns>成功か否か</returns>
        public static List<LendingHistoryRecord> GetDueDateOfBook(Book _book)
        {
            List<LendingHistoryRecord> result = new List<LendingHistoryRecord>();
            using (SQLiteConnection cn = new SQLiteConnection(dbConStr))
            {
                cn.Open();
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM " + TABLE_NAME
                    + " WHERE completion_date IS NULL"
                    + " AND book_id = @BOOK_ID"
                    + ";";

                cmd.Parameters.Add(new SQLiteParameter("@BOOK_ID", _book.Id));
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        LendingHistoryRecord history = new LendingHistoryRecord(
                            reader[0].ToString(),   // id
                            reader[1].ToString(),   // user_id
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
                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// ユーザの未返却本一覧をDBから取得
        /// </summary>
        /// <param name="_user">ユーザ</param>
        /// <returns>取得した貸出履歴のリスト</returns>
        public static List<LendingHistoryRecord> GetUnreturnedBookFromUser(User _user)
        {
            List<LendingHistoryRecord> result = new List<LendingHistoryRecord>();
            using (SQLiteConnection cn = new SQLiteConnection(dbConStr))
            {
                cn.Open();
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM " + TABLE_NAME
                    + " INNER JOIN book ON lending_history.book_id = book.id"
                    + " WHERE completion_date IS NULL"
                    + " AND user_id = @USER_ID"
                    + ";";

                cmd.Parameters.Add(new SQLiteParameter("@USER_ID", _user.Id));
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book _book = new Book(
                        reader[7].ToString(),   // id
                        reader[8].ToString(),   // isbn
                        reader[9].ToString(),   // title
                        reader[10].ToString(),  // author
                        reader[11].ToString(),  // publisher
                        reader[12].ToString(),  // series
                        reader[13].ToString(),  // caption
                        reader[14].ToString(),  // image_url
                        reader[15].ToString(),  // created_at
                        reader[16].ToString()   // edited_at
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
                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// デバッグ用
        /// </summary>
        public void Show()
        {
            Console.WriteLine("============================");
            Console.WriteLine("Lending history");
            if (user == null)
            {
                Console.WriteLine("User ID       : " + userId);
            }
            else
            {
                user.Show();
            }

            book.Show();
            Console.WriteLine("DueDate:  " + returnDate);
            if (completionDate == null)
            {
                Console.WriteLine("CompletionDate:    NULL");
            }
            else
            {
                Console.WriteLine("CompletionDate:    " + completionDate);
            }
        }
    }
}
