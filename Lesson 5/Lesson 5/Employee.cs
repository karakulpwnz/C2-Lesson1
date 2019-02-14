using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_5
{
    /// <summary>
    /// Класс - сотрудник
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Возраст сотрудника
        /// </summary>
        public string Age { get; set; }
        /// <summary>
        /// Департамент, в котором сотрудник работает
        /// </summary>
        public string Department { get; set; }
    }
}
