using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Serilog;
using KalumManagement.Models;
namespace KalumManagement.Utilities
{
    public class Utilerias
    {
         public static void ImprimirLog(AppLog appLog, int responsecode, string message, string typelog)
        {

            appLog.RespoonseCode = responsecode;
            appLog.message = message;
            appLog.Datetime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            if(typelog.Equals("ERROR"))
            {
                appLog.Level = 40;
                Log.Error(JsonSerializer.Serialize(appLog));
                
            }
            else if (typelog.Equals("INFORMATION"))
            {
                appLog.Level = 20;
                Log.Error(JsonSerializer.Serialize(appLog));

            }

            else if (typelog.Equals("DEBUG"))
            {
                appLog.Level = 10;
                Log.Error(JsonSerializer.Serialize(appLog));

            }

            appLog.ResponseTime = Convert.ToInt16(DateTime.Now.ToString("fff")) - appLog.ResponseTime;
        }

    }
}