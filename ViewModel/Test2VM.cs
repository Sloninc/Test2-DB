using System;
using System.ComponentModel;
using Test2.View;
using Test2.Services;
using Test2.Services.Abstract;
using Test2.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Test2.Infrastructure;

namespace Test2.ViewModel
{
    public class Test2VM:  INotifyPropertyChanged
    {
        RelayCommand? addCommand;
        RelayCommand? editCommand;
        RelayCommand? deleteCommand;
        private readonly ITestsService _testsService;
        private readonly IParametersService _parametersService;
        private List<Test> _allTests;
        //private List<Parameter> _allParameters;
        public Test2VM(ITestsService testsService, IParametersService parametersService)
        {
            _testsService = testsService;
            _parametersService = parametersService;
            _allTests = _testsService.GetAll();
        }
        public List<Test> AllTests
        {
            get { return _allTests; }
            set
            {
                _allTests = value;
                NotifyPropertyChanged("AllTests");
            }
        }
        //public List<Parameter> AllParameters
        //{
        //    get
        //    {
        //        return _allParameters;
        //    }
        //    private set
        //    {
        //        _allParameters = value;
        //        NotifyPropertyChanged("AllParameters");
        //    }
        //}
        //свойства для Теста
        //public DateTime TestDateVM { get; set; }
        public string BlockNameVM { get; set; }
        public string NoteVM { get; set; }

        private RelayCommand _addNewTest;
        public RelayCommand AddNewTest
        {
            get
            {
                return _addNewTest ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (BlockNameVM == null || BlockNameVM.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    else
                    {
                        var test = _testsService.Create(new Test { TestDate = DateTime.UtcNow, BlockName = BlockNameVM, Note = NoteVM });
                        resultStr = $"Тест {test.TestId} для {test.BlockName} создан";
                        UpdateAllDataView();
                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }
        private RelayCommand deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return deleteItem ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    //если тест
                    if (SelectedTabItem.Name == "TestTab" && SelectedTest != null)
                    {
                        var isDeleted = _testsService.Delete(SelectedTest.TestId);
                        if (isDeleted)
                        {
                            resultStr = $"Тест {SelectedTest.TestId} для {SelectedTest.BlockName} удален";
                            UpdateAllDataView();
                        }
                    }
                    //если параметр
                    if (SelectedTabItem.Name == "ParametersTab" && SelectedParameter != null)
                    {
                        var isDeleted = _parametersService.Delete(SelectedParameter.ParameterId);
                        if (isDeleted)
                        {
                            resultStr = $"Параметр {SelectedParameter.ParameterName} удален";
                            UpdateAllDataView();
                        }
                    }
                    //обновление
                    SetNullValuesToProperties();
                    ShowMessageToUser(resultStr);
                }
                    );
            }
        }
        //свойства для выделенных элементов
        public TabItem SelectedTabItem { get; set; }
        public static Test SelectedTest { get; set; }
        public static Parameter SelectedParameter { get; set; }
        private void SetRedBlockControll(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }
        private void SetNullValuesToProperties()
        {
            //для теста
            BlockNameVM = null;
            NoteVM = null;
        }
        private void UpdateAllDataView()
        {
            UpdateAllTestsView();
            //UpdateAllParametersView();
        }
        private RelayCommand _editTest;
        public RelayCommand EditTest
        {
            get
            {
                return _editTest ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран тест";
                    string noBlockNameStr = "Не выбран параметр";
                    if (SelectedTest != null)
                    {
                        if (BlockNameVM != null)
                        {
                            var test = _testsService.Update(new Test { BlockName = BlockNameVM, TestDate = DateTime.UtcNow, Note = NoteVM});
                            resultStr = $"Тест {SelectedTest.TestId} для {SelectedTest.BlockName} изменен";
                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else ShowMessageToUser(noBlockNameStr);
                    }
                    else ShowMessageToUser(resultStr);
                }
                );
            }
        }
        private void UpdateAllTestsView()
        {
            AllTests = _testsService.GetAll();
            //MainWindow.AllTestsView.ItemsSource = null;
            //MainWindow.AllTestsView.Items.Clear();
            //MainWindow.AllTestsView.ItemsSource = AllTests;
            //MainWindow.AllTestsView.Items.Refresh();
        }
        private void ShowMessageToUser(string message)
        {
            MessageView messageView = new MessageView(message);
            SetCenterPositionAndOpen(messageView);
        }
        private RelayCommand openAddNewTestWnd;
        public RelayCommand OpenAddNewTestWnd
        {
            get
            {
                return openAddNewTestWnd ?? new RelayCommand(obj =>
                {
                    OpenAddTestWindowMethod();
                }
                    );
            }
        }
        private RelayCommand openAddNewParameterWnd;
        public RelayCommand OpenAddNewParameterWnd
        {
            get
            {
                return openAddNewParameterWnd ?? new RelayCommand(obj =>
                {
                    OpenAddParameterWindowMethod();
                }
                );
            }
        }
        //методы открытия окон
        private void OpenAddTestWindowMethod()
        {
            AddNewTest newTestWindow = new AddNewTest();
            SetCenterPositionAndOpen(newTestWindow);
        }
        private void OpenAddParameterWindowMethod()
        {
            AddNewParameter newParameterWindow = new AddNewParameter();
            SetCenterPositionAndOpen(newParameterWindow);
        }
        //методы редактирования окон
        private void OpenEditTestWindowMethod()
        {
            EditTest editTestWindow = new EditTest();
            SetCenterPositionAndOpen(editTestWindow);
        }
        private void OpenEditParameterWindowMethod()
        {
            EditParameter editParameterWindow = new EditParameter();
            SetCenterPositionAndOpen(editParameterWindow);
        }
        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
