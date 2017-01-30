using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebLinksNet;

namespace WebLinksNet.Tests
{
    [TestClass]
    public class WebLinksParserTests
    {
        /// <summary>
        /// Test the execution of the parser with an empty parameter. An empty list should be returned.
        /// </summary>
        [TestMethod]
        public void TestEmpty()
        {
            var result = WebLinksParser.Parse("");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 0);
        }

        /// <summary>
        /// Test the execution of the parser with a null parameter. An exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNull()
        {
            WebLinksParser.Parse(null);
        }

        /// <summary>
        /// Parse a simple WebLink
        /// </summary>
        [TestMethod]
        public void TestSimpleParse()
        {
            var result = WebLinksParser.Parse("</entries?page=2>; rel=\"next\" title=\"This is a test\"");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 1);

            var webLink = result[0];
            Assert.AreEqual("/entries?page=2", webLink.Url, "Url property");
            Assert.AreEqual("next", webLink.Rel, "Rel property");
            Assert.AreEqual("This is a test", webLink.Title, "Title proprty");
        }

        /// <summary>
        /// Parse Test parsing multiple WebLinks
        /// </summary>
        [TestMethod]
        public void TestMultipleParse()
        {
            var result = WebLinksParser.Parse("</entries?page=2>; rel=\"next\", </entries?page=9>; rel=\"last\"");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 2);
        }

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

            Assert.AreEqual("", webLink.ToString());
        }
    }
}
