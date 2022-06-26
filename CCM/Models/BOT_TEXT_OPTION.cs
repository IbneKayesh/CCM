using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCM.Models
{
    public class BOT_TEXT_OPTION
    {
        public int ID { get; set; }
        public int ASK_ID { get; set; }
        public string OPTION_TEXT { get; set; }
        public int NEXT_ASK_ID { get; set; }
    }
}