using CCM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            Session["chlg"] = null;
            Session["cssign"] = null;
            return View();
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





        //Website Pages
        public ActionResult Chat()
        {
            //string userSession = getUserSesssion(sesionID);
            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult CustomerSignup(string CUSTOMER_CONTACT, string CUSTOMER_NAME)
        {
            string sesionID = System.Web.HttpContext.Current.Session.SessionID;
            CUSTOMER obj = new CUSTOMER();
            obj.ID = sesionID;
            obj.COUNTRY_CODE = "+88";
            obj.CUSTOMER_CONTACT = CUSTOMER_CONTACT;
            obj.CUSTOMER_NAME = CUSTOMER_NAME;
            if (!TryValidateModel(obj, nameof(obj)))
            {
                return Json("f", JsonRequestBehavior.AllowGet);
            }
            Session["cssign"] = obj;

            List<CHAT> objChatInit = new List<CHAT>();
            CHAT objSingle = new CHAT { ID = sesionID, CHAT_TEXT = "Hi, I am AI Agent.", CHAT_TIME = DateTime.Now.ToString(), REPLY_BY = "user", CHAT_NO = 1, CAHT_BOT = 1 };
            objChatInit.Add(objSingle);
            Session["chlg"] = objChatInit;
            return Json("t", JsonRequestBehavior.AllowGet);
        }

        //Load/reload/refresh same session
        public JsonResult getTextLog()
        {
            List<CHAT> obj = new List<CHAT>();
            if (Session["chlg"] != null)
            {
                obj = (List<CHAT>)Session["chlg"];
            }
            return Json(obj.OrderBy(x => x.CHAT_NO), JsonRequestBehavior.AllowGet);
        }

        //single text messages
        [HttpGet]
        public JsonResult SendText(string text)
        {
            List<CHAT> objChatList = new List<CHAT>();

            if (Session["cssign"] == null)
            {
                CHAT objInitiate = new CHAT { ID = "OpenChatId", CHAT_TEXT = "To start conversion, enter your information", CHAT_TIME = DateTime.Now.ToString(), REPLY_BY = "self", CHAT_NO = 1, CAHT_BOT = 1 };
                return Json(objInitiate, JsonRequestBehavior.AllowGet);
            }

            CUSTOMER objSign = (CUSTOMER)Session["cssign"];


            if (Session["chlg"] != null)
            {
                objChatList = (List<CHAT>)Session["chlg"];
            }
            CHAT objSingle = new CHAT();
            if (Session["botAsk"] != null)
            {
                //BOT_TEXT_ASK objAsk = (BOT_TEXT_ASK)Session["botAsk"];
                int tempInt;
                bool successfullyParsed = int.TryParse(text, out tempInt);
                if (successfullyParsed)
                {
                    objChatList = SelectAutoBot(tempInt);
                }
                else
                {
                    objChatList = SelectAutoBot(1);
                }
            }
            else
            {
                objChatList.Add(new CHAT { ID = objSign.ID, CHAT_TEXT = text, CHAT_TIME = DateTime.Now.ToString(), REPLY_BY = "self", CHAT_NO = objChatList.Count + 1, CAHT_BOT = 0 });
            }
            objChatList.Add(objSingle);
            Session["chlg"] = objChatList;
            return Json(objChatList, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult SendBotText(int id = 0)
        {
            List<CHAT> obj = SelectAutoBot(id);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public List<CHAT> SelectAutoBot(int id = 0)
        {
            List<CHAT> objList = new List<CHAT>();
            BOT_TEXT_ASK objAsk = LoadBotDb(1);
            objList.Add(new CHAT { ID = "0", CHAT_TEXT = objAsk.BOT_TEXT, CHAT_TIME = DateTime.Now.ToString(), REPLY_BY = "user", CHAT_NO = 1, CAHT_BOT = 1 });
            foreach (BOT_TEXT_OPTION option in objAsk.BOT_TEXT_OPTION)
            {
                objList.Add(new CHAT { ID = "0", CHAT_TEXT = option.OPTION_TEXT, CHAT_TIME = DateTime.Now.ToString(), REPLY_BY = "user", CHAT_NO = 1, CAHT_BOT = 1 });
            }

            Session["botAsk"] = objAsk;
            return objList;
        }
        public BOT_TEXT_ASK LoadBotDb(int id)
        {
            List<BOT_TEXT_ASK> objList = new List<BOT_TEXT_ASK>();

            List<BOT_TEXT_OPTION> objOpt = new List<BOT_TEXT_OPTION>();

            BOT_TEXT_ASK obj = new BOT_TEXT_ASK();
            obj.ID = 1;
            obj.BOT_TEXT = "Complaint for ?";
            objOpt.Add(new BOT_TEXT_OPTION { ID = 1, ASK_ID = 1, OPTION_TEXT = "1) Mobile", NEXT_ASK_ID = 2 });
            objOpt.Add(new BOT_TEXT_OPTION { ID = 2, ASK_ID = 1, OPTION_TEXT = "2) Battery", NEXT_ASK_ID = 3 });
            objOpt.Add(new BOT_TEXT_OPTION { ID = 3, ASK_ID = 1, OPTION_TEXT = "3) Screen", NEXT_ASK_ID = 4 });
            obj.BOT_TEXT_OPTION = objOpt;
            objList.Add(obj);

            obj = new BOT_TEXT_ASK();
            obj.ID = 2;
            obj.BOT_TEXT = "Mobile Desk";
            objOpt = new List<BOT_TEXT_OPTION>();

            objOpt.Add(new BOT_TEXT_OPTION { ID = 1, ASK_ID = 2, OPTION_TEXT = "1) Samsung", NEXT_ASK_ID = 3 });
            objOpt.Add(new BOT_TEXT_OPTION { ID = 2, ASK_ID = 2, OPTION_TEXT = "2) Xiomi", NEXT_ASK_ID = 3 });
            obj.BOT_TEXT_OPTION = objOpt;
            objList.Add(obj);

            obj = new BOT_TEXT_ASK();
            obj.ID = 3;
            obj.BOT_TEXT = "Mobile End";
            objList.Add(obj);

            obj = new BOT_TEXT_ASK();
            obj.ID = 3;
            obj.BOT_TEXT = "Battery Desk";
            objList.Add(obj);

            obj = new BOT_TEXT_ASK();
            obj.ID = 4;
            obj.BOT_TEXT = "Screen Desk";
            objList.Add(obj);

            if (id == 0)
            {
                return objList.Where(x => x.ID == 1).First();
            }
            else
            {
                return objList.Where(x => x.ID == id).First();
            }
        }








        // User Support

        public ActionResult Support()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Support(CHAT_SUPPORT _obj)
        {
            List<CHAT> obj = new List<CHAT>();
            if (Session["chlg"] != null)
            {
                obj = (List<CHAT>)Session["chlg"];
            }
            CHAT objSingle = new CHAT { ID = "xx", CHAT_TEXT = _obj.TEXT_MSG, CHAT_TIME = DateTime.Now.ToString(), REPLY_BY = "user", CHAT_NO = obj.Count + 1 };
            obj.Add(objSingle);
            Session["chlg"] = obj;
            return View();
        }
    }
}