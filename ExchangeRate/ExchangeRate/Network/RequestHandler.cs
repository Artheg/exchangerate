using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ExchangeRate.Network
{
    static public class RequestHandler
    {
        public static async Task<string> GetData(string uri, string year)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri + year);

                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                Stream stream = response.GetResponseStream();

                using (StreamReader reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync().ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
