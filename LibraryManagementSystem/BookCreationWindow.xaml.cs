using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Modules;

namespace LibraryManagementSystem
{
    public delegate void CreateBookDelegate(Book book);
    /// <summary>
    /// BookCreationWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class BookCreationWindow : Window
    {
        public CreateBookDelegate createBookDelegate;

        public BookCreationWindow()
        {
            InitializeComponent();
        }

        private void buttonBookCreation_Click(object sender, RoutedEventArgs e)
        {
            if (this.textBoxTitle.Text == string.Empty)
            {
                this.labelWarningTitle.Content = "Please enter title.";
                this.labelWarningTitle.Visibility = Visibility.Visible;
                return;
            }
            var newBook = new Book();
            newBook.Title = this.textBoxTitle.Text;
            newBook.Author = this.textBoxAuthor.Text;
            newBook.Publisher = this.textBoxPublisher.Text;
            if (Book.Save(newBook) == Result.Success)
            {
                createBookDelegate(newBook);
            }
            this.Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSearchFromIsbn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Search From ISBN");
            
            List<Book> books = Book.GetBookFromIsbn(this.textBoxIsbn.Text);
            if (books.Count == 1)
            {
                Book book = books[0];
                this.textBoxTitle.Text = book.Title;
                this.textBoxPublisher.Text = book.Publisher;
                this.textBoxAuthor.Text = book.Author;
                this.textBoxIsbn.Text = book.Isbn;
            }
            foreach(Book book in books)
            {
                Console.WriteLine("================");
                book.Show();
            }
        }
    }
}
