using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Test2.ViewModel;

namespace Test2.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewTest.xaml
    /// </summary>
    public partial class AddNewTest : Window
    {
        public AddNewTest(Test2VM test2VM)
        {
            InitializeComponent();
            DataContext = test2VM;
        }
        private void BlockName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var a = DataContext as Test2VM;
            a.ParameterNameVM = null;
            BlockName.BorderBrush = Brushes.Gray;
        }
    }
}
