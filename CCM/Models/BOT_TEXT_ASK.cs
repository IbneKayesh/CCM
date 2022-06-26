using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCM.Models
{
    public class BOT_TEXT_ASK
    {
        public int ID { get; set; }
        public string BOT_TEXT { get; set; }
        public List<BOT_TEXT_OPTION> BOT_TEXT_OPTION { get; set; }
    }
}