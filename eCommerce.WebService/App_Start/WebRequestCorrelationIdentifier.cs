using eCommerce.Helpers.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace eCommerce.WebService.App_Start
{
    public class WebRequestCorrelationIdentifier : IRequestCorrelationIdentifier
    {
        public string CorrelationID { get; private set; }

        public WebRequestCorrelationIdentifier()
        {
            string ipAddress = getIPAddress();
            string userName = HttpContext.Current.Request.Params["LOGON_USER"];
            string userAgent = HttpContext.Current.Request.UserAgent.ToString();
            string time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            string rawId = time + "|" + ipAddress + "|" + userName + "|" + userAgent;

            this.CorrelationID = md5Hash(rawId);
        }

        private static string getIPAddress()
        {
            string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ip))
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            else
                ip = ip.Split(',')[0];

            return ip;
        }

        private static string md5Hash(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}