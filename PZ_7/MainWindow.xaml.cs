using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PZ_7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<Student> _students = Student.StudentList;
        public MainWindow()
        {
            InitializeComponent();
        }

        List<Student>? ReadFromFile()
        {
            return null;
        }

        void WriteToFile(string path, List<Student> students)
        {

        }

        string FindElement(string criterion)
        {
            switch (criterion)
            {
                default:
                {
                    break;
                }
            }

            return string.Empty;
        }

        private void AddStudent_OnClick(object sender, RoutedEventArgs e)
        {
            Student student = new Student("Kent C.K.", "55555", new []{6,7,8,9,10});
            Student.AddStudent(student);
        }
    }
}
