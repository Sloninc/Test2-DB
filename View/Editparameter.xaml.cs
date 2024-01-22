using System;
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


namespace Test2.View
{
    /// <summary>
    /// Логика взаимодействия для Edit_parameter.xaml
    /// </summary>
    public partial class EditParameter : Window
    {
        public EditParameter(Test2VM test2VM)
        {
            InitializeComponent();
            DataContext = test2VM;
        }
        private void RequiredValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var a = DataContext as Test2VM;
            a.RequiredValueVM = null;
            RequiredValue.BorderBrush = Brushes.Gray;
        }

        private void ParameterName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var a = DataContext as Test2VM;
            a.ParameterNameVM = null;
            ParameterName.BorderBrush = Brushes.Gray;
        }

        private void MeasuredValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var a = DataContext as Test2VM;
            a.MeasuredValueVM = null;
            MeasuredValue.BorderBrush = Brushes.Gray;
        }
    }
}
