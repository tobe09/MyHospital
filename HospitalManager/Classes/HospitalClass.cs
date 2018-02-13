using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HospitalManager
{
    public class HospitalClass
    {
        internal static string BaseAddress { get { return HttpContext.Current.Server.MapPath("~"); } }

        /// <summary>
        /// Returns the name of a specified table using a dictionary of values
        /// </summary>
        /// <param name="userIdSubString"></param>
        /// <returns></returns>
        internal static string getTableName(string userIdSubString)
        {
            //using RIP (Replace If with Polymorphism) design pattern 
            Dictionary<string, string> user = new Dictionary<string, string>();
            user.Add("PA", "patients");
            user.Add("DC", "doctors");
            user.Add("ST", "staffs");
            user.Add("AD", "administrators");
            user.Add("SU", "superusers");
            return user[userIdSubString.Substring(0,2)];
        }

        //gets the database login values and the encryption key
        private static string getTextInfo()
        {
            TextReader ts = new StreamReader(BaseAddress + "App_Data\\Info.txt");
            return TextInfoDecryptor(ts.ReadToEnd());
        }

        // To generate the connection string
        internal static string getConnectionString()
        {
            string s = getTextInfo();
            string constr = s.Remove(s.Length - 16);
            return "Data Source=orcl;User ID=" + constr + ";Password=" + constr + ";Unicode=True;Persist Security Info=True;";
        }

        /// <summary>
        /// Returns a datatable which matches the prescribed query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataTable getDataTable(string query)
        {
            DataTable dt = new DataTable();
            using (OracleConnection con = new OracleConnection(getConnectionString()))
            {
                using (OracleCommand cmd = new OracleCommand(query, con))
                {
                    con.Open();
                    OracleDataAdapter adp = new OracleDataAdapter(cmd);
                    adp.Fill(dt);
                    return dt;
                }
            }
        }

        //Returns the encyption key for user details
        private static string myEncryptionKey()
        {
            string s = getTextInfo();
            return s.Remove(0, 13);
        }

        /// <summary>
        /// Return an encypted cipher text given an ordinary clear text
        /// </summary>
        /// <param name="clearText"></param>
        /// <returns></returns>
        public static string Encrypt(string clearText)
        {
            string cipherText;
            string EncryptionKey = myEncryptionKey();
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    cipherText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return cipherText;
        }

        /// <summary>
        /// Returns a decypted clear text given an encrypted cipher text
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText)
        {
            string clearText;
            string EncryptionKey = myEncryptionKey();
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    clearText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return clearText;
        }

        /// <summary>
        /// Returns a string in pascal case format
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string PascalCasing(string value) { return value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length - 1).ToLower(); }

        /// <summary>
        /// Draw a html table in a given width in pixels given a datatable
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string drawDataTableInHtml(DataTable dt, int width)
        {
            string s = "<div style=\"font-size: 16px; text-align:center;\">";
            s += "<table style=\"width:" + width + "px;\" border=1><tr style=\"color:blue; height:30px; font-size: 20px;\"><td><strong>S/N</strong></td>";
            for (int i = 0; i < dt.Columns.Count; i++) { s += "<td><strong>" + dt.Columns[i].ColumnName.ToString() + "</strong></td>"; }
            s += "</tr>";
            int sn = 1;
            foreach (DataRow row in dt.Rows)
            {
                s += "<tr>";
                s += "<td>" + sn + "</td>";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    s += "<td>" + row[i] + "</td>";
                }
                s += "</tr>";
                sn++;
            }
            s += "</table></div>";
            s = "(Number of rows processed: " + (sn - 1) + ")<br>" + s;
            return s;
        }

        /// <summary>
        /// Protect a database input parameter from performing sql injection
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool sqlProtect(string param)
        {
            bool safe = true;
            if (param.Contains("'") || param.Contains("--")) safe = false;
            return safe;
        }

        /// <summary>
        /// Returns the last transaction Id + 1 from the audit trail
        /// </summary>
        /// <returns></returns>
        internal static string getTransactionId() 
        {
            string lastId = DataProvider.GeneralClass.getLastTransactionId();
            DataTable dt = HospitalClass.getDataTable(lastId);
            if (dt.Rows[0][0].ToString() == "") return "1";
            else
            {
                int newId = int.Parse(dt.Rows[0][0].ToString()) + 1;
                return newId.ToString();
            }
        }

        /// <summary>
        /// Logs exceptions and errors to a text file
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool Log(Exception ex)
        {
            bool status;
            string errorFile = ex.GetType().ToString() == "System.Threading.ThreadAbortException" ? "ThreadAbortExceptions.txt" : "Log.txt";
            string path = BaseAddress + "App_Data\\" + errorFile;
            string lastNumber = LastExceptionNumber(errorFile, path);
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

        public static string LastExceptionNumber(string errorFile, string path)
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

        public static string TextInfoEncryptor(string textClearText)
        {
            String clearValue = "";
            for (int i = 0; i < textClearText.Length; i++)
            {
                clearValue += (char)((int)(textClearText[i]) + 10);
            }
            return clearValue;
        }

        public static string TextInfoDecryptor(string textCipherText)
        {
            String encryptedValue = "";
            for (int i = 0; i < textCipherText.Length; i++)
            {
                encryptedValue += (char)((int)(textCipherText[i]) - 10);
            }
            return encryptedValue;
        }

    }
}