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

namespace LibraryManagementSystem
{
    public delegate void RegistrationAccountDelegate(User user);

    /// <summary>
    /// RegistrationAccountWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class RegistrationAccountWindow : Window
    {
        public RegistrationAccountDelegate registrationAccountDelegate;

        public RegistrationAccountWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 登録ボタンが押された時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("ButtonRegister_Clicked");
            bool invaliedFlag = false;
            string name, email, password;

            if ((name = this.textBoxName.Text) == string.Empty)
            {
                invaliedFlag = true;
            }
            if ((email = this.textBoxEmail.Text) == string.Empty)
            {
                invaliedFlag = true;
            }
            if ((password = this.passwordBox1.Password) == string.Empty)
            {
                invaliedFlag = true;
            }
            if (this.passwordBox2.Password == string.Empty)
            {
                invaliedFlag = true;
            }
            if (password != this.passwordBox2.Password)
            {
                invaliedFlag = true;
            }

            if (invaliedFlag)
            {
                Console.WriteLine("InvaliedFlag");
                return;
            }
            Console.WriteLine("Success");
            User user = new User();
            user.Name = name;
            user.Email = email;
            user.Password = password;
            user = User.Save(user);
            if (user != null)
            {
                registrationAccountDelegate(user);
            }

            this.Close();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            this.DragMove();
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
