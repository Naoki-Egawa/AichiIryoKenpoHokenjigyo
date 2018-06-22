using AichiIryoKenpoHokenjigyo.Class;
using AichiIryoKenpoHokenjigyo.Model;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Collections.Generic;

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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            CompanyInfomation[] a = new CompanyInfomation[1];

            a[0] = new CompanyInfomation
            {
                Kigou = int.Parse(kigou.Text),
                JigyonusiName = jigyonusiname.Text,
                CompanyName = jigyosyoname.Text,
                PostalCode = postalcode.Text,
                Address = address.Text,
                Tel = tel.Text,
            };
            
            //シリアライズ
            string json = JsonConvert.SerializeObject(a, Formatting.Indented);
            //シリアライズ化したものを表示
            Console.WriteLine(json);

            string fullFileName = @"D:\compamy-master.json";
            
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fullFileName, false, System.Text.Encoding.GetEncoding("shift_jis"));
            sw.Write(json);
            sw.Close();

            MessageBox.Show("事業所基本情報を保存しました。");

            this.Close();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // コレクションのデシリアライズ
                var text = File.ReadAllText(@"D:\compamy-master.json", System.Text.Encoding.GetEncoding("shift_jis"));
                var list = JsonConvert.DeserializeObject<List<CompanyInfomation>>(text);

                if(list.Count <= 0)
                {
                    MessageBox.Show("事業所基本情報の登録がありません。新規で登録を行ってください。","しんせいくん");
                    return;
                }

                kigou.Text = list[0].Kigou.ToString();
                jigyosyoname.Text = list[0].CompanyName.ToString();
                jigyonusiname.Text = list[0].JigyonusiName.ToString();
                postalcode.Text = list[0].PostalCode.ToString();
                address.Text = list[0].Address.ToString();
                tel.Text = list[0].Tel.ToString();


            }
            catch (Exception ex)
            {
                MessageBox.Show("事業所基本情報の登録がありません。新規で登録を行ってください。", "しんせいくん");
                return;
            }
        }
    }
}
