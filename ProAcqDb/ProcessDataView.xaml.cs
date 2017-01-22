using System.Windows;
using System.Data;

namespace ProAcqDb
{
    /// <summary>
    /// show Process data from DB in DataGrid
    /// </summary>
    
    public partial class ProcessDataView : Window
    {
        DBTools processDataTable = new DBTools();

        public ProcessDataView()
        {
            InitializeComponent();

            PDView1.ItemsSource = processDataTable.get().AsDataView();
        }
    }
}
