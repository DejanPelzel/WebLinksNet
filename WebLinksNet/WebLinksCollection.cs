using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.WebLinks
{
    /// <summary>
    /// A collection of web links
    /// </summary>
    public class WebLinksCollection : List<WebLink>
    {
        /// <summary>
        /// The internal list of WebLinks in this collection
        /// </summary>
        private List<WebLink> _links = new List<WebLink>();

        /// <summary>
        /// Create a new weblinks collection
        /// </summary>
        public WebLinksCollection()
        {
            
        }

        /// <summary>
        /// Returns a textual header representation of the web link collection
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var webLink in this)
            {
                if (sb.Length > 0)
                    sb.Append(", ");

                sb.Append(webLink.ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// Parses the given weblink header and returns a list of WebLink objects
        /// </summary>
        /// <param name="webLinkHeader"></param>
        /// <returns></returns>
        public static WebLinksCollection Parse(string webLinkHeader)
        {
            // Validate the parameters
            if (webLinkHeader == null)
            {
                throw new ArgumentNullException(nameof(webLinkHeader));
            }

            var attributeNameBuilder = new StringBuilder();

            // Parse the data
            var collection = new WebLinksCollection();

            var webLinkParts = webLinkHeader.Split(',');
            for (var i = 0; i < webLinkParts.Length; i++)
            {
                var webLink = new WebLink();

                var part = webLinkParts[i].Trim();
                // Simple validation
                if ((!part.StartsWith("<") || !part.Contains(">")))
                {
                    if (string.IsNullOrWhiteSpace(part))
                    {
                        continue;
                    }
                    else
                    {
                        throw new WebLinksParserException();
                    }
                }

                // Parse the URL
                var urlEndIndex = part.IndexOf('>') - 1;
                webLink.Url = part.Substring(1, urlEndIndex);

                attributeNameBuilder.Clear();
                for (var n = urlEndIndex + 3; n < part.Length; n++)
                {
                    var c = part[n];

                    // These characters should not be present here
                    if (c == '"' || c == '<' || c == '>' || c == ',')
                        throw new WebLinksParserException();

                    if (c != '=')
                    {
                        attributeNameBuilder.Append(c);
                        continue;
                    }

                    // Clean up and validate the name
                    var name = attributeNameBuilder.ToString();
                    attributeNameBuilder.Clear();
                    name = name.Trim();
                    foreach (var nameChar in name)
                    {
                        if (char.IsWhiteSpace(nameChar))
                        {
                            throw new WebLinksParserException();
                        }
                    }

                    // Read the value
                    string value = "";
                    while (n < part.Length)
                    {
                        n++;

                        if (char.IsWhiteSpace(part[n]))
                        {
                            continue;
                        }
                        else if (part[n] == '"')
                        {
                            // Read the value
                            var valueEndIndex = part.IndexOf('"', n + 1);
                            value = part.Substring(n + 1, valueEndIndex - n - 1);


                            // Advance the original loop
                            n = valueEndIndex;
                            break;
                        }
                    }

                    // Construct an attribute
                    webLink.AddAttribute(name, value);
                }

                collection.Add(webLink);
            }

            return collection;
        }
    }
}
