﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManagementSystem
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        void Init(object sender, StartupEventArgs e)
        {
            Models.Model.InitDB();
        }
    }
}
