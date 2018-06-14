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
using System.Windows.Shapes;
using AichiIryoKenpoHokenjigyo.Model;

namespace AichiIryoKenpoHokenjigyo
{
    /// <summary>
    /// CompanySetting.xaml の相互作用ロジック
    /// </summary>
    public partial class CompanySetting : Window
    {
        public CompanySetting()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                var db = new Kiisdbcontext();
                var q = db.CompanyInfomationTable;

                CompanyInfomationTable t = new CompanyInfomationTable()
                {
                    Kigou = 1,
                    CompanyName = "愛知県医療健康保険組合"
                };

                q.Add(t);

                db.SaveChanges();

                MessageBox.Show("Test");
            }
            catch (Exception)
            {

                throw;
            }
        }

    }

}
