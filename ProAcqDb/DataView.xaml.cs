using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Data;
using System.Data.SQLite;

namespace ProAcqDb
{
    /// <summary>
    /// Interaktionslogik für DataView.xaml
    /// </summary>
    public partial class DataView : Window
    {

        public DataView()
        {
            InitializeComponent();
            //        dataView.ItemsSource = CreateDataList();

            DBTools datadb = new DBTools();

            DataSet ds = new DataSet();

            dataView.ItemsSource = datadb.get();
        }



        private List<Data> CreateDataList()
        {
            List<Data> liste = new List<Data>();
            liste.Add(new Data { Date = "20170118", absTime = "11:23:45", relTime = "00:01:12", Tool = "Stop Watch", Memo = "Stop Watch Time memo from Data.cs, Stop Watch Time memo from Data.cs, Stop Watch Time memo from Data.cs" });
            liste.Add(new Data { Date = "20170118", absTime = "11:34:10", relTime = "00:02:43", Tool = "Stop Watch", Memo = "Stop Watch Time memo from Data.cs" });
            liste.Add(new Data { Date = "20170118", absTime = "11:43:45", relTime = "0", Tool = "Time Stamp", Memo = "Time Stamp memo from Data.cs" });
            liste.Add(new Data { Date = "20170118", absTime = "11:51:57", relTime = "0", Tool = "Time Stamp", Memo = "Time Stamp memo from Data.cs" });
            liste.Add(new Data { Date = "20170118", absTime = "11:52:02", relTime = "0", Tool = "Time Stamp", Memo = "Time Stamp memo from Data.cs" });
            liste.Add(new Data { Date = "20170118", absTime = "11:52:43", relTime = "0", Tool = "Time Stamp", Memo = "Time Stamp memo from Data.cs" });
            liste.Add(new Data { Date = "20170118", absTime = "11:52:44", relTime = "0", Tool = "Time Stamp", Memo = "Time Stamp memo from Data.cs" });
            liste.Add(new Data { Date = "20170118", absTime = "11:52:47", relTime = "0", Tool = "Time Stamp", Memo = "Time Stamp memo from Data.cs" });
            liste.Add(new Data { Date = "20170118", absTime = "11:52:56", relTime = "0", Tool = "Time Stamp", Memo = "Time Stamp memo from Data.cs" });
            liste.Add(new Data { Date = "20170118", absTime = "11:52:57", relTime = "0", Tool = "Time Stamp", Memo = "Time Stamp memo from Data.cs" });
            liste.Add(new Data { Date = "20170118", absTime = "11:52:59", relTime = "0", Tool = "Time Stamp", Memo = "Time Stamp memo from Data.cs" });
            liste.Add(new Data { Date = "20170118", absTime = "11:53:03", relTime = "00:18:57", Tool = "Stop Watch", Memo = "Stop Watch Time memo from Data.cs" });
            liste.Add(new Data { Date = "20170118", absTime = "11:53:10", relTime = "00:19:03", Tool = "Stop Watch", Memo = "Stop Watch Time memo from Data.cs" });
            return liste;
        }

        


    }

}
