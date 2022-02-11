using System.Linq;
using System.Net;
using System.Web;

namespace CarLeasePPT.Helpers
{
    public static class IpAddressHelper
    {
        #region Public Methods and Operators

        public static byte[] GetIpAddressBytes(HttpContext currentContext)
        {
            var ip = currentContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            ip = string.IsNullOrEmpty(ip)
                ? currentContext.Request.ServerVariables["REMOTE_ADDR"]
                : ip.Split(',').Last().Trim();
            if (string.IsNullOrWhiteSpace(ip)) return new byte[16];

            IPAddress ipAddress;
            if (!IPAddress.TryParse(ip, out ipAddress)) return new byte[16];
            var ipAddressBytes = ipAddress.GetAddressBytes();
            return ipAddressBytes;
        }

        #endregion
    }
}