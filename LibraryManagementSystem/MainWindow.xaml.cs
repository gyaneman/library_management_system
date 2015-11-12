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
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Book book;
        ObservableCollection<BookEntity> books;
        public MainWindow()
        {
            InitializeComponent();
            book = new Book();
            var books = book.GetAllBooks();
            Console.Write("num:" + books.Count);
        }

        public void UpdateDataGrid()
        {
            books = new ObservableCollection<BookEntity>(book.GetAllBooks());
        }

        public void dataGrid_SelectionChanged(object sender, EventArgs e)
        {
            Console.WriteLine("dataGrid selection changed");
        }
    }
}
