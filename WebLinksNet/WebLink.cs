using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLinksNet
{
    /// <summary>
    /// A WebLink object representing the web link data
    /// </summary>
    public class WebLink
    {
        /// <summary>
        /// The list of attributes of the WebLink
        /// </summary>
        private readonly Dictionary<string, string> _attributes = new Dictionary<string, string>();

        /// <summary>
        /// The URL of the link
        /// </summary>
        public string Url { get; set; } = null;

        /// <summary>
        /// The title of the link
        /// </summary>
        public string Title
        {
            get
            {
                return this._attributes.ContainsKey("title") ? this._attributes["title"] : null;
            }
            set
            {
                this._attributes["title"] = value;
            }
        }

        /// <summary>
        /// The rel attribute of the link
        /// </summary>
        public string Rel
        {
            get
            {
                return this._attributes.ContainsKey("rel") ? this._attributes["rel"] : null;
            }
            set
            {
                this._attributes["rel"] = value;
            }
        }

        /// <summary>
        /// Returns a textual representation of the WebLink
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append('<');
            sb.Append(this.Url);
            sb.Append(">;");

            foreach(var item in this._attributes)
            {
                sb.Append(' ');
                sb.Append(item.Key);
                sb.Append("=\"");
                sb.Append(item.Value);
                sb.Append('"');
            }

            return sb.ToString();
        }

        /// <summary>
        /// Register an attribute to the WebLink object
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        internal void AddAttribute(string attributeName, string attributeValue)
        {
            // Validate parameters
            if (string.IsNullOrWhiteSpace(attributeName))
            {
                throw new ArgumentNullException(nameof(attributeName));
            }

            this._attributes.Add(attributeName, attributeValue);
        }
    }
}
