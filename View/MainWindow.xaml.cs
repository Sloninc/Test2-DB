using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Test2.ViewModel;
using System.ComponentModel;

namespace Test2.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(Test2VM test2VM)
        {
            InitializeComponent();
            DataContext = test2VM;
        }
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;
        void GridViewColumnHeaderClickedHandler(object sender,
                                                RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            var listView = e.Source as ListView;
            ListSortDirection direction;
            Test.SelectedItem = null;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }
                    //var columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
                    //var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;
                    var columnBinding = headerClicked.Column.Header as TextBlock;
                    var sortBy = columnBinding?.Text;
                    Sort(sortBy, direction, listView);

                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowUp"] as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowDown"] as DataTemplate;
                    }

                    // Remove arrow from previously sorted header
                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }
        private void Sort(string sortBy, ListSortDirection direction, ListView listView)
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(listView.ItemsSource);
            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }

        private void Test_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var block = e.OriginalSource as ScrollViewer;
            var listView = e.Source as ListView;
            if (listView.SelectedIndex < 0||block!=null)
            {
                Test.SelectedItem = null;
                return;
            }
            var context = DataContext as Test2VM;
            TestView testView = new TestView(context);
            context.OpenTestViewWindowMethod();
        }

        private void ContextMenu_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            var textBlock = e.OriginalSource as ScrollViewer;
            if (textBlock != null)
            {
                Test.SelectedItem = null;
                e.Handled= true;
                return;
            }
        }

        private void Parameter_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            var textBlock = e.OriginalSource as ScrollViewer;
            if (textBlock != null)
            {
                Parameter.SelectedItem = null;
                e.Handled = true;
                return;
            }
        }
    }
}
