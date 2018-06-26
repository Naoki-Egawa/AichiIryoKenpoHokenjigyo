using AichiIryoKenpoHokenjigyo.Class;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Generic;
using AichiIryoKenpoHokenjigyo.Model;
using AichiIryoKenpoHokenjigyo.Class;

namespace AichiIryoKenpoHokenjigyo
{
    /// <summary>
    /// HojokinSinseiTouroku.xaml の相互作用ロジック
    /// </summary>
    public partial class HojokinSinseiTouroku : Window
    {

        public DataTable TekiyoMaster { get; set; }

        List<HojokinSinseiMaster> hojokinSinseiMasters = new List<HojokinSinseiMaster>();

        public HojokinSinseiTouroku()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            jigyo_kubun_code jigyo_Kubun_Code = new jigyo_kubun_code();

            var b = jigyo_Kubun_Code.getJigyoKubun();

            jigyokubun.ItemsSource = jigyo_Kubun_Code.getJigyoKubun();

            var filePass = @"D:\tekiyo-dammy-data.csv";

            year.Text = DateTimeOperation.GetCurrentYear().ToString();

            await Task.Run(() =>
            {

                TekiyoMaster = GetCSVData.GetDataTableFromCSV(filePass, false);


            });




        }

        private void btKanyusyaHyouji_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var kigou = kigo.Text;
                var bagnou = bango.Text;


                var a = TekiyoMaster.AsEnumerable().First(x => x.ItemArray[2].ToString().Trim() == kigou & x.ItemArray[3].ToString().Trim() == bagnou);

                name.Text = a.ItemArray[(int)TekiyoMasterCSVsoeji.TekiyoMasterCSVsoejiEnum.氏名_漢字].ToString();
                zokugara.Text = a.ItemArray[(int)TekiyoMasterCSVsoeji.TekiyoMasterCSVsoejiEnum.続柄].ToString();
                birthday.Text = a.ItemArray[(int)TekiyoMasterCSVsoeji.TekiyoMasterCSVsoejiEnum.生年月日].ToString().GetKiisGRDatetime().Datetime_JP;
                syutokuDate.Text = a.ItemArray[(int)TekiyoMasterCSVsoeji.TekiyoMasterCSVsoejiEnum.本人資格取得日_取得認定情報].ToString();
                kojinid.Text = a.ItemArray[(int)TekiyoMasterCSVsoeji.TekiyoMasterCSVsoejiEnum.個人番号].ToString();
            }
            catch (InvalidOperationException ex)
            {

                MessageBox.Show("入力の記号番号からは該当者が見つかりませんでした。");
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("入力の記号番号からは該当者が見つかりませんでした。");
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                HojokinSinseiMaster t = new HojokinSinseiMaster()
                {
                    記号 = int.Parse(kigo.Text),
                    番号 = int.Parse(bango.Text),
                    対象者氏名_漢字 = name.Text,
                    実施年月日 = jissiDate.Text.GetKiisGRDatetime().TypeofDatetime,
                    続柄コード = zokugara.Text,
                    補助金区分コード = jigyokubun.Text,
                    個人ID = kojinid.Text,
                    実施年度 = year.Text,

                };

                hojokinSinseiMasters.Add(t);

                dgv.ItemsSource = hojokinSinseiMasters;


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
