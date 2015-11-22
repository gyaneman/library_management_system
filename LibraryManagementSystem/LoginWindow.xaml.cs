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
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem
{
    public delegate void SuccessfulLogin();

    /// <summary>
    /// LoginWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class LoginWindow : Window
    {
        /// <summary>
        /// requestUserのパスワードと一致していたときに呼ばれるデリゲート
        /// </summary>
        public SuccessfulLogin successfulLoginDelegate;

        /// <summary>
        /// ログインリクエストしたユーザ
        /// </summary>
        public User requestUser;

        public LoginWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// requestUserのパスワードと一致するか検証
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            successfulLoginDelegate();
            this.Close();
        }
    }
}
