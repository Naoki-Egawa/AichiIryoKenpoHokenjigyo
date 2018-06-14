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
using System.Data.SQLite;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace AichiIryoKenpoHokenjigyo
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataTable TekiyoMaster { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var f = new CompanySetting();
            f.ShowDialog();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var sqlConnectionSb = new SQLiteConnectionStringBuilder { DataSource = "denco.db" };

            var filePass = @"D:\tekiyo.csv";

            await Task.Run(() =>
           {

               TekiyoMaster = GetDataTableFromCSV(filePass, false);
           });

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var filepass = @"D:\c.csv";

            var dt = GetDataTableFromCSV(filepass, false);

            var kensakuNo = "";

            foreach (var item in dt.AsEnumerable().Where(x => x.ItemArray[3].ToString() == "MN"))
            {
                if (item.ItemArray[3].ToString() == "MN")
                {
                    var a = double.Parse(item.ItemArray[6].ToString());

                    kensakuNo = a.ToString("00000000000000000");

                }
                else if (item.ItemArray[3].ToString() == "SY")
                {
                    Console.WriteLine($"{kensakuNo} : {item.ItemArray[4].ToString()}");
                }
            }
            MessageBox.Show("Test");
        }

        public static DataTable GetDataTableFromCSV(String strFilePath, Boolean isInHeader = true)
        {
            DataTable dt = new DataTable();
            String strInHeader = isInHeader ? "YES" : "NO";                // ヘッダー設定
            String strCon = "Provider=Microsoft.ACE.OLEDB.12.0;"      // プロバイダ設定
                                                                      //= "Provider=Microsoft.Jet.OLEDB.4.0;"     // Jetでやる場合
                                + "Data Source=" + System.IO.Path.GetDirectoryName(strFilePath) + "\\; "          // ソースファイル指定
                                + "Extended Properties=\"Text;HDR=" + strInHeader + ";FMT=Delimited\"";
            OleDbConnection con = new OleDbConnection(strCon);
            String strCmd = "SELECT * FROM [" + System.IO.Path.GetFileName(strFilePath) + "]";

            // 読み込み
            OleDbCommand cmd = new OleDbCommand(strCmd, con);
            OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
            adp.Fill(dt);

            return dt;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {

                var kigou = bb.Text;
                var bagnou = aa.Text;

                int soeji = 3;

                var a = TekiyoMaster.AsEnumerable().First(x => x.ItemArray[2].ToString().Trim() == kigou & x.ItemArray[soeji].ToString().Trim() == bagnou);

                //foreach (var item in TekiyoMaster.AsEnumerable())
                //{

                //    MessageBox.Show(item.ItemArray[5].ToString());
                //}
                MessageBox.Show(a.ItemArray[5].ToString());
            }
            catch (InvalidOperationException ex)
            {

                MessageBox.Show("見つかりませんでした。");
            }
        }
    }
}