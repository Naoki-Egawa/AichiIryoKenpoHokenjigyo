using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AichiIryoKenpoHokenjigyo.Properties;


namespace AichiIryoKenpoHokenjigyo.Model
{
    public class TekiyoMasterData
    {
            public int ID { get; set; }
            public int 記号 { get; set; }
            public int 番号 { get; set; }
            public string 氏名_カナ { get; set; }
            public string 氏名_漢字 { get; set; }
            public string 続柄コード { get; set; }
            public string 性別コード { get; set; }
            public int 個人ID { get; set; }
            public string 生年月日 { get; set; }
            public string 取得認定年月日 { get; set; }
            public string 喪失削除年月日 { get; set; }

            public string 郵便番号 { get; set; }
            public string 住所 { get; set; }
            public string 標準報酬月額 { get; set; }

            public string カード証交付日 { get; set; }
            public string カード証交付事由コード { get; set; }
            public string カード証交付事由 { get; set; }
            public string カード証有効期限 { get; set; }
            public string カード証回収日 { get; set; }
            public string カード証回収状況コード { get; set; }
            public string カード証回収状況 { get; set; }
            public string カード証発行時記号 { get; set; }
            public string カード証発行時番号 { get; set; }
        
    }
}
