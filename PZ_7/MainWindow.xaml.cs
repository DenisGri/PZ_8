using PZ_8;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private readonly ObservableData _observableData = new ObservableData();
        private readonly ObservableData _observableDataBackUp = new ObservableData();
        private GridViewColumnHeader _listViewSortCol = null;
        private SortAdorner _listViewSortAdorner = null;

        public MainWindow()
        {
            InitializeComponent();
            lvStudents.ItemsSource = _observableData.Students;
        }

        private void BackUpStudents()
        {
            _observableDataBackUp.Students.Clear();
            foreach (var student in _observableData.Students)
            {
                _observableDataBackUp.Students.Add(student);
            }
        }

        private void FilteredLV_LNameChanged(object sender, TextChangedEventArgs e)
        {
            if (cbFilter.SelectedIndex == -1) return;
            BackUpStudents();
            List<Student>? tempFiltered = null;

            switch (cbFilter.SelectedIndex)
            {
                case 0:
                {
                    tempFiltered = _observableData.Students.Where(stu =>
                            stu.Fio.Contains(tbFilter.Text, StringComparison.InvariantCultureIgnoreCase))
                        .ToList();
                    break;
                }
                case 1:
                {
                    tempFiltered = _observableData.Students.Where(stu =>
                            stu.Group.Contains(tbFilter.Text, StringComparison.InvariantCultureIgnoreCase))
                        .ToList();
                    break;
                }
                case 2:
                {
                    tempFiltered = _observableData.Students.Where(stu =>
                            stu.GetStringScores.Contains(tbFilter.Text, StringComparison.InvariantCultureIgnoreCase))
                        .ToList();
                    break;
                }
                default:
                    break;
            }

            for (var i = _observableData.Students.Count - 1; i >= 0; i--)
            {
                var item = _observableData.Students[i];
                if (!tempFiltered!.Contains(item))
                {
                    _observableData.Students.Remove(item);
                }
            }

            foreach (var item in tempFiltered!.Where(item => !_observableData.Students.Contains(item)))
            {
                _observableData.Students.Add(item);
            }
        }

        private void AddStudent_OnClick(object sender, RoutedEventArgs e)
        {
            BackUpStudents();
            var newStudent = new Student();
            var sub = new SubWindow();
            if (sub.ShowDialog() == true)
            {
                newStudent.Fio = sub.Fio;
                newStudent.Group = sub.Group;
                for (var i = 0; i < newStudent.Scores.Length; i++)
                {
                    newStudent.Scores[i] = sub.Scores[i];
                }

                _observableData.Students.Add(newStudent);
            }
        }

        private void DeleteStudent_OnClick(object sender, RoutedEventArgs e)
        {
            BackUpStudents();
            _observableData.Students.RemoveAt(lvStudents.SelectedIndex);
        }

        private void OpenFile_OnClick(object sender, RoutedEventArgs e)
        {
            BackUpStudents();
            _observableData.Students.Clear();
            var data = FileRW.ReadFromFile();
            foreach (var s in data)
            {
                _observableData.Students.Add(s);
            }
        }

        private void SaveFile_OnClick(object sender, RoutedEventArgs e)
        {
            FileRW.WriteToFile(_observableData.Students);
        }

        private void GoodStudents_OnClick(object sender, RoutedEventArgs e)
        {
            BackUpStudents();
            var students = new ObservableCollection<Student>();
            foreach (var student in _observableData.Students)
            {
                if (student.Scores.Any(t => t >= 9))
                {
                    students.Add(student);
                }
            }

            if (students.Count != 0)
            {
                _observableData.Students.Clear();
                foreach (var student in students)
                {
                    _observableData.Students.Add(student);
                }
            }
            else
            {
                MessageBox.Show("Студентов с оценками 9 и 10 не обнаружено", "MainApp");
            }
        }

        private void lvUsersColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            var column = sender as GridViewColumnHeader;
            var sortBy = column.Tag.ToString();
            if (_listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(_listViewSortCol)?.Remove(_listViewSortAdorner);
                lvStudents.Items.SortDescriptions.Clear();
            }

            var newDirection = ListSortDirection.Ascending;
            if (_listViewSortCol == column && _listViewSortAdorner.Direction == newDirection)
            {
                newDirection = ListSortDirection.Descending;
            }

            _listViewSortCol = column;
            _listViewSortAdorner = new SortAdorner(_listViewSortCol, newDirection);
            AdornerLayer.GetAdornerLayer(_listViewSortCol)?.Add(_listViewSortAdorner);
            lvStudents.Items.SortDescriptions.Add(new SortDescription(sortBy, newDirection));
        }

        private void Undo_OnClick(object sender, RoutedEventArgs e)
        {
            _observableData.Students.Clear();
            foreach (var student in _observableDataBackUp.Students)
            {
                _observableData.Students.Add(student);
            }
        }
    }
}