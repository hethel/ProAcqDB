using System;
using System.IO;
using System.Data;
using System.Data.SQLite;
using System.Windows;

namespace ProAcqDb
{
    class DBTools
    {
        public bool DirExist { get; set; }      // directory flag
        string dataSource;                      // current DB
        string dbTools_Path;                    // DB & Data Log directory

        public DBTools()
        {
            dataSource = @"C:\ProAcq\Db.sqlite";    // current DB
            dbTools_Path = @"C:\ProAcq";            // DB & Data Log directory
        }

        public void DirCheck()
        {
            try
            {
                if (!Directory.Exists(dbTools_Path))
                {
                    DirExist = false;
                }
                else
                    DirExist = true;
            }
            
            catch (Exception ex)
            {
                MessageBox.Show("DB Tools\n" + dbTools_Path + " Check\n" + ex.Message);
            }
        }


        public void CreateDB()
        {
            DirCheck();

            if (DirExist == true)
            { 

                try
                {
 
                    if (!File.Exists(dataSource))
                        {
                        SQLiteConnection.CreateFile(dataSource);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("DB Tools\n" + " DB Create Check \n" + ex.Message);
                }
            }
        }


        public void CreateTable()
        {
            DirCheck();
            try
            {
                if (DirExist == true)
                { 

                    using (SQLiteConnection con = new SQLiteConnection(@"Data Source=" + dataSource))
                    {
                        con.Open();
                        string commandText = "CREATE TABLE IF NOT EXISTS Data (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Date TEXT, absTime TEXT, relTime TEXT, Tool TEXT, Memo TEXT)";

                        SQLiteCommand cmd = new SQLiteCommand(commandText, con);
                            cmd.ExecuteNonQuery();
                            cmd.Connection.Close();
                            cmd.Dispose();
                    }

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("DB Tools\n" + dbTools_Path + "Check\n" + ex.Message);
            }
        }


        public void Insert(string Date, string absTime, string relTime, string tool, string memo)
        {
            try
            {
                DirCheck();
                if (DirExist == true)
                {

                    using (SQLiteConnection con = new SQLiteConnection(@"Data Source=" + dataSource))
                    {
                        con.Open();
                        SQLiteCommand cmd = con.CreateCommand();
                        cmd.Parameters.AddWithValue("@Date", Date);
                        cmd.Parameters.AddWithValue("@absTime", absTime);
                        cmd.Parameters.AddWithValue("@relTime", relTime);
                        cmd.Parameters.AddWithValue("@Tool", tool);
                        cmd.Parameters.AddWithValue("@Memo", memo);
                        cmd.CommandText = "INSERT INTO Data (Date, absTime, relTime, Tool, Memo) Values (@Date, @absTime, @relTime, @Tool, @memo)";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("DB Tools\n" + dbTools_Path + " Check\n" + ex.Message);
            }

        }

        public DataTable Get()
        {
            DataTable processDataTable = new DataTable();

            DirCheck();

            if (DirExist == true)
            {
                try
                {
                    CreateDB();
                    CreateTable();

                    using (SQLiteConnection con = new SQLiteConnection(@"Data Source=" + dataSource))
                    {
                        con.Open();
                        SQLiteCommand cmd = con.CreateCommand();
                        cmd.CommandText = "SELECT * FROM Data";

                        SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                        da.Fill(processDataTable);

                        cmd.Dispose();
                    }

                }

                catch (Exception ex)
                {
                    MessageBox.Show("DB Tools\n" + "DataTable Transfer\n" + ex.Message);
                }
            }

            return processDataTable;
        }

    }  
}

