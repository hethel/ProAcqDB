using System;
using System.IO;
using System.Windows;

namespace ProAcqDb
{
    /// <summary>
    /// MainWindow - Process Acquisition
    /// </summary>

    public partial class MainWindow : Window
    {
        readonly System.Windows.Threading.DispatcherTimer timer;

        StopWatch stopWatch = new StopWatch();  // reference needed to disable context menu
        readonly DBTools dbTool = new DBTools();         // DB & Log Data adjustments


        public MainWindow()
        {
            InitializeComponent();

            // ProAcqDB init
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);

            // start timer and display current time
            timer.Start();
            timer.Tick += new EventHandler(ShowTime);

            // StopWatch not activ
            stopWatch.Stopwatch_active = false;
        }

        //Method for Timer
        private void ShowTime(object sender, EventArgs e)
        {
            // display current time on label
            label.Content = DateTime.Now.ToString("HH:mm:ss");

            // ? activate context menu for Stopwatch
            if (stopWatch.Stopwatch_active == false)
                CMenu1.IsEnabled = true;
        }

        //***********************************************************
        // Context-Menus

        // Context menu - StopWatch
        private void CMenu1_Click(object sender, RoutedEventArgs e)
        {
            // ? StopWatch window now active
            if (stopWatch.Stopwatch_active == false)
            {
                //make sure there is only one instance
                stopWatch = null;               // clean old reference
                stopWatch = new StopWatch();    // set new reference

                // show and set stop watch window in center of the screen
                stopWatch.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                stopWatch.Show();

                CMenu1.IsEnabled = false;
            }
        }

         // Context-Menu Time Stamp
        private void CMenu2_Click(object sender, RoutedEventArgs e)
        {
            TimeStamp timeStamp = new TimeStamp();
            timeStamp.Show();
        }

        private void CMenu3_Click(object sender, RoutedEventArgs e)
        {
            ProcessDataView processDataView = new ProcessDataView();

            dbTool.DirCheck();
            if (dbTool.DirExist == true)
                processDataView.Show();
        }

        // Context-Menu Help message
        private void CMenu4_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ProAqcDB is a small tool for visual process identification and manually process recording.\n" + "\n" +
                            "This tool can be used for measuring and recording of process properties like a relative time span with the \"Stop Watch App\" and an absolute time clock with the \"Time Stamp App\".\n" + "\n" +
                            "It automatically writes data in a database to save process data from memo fields (c:\\ProAcq\\).\n" + "\n" +
                            "The data are saved in a SQLite Database when closing the windows.\n" + "\n" +
                            "20 Dec 2020 ");
        }

        // *** Close application
        private void ProAqcWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ProAqcWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            timer.Stop();
        }

        private void ProAqcWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(@"c:\ProAcq\");
            try
            {
                // create and ProAcq directory 
                if (!di.Exists)
                {
                    if (MessageBox.Show("You need to create\n" + @"directory c:\ProAcq\" + "\nto save process properties.", "Inquiry", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        di.Create();
                        Directory.SetCurrentDirectory(@"c:\ProAcq\");
                    }
                    else
                        MessageBox.Show("You are not able to save any process properties!");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(@"creating directory c:\ProAcq\\n" + ex.Message);
            }

            // disable Data View option in context menue
            dbTool.DirCheck();
            if (dbTool.DirExist == false)
                CMenu3.IsEnabled = false;
        }
    }
}
