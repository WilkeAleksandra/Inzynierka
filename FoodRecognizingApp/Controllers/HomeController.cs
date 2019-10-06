using FoodRecognizingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.IO;
using System.Diagnostics;

namespace FoodRecognizingApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ModelsList modelsList = new ModelsList();
            Model model = new Model("NASNet-Mobile", "D:\\STUDIA\\inzynierka\\models\\NasNetMobile\\NasNetMobile.hdf5");
            modelsList.models.Add(model);

            return View(modelsList);
        }

        [HttpPost]
        public ActionResult Index(ModelsList request)
        {
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            var searchPaths = engine.GetSearchPaths();
            searchPaths.Add(@"D:\STUDIA\FoodRecognizingApp\packages\DynamicLanguageRuntime.1.1.2");
            //searchPaths.Add(@"D:\STUDIA\FoodRecognizingApp\packages\IronPython.2.7.9\lib");
            //searchPaths.Add(@"C:\Users\Aleksandra\Anaconda2\Lib");
            //searchPaths.Add(@"C:\Python27");
            //searchPaths.Add(@"C:\Python27\Lib");
            searchPaths.Add(@"C:\Users\Aleksandra\AppData\Local\Programs\Python\Python37-32\Lib");
            searchPaths.Add(@"C:\Users\Aleksandra\AppData\Local\Programs\Python\Python37-32\lib\site-packages");
            //searchPaths.Add(@"C:\Users\Aleksandra\AppData\Local\Programs\Python\Python37-32");
            searchPaths.Add(@"D:\STUDIA\FoodRecognizingApp");
            //searchPaths.Add(@"D:\STUDIA\inzynierka\projektInzynierski\src\networks");
            engine.SetSearchPaths(searchPaths);

            String result = "";
            bool isAllChecked = false;
            ModelsList modelsList = new ModelsList();
            Model model = new Model("NASNet -Mobile", "D:\\STUDIA\\inzynierka\\models\\NasNetMobile\\NasNetMobile.hdf5");
            modelsList.models.Add(model);

            request.photo = Request.Files["photo"];
            string filePath = Path.Combine("~/App_Data/", Path.GetFileName(request.photo.FileName));
            request.photo.SaveAs(Server.MapPath(Path.Combine("~/App_Data/", Path.GetFileName(request.photo.FileName))));

            if (request.selected == "all")
                isAllChecked = true;
            else
            {
                isAllChecked = false;
                foreach (Model m in modelsList.models)
                    if (m.name == Session["model"].ToString())
                        modelsList.checkedModel = m;
            }

            if (isAllChecked)
            {
                foreach (Model m in modelsList.models)
                {
                    scope.SetVariable("photo", filePath);
                    scope.SetVariable("model", m.path);

                    // result = engine.Execute<string>(scope);
                    //modelsList.results.Add(result);

                    engine.ExecuteFile("D:\\STUDIA\\inzynierka\\projektInzynierski\\src\\networks\\predict.py", scope);
                    result = scope.GetVariable("result");

                    //var psi = new ProcessStartInfo();
                    //psi.FileName = @"C:\Python27\python.exe";

                    //var script = @"D:\STUDIA\inzynierka\projektInzynierski\src\networks\predict.py";
                    //var photo = @"C:\Users\Aleksandra\Desktop\testowe.jpg";
                    //var modelPath = m.path;
                    //psi.Arguments = $"\"{script}\" \"{photo}\" \"{modelPath}\"";
                    //psi.UseShellExecute = false;
                    //psi.CreateNoWindow = true;
                    //psi.RedirectStandardOutput = true;
                    //psi.RedirectStandardError = true;

                    //var error = "";
                    //var results = "";

                    //using(var process = Process.Start(psi))
                    //{
                    //    error = process.StandardError.ReadToEnd();
                    //    results = process.StandardOutput.ReadToEnd();
                    //}

                   // modelsList.results.Add(result);
                }
            }
            else
            {
                scope.SetVariable("photo", filePath);
                scope.SetVariable("model", request.checkedModel.path);
                engine.ExecuteFile("D:\\STUDIA\\inzynierka\\projektInzynierski\\src\\networks\\predict.py", scope);
                result = scope.GetVariable("result");
                modelsList.results.Add(result);
            }

            return View(modelsList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}