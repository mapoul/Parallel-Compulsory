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

namespace Parallel_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RadioSeq.IsChecked = true;
        }

        private void Sequential_Checked(object sender, RoutedEventArgs e)
        {
            RadioPara.IsChecked = false;
        }

        private void Parallel_Checked(object sender, RoutedEventArgs e)
        {
            RadioSeq.IsChecked = false;
        }

        private void TextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            text.Text = "";
        }
    }
}
