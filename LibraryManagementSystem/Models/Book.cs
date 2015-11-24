using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using LibraryManagementSystem.Modules;
using RakutenBook = LibraryManagementSystem.Modules.RakutenJsonModels.RakutenBook;
namespace LibraryManagementSystem.Models
{
    public class Book: Model
    {
        /// <summary>
        /// テーブル名
        /// </summary>
        private static string TABLE_NAME = "book";

        /// <summary>
        /// Bookのプロパティ
        /// </summary>
        private string isbn = null;
        private string title = null;
        private string author = null;
        private string publisher = null;
        private string series = null;
        private string caption = null;
        private string image_url = null;

        /// <summary>
        /// 新しいBookを登録する時のインスタンス作成
        /// </summary>
        public Book() : base()
        {
        }

        /// <summary>
        /// Bookのインスタンス生成
        /// _idはユニークな値でなければならないので、DBのデータからインスタンスを
        /// 作成するときに使う
        /// </summary>
        /// <param name="_id">ID(ユニークでなければならない)</param>
        /// <param name="_isbn">ISBN</param>
        /// <param name="_title">タイトル. nullは不可</param>
        /// <param name="_author">筆者</param>
        /// <param name="_publisher">出版社</param>
        /// <param name="_series">シリーズものであればそのID. シリーズものでなければnull</param>
        /// <param name="_caption">本の説明</param>
        /// <param name="_image_url">画像のURL</param>
        /// <param name="_created_at">レコード作成日時</param>
        /// <param name="_edited_at">データの最終変更日時</param>
        public Book(
            string _id,
            string _isbn,
            string _title,
            string _author,
            string _publisher,
            string _series,
            string _caption,
            string _image_url,
            string _created_at,
            string _edited_at)
            : base(_id, _created_at, _edited_at)
        {
            Console.WriteLine(_id);
            this.isbn = _isbn;
            this.title = _title;
            this.author = _author;
            this.publisher = _publisher;
            this.series = _series;
            this.caption = _caption;
            this.image_url = _image_url;
        }

        /// <summary>
        /// this.titleのgetter&setter
        /// </summary>
        public string Title
        {
            set
            {
                this.title = value;
                Edited();
            }
            get
            {
                return this.title;
            }
        }


        /// <summary>
        /// this.isbnのgetter&setter
        /// </summary>
        public string Isbn
        {
            set
            {
                this.isbn = value;
                Edited();
            }
            get
            {
                return this.isbn;
            }
        }

        /// <summary>
        /// this.authorのgetter&setter
        /// </summary>
        public string Author
        {
            set
            {
                this.author = value;
                Edited();
            }
            get
            {
                return this.author;
            }
        }

        /// <summary>
        /// this.publisherのgetter&setter
        /// </summary>
        public string Publisher
        {
            set
            {
                this.publisher = value;
                Edited();
            }
            get
            {
                return this.publisher;
            }
        }

        /// <summary>
        /// this.seriesのgetter&setter
        /// </summary>
        public string Series
        {
            set
            {
                this.series = value;
                Edited();
            }
            get
            {
                return this.series;
            }
        }

        /// <summary>
        /// captionのgetter setter
        /// </summary>
        public string Caption
        {
            set
            {
                this.caption = value;
                Edited();
            }
            get
            {
                return this.caption;
            }
        }

        /// <summary>
        /// image_urlのgetter setter
        /// </summary>
        public string ImageUrl
        {
            set
            {
                this.image_url = value;
                Edited();
            }
            get
            {
                return this.image_url;
            }
        }

