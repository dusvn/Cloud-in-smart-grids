using HealthStatusService.Models;
using Reddit_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthStatusService.Controllers
{
    public class HomeController : Controller
    {

        HealthCheckRepository healthRepository=new HealthCheckRepository();

        public ActionResult Index()
        {

            List<Health> health = healthRepository.GetLastHour();
            List<HealthGraph> graph = new List<HealthGraph>();


            if (health.Count == 0)
            {
                ViewBag.Labels = new List<string>().ToArray();
                ViewBag.Dataset = new List<int>().ToArray();
                ViewBag.Percent = 0;

                return View();

            }

            health = health.OrderBy(k => k.HealthDateTime).ToList();

            health.ForEach((k) => {

                graph.Add(new HealthGraph(k.HealthDateTime, k.State));

            });


            var label = graph.Select(s => s.X.ToLocalTime().ToString("HH:mm:ss")).ToArray();
            var dataset = graph.Select(s => s.Y == "OK" ? 1 : 0).ToArray();
            var per = healthRepository.Get24HourPercent();

            ViewBag.Labels=label;
            ViewBag.Dataset = dataset;
            ViewBag.Percent = per*100;

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
    }
}