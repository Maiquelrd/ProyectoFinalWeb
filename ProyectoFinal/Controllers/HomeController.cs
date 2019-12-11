
using LumenWorks.Framework.IO.Csv;
using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {


            return View(new List<Lector>());
        }


        [HttpPost]
        public ActionResult Contact(HttpPostedFileBase postedFile)
        {
            List<Lector> Lectura = new List<Lector>();
            string filePath = string.Empty;

            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);

                }
                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                string csvdatos = System.IO.File.ReadAllText(filePath);

                foreach (string row in csvdatos.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {

                        Lectura.Add(new Lector
                        {

                            ID = Convert.ToInt32(row.Split(';')[0]),
                            Nombre = row.Split(';')[1],
                            Pais = row.Split(';')[2]

                        });
                    }
                }

            }

            return View(Lectura);
        }
    }
}