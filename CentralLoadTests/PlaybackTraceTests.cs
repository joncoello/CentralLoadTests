using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CentralLoadTests {

    [TestClass]
    public class PlaybackTraceTests {

        [TestMethod]
        public void Login() {

            var traceContents = CentralLoadTests.Properties.Resources.login;
            var document = XDocument.Parse(traceContents);

            var query =
                from el in document.Root.Descendants("Column")
                where (string)el.Attribute("name") == "TextData"
                select new TraceItem() {
                    CommandText = el.Value
                };

            var results = query.ToList();

            foreach (var result in results) {
                ExecuteCommand(result);
            }

        }

        private void ExecuteCommand(TraceItem result) {
            using (var conn = new SqlConnection(@"server = .\sql2014 ; database = central ; user id = sa ; pwd = Afpftcb1td")) {
                conn.Open();
                using (var cmd = new SqlCommand(result.CommandText, conn)) {
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
