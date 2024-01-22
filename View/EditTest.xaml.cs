using System.Windows;
using Test2.ViewModel;
using System.Windows.Input;
using System.Windows.Media;

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
        private void BlockName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var a = DataContext as Test2VM;
            a.ParameterNameVM = null;
            BlockName.BorderBrush = Brushes.Gray;
        }
    }
}
