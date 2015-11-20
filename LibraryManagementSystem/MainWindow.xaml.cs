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
        ObservableCollection<Book> booksToBeDisplayed;
        public ObservableCollection<Person> people { get; set; } = new ObservableCollection<Person>();

        public MainWindow()
        {
            InitializeComponent();
            var books = Book.GetAllBooks();
            booksToBeDisplayed = new ObservableCollection<Book>(books);
            this.dataGrid.ItemsSource = booksToBeDisplayed;
        }
        
        public void UpdateDataGrid()
        {
            booksToBeDisplayed = new ObservableCollection<Book>(Book.GetAllBooks());
        }

        public void dataGrid_SelectionChanged(object sender, EventArgs e)
        {
            Console.WriteLine("dataGrid selection changed");
        }

        private void openBookCreationWindowButton_Click(object sender, RoutedEventArgs e)
        {
            var bookCreationWindow = new BookCreationWindow();
            bookCreationWindow.ShowDialog();
            UpdateDataGrid();
        }
    }
}
