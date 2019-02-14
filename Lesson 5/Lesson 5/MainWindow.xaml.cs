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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lesson_5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private static List<Employee> employees;
        public static List<Department> departments { get; set; }
        private static List<string> names;
        private static List<string> surnames;

        /// <summary>
        /// Создание первоначального списка сотрудников
        /// </summary>
        private void CreateUsers()
        {
            Random rnd = new Random();

            departments.Add(new Department { name = "Design" });

            departments.Add(new Department { name = "Analytic" });

            departments.Add(new Department { name = "Develop" });

            foreach (Department dept in departments)
            {
                Console.WriteLine(dept.name);
            }

            string _name = "";
            string _surname = "";
            string _dept = "";

            for (int i = 0; i < 20; i++)
            {
                _name = names[rnd.Next(0, names.Count)];
                _surname = surnames[rnd.Next(0, surnames.Count)];
                _dept = departments[rnd.Next(0, departments.Count)].name.ToString();
                employees.Add(new Employee { Name = _name, Surname = _surname, Age = rnd.Next(18, 55).ToString(), Department = _dept });
            }


            foreach (Employee empl in employees)
            {
                Console.WriteLine(empl.Surname + " " + empl.Name + "   " + empl.Department);
            }
        }

        /// <summary>
        /// Вывод списков в таблицы
        /// </summary>
        public void LoadData()
        {
            employeesList.ItemsSource = employees;
            departmentsList.ItemsSource = departments;
        }

        public MainWindow()
        {
            InitializeComponent();

            employees = new List<Employee>();
            departments = new List<Department>();
            surnames = new List<string>() { "Andersen", "Shulz", "Jefferson", "Johnson", "Clinton", "Cartright", "Thompson", "Kim"};
            names = new List<string>() { "John", "Melissa", "Thomas", "Peter", "Angela", "Julia", "Mark" };

            CreateUsers();

            LoadData();

        }

        /// <summary>
        /// Редактирование сотрудника в списке
        /// </summary>
        /// <param name="empl">Сотрудник</param>
        /// <param name="index">Номер сотрудника в списке</param>
        public void import(Employee empl, int index)
        {
            employees.Insert(index, empl);
            employees.RemoveAt(index+1);
            employeesList.Items.Refresh();
        }


        private void Edit_Empl_Click(object sender, RoutedEventArgs e)
        {
            Employee_Edit emplEdit = new Employee_Edit();
            emplEdit.Owner = this;
            emplEdit.send(employees[employeesList.SelectedIndex], employeesList.SelectedIndex);
            emplEdit.Show();
        }
    }
}
