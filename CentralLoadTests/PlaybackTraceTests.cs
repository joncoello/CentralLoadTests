using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.Collections.Generic;

namespace CentralLoadTests {
    [TestClass]
    public class PlaybackTraceTests {
        [TestMethod]
        public void Playback() {

            var traceContents = CentralLoadTests.Properties.Resources.login;
            var document = XDocument.Parse(traceContents);

            var query =
                from el in document.Root.Descendants("Column")
                where (string)el.Attribute("name") == "TextData"
                select new TraceItem() {
                    CommandText = el.Value
                };

            var results = query.ToList();

        }
    }
}
