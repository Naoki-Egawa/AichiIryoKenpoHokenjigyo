using System;
using System.Data;
using System.Data.OleDb;

namespace AichiIryoKenpoHokenjigyo.Class
{
    public class GetCSVData
    {
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
    }
}
