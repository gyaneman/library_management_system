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

        /// <summary>
        /// 現在ログイン中のユーザ
        /// </summary>
        User currentUser;

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

        private void OpenBookCreationWindowButton_Click(object sender, RoutedEventArgs e)
        {
            var bookCreationWindow = new BookCreationWindow();
            bookCreationWindow.createBookDelegate += AddBook;
            bookCreationWindow.ShowDialog();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser == null)
            {
                var userListWindow = new UserListWindow();
                userListWindow.loginDelegate = Login;
                userListWindow.ShowDialog();
            }
            else
            {
                if (MessageBox.Show(
                    "Do you want to logout?", 
                    "Logout", 
                    MessageBoxButton.OKCancel, 
                    MessageBoxImage.Information, 
                    MessageBoxResult.Cancel
                    ) == MessageBoxResult.OK )
                {
                    Logout();
                }
            }
        }

        /// <summary>
        /// LibraryManagementSystem.LoginDelegateのメソッド
        /// </summary>
        /// <param name="user">ログインしたユーザ</param>
        public void Login(User user)
        {
            Console.WriteLine(user.Name);
            currentUser = user;
            this.labelUserName.Content = currentUser.Name;
            this.LoginButton.Content = "Logout";
        }

        public void Logout()
        {
            currentUser = null;
            this.labelUserName.Content = "Guest";
            this.LoginButton.Content = "Login";
        }

        public void AddBook(Book book)
        {
            booksToBeDisplayed.Add(book);
        }
    }
}
