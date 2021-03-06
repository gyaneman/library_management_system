﻿using System;
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
            this.lendingHistoryButton.IsEnabled = true;
        }

        /// <summary>
        /// ログアウト時に呼び出す
        /// </summary>
        public void Logout()
        {
            currentUser = null;
            this.labelUserName.Content = "Guest";
            this.LoginButton.Content = "Login";
            this.lendingHistoryButton.IsEnabled = false;
        }

        /// <summary>
        /// 本を追加する
        /// delegateの関数
        /// </summary>
        /// <param name="book"></param>
        public void AddBook(Book book)
        {
            booksToBeDisplayed.Add(book);
        }

        /// <summary>
        /// 履歴をクリックされた時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lendingHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            LendingHistoryWindow lendingHistoryWindow = new LendingHistoryWindow(currentUser);
            lendingHistoryWindow.Show();
        }

        /// <summary>
        /// データグリッド上のアイテムをダブルクリックしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var book = this.dataGrid.SelectedItem as Book;
            if (book == null)
            {
                return;
            }
            var bookDetailsWindow = new BookDetailsWindow(book, currentUser);
            bookDetailsWindow.ShowDialog();
        }



        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch (System.InvalidOperationException)
            {
                Console.WriteLine("UserListWindow: Error");
            }
        }

        private void MinimizationButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizationButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void NormalizationButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
