using System;
using System.ComponentModel;
using Test2.Services;
using Test2.Services.Abstract;
using Test2.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Test2.Infrastructure
{
    internal class Test2VM:  INotifyPropertyChanged
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
        //public DateTime TestDate { get; set; }
        //public string BlockName { get; set; }
        //public string Note { get; set; }

        public RelayCommand AddNewTest
        {
            get
            {
                return addNewTest ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (BlockName == null || BlockName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateDepartment(DepartmentName);
                        UpdateAllDataView();

                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }
    }
}
