using Test2.Model;
using System.Collections.Generic;

namespace Test2.Services.Abstract
{
    internal interface IParametersService
    {
        /// <summary>
        /// Получить все параметры
        /// </summary>
        /// <returns></returns>
        public List<Parameter> GetAll();
        /// <summary>
        /// Получить все параметры теста
        /// </summary>
        /// <param name="testID">идентификатор теста</param>
        /// <returns></returns>
        public List<Parameter> GetAllPerTest(int testID);
        /// <summary>
        /// Получить параметр
        /// </summary>
        /// <param name="parameterID">идентификатор параметра</param>
        /// <returns></returns>
        public Parameter GetByParameterID(int parameterID);
        /// <summary>
        /// Получить кол-во параметров теста
        /// </summary>
        /// <param name="testID">идентификатор теста</param>
        /// <returns></returns>
        public int Count(int testID); 
        /// <summary>
        /// Создать параметр
        /// </summary>
        /// <param name="parameter">параметр</param>
        /// <returns></returns>
        public int Create(Parameter parameter);
        /// <summary>
        /// Изменить параметр
        /// </summary>
        /// <param name="parameter">параметр</param>
        /// <returns></returns>
        public bool Update(Parameter parameter);
        /// <summary>
        /// удалить параметр
        /// </summary>
        /// <param name="parameterID">идентификатор параметра</param>
        /// <returns></returns>
        public bool Delete(int parameterID);
    }
}
