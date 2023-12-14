using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Test2.Data;
using Test2.Model;
using Test2.Services.Abstract;
using System.Collections.Generic;


namespace Test2.Services
{
    public class ParametersService : IParametersService
    {
        private readonly Test2Context _dbContext;
        public ParametersService(Test2Context dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Parameter> GetAll()
        {
            var parameters = _dbContext.Parameters.ToList();
            return parameters;
        }
        public List<Parameter> GetAllPerTest(int testID)
        {
            var parameters = _dbContext.Parameters.
                Where(x=>x.TestId==testID).
                ToList();
            return parameters;
        }
        public Parameter GetByParameterID(int parameterID)
        {
            var parameter = _dbContext.Parameters.FirstOrDefault(x => x.ParameterId == parameterID);
            return parameter;
        }
        public int Count(int testID)
        {
            var count = _dbContext.Parameters.
                Where(x => x.TestId == testID).
                Count();
            return count;
        }
        public Parameter Create(Parameter parameter)
        {
            var test = _dbContext.Tests.FirstOrDefault(x => x.TestId == parameter.TestId);
            if (test != null)
            {
                _dbContext.Parameters.Add(parameter);
                _dbContext.SaveChanges();
                return parameter;
            }
            else throw new Exception();
        }
        public bool Update(Parameter parameter)
        {
            var entity = _dbContext.Parameters.FirstOrDefault(x => x.ParameterId == parameter.ParameterId);
            if (entity != null)
            {
                _dbContext.Entry(entity).CurrentValues.SetValues(parameter);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Delete(int parameterID)
        {
            var parameter = _dbContext.Parameters.FirstOrDefault(x => x.ParameterId == parameterID);
            if (parameter != null)
            {
                _dbContext.Remove(parameter);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
