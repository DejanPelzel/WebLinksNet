using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebLinks;

namespace WebLinksNet.Tests
{
    [TestClass]
    public class WebLinksCollectionTests
    {
        /// <summary>
        /// Tests the ToString override of the WebLinksCollection class that should return a formed Web Links HTTP header.
        /// </summary>
        [TestMethod]
        public void TestCollectionToString()
        {
            var collection = new WebLinksCollection();
            collection.Add(new WebLink()
            {
                Url = "/test",
                Rel = "prev",
                Title = "my-title"
            });

            Assert.AreEqual("</test>; rel=\"prev\" title=\"my-title\"", collection.ToString());
        }
    }
}
