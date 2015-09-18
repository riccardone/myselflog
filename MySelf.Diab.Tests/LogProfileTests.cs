using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySelf.Diab.EventStore;
using CrossCutting.Repository;

namespace MySelf.Diab.Tests
{
    [TestClass]
    public class LogProfileTests
    {
        [TestMethod]
        public void CreateLogProfile()
        {
            var repo = new EventStoreDomainRepository();
            var logCommands = new EventStoreLogCommands(repo);
            var message = new Model.LogProfile {
                GlobalId = new Guid("6b4b020d-12c6-42ca-8100-d3c6d6bf5f36"),
                Name= "Default",
                Person = new Model.Person { Id = 2, Email = "riccardo@dinuzzo.it" }
            };

            logCommands.AddLogProfile(message);
        }
    }
}
