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

namespace PZ_7
{
    /// <summary>
    /// Interaction logic for subWindow.xaml
    /// </summary>
    public partial class SubWindow : Window
    {
        public string Fio => tbFio.Text;
        public string Group => tbGroup.Text;
        public int[] Scores => StrToIntArray(tbScores.Text);

        private int[] StrToIntArray(string str)
        {
            try
            {
                var ia = str.Split(';', '.', ',', ' ').Select(n => Convert.ToInt32(n)).ToArray();
                return ia;
            }
            catch (Exception e)
            {
                MessageBox.Show("Только цифры разделенные с помощью ';', '.', ',', ' '", "Ошибка!");
            }

            return Array.Empty<int>();
        }
        public SubWindow()
        {
            InitializeComponent();
        }

        private void btnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
