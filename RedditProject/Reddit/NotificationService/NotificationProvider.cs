﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService
{
    public class NotificationProvider : INotification
    {
        public string Message()
        {
            return "NotificationService is working...";
        }
    }
}
