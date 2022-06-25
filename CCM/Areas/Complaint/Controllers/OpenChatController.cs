using CCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCM.Areas.Complaint.Controllers
{
    public class OpenChatController : Controller
    {
        // GET: Complaint/OpenChat
        public ActionResult Index()
        {
            string sesionID = System.Web.HttpContext.Current.Session.SessionID;
            //string userSession = getUserSesssion(sesionID);
            CUSTOMER obj = new CUSTOMER();
            obj.ID = sesionID;
            return View(obj);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(CUSTOMER _obj)
        {
            //return View(_obj);
            string id = Guid.NewGuid().ToString();
            return RedirectToAction("Chat", new { id = id });
        }
        public string getUserSesssion(string sesionID)
        {
            //List<string> sessionList = new List<string>();
            return sesionID;
        }


        public ActionResult Chat(string id)
        {
            List<CHAT> obj = new List<CHAT>();
            obj.Add(new CHAT { ID = id, CHAT_TEXT = "Hello", CHAT_TIME = "4:00 PM", REPLY_BY = 0, CHAT_NO = 1 });
            obj.Add(new CHAT { ID = id, CHAT_TEXT = "How are you?", CHAT_TIME = "4:01 PM", REPLY_BY = 1, CHAT_NO = 2 });
            obj.Add(new CHAT { ID = id, CHAT_TEXT = "I'm fine, and you?", CHAT_TIME = "4:02 PM", REPLY_BY = 0, CHAT_NO = 3 });
            obj.Add(new CHAT { ID = id, CHAT_TEXT = "I'm fine, where are you from?", CHAT_TIME = "4:02 PM", REPLY_BY = 1, CHAT_NO = 4 });
            obj.Add(new CHAT { ID = id, CHAT_TEXT = "From Bangladesh, you?", CHAT_TIME = "4:03 PM", REPLY_BY = 0, CHAT_NO = 5 });
            obj.Add(new CHAT { ID = id, CHAT_TEXT = "From India", CHAT_TIME = "4:05 PM", REPLY_BY = 1, CHAT_NO = 6 });
            obj.Add(new CHAT { ID = id, CHAT_TEXT = "I'm so happy to meet with you", CHAT_TIME = "4:06 PM", REPLY_BY = 0, CHAT_NO = 7 });
            obj.Add(new CHAT { ID = id, CHAT_TEXT = "Thank you!", CHAT_TIME = "4:07 PM", REPLY_BY = 1, CHAT_NO = 8 });
            obj.Add(new CHAT { ID = id, CHAT_TEXT = "Welcome", CHAT_TIME = "4:08 PM", REPLY_BY = 0, CHAT_NO = 9 });
            return View(obj.OrderBy(x => x.CHAT_NO));
        }

        [HttpPost]
        public ActionResult Chat(CHAT _obj)
        {
            return View();
        }
    }
}