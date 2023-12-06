using System;
using System.Collections.Generic;
using System.Text;
using Test2.Data;
using System.Linq;

namespace Test2.Model
{
    public static class DataWorker
    {
        public static List<Test> GetAllTests()
        {
            using(Test2Context db = new Test2Context())
            {
                return db.Tests.ToList();
            }
        }
        public static List<Parameter> GetAllParameters()
        {
            using (Test2Context db = new Test2Context())
            {
                return db.Parameters.ToList();
            }
        }
        public static string CreateTest(string blockName, string note = null)
        {
            using(Test2Context db=new Test2Context())
            {
                Test test = new Test() { BlockName = blockName, Note = note };
                db.Tests.Add(test);
                db.SaveChanges();
                return $"Тест {blockName} создан";
            }
        }
        public static string CreateParameter(string parameterName, decimal requiredValue, decimal measuredValue)
        {
            using (Test2Context db = new Test2Context())
            {
                Parameter parameter = new Parameter() { MeasuredValue = measuredValue, ParameterName = parameterName, RequiredValue = requiredValue };     
                db.Parameters.Add(parameter);
                db.SaveChanges();
                return $"Параметр {parameterName} создан";
            }
        }
        public static string DeleteTest(Test test)
        {
            using (Test2Context db = new Test2Context())
            {
                db.Tests.Remove(test);
                db.SaveChanges();
                return $"Тест {test.BlockName} удален";
            }
        }
        public static string DeleteParameter(Parameter parameter)
        {
            using(Test2Context db = new Test2Context())
            {
                db.Parameters.Remove(parameter);
                db.SaveChanges();
                return $"Параметр {parameter.ParameterName} удален";
            }
        }
        public static string EditTest(Test oldTest, string blockName, string note = null)
        {
            using (Test2Context db = new Test2Context())
            {
                Test test = db.Tests.FirstOrDefault(x => x.TestId == oldTest.TestId);
                test.BlockName = blockName;
                if(note != null)
                    test.Note = note;
                db.SaveChanges();
                return $"Тест {blockName} изменен";
            }
        }
        public static string EditParameter(Parameter oldParameter, string parameterName, decimal requiredValue, decimal measuredValue)
        {
            using(Test2Context db = new Test2Context())
            {
                Parameter parameter = db.Parameters.FirstOrDefault(x=>x.ParameterId==oldParameter.ParameterId); 
                parameter.ParameterName = parameterName;
                parameter.RequiredValue = requiredValue;
                parameter.MeasuredValue = measuredValue;
                db.SaveChanges();
                return $"Параметр {parameter.ParameterName} изменен";
            }
        }
    }
}
