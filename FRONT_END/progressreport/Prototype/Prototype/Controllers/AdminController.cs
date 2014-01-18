using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Data.OleDb;

namespace Prototype.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch(Exception e)
            {
                return View("~/Home/ErrorView");
            }
        }


        // POST: /Admin/

        
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {

            try
            {
                string link = Request.Form["link"];


                var html = new WebClient().DownloadString(link);
                StreamWriter oSw = new StreamWriter("E:\\8thSemester\\FYP\\Prototype\\HTML.html");
                oSw.WriteLine(html);
                oSw.Close();

                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.Load("E:\\8thSemester\\FYP\\Prototype\\HTML.html");

                var body = document.DocumentNode.SelectNodes("//body").Single();
                string head = null;
                string author = null;
                string body1 = null;

                StreamWriter file = new StreamWriter("E:\\8thSemester\\FYP\\Prototype\\NEWS.txt", true);

                foreach (HtmlAgilityPack.HtmlNode div in body.SelectNodes("//div"))
                {
                    var classValue = div.Attributes["class"] == null ? null : div.Attributes["class"].Value;

                    if (classValue == "heading")
                    {
                        head = document.DocumentNode.SelectSingleNode("//body").SelectNodes("//div").Single(n => n.Attributes.Any(a => a.Name == "class" && a.Value == "heading")).InnerText;
                        //write innerText into a table at place [i][column1]
                    }
                    else if (classValue == "authorname")
                    {
                        author = document.DocumentNode.SelectSingleNode("//body").SelectNodes("//div").Single(n => n.Attributes.Any(a => a.Name == "class" && a.Value == "authorname")).InnerText;
                    }
                    else if (classValue == "bodytext")
                    {
                        body1 = document.DocumentNode.SelectSingleNode("//body").SelectNodes("//div").Single(n => n.Attributes.Any(a => a.Name == "class" && a.Value == "bodytext")).InnerText;

                    }
                    file.WriteLine(link + ";" + head + ";" + author + ";" + body1);
                }

                //Page.ClientScript.RegisterStartupScript(GetType(), "UserDialogScript", "alert(\"User successfully updated\");", true);

                return View();
            }
            catch (Exception ex)
            {
                return View("~Home/ErrorView");
            }
        }

    }
}
