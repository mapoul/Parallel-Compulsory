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
        bool seqIsSelected = false;
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
            Button.IsEnabled = false;
            seqIsSelected = (bool)RadioSeq.IsChecked;
            ListBox.Items.Clear();
            long start = long.Parse(inputStart.Text);
            long end = long.Parse(inputEnd.Text);


            Task t = Task.Factory.StartNew(() =>
            {

                List<long> result = new List<long>();
                Stopwatch sw = new Stopwatch();

                fillProgressBar(start, end);
                sw.Start();
                if (seqIsSelected == true)
                {
                    result = pg.GetPrimesSequential(start, end);
                }
                else
                {
                    result = pg.GetPrimesParallel(start, end);
                }
                sw.Stop();


                Dispatcher.Invoke(() =>
                {
                    lblTime.Content = "Time: " + sw.ElapsedMilliseconds + " Milliseconds";
                    lblAmount.Content = "Total of " + result.Count + " prime numbers found within range";
                    foreach (var item in result)
                    {
                        ListBox.Items.Add(item);
                    }
                    Button.IsEnabled = true;
                });
            });

        }

        private void fillProgressBar(long start, long end)
        {
            Task t = Task.Factory.StartNew(() =>
            {

                long progress = 0;
                do
                {
                    if (seqIsSelected)
                    {
                        progress = pg.GetSeqLong();
                    }
                    else
                    {
                        progress = pg.GetParallelLong();
                    }
                    Dispatcher.Invoke(() =>
                    {
                        ProgressBar.Value = (double)(progress - start) / (double)(end - start) * 100;
                    });

                    Thread.Sleep(10);

                    Dispatcher.Invoke(() =>
                    {
                        ProgressBar.Value = 100;
                    });

                } while (progress < end);


            });
        }
    }
}
