using Common;
using Reddit_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMonitoringService
{
    public class HMSProvider : IHealthMonitoring
    {
        public bool AddHealthEmail(string email)
        {
            AdminsRepository ad = new AdminsRepository();

            var a=ad.CheckIfMailExists(email);

            if (a) { return a; }

            AdminMail a1 = new AdminMail(email);

            ad.AddMail(a1);

            return a;
        }
    }
}
