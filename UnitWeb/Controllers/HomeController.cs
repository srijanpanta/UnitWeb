using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitWeb.AzureServiceReference;
//using UnitWeb.LocalServerReference;

namespace UnitWeb.Controllers
{
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
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Convert(string fromUnit, string toUnit, string value)
        {
            try
            {
                // Validate input
                if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(fromUnit) || string.IsNullOrEmpty(toUnit))
                {
                    ViewBag.Result = "Please fill in all fields.";
                    return View("Index");
                }

                double inputValue;
                if (!double.TryParse(value, out inputValue))
                {
                    ViewBag.Result = "Invalid value. Please enter a valid number.";
                    return View("Index");
                }

                // Call the WCF service to perform the conversion
                using (Service1Client client = new Service1Client())
                {
                    double result;
                    if (fromUnit == "kilograms" || fromUnit == "pounds" || fromUnit=="grams")
                    {
                        result = client.ConvertWeight(inputValue, fromUnit, toUnit);
                    }
                    else
                    {
                        result = client.ConvertLength(inputValue, fromUnit, toUnit);
                    }

                    ViewBag.Result = result;
                }

                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Result = $"An error occurred: {ex.Message}";
                return View("Index");
            }
        }
    }
}