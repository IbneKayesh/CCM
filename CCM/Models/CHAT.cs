using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCM.Models
{
    public class CHAT
    {
        public string ID { get; set; }
        public string CHAT_TEXT { get; set; }
        public string CHAT_TIME { get; set; }
        public int REPLY_BY { get; set; }
        public int CHAT_NO { get; set; }
    }
}