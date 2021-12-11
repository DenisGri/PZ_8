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
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;

        public MainWindow()
        {
            InitializeComponent();
            lvStudents.ItemsSource = _observableData.Students;
        }

        private void FilteredLV_LNameChanged(object sender, TextChangedEventArgs e)
        {
            if (cbFilter.SelectedIndex == -1) return;
            List<Student> TempFiltered = null;

            switch (cbFilter.SelectedIndex)
            {
                case 0:
                {
                    TempFiltered = _observableData.Students.Where(stu =>
                            stu.Fio.Contains(tbFilter.Text, StringComparison.InvariantCultureIgnoreCase))
                        .ToList();
                    break;
                }
                case 1:
                {
                    TempFiltered = _observableData.Students.Where(stu =>
                            stu.Group.Contains(tbFilter.Text, StringComparison.InvariantCultureIgnoreCase))
                        .ToList();
                    break;
                }
                case 2:
                {

                    TempFiltered = _observableData.Students.Where(stu =>
                            stu.GetStringScores.Contains(tbFilter.Text, StringComparison.InvariantCultureIgnoreCase))
                        .ToList();
                    break;
                    }
                default:
                    break;
            }

            for (int i = _observableData.Students.Count - 1; i >= 0; i--)
            {
                var item = _observableData.Students[i];
                if (!TempFiltered.Contains(item))
                {
                    _observableData.Students.Remove(item);
                }
            }

            foreach (var item in TempFiltered)
            {
                if (!_observableData.Students.Contains(item))
                {
                    _observableData.Students.Add(item);
                }
            }
        }

        private void AddStudent_OnClick(object sender, RoutedEventArgs e)
        {
            Student newStudent = new Student();
            SubWindow sub = new SubWindow();
            if (sub.ShowDialog() == true)
            {
                newStudent.Fio = sub.Fio;
                newStudent.Group = sub.Group;
                for (int i = 0; i < newStudent.Scores.Length; i++)
                {
                    newStudent.Scores[i] = sub.Scores[i];
                }

                _observableData.Students.Add(newStudent);
            }
        }

        private void DeleteStudent_OnClick(object sender, RoutedEventArgs e)
        {
            _observableData.Students.RemoveAt(lvStudents.SelectedIndex);
        }

        private void OpenFile_OnClick(object sender, RoutedEventArgs e)
        {
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
            var students = new ObservableCollection<Student>();
            foreach (var student in _observableData.Students)
            {
                for (int i = 0; i < student.Scores.Length; i++)
                {
                    if (student.Scores[i] >= 9)
                    {
                        students.Add(student);
                        break;
                    }
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
            GridViewColumnHeader? column = sender as GridViewColumnHeader;
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol)?.Remove(listViewSortAdorner);
                lvStudents.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDirection = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDirection)
            {
                newDirection = ListSortDirection.Descending;
            }

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDirection);
            AdornerLayer.GetAdornerLayer(listViewSortCol)?.Add(listViewSortAdorner);
            lvStudents.Items.SortDescriptions.Add(new SortDescription(sortBy, newDirection));
        }
    }
}