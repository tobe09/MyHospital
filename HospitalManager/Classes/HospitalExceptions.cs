using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace HospitalManager
{
    public static class HospitalExceptions
    {

        /// <summary>
        /// Logging errors using an Extension Method on an Exception Object
        /// </summary>
        public static bool Log(this Exception ex)
        {
            bool status;
            string errorFile = ex.GetType().ToString() == "System.Threading.ThreadAbortException" ? "ThreadAbortExceptions.txt" : "Log.txt";
            string path = HttpContext.Current.Server.MapPath("~") + "App_Data\\" + errorFile;
            string lastNumber = LastExceptionNumber(path);

            if (!lastNumber.StartsWith("Exception"))
            {
                string newNumber = (int.Parse(lastNumber) + 1) + "";
                using (StreamWriter sw = File.AppendText(path))
                {
                    //StringBuilder sb = new StringBuilder();
                    //TextWriter tw2 = new StringWriter(sb);
                    string errorMessage = "\r\n\r\nDate: " + DateTime.Now.ToLongDateString() + "\r\nTime: " + DateTime.Now.ToLongTimeString() +
                                          "\r\nError message: " + ex.Message + "\r\nError's stack trace: " + ex.StackTrace +
                                          "\r\n#########################################################################################\r\n\r\n\r\n" +
                                          "Exception " + newNumber;
                    sw.Write(errorMessage);
                    sw.Flush();
                }
                status = true;
            }

            else
            {
                status = false;
            }

            return status;
        }

        public static string LastExceptionNumber(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    File.WriteAllText(path, "Exception 1");
                }
                string allText = File.ReadAllText(path);
                string lastNumber = allText.Length == 0 ? "1" : allText.Substring(allText.Length - 1, 1);
                int outNumber;
                lastNumber = int.TryParse(lastNumber, out outNumber) ? lastNumber : "1";
                return lastNumber;
            }
            catch (Exception ex)
            {
                return "Exception: " + ex.Message;
            }
        }

    }
}