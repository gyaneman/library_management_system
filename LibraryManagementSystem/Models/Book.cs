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
        public List<Entities.BookEntity> GetAllBooks()
        {
            List<Entities.BookEntity> result = new List<Entities.BookEntity>();
            using (SQLiteConnection cn = new SQLiteConnection(dbConStr))
            {
                cn.Open();
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM book";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BookEntity book = new BookEntity(reader["id"].ToString());
                        book.Isbn = reader["isbn"].ToString();
                        book.Title = reader["title"].ToString();
                        book.Author = reader["author"].ToString();
                        book.Publisher = reader["publisher"].ToString();
                        book.CreatedAt = reader["created_at"].ToString();
                        book.EditedAt = reader["edited_at"].ToString();
                        book.Print();
                        result.Add(book);
                    }
                }
                cn.Close();
            }
            return result;
        }
    }
}
