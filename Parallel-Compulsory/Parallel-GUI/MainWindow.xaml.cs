using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
using Parallel_Compulsory;

namespace Parallel_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PrimeGenerator pg = new PrimeGenerator();
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListBox.Items.Clear();
            Task t = Task.Factory.StartNew(() =>
            {

                List<long> result = new List<long>();
                long start = 0;
                long end = 0;
                Stopwatch sw = new Stopwatch();

                Dispatcher.Invoke(() =>
                {
                    start = long.Parse(inputStart.Text);
                    end = long.Parse(inputEnd.Text);
                });


                fillProgressBar(start, end);
                sw.Start();
                if (RadioSeq.IsChecked == true)
                {
                    result = pg.GetPrimesSequential(start, end);
                }
                else
                {
                    result = pg.GetPrimesParallel(start, end);
                }
                sw.Stop();
                lblTime.Content = "Time: " + sw.ElapsedMilliseconds + " Milliseconds";

                Dispatcher.Invoke(() =>
                {
                    foreach (var item in result)
                    {
                        ListBox.Items.Add(item);
                    }
                });
            });
        }

        private void fillProgressBar(long start, long end)
        {
            Task t = Task.Factory.StartNew(() =>
            {
                while(pg.GetParallelLong() < end)
                {
                    Dispatcher.Invoke(() =>
                    {
                        ProgressBar.Value = pg.GetParallelLong() / end * 100;
                    });
                   
                    Thread.Sleep(100);
                }
            });
        }
    }
}
