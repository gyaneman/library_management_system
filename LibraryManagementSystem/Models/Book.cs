using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

using LibraryManagementSystem.Entities;

namespace LibraryManagementSystem.Models
{
    class Book: Model
    {
        /// <summary>
        /// テーブル名
        /// </summary>
        private const string TABLE_NAME = "book";

        /// <summary>
        /// 全ての本をデータベースから取得
        /// </summary>
        /// <returns>BookEntityのリスト</returns>
        public static List<Entities.BookEntity> GetAllBooks()
        {
            List<Entities.BookEntity> result = new List<Entities.BookEntity>();
            using (SQLiteConnection cn = new SQLiteConnection(dbConStr))
            {
                cn.Open();
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM " + TABLE_NAME;
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BookEntity book = new BookEntity(
                            reader["id"].ToString(),
                            reader["isbn"].ToString(),
                            reader["title"].ToString(),
                            reader["author"].ToString(),
                            reader["publisher"].ToString(),
                            reader["series"].ToString(),
                            reader["created_at"].ToString(),
                            reader["edited_at"].ToString()
                            );
                        book.Print();
                        result.Add(book);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// データベースに本のデータを新規保存する
        /// </summary>
        /// <param name="book">データベースに保存する本の情報</param>
        /// <returns>保存に成功したときはResult.Success、失敗時はResult.Failed</returns>
        public static Result Save(BookEntity book)
        {
            if (book.Title == null)
            {
                return Result.Failed;
            }

            using (SQLiteConnection cn = new SQLiteConnection(dbConStr))
            {
                cn.Open();
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText = 
                    "INSERT INTO "
                    + TABLE_NAME + " (isbn, title, author, publisher, series, created_at, edited_at) " 
                    + "VALUES (@ISBN, @TITLE, @AUTHOR, @PUBLISHER, @SERIES, DATETIME('now'), DATETIME('now'))";

                cmd.Parameters.Add(new SQLiteParameter("@ISBN", book.Isbn));
                cmd.Parameters.Add(new SQLiteParameter("@TITLE", book.Title));
                cmd.Parameters.Add(new SQLiteParameter("@AUTHOR", book.Author));
                cmd.Parameters.Add(new SQLiteParameter("@PUBLISHER", book.Publisher));
                cmd.Parameters.Add(new SQLiteParameter("@SERIES", book.Series));
                Console.WriteLine(cmd.ToString() + ":  " + cmd.CommandText);
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            return Result.Success;
        }
    }
}
