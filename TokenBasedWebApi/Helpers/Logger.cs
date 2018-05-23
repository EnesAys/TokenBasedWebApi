using LengerProje.DATA;
using LengerProje.Helpers.Enums;
using LengerProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LengerProje.Helpers
{
    public static class Logger
    {
        public static void SendLog(string methodname,object data,bool? isError, LogTypes? logtype)
        {
            using (var db = new ModelEntities())
            {
                var logs = new Logs();
                logs.MethodName = methodname;
                logs.Data = data.ToString();
                logs.IsError = isError==null? false:isError.Value;
                logs.LogTypeID = logtype == null ? Convert.ToInt32(LogTypes.Response) : Convert.ToInt32(logtype.Value);
                logs.CreatedDate = DateTime.Now;
                db.Logs.Add(logs);
                db.SaveChanges();
            }

        }

    }
}