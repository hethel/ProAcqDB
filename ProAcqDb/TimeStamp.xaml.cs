using System;
using System.Windows;
using System.IO;


namespace ProAcqDb
{
    /// <summary>
    /// TimeStamp - absolut time reference
    /// </summary>
    public partial class TimeStamp : Window
    {
        string ts_Date;     // ts date time stamp
        string ts_DateDB;   // DB entry date
        string ts_Time;     // selected Time Stamp
        string ts_Memo;     // add a process description
        string ts_LogName;  // Time Stamp Log name
        string ts_Path;     // memo time directory

        DirectoryInfo ts_di;    // c:/ProAcq
        DBTools ts_db;       // Database


        public TimeStamp()
        {
            InitializeComponent();

            ts_Date = DateTime.Now.ToString("yyyyMMdd_HHmmss"); 
            label.Content = DateTime.Now.ToString("HH:mm:ss");  // show a relevant time 
            ts_DateDB = DateTime.Now.ToString("yyyyMMdd");
            ts_LogName = ts_Date + "_TimeStamp" + ".txt";
            ts_Time = label.Content.ToString();                 // set a relevant time
            ts_Path = @"c:\ProAcq";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // desription from textbox
            ts_Memo = textBox.Text.ToString();

            ts_di = new DirectoryInfo(ts_Path);

            try
            {
                 if (ts_di.Exists)
                    {
                       // Set current directory
                       Directory.SetCurrentDirectory(ts_Path);
                    }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Time Stamp\n" + "set ProAcq current directory\n" + ex.Message);
            }

            // save process memo with absolute time
            try
            {
                if (ts_di.Exists)
                {
                    File.AppendAllText(ts_LogName, ts_Time + " | " + ts_Memo);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Time Stamp"+ "\n" + "file content writing\n" + ex.Message);
            }

            // Database
            ts_db = new DBTools();

            try
            {
                ts_db.DirCheck();
                if (ts_db.DirExist == true)
                {
                    ts_db.CreateDB();
                    ts_db.CreateTable();
                    ts_db.Insert(ts_DateDB, ts_Time, "0", "Time Stamp", ts_Memo);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("DB access" + "\n" + ex.Message);
            }

        }
    }
}
