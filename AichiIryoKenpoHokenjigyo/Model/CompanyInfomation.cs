using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AichiIryoKenpoHokenjigyo.Model
{
    class CompanyInfomation
    {
        [Key]
        public int Kigou { get; set; }
        public string CompanyName { get; set; }
        public string JigyonusiName { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
    }
}
