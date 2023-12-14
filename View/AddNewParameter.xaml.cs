﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Test2.ViewModel;
using System.Windows;

namespace Test2.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewParameter.xaml
    /// </summary>
    public partial class AddNewParameter : Window
    {
        public AddNewParameter(Test2VM test2VM)
        {
            InitializeComponent();
            DataContext = test2VM;
        }
    }
}
