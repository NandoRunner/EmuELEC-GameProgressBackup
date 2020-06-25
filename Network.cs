using System;
using System.Net;
using System.Net.NetworkInformation;

namespace EmuELEC_GameProgressBackup
{
    public static class Network
    {
        public static bool InternetConnectionExists()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool HostExists(string host)
        {
            Ping pinger = new Ping();

            try
            {
                PingReply reply = pinger.Send(host);
                return reply.Status == IPStatus.Success;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                pinger.Dispose();
            }

        }
    }
}