        /// <summary>
        /// 全ての本をデータベースから取得
        /// </summary>
        /// <returns>取得した本のデータをBookのリストで返す</returns>
        public static List<Book> GetAllBooks()
        {
            List<Book> result = new List<Book>();
            using (SQLiteConnection cn = new SQLiteConnection(dbConStr))
            {
                cn.Open();
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM " + TABLE_NAME;
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book book = new Book(
                            reader["id"].ToString(),
                            reader["isbn"].ToString(),
                            reader["title"].ToString(),
                            reader["author"].ToString(),
                            reader["publisher"].ToString(),
                            reader["series"].ToString(),
                            reader["caption"].ToString(),
                            reader["image_url"].ToString(),
                            reader["created_at"].ToString(),
                            reader["edited_at"].ToString()
                            );
                        book.Show();
                        result.Add(book);
                    }
                }
                cn.Close();
            }
            return result;
        }

        public static List<Book> FindFromIsbn(string _isbn)
        {
            List<Book> result = new List<Book>();
            using (SQLiteConnection cn = new SQLiteConnection(dbConStr))
            {
                cn.Open();
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM " + TABLE_NAME + @" WHERE isbn = @ISBN;";
                cmd.Parameters.Add(new SQLiteParameter("@ISBN", _isbn));
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book book = new Book(
                            reader["id"].ToString(),
                            reader["isbn"].ToString(),
                            reader["title"].ToString(),
                            reader["author"].ToString(),
                            reader["publisher"].ToString(),
                            reader["series"].ToString(),
                            reader["caption"].ToString(),
                            reader["image_url"].ToString(),
                            reader["created_at"].ToString(),
                            reader["edited_at"].ToString()
                            );
                        book.Show();
                        result.Add(book);
                    }
                }
                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// データベースに本のデータを新規保存する
        /// </summary>
        /// <param name="book">データベースに保存する本の情報</param>
        /// <returns>保存に成功したときはResult.Success、失敗時はResult.Failed</returns>
        public static Result Save(Book book)
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
                    + TABLE_NAME + " (isbn, title, author, publisher, series, caption, image_url, created_at, edited_at) " 
                    + "VALUES (@ISBN, @TITLE, @AUTHOR, @PUBLISHER, @SERIES, @CAPTION, @IMAGE_URL, DATETIME('now'), DATETIME('now'))";

                cmd.Parameters.Add(new SQLiteParameter("@ISBN", book.Isbn));
                cmd.Parameters.Add(new SQLiteParameter("@TITLE", book.Title));
                cmd.Parameters.Add(new SQLiteParameter("@AUTHOR", book.Author));
                cmd.Parameters.Add(new SQLiteParameter("@PUBLISHER", book.Publisher));
                cmd.Parameters.Add(new SQLiteParameter("@SERIES", book.Series));
                cmd.Parameters.Add(new SQLiteParameter("@CAPTION", book.Caption));
                cmd.Parameters.Add(new SQLiteParameter("@IMAGE_URL", book.ImageUrl));
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            return Result.Success;
        }

        /// <summary>
        /// ISBNコードで楽天APIから本を検索する
        /// </summary>
        /// <param name="_isbn">検索する本のISBNコード</param>
        /// <returns>検索結果をBookのListで返す</returns>
        public static List<Book> GetBookFromIsbn(string _isbn)
        {
            List<Book> books = new List<Book>();
            RakutenBook response = RakutenBookApiController.GetBookDataFromIsbn<RakutenBook>(_isbn);
            foreach(RakutenBook.Item item in response.Items)
            {
                books.Add(new Book
                {
                    isbn = item.isbn,
                    title = item.title,
                    author = item.author,
                    publisher = item.publisherName,
                    caption = item.itemCaption,
                    image_url = item.largeImageUrl,
                });
            }
            return books;
        }

        /// <summary>
        /// Bookのデータを表示する
        /// </summary>
        public void Show()
        {
            if (title != null)
            {
                Console.WriteLine("Title:     " + title);
            }
            if (author != null)
            {
                Console.WriteLine("Author:    " + author);
            }
            if (isbn != null)
            {
                Console.WriteLine("ISBN:      " + isbn);
            }
            if (publisher != null)
            {
                Console.WriteLine("Publisher: " + publisher);
            }
            if (caption != null)
            {
                Console.WriteLine("Caption: " + caption);
            }
            if (caption != null)
            {
                Console.WriteLine("Image URL: " + image_url);
            }
        }
    }
}
