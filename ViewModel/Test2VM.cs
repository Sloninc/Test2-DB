﻿using System;
using System.ComponentModel;
using Test2.View;
using Test2.Services;
using Test2.Services.Abstract;
using Test2.Model;
using System.Collections.Generic;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Test2.Infrastructure;

namespace Test2.ViewModel
{
    public class Test2VM:  INotifyPropertyChanged
    {
        //RelayCommand? addCommand;
        //RelayCommand? editCommand;
        //RelayCommand? deleteCommand;
        private readonly ITestsService _testsService;
        private readonly IParametersService _parametersService;
        private List<Test> _allTests;
        private List<Parameter> _allParameters;
        public Test2VM(ITestsService testsService, IParametersService parametersService)
        {
            _testsService = testsService;
            _parametersService = parametersService;
            _allTests = _testsService.GetAll();
            _allParameters = _parametersService.GetAll();
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
        public List<Parameter> AllParameters
        {
            get
            {
                return _allParameters;
            }
            private set
            {
                _allParameters = value;
                NotifyPropertyChanged("AllParameters");
            }
        }
        public List<Parameter> TestParameters
        {
            get
            {
                return _parametersService.GetAllPerTest(SelectedTest.TestId);
            }
            //private set
            //{
            //    _allParameters = value;
            //    NotifyPropertyChanged("AllParameters");
            //}
        }
        //свойства для Теста
        private DateTime _testDateVM;
        public DateTime TestDateVM { get
            {
                if(_testDateVM==DateTime.MinValue)
                return _testDateVM=DateTime.UtcNow;
                else return _testDateVM;
            } 
            set
            {
                _testDateVM = value;    
            }
        }
        public string BlockNameVM { get; set; }
        public string NoteVM { get; set; }

        //свойства для Параметра
        public string ParameterNameVM { get; set; }
        public decimal RequiredValueVM { get; set; }
        public decimal MeasuredValueVM { get; set; }
        public Test TestVM { get; set; }

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
                        SetRedBlockControll(wnd, "BlockName");
                    }
                    else
                    {
                        var test = _testsService.Create(new Test { TestDate = TestDateVM, BlockName = BlockNameVM, Note = NoteVM });
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
        private RelayCommand _addNewParameter;
        public RelayCommand AddNewParameter
        {
            get
            {
                return _addNewParameter ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (ParameterNameVM == null || ParameterNameVM.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "ParameterName");
                    }
                    else
                    {
                        var parameter = _parametersService.Create(new Parameter { TestId = TestVM.TestId, ParameterName = ParameterNameVM, 
                            RequiredValue = RequiredValueVM, MeasuredValue = MeasuredValueVM });
                        resultStr = $"Параметр {parameter.ParameterName} создан";
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
                    if (SelectedTabItem.Name == "Tests" && SelectedTest != null)
                    {
                        var isDeleted = _testsService.Delete(SelectedTest.TestId);
                        if (isDeleted)
                        {
                            resultStr = $"Тест {SelectedTest.TestId} для {SelectedTest.BlockName} удален";
                            UpdateAllDataView();
                        }
                    }
                    //если параметр
                    if (SelectedTabItem.Name == "Parameters" && SelectedParameter != null)
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
            _testDateVM = DateTime.MinValue;
            BlockNameVM = null;
            NoteVM = null;
            ParameterNameVM = null;
            RequiredValueVM = 0;
            MeasuredValueVM = 0;
            TestVM = null;
        }
        private void UpdateAllDataView()
        {
            AllTests = _testsService.GetAll();
            AllParameters = _parametersService.GetAll();
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
                    if (SelectedTest != null)
                    {
                        if (BlockNameVM == null || BlockNameVM.Replace(" ", "").Length == 0)
                        {
                            SetRedBlockControll(window, "BlockName");
                        }
                        else
                        {
                            var test = _testsService.Update(new Test { TestId = SelectedTest.TestId, BlockName = BlockNameVM, 
                                TestDate = TestDateVM, Note = NoteVM });
                            resultStr = $"Тест {SelectedTest.TestId} для {SelectedTest.BlockName} изменен";
                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                    }
                    else ShowMessageToUser(resultStr);
                }
                );
            }
        }
        private RelayCommand _editParameter;
        public RelayCommand EditParameter
        {
            get
            {
                return _editParameter ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран Параметр";
                    if (SelectedParameter != null)
                    {
                        if (ParameterNameVM == null || ParameterNameVM.Replace(" ", "").Length == 0)
                        {
                            SetRedBlockControll(window, "ParameterName");
                        }
                        else
                        {
                            var parameter = _parametersService.Update(new Parameter { ParameterId = SelectedParameter.ParameterId, 
                                TestId = SelectedParameter.TestId, ParameterName = ParameterNameVM, RequiredValue = RequiredValueVM,
                                MeasuredValue = MeasuredValueVM });
                            resultStr = $"Параметр {ParameterNameVM} изменен";
                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                    }
                    else ShowMessageToUser(resultStr);
                }
                );
            }
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
                    SetNullValuesToProperties();
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
                    SetNullValuesToProperties();
                    OpenAddParameterWindowMethod();
                }
                );
            }
        }
        //private RelayCommand openTestViewWnd;
        //public RelayCommand OpenTestViewWnd
        //{
        //    get
        //    {
        //        return openTestViewWnd ?? new RelayCommand(obj =>
        //        {
        //            SetNullValuesToProperties();
        //            OpenTestViewWindowMethod();
        //        }
        //        );
        //    }
        //}
        //методы открытия окон
        public void OpenTestViewWindowMethod()
        {
            TestView testView = new TestView(this);
            SetCenterPositionAndOpen(testView);
        }
        private void OpenAddTestWindowMethod()
        {
            AddNewTest newTestWindow = new AddNewTest(this);
            SetCenterPositionAndOpen(newTestWindow);
        }
        private void OpenAddParameterWindowMethod()
        {
            AddNewParameter newParameterWindow = new AddNewParameter(this);
            SetCenterPositionAndOpen(newParameterWindow);
        }
        private RelayCommand openEditItemWnd;
        public RelayCommand OpenEditItemWnd
        {
            get
            {
                return openEditItemWnd ?? new RelayCommand(obj =>
                {
                    
                    //если тест
                    if (SelectedTabItem.Name == "Tests" && SelectedTest != null)
                    {
                        TestDateVM = DateTime.UtcNow;
                        BlockNameVM = SelectedTest.BlockName;
                        NoteVM = SelectedTest.Note;
                        OpenEditTestWindowMethod();
                    }
                    //если параметер
                    if (SelectedTabItem.Name == "Parameters" && SelectedParameter != null)
                    {
                        ParameterNameVM = SelectedParameter.ParameterName;
                        RequiredValueVM = SelectedParameter.RequiredValue;
                        MeasuredValueVM = SelectedParameter.MeasuredValue;
                        OpenEditParameterWindowMethod();
                    }
                }
                    );
            }
        }
        //методы редактирования окон
        private void OpenEditTestWindowMethod()
        {
            EditTest editTestWindow = new EditTest(this);
            SetCenterPositionAndOpen(editTestWindow);
        }
        private void OpenEditParameterWindowMethod()
        {
            EditParameter editParameterWindow = new EditParameter(this);
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
