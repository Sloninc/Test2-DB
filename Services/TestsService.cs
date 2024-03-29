﻿using Test2.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Test2.Data;
using Test2.Services.Abstract;
using System.Collections.Generic;

namespace Test2.Services
{
    public class TestsService: ITestsService
    {
        private readonly Test2Context _dbContext;
        public TestsService(Test2Context dbContext)
        {
            _dbContext = dbContext;
        }   
        public List<Test> GetAll()
        {
            var tests = _dbContext.Tests.ToList();
            return tests;
        }
        public Test GetByTestID(int testID)
        {
            var test = _dbContext.Tests.FirstOrDefault(x => x.TestId == testID);
            return test;
        }
        public Test Create(Test test)
        {
            _dbContext.Tests.Add(test);
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return test;
        }
        public bool Update(Test test)
        {
            var entity = _dbContext.Tests.FirstOrDefault(x => x.TestId == test.TestId);
            if(entity != null)
            {
                _dbContext.Entry(entity).CurrentValues.SetValues(test);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Delete(int testID)
        {
            var test = _dbContext.Tests.FirstOrDefault(x => x.TestId == testID);
            if (test != null)
            {
                _dbContext.Remove(test);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
