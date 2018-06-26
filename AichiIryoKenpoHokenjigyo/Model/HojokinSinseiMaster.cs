using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AichiIryoKenpoHokenjigyo.Model
{

    public class HojokinSinseiMaster
    {

            public int ID { get; set; }

            public string 実施年度 { get; set; }
            public int 取込履歴ID { get; set; }

            public string 補助金区分コード { get; set; }
            public int 記号 { get; set; }
            public int 番号 { get; set; }

            public string 続柄コード { get; set; }

            public string 対象者氏名_漢字 { get; set; }

            public string 個人ID { get; set; }

            public DateTime 実施年月日 { get; set; }
            public string ErrorCode { get; set; }

            public string Remarks { get; set; }

            public string 資格取得日 { get; set; }
            public string 資格喪失日 { get; set; }
    }

}
