using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.WebLinks
{
    public static class HttpWebResponseExtension
    {
        /// <summary>
        /// Returns a collection of web links from the given HttpWebResponse if they are present in the header. If not, null is returned.
        /// </summary>
        /// <param name="webResponse"></param>
        public static WebLinksCollection GetWebLinks(this HttpWebResponse webResponse)
        {
            if (!webResponse.Headers.AllKeys.Contains("Link"))
            {
                return null;
            }

            return WebLinksCollection.Parse(webResponse.Headers["Link"]);
        }
    }
}
