using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homework_3.Models;

namespace Homework_3.Controllers
{
    public class MediaController : Controller
    {
        // GET: Media

        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Home(HttpPostedFileBase file, FormCollection collection)
        {
            //Recive option from radio button using form collection
            string value = Convert.ToString(collection["optradio"]);

            //check the option
            if (value == "Document")
            {
                file.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/Media/Documents"), file.FileName));
            }
            else if (value == "Image")
            {
                file.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/Media/Images"), file.FileName));
            }
            else
            {
                file.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/Media/Videos"), file.FileName));
            }
            return RedirectToAction("Home");
        }

        public ActionResult Files()
        {
            List<Filemodel> files = new List<Filemodel>();

            string[] Documents = Directory.GetFiles(Server.MapPath("~/Content/Media/Documents"));
            string[] Images = Directory.GetFiles(Server.MapPath("~/Content/Media/Images"));
            string[] Videos = Directory.GetFiles(Server.MapPath("~/Content/Media/Videos"));

            foreach (var file in Documents)
            {
                Filemodel locatedFile = new Filemodel();
                locatedFile.FileName = Path.GetFileName(file);
                locatedFile.FileType = "doc";
                files.Add(locatedFile);
            }
            foreach (var file in Images)
            {
                Filemodel locatedFile = new Filemodel();
                locatedFile.FileName = Path.GetFileName(file);
                locatedFile.FileType = "img";
                files.Add(locatedFile);
            }
            foreach (var file in Videos)
            {
                Filemodel locatedFile = new Filemodel();
                locatedFile.FileName = Path.GetFileName(file);
                locatedFile.FileType = "vid";
                files.Add(locatedFile);
            }

            return View(files);
        }

        

        public ActionResult Images()

        {
            List<Filemodel> Images = new List<Filemodel>();

            string[] Imagelocations = Directory.GetFiles(Server.MapPath("~/Content/Media/Images"));

            foreach (var file in Imagelocations)
            {
                Filemodel locatedFile = new Filemodel();
                locatedFile.FileName = Path.GetFileName(file);
                locatedFile.FileType = "img";
                Images.Add(locatedFile);
            }
                return View(Images);
            }
        
        public ActionResult Videos()
        {
            List<Filemodel> Videos = new List<Filemodel>();

            string[] Videosloacation = Directory.GetFiles(Server.MapPath("~/Content/Media/Videos"));

            foreach (var file in Videosloacation)
            {
                Filemodel locatedFile = new Filemodel();
                locatedFile.FileName = Path.GetFileName(file);
                locatedFile.FileType = "vid";
                Videos.Add(locatedFile);
            }

            return View(Videos);
        }

        public ActionResult AboutMe()
        {

            return View();
        }



        public FileResult DownloadFile(string fileName, string fileType)
        {

            byte[] bytes = null;

            if (fileType == "doc")
            {
                bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Media/Documents/") + fileName);
            }
            else if (fileType == "vid")
            {
                bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Media/Videos/") + fileName);
            }
            else
            {
                bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Media/Images/") + fileName);
            }


            return File(bytes, "application/octet-stream", fileName);
        }

        public ActionResult DeleteFile(string fileName, string fileType)
        {
            string filelocation = null;
            if (fileType == "doc")
            {
                filelocation = Server.MapPath("~/Content/Media/Documents/") + fileName;
            }
            else if (fileType == "vid")
            {
                filelocation = Server.MapPath("~/Content/Media/Videos/") + fileName;
            }
            else
            {
                filelocation = Server.MapPath("~/Content/Media/Images/") + fileName;
            }

            System.IO.File.Delete(filelocation);


            return RedirectToAction("Home");
        }
    }
}