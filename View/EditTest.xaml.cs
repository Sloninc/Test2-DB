using System.Windows;
using Test2.ViewModel;

namespace Test2.View
{
    /// <summary>
    /// Логика взаимодействия для EditTest.xaml
    /// </summary>
    public partial class EditTest : Window
    {
        public EditTest(Test2VM test2VM)
        {
            InitializeComponent();
            DataContext = test2VM;
        }
    }
}
