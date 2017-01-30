using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebLinks;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebLinksNet.Tests
{
    [TestClass]
    public class WebLinkTests
    {
        /// <summary>
        /// Tests if a WebLink .ToString() works as expected
        /// </summary>
        [TestMethod]
        public void TestWebLinkToString()
        {
            var webLink = new WebLink()
            {
                Url = "/entries?page=2",
                Title = "Simple Title",
                Rel = "next"
            };
            Assert.AreEqual("</entries?page=2>; title=\"Simple Title\" rel=\"next\"", webLink.ToString());

            webLink = new WebLink()
            {
                Url = "/entries?page=2",
                Rel = "next"
            };
            Assert.AreEqual("</entries?page=2>; rel=\"next\"", webLink.ToString());

            webLink = new WebLink()
            {
                Url = "/entries?page=2",
                Title = "Hello"
            };
            Assert.AreEqual("</entries?page=2>; title=\"Hello\"", webLink.ToString());
        }
    }
}
