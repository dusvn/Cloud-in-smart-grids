using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthStatusService.Models
{
    public class HealthGraph
    {

        public DateTime X { get; set; }
        public string Y { get; set; }

        public HealthGraph(DateTime x, string y)
        {
            X = x;

            Y = y;

        }

    }
}