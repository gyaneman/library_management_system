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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
//using JsonConfig;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Entities;


namespace LibraryManagementSystem
{
    public class Person
    {
        public string first { get; set; } = "abc";
        public string last { get; set; } = "def";
    }

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<BookEntity> booksToBeDisplayed;
        public ObservableCollection<Person> people { get; set; } = new ObservableCollection<Person>();

        public MainWindow()
        {
            InitializeComponent();
            var books = Book.GetAllBooks();
            booksToBeDisplayed = new ObservableCollection<BookEntity>(books);
            this.dataGrid.ItemsSource = booksToBeDisplayed;
        }

        public void InsertBookTest()
        {
            BookEntity book = new BookEntity();
            book.Title = "エリック・エヴァンスのドメイン駆動設計";
            book.Author = "Eric Evans";
            book.Publisher = "翔泳社";
            Book.Save(book);
        }

        public void UpdateDataGrid()
        {
            booksToBeDisplayed = new ObservableCollection<BookEntity>(Book.GetAllBooks());
        }

        public void dataGrid_SelectionChanged(object sender, EventArgs e)
        {
            Console.WriteLine("dataGrid selection changed");
        }

        private void openBookCreationWindowButton_Click(object sender, RoutedEventArgs e)
        {
            var bookCreationWindow = new BookCreationWindow();
            bookCreationWindow.ShowDialog();
        }
    }
}
