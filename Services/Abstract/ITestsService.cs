using Test2.Model;
using System.Collections.Generic;

namespace Test2.Services.Abstract
{
    public interface ITestsService
    {
        /// <summary>
        /// Получить список всех тестов
        /// </summary>
        /// <returns></returns>
        public List<Test> GetAll();
        /// <summary>
        /// Получить список по TestID
        /// </summary>
        /// <param name="testID">идентификатор</param>
        /// <returns></returns>
        public Test GetByTestID(int testID);
        /// <summary>
        /// Создать тест
        /// </summary>
        /// <param name="test">Тест</param>
        /// <returns></returns>
        public Test Create(Test test);
        /// <summary>
        /// Изменить тест
        /// </summary>
        /// <param name="test">Тест</param>
        /// <returns></returns>
        public bool Update(Test test);
        /// <summary>
        /// Удалить тест
        /// </summary>
        /// <param name="testID">идентификатор</param>
        public bool Delete(int testID);
    }
}
