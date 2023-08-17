using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KalumManagement.Models
{
    public class AppLog
    {
          public AppLog()
        {
            IPAddress[] address = Dns.GetHostEntry(this.HostName).AddressList;
            for (int i = 0; i < address.Length; i++)
            {
                this.Ip = address[i].ToString();

            }


        }
        public string Name { get; set; } = "WebServicesEnrollment";

        public string HostName { get; set; } = Dns.GetHostName();

        public string ApiKey { get; set; } = "N/A";

        public string Uri { get; set; } = "N/A";

        public int RespoonseCode { get; set; }

        public long ResponseTime { get; set; }

        public string Ip { get; set; }

        public int Level { get; set; }

        public string message { get; set; }

        public string Datetime { get; set; }

        public string Version { get; set; }


    }
}