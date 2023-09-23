using System.Data;
using Test.Data;
using Test.Models;

namespace UnitTests {
    [TestClass]
    public class DatabaseTests {
        private Database Database;
        private static readonly Role role = new() { Name = "test" };


        [TestInitialize]
        public void Init() {
            Database = new Database(":memory:");
        }


        [TestMethod]
        public async Task CanAddRole() {
            await Database.AddItem(role);
            var items = await Database.GetAll();
            Assert.AreEqual(role.Name, items.First().Name);
        }

        [TestMethod]
        public async Task CanRemoveRole() {
            await Database.AddItem(role);
            await Database.DeleteItem(role);
            var items = await Database.GetAll();
            Assert.AreEqual(0, items.Count);
        }

        [TestMethod]
        public async Task CanChangeRole() {
            await Database.AddItem(role);

            var items = await Database.GetAll();
            items.First().Name = "changed role";
            await Database.AddItem(items.First());

            items = await Database.GetAll();

            Assert.AreEqual(1, items.Count);
            Assert.AreEqual("changed role", items.First().Name);
        }
    }
}
