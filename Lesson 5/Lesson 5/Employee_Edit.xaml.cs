using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lesson_5
{
    /// <summary>
    /// Логика взаимодействия для Employee_Edit.xaml
    /// </summary>
    public partial class Employee_Edit : Window
    {
        public int Index { get; set; }
        private Employee editedEmpl;

        /// <summary>
        /// Принимает параметры сотрудника
        /// </summary>
        /// <param name="empl">Сотрудник</param>
        /// <param name="n">Порядковый номер в списке сотрудников</param>
        public void send(Employee empl, int index)
        {
            Name.Text = empl.Name;
            Surname.Text = empl.Surname;
            Age.Text = empl.Age;
            Dept.Text = empl.Department;
            Index = index;
        }

        public Employee_Edit()
        {
            InitializeComponent();
            Index = 0;
            editedEmpl = new Employee();
            List<string> _depts = new List<string>();

            foreach (Department dept in MainWindow.departments)
            {
                _depts.Add(dept.name);
            }
            Dept.ItemsSource = _depts;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            editedEmpl.Name = Name.Text;
            editedEmpl.Surname = Surname.Text;
            editedEmpl.Age = Age.Text;
            editedEmpl.Department = Dept.Text;
            MainWindow main = this.Owner as MainWindow;
            main.import(editedEmpl, Index);
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
