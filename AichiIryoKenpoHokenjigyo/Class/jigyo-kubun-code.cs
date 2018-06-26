using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AichiIryoKenpoHokenjigyo.Class
{


    public class jigyo_kubun_code
    {
        public string JigyoCode { get; set; }
        public string JigyoName { get; set; }


        //public jigyo_kubun_code[] getJigyoKubun()
        //{
        //    jigyo_kubun_code[] jigyo_Kubun_Code =
        //    {
        //        new jigyo_kubun_code
        //        {
        //            JigyoCode = "H101",
        //            JigyoName="人間ドックＡコース",
        //        },
        //        new jigyo_kubun_code
        //        {
        //            JigyoCode ="H301",
        //            JigyoName="インフルエンザ予防接種",
        //        }
        //    };

        //    return jigyo_Kubun_Code;
        //}

        public string[] getJigyoKubun()
        {
            string[] jigyo_Kubun_Code =
            {
               string.Empty,
               "H101:人間ドックAコース",
               "H102:人間ドックAコース（胃なし）",
               "H301:インフルエンザ予防接種",

               };

            return jigyo_Kubun_Code;
        }


    }


}
