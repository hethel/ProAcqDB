using System;
using System.Windows;
using System.IO;


namespace ProAcqDb
{
    /// <summary>
    /// Stop Watch Memo on spezific time span
    /// </summary>
    public partial class StWtchMemo : Window
    {
        string sw_Date;     // sw date time stamp
        string sw_DateDB;   // DB entry date
        string sw_Memo;     // stop watch memo
        string sw_Time;     // stop watch memo time
        string sw_absTime;  // absolute Time stamp
        string sw_LogName;  // stop watch memo file name
        string sw_Path;     // memo time directory

        DBTools sw_db;

        // create instance of StopWatch MainWindows
        readonly StopWatch sw = new StopWatch();
        readonly DirectoryInfo sw_di;

        public StWtchMemo()
        {
            InitializeComponent();

            sw_Date = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            sw_DateDB = DateTime.Now.ToString("yyyyMMdd");
            sw_absTime = DateTime.Now.ToString("HH:mm:ss"); // show a absolute time 
            sw_LogName = sw_Date + "_StopWatch" + ".txt";
            labelSWM.Content = sw.MemoTime();
            sw_Time = labelSWM.Content.ToString();          // set the relevant time span
            sw_di = new DirectoryInfo(@"c:\ProAcq");        // instance of memo time directory
            sw_Path = @"c:\ProAcq";                         // DB & Log directory
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // desription from textbox
            sw_Memo = textBox.Text.ToString();

            // set ProAcq directory 
            try
            {
                 if (sw_di.Exists)
                 {
                     // Set current directory
                     Directory.SetCurrentDirectory(sw_Path);
                 }
            }

            catch (Exception ex)
            {
                MessageBox.Show("set ProAcq current directory\n" + ex.Message);
            }

            // save process memo with absolute time and relative time span
            try
            {
                if (sw_di.Exists)
                {
                    File.AppendAllText(sw_LogName, sw_Time + " | " + sw_Memo);
                }
             }

            catch (Exception ex)
            {
                MessageBox.Show("file content writing\n" + ex.Message);
            }

            // Database
            sw_db = new DBTools();

            if (sw_Time == String.Empty) sw_Time = "0";

            try
            {
                sw_db.DirCheck();
                if (sw_db.DirExist == true)
                {
                    sw_db.CreateDB();
                    sw_db.CreateTable();
                    sw_db.Insert(sw_DateDB, sw_absTime, sw_Time, "Stop Watch", sw_Memo);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("DB access \n" + ex.Message);
            }
        }
    }
}
