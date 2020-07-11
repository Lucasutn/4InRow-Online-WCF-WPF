﻿using MVVM.Models;
using MVVM.ViewModels;
using MVVM.Views;
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

namespace MVVM
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static MainViewModel mainWindow = new MainViewModel(); // testing "static" --remove if possible

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = mainWindow;
        }
    }
}
