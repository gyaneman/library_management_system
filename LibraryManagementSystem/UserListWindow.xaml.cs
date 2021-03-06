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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem
{
    public delegate void LoginDelegate(User user);

    /// <summary>
    /// UserListWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class UserListWindow : Window
    {
        /// <summary>
        /// ログインしたときに呼ぶデリゲート
        /// </summary>
        public LoginDelegate loginDelegate;

        /// <summary>
        /// 選択されたユーザ
        /// </summary>
        private User selectedUser;

        /// <summary>
        /// datagridにバインドするユーザコレクション
        /// </summary>
        ObservableCollection<User> usersToBeDisplayed;

        /// <summary>
        /// DBから全ユーザを取得して、datagridにバインドする
        /// </summary>
        public UserListWindow()
        {
            InitializeComponent();
            var users = User.GetAllUsers();
            usersToBeDisplayed = new ObservableCollection<User>(users);
            this.dataGrid.ItemsSource = usersToBeDisplayed;
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

        /// <summary>
        /// ダブルクリックされたユーザのパスワード入力を求めるウィンドウを表示する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            User user = this.dataGrid.SelectedItem as User;
            selectedUser = user;
            if (user == null)
            {
                return;
            }
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.requestUser = user;
            loginWindow.successfulLoginDelegate = SuccessfulLogin;
            loginWindow.ShowDialog();
        }


        /// <summary>
        /// LibraryManagementSystem.SuccessfulLoginのメソッド
        /// </summary>
        public void SuccessfulLogin()
        {
            if (selectedUser != null)
            {
                loginDelegate(selectedUser);
            }
            Console.WriteLine("Closing user list");
            this.Close();
        }

        /// <summary>
        /// 新規ユーザ登録の画面を表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCreationNewAccount_Click(object sender, RoutedEventArgs e)
        {
            RegistrationAccountWindow registrationWindow = new RegistrationAccountWindow();
            registrationWindow.registrationAccountDelegate = AddUser;
            registrationWindow.ShowDialog();
        }

        public void AddUser(User user)
        {
            usersToBeDisplayed.Add(user);
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
